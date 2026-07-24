using Json.Path;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace theRightDirection;

internal class SensitiveDataRemover(string sensitiveData)
{
    private static readonly Dictionary<string, int> tokennames = new()
    {
        { "token", 10 },
        { "apikey", 10 },
        { "fmetoken", 10 },
        { "email", 5 },
        { "relatienummer", 3},
        { "telefoon", 3 },
        { "naam", 3},
        { "clientid", 3},
        { "client_id", 3},
        { "access_token", 10},
        { "accesstoken", 10},
        { "refresh_token", 10},
        { "refreshttoken", 10}
    };

    public string Remove()
    {
        JsonNode jsonNode = null;
        try
        {
            jsonNode = JsonNode.Parse(sensitiveData);
            return RemoveForJson(jsonNode);
        }
        catch (Exception e)
        {
        }
        return RemoveForText();
    }

    private string RemoveForText()
    {
        var newText = sensitiveData;
        var parts = sensitiveData.Split("&", StringSplitOptions.RemoveEmptyEntries);
        foreach (var part in parts)
        {
            var keyValue = part.Split("=", StringSplitOptions.RemoveEmptyEntries);
            if (keyValue.Length == 2)
            {
                var key = keyValue[0].ToLowerInvariant();
                if (tokennames.ContainsKey(key))
                {
                    var stripLength = tokennames[key];
                    var newValue = keyValue[1].ToStripForLogging(stripLength);
                    newText = newText.Replace(keyValue[1], newValue);
                }
            }
        }
        return newText;
    }

    private string RemoveForJson(JsonNode jsonNode)
    {
        var newJson = sensitiveData;
        foreach (var tokenName in tokennames)
        {
            var path = JsonPath.Parse($"$..{tokenName.Key}");
            var results = path.Evaluate(jsonNode);
            var nodes = results.Matches;
            nodes.ForEach(n =>
            {
                var value = n.Value?.ToString();
                if (value.HasText())
                {
                    newJson = newJson.Replace(value, value.ToStripForLogging(tokenName.Value));
                }
            });
        }
        return newJson;
    }
}
