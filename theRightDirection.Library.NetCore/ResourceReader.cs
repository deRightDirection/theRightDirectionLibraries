using System.IO;
using System.Reflection;

namespace theRightDirection;

public class ResourceReader
{
    public string ReadDataFromResource(string resourceName, string subFolder = "")
    {
        var assembly = Assembly.GetCallingAssembly();
        var resourcePath = GetStreamToResource(assembly, resourceName, subFolder);
        using var stream = assembly.GetManifestResourceStream(resourcePath);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    public byte[] ReadDataFromResourceAsFile(string resourceName, string subFolder = "")
    {
        var assembly = Assembly.GetCallingAssembly();
        var resourcePath = GetStreamToResource(assembly, resourceName, subFolder);
        using var stream = assembly.GetManifestResourceStream(resourcePath);
        using MemoryStream ms = new MemoryStream();
        stream?.CopyTo(ms);
        return ms.ToArray();
    }

    private string GetStreamToResource(Assembly assembly, string resourceName, string subFolder)
    {
        var module = assembly.ManifestModule.Name.Replace(".dll", string.Empty);
        var resourcePath = $"{module}.{resourceName}";
        if (subFolder.HasText())
        {
            resourcePath = $"{module}.{subFolder}.{resourceName}";
        }
        return resourcePath;
    }
}