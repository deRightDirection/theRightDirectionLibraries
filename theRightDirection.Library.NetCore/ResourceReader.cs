using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic.CompilerServices;

namespace theRightDirection;

public class ResourceReader
{
    /// <summary>
    /// get the list of resources in the folder, resource files are not allowed to have multiple .-character in the name except for the extension
    /// </summary>
    public IEnumerable<string> GetResources(string subFolder = "", bool callingAssembly = true)
    {
        var assembly = callingAssembly ? Assembly.GetCallingAssembly() : Assembly.GetExecutingAssembly();
        var resources = assembly.GetManifestResourceNames();
        var result = new List<string>();
        var module = assembly.ManifestModule.Name.Replace(".dll", string.Empty);
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
        var assembly = Assembly.GetExecutingAssembly();
        var resourcePath = GetStreamToResource(assembly, resourceName, subFolder);
        using var stream = assembly.GetManifestResourceStream(resourcePath);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    public byte[] ReadDataFromResourceAsFile(string resourceName, string subFolder = "")
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourcePath = GetStreamToResource(assembly, resourceName, subFolder);
        using var stream = assembly.GetManifestResourceStream(resourcePath);
        using MemoryStream ms = new MemoryStream();
        stream?.CopyTo(ms);
        return ms.ToArray();
    }

    public X509Certificate2 ReadDataFromResourceAsCertificate(string resourceName, string subFolder = "", bool isExecutingAssembly = true)
    {
        var assembly = isExecutingAssembly ? Assembly.GetExecutingAssembly() : Assembly.GetCallingAssembly();
        var resourcePath = GetStreamToResource(assembly, resourceName, subFolder);
        using var stream = assembly.GetManifestResourceStream(resourcePath);
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