using System;
using System.Text.Json.Nodes;

namespace theRightDirection;

public static partial class Extensions
{
    public static JsonNode GetNodeUsingPath(this JsonNode root, string path)
    {
        if (root == null || string.IsNullOrEmpty(path))
            return null;
        string[] parts = path.Split('.', StringSplitOptions.RemoveEmptyEntries);
        JsonNode currentNode = root;

        // ignore 0th element as it is for root $
        for (int x = 1; x < parts.Length; x++)
        {
            if (currentNode == null)
                return null;
            currentNode = currentNode[parts[x]];
        }
        return currentNode;
    }
}
