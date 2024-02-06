using skit.Core.Files.Enums;
using skit.Shared.Abstractions.Entities;

namespace skit.Core.Files.Entities;

public sealed class SystemFile : Entity
{
    public string Name { get; private set; }
    public FileType Type { get; private set; }
    public long Size { get; private set; }
    public string S3Key { get; private set; }
    public string ContentType { get; private set; }

    private SystemFile() { }

    private SystemFile(string name, FileType type, long size, string s3Key, string contentType)
    {
        Name = name;
        Type = type;
        Size = size;
        S3Key = s3Key;
        ContentType = contentType;
    }

    public static SystemFile Create(string name, FileType type, long size, string s3Key, string contentType)
        => new(name, type, size, s3Key, contentType);
}