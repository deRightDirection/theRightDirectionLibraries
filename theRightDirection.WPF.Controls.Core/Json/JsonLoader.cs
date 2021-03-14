using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace theRightDirection.WPF.Controls.Json
{
    internal class JsonLoader
    {
        private List<JsonInformation> _root;

        public JsonLoader()
        {
            _root = new List<JsonInformation>();
        }

        public List<JsonInformation> LoadJsonToTreeView(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            var @object = JObject.Parse(json);
            AddObjectNodes(@object, "JSON", null);
            return _root;
        }

        public void AddObjectNodes(JObject @object, string name, JsonInformation parent)
        {
            var node = new JsonInformation() { Name = name };
            var arrayObjectNode = name.Contains("[") && name.Contains("]");
            node.Type = arrayObjectNode ? TypeOfJsonElement.ArrayObjectNode : node.Type;
            node.ShowDivider = node.Type != TypeOfJsonElement.ArrayObjectNode;
            if (parent == null)
            {
                _root.Add(node);
            }
            else
            {
                parent.Items.Add(node);
            }
            foreach (var property in @object.Properties())
            {
                AddTokenNodes(property.Value, property.Name, node);
            }
        }

        private void AddArrayNodes(JArray array, string name, JsonInformation parent)
        {
            var node = new JsonInformation() { Name = name, Value = $"array [{array.Count}]", Type = TypeOfJsonElement.Array };
            parent.Items.Add(node);

            for (var i = 0; i < array.Count; i++)
            {
                AddTokenNodes(array[i], string.Format("[{0}]", i), node);
            }
        }

        private void AddTokenNodes(JToken token, string name, JsonInformation parent)
        {
            if (token is JValue jtoken)
            {
                var node = new JsonInformation() { Name = name, Value = jtoken.Value == null ? string.Empty : jtoken.Value.ToString(), Type = TypeOfJsonElement.Value };
                parent.Items.Add(node);
            }
            else if (token is JArray)
            {
                AddArrayNodes((JArray)token, name, parent);
            }
            else if (token is JObject)
            {
                AddObjectNodes((JObject)token, name, parent);
            }
        }
    }
}