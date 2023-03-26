using System;

public class ResourceReader
{
    public string ReadDataFromResource(string resourceName)
    {
        var assembly = Assembly.GetCallingAssembly();
        var module = assembly.ManifestModule.Name.Replace(".dll", string.Empty);
        using var stream = assembly.GetManifestResourceStream($"{module}.{resourceName}");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
    public byte[] ReadDataFromResourceAsFile(string resourceName)
    {
        var assembly = Assembly.GetCallingAssembly();
        var module = assembly.ManifestModule.Name.Replace(".dll", string.Empty);
        using var stream = assembly.GetManifestResourceStream($"{module}.{resourceName}");
        using MemoryStream ms = new MemoryStream();
        stream?.CopyTo(ms);
        return ms.ToArray();
    }
}