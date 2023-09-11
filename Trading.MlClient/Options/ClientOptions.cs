namespace Trading.MlClient.Options;

public class ClientOptions
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }

    internal Uri GetUrl()
    {
        return new Uri($"http://{Host}:{Port}");
    }
}