namespace skit.Infrastructure.Integrations.FileStorage.Configuration;

public sealed class S3Config
{
    public string S3Url { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string BucketName { get; set; }
    public TimeSpan UrlExpires { get; set; }
}