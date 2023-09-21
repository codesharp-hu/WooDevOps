public class HostingEnvironment
{
    public int Id { get; set; }
    public Uri? ApiEndpoint { get; set; }
    private string? ConsumerKey { get; set; }
    private string? ConsumerSecret { get; set; }
    public string? SshEndpoint { get; set; }
}
