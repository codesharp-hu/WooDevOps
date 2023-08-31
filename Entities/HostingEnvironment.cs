public class HostingEnvironment
{
    public int Id { get; set; }
    public Uri? ApiEndpoint { get; set; }
    private string? ConsumerKey;
    private string? ConsumerSecret;
    public string? SshEndpoint { get; set; }
}
