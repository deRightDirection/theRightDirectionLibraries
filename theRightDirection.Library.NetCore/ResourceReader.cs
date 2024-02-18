using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace theRightDirection;

public class ResourceReader
{
    private readonly Assembly _assembly;

    public ResourceReader(Assembly assembly)
    {
        _assembly = assembly;
    }
    /// <summary>
    /// get the list of resources in the folder, resource files are not allowed to have multiple .-character in the name except for the extension
    /// </summary>
    public IEnumerable<string> GetResources(string subFolder = "")
    {
        var resources = _assembly.GetManifestResourceNames();
        var result = new List<string>();
        var module = _assembly.ManifestModule.Name.Replace(".dll", string.Empty);
        resources.ForEach(x =>
        {
            var resourceWithoutPrefix = x.Replace($"{module}.", string.Empty);
            var add = CanFileBeAdded(resourceWithoutPrefix, subFolder);
            if (add)
            {
                var fileNameToAdd = subFolder.HasText()
                    ? resourceWithoutPrefix.Replace($"{subFolder}.", string.Empty)
                    : resourceWithoutPrefix;
                result.Add(fileNameToAdd);
            }
        });
        return result;
    }

    private static bool CanFileBeAdded(string fileName, string subFolder)
    {
        var splitParts = fileName.Split('.');
        if (subFolder.HasNoText())
        {
            if (splitParts.Length == 2)
            {
                return true;
            }
        }
        else
        {
            if (splitParts.Length > 2 && splitParts[0].Equals(subFolder, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }

    public string ReadDataFromResource(string resourceName, string subFolder = "")
    {
        var resourcePath = GetStreamToResource(_assembly, resourceName, subFolder);
        using var stream = _assembly.GetManifestResourceStream(resourcePath);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    public byte[] ReadDataFromResourceAsFile(string resourceName, string subFolder = "")
    {
        var resourcePath = GetStreamToResource(_assembly, resourceName, subFolder);
        using var stream = _assembly.GetManifestResourceStream(resourcePath);
        using MemoryStream ms = new MemoryStream();
        stream?.CopyTo(ms);
        return ms.ToArray();
    }

    public X509Certificate2 ReadDataFromResourceAsCertificate(string resourceName, string subFolder = "")
    {
        var resourcePath = GetStreamToResource(_assembly, resourceName, subFolder);
        using var stream = _assembly.GetManifestResourceStream(resourcePath);
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return new X509Certificate2(memoryStream.ToArray());
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