using System.Text; 
using Newtonsoft.Json.Linq;

Console.WriteLine("WooCommerce migrator");

var configFileContent = File.ReadAllText(args[0]);
var config = JObject.Parse(configFileContent)!;
var production = config["production"]!.ToObject<Environment>()!;
var staging = config["staging"]!.ToObject<Environment>()!;
string mode = args[1];
var orders = new Resource { name = "orders", path = "wp-json/wc/v2/orders", createdDateProperty = "date_created", modifiedDateProperty = "date_modified" };
var media = new Resource { name = "media", path = "wp-json/wp/v2/media", createdDateProperty = "date", modifiedDateProperty = "modified" };
var products = new Resource { name = "products", path = "wp-json/wc/v2/products", createdDateProperty = "date_created", modifiedDateProperty = "date_modified" };

DateTime inputDate = Convert.ToDateTime(config["date"]!.ToString());
DateTime deleteStagingOrdersAfter = Convert.ToDateTime(config["deleteStagingOrdersAfter"]!.ToString());
DateTime lastCheckedDate = inputDate;

switch (mode)
{
    case "export":
        {
            using var client = GetHttpClient(production);
            Console.WriteLine($"exporting...");
            if (Directory.Exists("data"))
                Directory.Delete("data", true);
            Directory.CreateDirectory("data");

            lastCheckedDate = await ExportProducts(production, inputDate, lastCheckedDate, client);
            lastCheckedDate = await ExportOrders(production, inputDate, lastCheckedDate, client);


            config["lastCheckedDate"] = lastCheckedDate.ToString("yyyy-MM-ddTHH:mm:ss");
            File.WriteAllText(args[0], config.ToString());
            Console.WriteLine($"last checked date: {lastCheckedDate}");
            break;
        }

    case "products":
        Console.WriteLine($"migrating products ...");
        ImportProducts();
        break;
    case "orders":
        Console.WriteLine($"updating orders ...");
        UpdateOrders();
        break;
    case "dryclean":
    case "clean":
        {
	    Console.WriteLine($"running {mode} ...");
            using var client = GetHttpClient(staging);
            var route = $"{staging.url}{orders.path}?after={deleteStagingOrdersAfter.ToString("yyyy-MM-ddTHH:mm:ss")}";
            List<JObject> resourceList = await GetResources(client, orders, route);
            foreach (var order in resourceList)
            {
                var billing = order["billing"];
                Console.WriteLine($"{order["date_created"]} {billing?["first_name"]} {billing?["last_name"]}");
                if (mode == "clean")
                {
                    var resp = await client.DeleteAsync($"{staging.url}{orders.path}/{order["id"]}");
                    if (resp.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"deleted {order["id"]}");
                    }
                    else
                    {
                        Console.WriteLine($"failed to delete {order["id"]}");
                        return;
                    }
                }
            }
            break;
        }
    case "media":
        {
            using var productionClient = GetHttpClient(production);
            List<JObject> productionList = await GetResources(productionClient, media, $"{production.url}{media.path}?after={inputDate.ToString("yyyy-MM-ddTHH:mm:ss")}");
            using var stagingClient = GetHttpClient(production);
            List<JObject> stagingList = await GetResources(stagingClient, media, $"{staging.url}{media.path}?after={inputDate.ToString("yyyy-MM-ddTHH:mm:ss")}");
            Dictionary<string, List<int>> mediaMap = new Dictionary<string, List<int>>();
            foreach (var item in productionList)
            {
                var slug = item["slug"].ToString();
                if (!mediaMap.ContainsKey(slug))
                    mediaMap.Add(slug, new List<int>());
                mediaMap[slug].Add((int)item["id"]);
            }
            foreach (var item in stagingList)
            {
                var slug = item["slug"].ToString();
                if (!mediaMap.ContainsKey(slug))
                    continue;
                mediaMap[slug].Add((int)item["id"]);
            }
            Dictionary<int, int> map = new Dictionary<int, int>();
            foreach (var item in mediaMap)
            {
                if (item.Value.Count == 1)
                {
                    Console.WriteLine($"unmapped: {item.Key}");
                    continue;
                }
                if (item.Value.Count > 2)
                {
                    Console.WriteLine($"conflict: {item.Key}");
                    continue;
                }
                map.Add(item.Value[0], item.Value[1]);
            }
            config["mediaMap"] = JObject.FromObject(map);
            File.WriteAllText(args[0], config.ToString());
            break;
        }
    case "imageRef":
        var postImageReference = config["postImageReference"]?.ToObject<Dictionary<int, List<int>>>();
        if(postImageReference == null)
            break;
        var queries = new List<string>();
        foreach(var item in postImageReference)
        {
            var imageId = item.Key;
            var postId = item.Value.Min();
            queries.Add($"UPDATE wp_posts SET post_parent = {postId} WHERE ID = {imageId} and post_parent=0 and post_type='attachment';");
        }
        File.WriteAllText("fix_image_refs.sql", queries.Aggregate((s1,s2)=>$"{s1}\n{s2}"));

    break;
}


void ImportProducts()
{
    using var client = GetHttpClient(staging);
    var insertsFileContent = GetFileContent(@$"./data/{products.name}_inserts.json");
    var updatesFileContent = GetFileContent(@$"./data/{products.name}_updates.json");
    var mediaMap = config["mediaMap"]?.ToObject<Dictionary<int, int>>() ?? new Dictionary<int, int>();
    
    var inserts = JArray.Parse(insertsFileContent);
    var updates = JArray.Parse(updatesFileContent);
    Console.WriteLine($"{products.name} inserts: {inserts?.Count}");
    Console.WriteLine($"{products.name} updates: {updates?.Count}");
    var postImageReference = new Dictionary<int, List<int>>();

    var count = 0;
    var total = inserts?.Count + updates?.Count ?? 0;
    if(inserts != null) {
        List<JObject> failedInserts = new List<JObject>();
        foreach(JObject element in inserts)
        {
            ++count;
            UpdateImageReferences(mediaMap, postImageReference, element);
            element.Remove("id");

            var content = new StringContent(element.ToString(), Encoding.UTF8, "application/json");
            var result = client.PostAsync($"{staging.url}{products.path}", content).Result;
            Console.WriteLine($"INSERT {result.StatusCode}");
            Console.WriteLine($"{count} / {total}");
            if (!result.IsSuccessStatusCode)
            {
                failedInserts.Add(element);
                Console.WriteLine(result.Content.ReadAsStringAsync().Result);
            }
        }
        if (failedInserts.Any())
            WriteToFile($"data/{products.name}_failed_inserts.json", failedInserts);
    }
    if(updates != null) {
        var failedUpdates = new List<JObject>();
        foreach(JObject element in updates)
        {
            ++count;
            UpdateImageReferences(mediaMap, postImageReference, element);
            UpdateElement(products, staging, client, failedUpdates, element);
            Console.WriteLine($"{count} / {total}");
        }
        if (failedUpdates.Any())
            WriteToFile($"data/{products.name}_failed_updates.json", failedUpdates);
    }
    config["postImageReference"] = JObject.FromObject(postImageReference);
    File.WriteAllText(args[0], config.ToString());
}
void UpdateOrders()
{
    using var client = GetHttpClient(staging);
    var updatesFileContent = GetFileContent(@$"./data/{orders.name}_updates.json");
    var updates = JArray.Parse(updatesFileContent) ?? new JArray();
    Console.WriteLine($"{orders.name} updates: {updates.Count}");

    var failedUpdates = new List<JObject>();
    foreach (JObject element in updates)
    {
        element.Remove("line_items");
        UpdateElement(orders, staging, client, failedUpdates, element);
    }
    if (failedUpdates.Any())
        WriteToFile($"data/{orders.name}_failed_updates.json", failedUpdates);
}

void WriteToFile(string filePath, List<JObject> elements) 
{
    if (!elements.Any())
    {
        File.WriteAllText(filePath, "[]");
        return;
    }
    var commaSeparatedInserts = elements.Select(j => j.ToString(Newtonsoft.Json.Formatting.Indented)).Aggregate((s1, s2) => $"{s1},\n{s2}");
    File.WriteAllText(filePath, $"[\n{commaSeparatedInserts}\n]");
}

string GetFileContent(string path)
{
    if(File.Exists(path))
    {
        return File.ReadAllText(path);
    } else {
        Console.WriteLine($"{path} does not exist");
        return "[]";
    }
}

static HttpClient GetHttpClient(Environment env)
{
    var client = new HttpClient();
    client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(env.consumerKey + ":" + env.consumerSecret)));
    return client;
}

static async Task<List<JObject>> GetResources(HttpClient client, Resource resource, string route)
{
    var resourceList = new List<JObject>();
    List<JObject>? elements;
    var page = 0;
    do
    {
        ++page;
        var resp = await client.GetAsync($"{route}&page={page}");
        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine($"{route}&page={page}");
            Console.WriteLine(resp.Content.ReadAsStringAsync().Result);
            break;
        }
        var strJson = await resp.Content.ReadAsStringAsync();
        elements = JArray.Parse(strJson).ToList().Select(t => (JObject)t).ToList();
        Console.WriteLine($"{page}. received {resource.name} count: {elements?.Count}");
        if (elements != null)
        {
            resourceList.AddRange(elements);
        }
    } while (elements != null && elements.Count != 0);
    Console.WriteLine($"{resource.name} count: {resourceList.Count}");
    return resourceList;
}

static void UpdateImageReferences(Dictionary<int, int> mediaMap, Dictionary<int, List<int>> postImageReference, JObject element)
{
    foreach (var image in element["images"])
    {
        var imageId = (int)image["id"];
        if (mediaMap.ContainsKey(imageId))
            image["id"] = mediaMap[imageId];
        if (!postImageReference.ContainsKey(imageId))
            postImageReference.Add(imageId, new List<int>());
        postImageReference[imageId].Add((int)element["id"]);
    }
}

static void UpdateElement(Resource resource, Environment staging, HttpClient client, List<JObject> failedUpdates, JObject element)
{
    var content = new StringContent(element.ToString(), Encoding.UTF8, "application/json");
    var id = ((int)element.Property("id")!);
    var result = client.PutAsync($"{staging.url}{resource.path}/{id}", content).Result;
    Console.WriteLine($"UPDATE {id} {result.StatusCode}");
    if (!result.IsSuccessStatusCode)
    {
        failedUpdates.Add(element);
        Console.WriteLine(result.Content.ReadAsStringAsync().Result);
    }
}

static DateTime GetNewAndUpdatedResources(List<JObject> elements, Resource resource, DateTime inputDate, DateTime lastCheckedDate, out List<JObject> inserts, out List<JObject> updates)
{
    inserts = new List<JObject>();
    updates = new List<JObject>();
    foreach (var element in elements)
    {
        DateTime createdDate = ((DateTime)element.Property(resource.createdDateProperty)!);
        DateTime modifiedDate = ((DateTime)element.Property(resource.modifiedDateProperty)!);

        if (createdDate >= inputDate)
        {
            inserts.Add(element);
        }
        else if (modifiedDate >= inputDate)
        {
            updates.Add(element);
        }

        if (createdDate >= lastCheckedDate)
            lastCheckedDate = createdDate;
        if (modifiedDate >= lastCheckedDate)
            lastCheckedDate = modifiedDate;
    }

    return lastCheckedDate;
}


async Task<DateTime> ExportProducts(Environment production, DateTime inputDate, DateTime lastCheckedDate, HttpClient client)
{
    var route = $"{production.url}{products.path}?modified_after={inputDate.ToString("yyyy-MM-ddTHH:mm:ss")}";
    List<JObject> resourceList = await GetResources(client, products, route);
    Console.WriteLine($"{products.name} count: {resourceList.Count}");
    List<JObject> inserts, updates;
    lastCheckedDate = GetNewAndUpdatedResources(resourceList, products, inputDate, lastCheckedDate, out inserts, out updates);
    Console.WriteLine($"{products.name} inserts: {inserts.Count}");
    WriteToFile($"data/{products.name}_inserts.json", inserts);
    Console.WriteLine($"{products.name} updates: {updates.Count}");
    WriteToFile($"data/{products.name}_updates.json", updates);
    return lastCheckedDate;
}

async Task<DateTime> ExportOrders(Environment production, DateTime inputDate, DateTime lastCheckedDate, HttpClient client)
{
    var route = $"{production.url}{orders.path}?modified_after={inputDate.ToString("yyyy-MM-ddTHH:mm:ss")}";
    List<JObject> resourceList = await GetResources(client, orders, route);
    Console.WriteLine($"{orders.name} count: {resourceList.Count}");
    List<JObject> inserts, updates;
    lastCheckedDate = GetNewAndUpdatedResources(resourceList, orders, inputDate, lastCheckedDate, out inserts, out updates);
    Console.WriteLine($"{orders.name} inserts (dropped): {inserts.Count}");
    Console.WriteLine($"{orders.name} updates: {updates.Count}");
    WriteToFile($"data/{orders.name}_updates.json", updates);
    return lastCheckedDate;
}

struct Resource {
    public string name;
    public string path;
    public string createdDateProperty;
    public string modifiedDateProperty;
}

struct Environment {
    public string consumerKey;
    public string consumerSecret;
    public string url;
}