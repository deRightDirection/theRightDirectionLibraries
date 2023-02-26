using System.Collections.Generic;

namespace theRightDirection.WPF.Controls.Json
{
    internal class JsonInformation
    {
        public JsonInformation()
        {
            Items = new List<JsonInformation>();
            ShowDivider = true;
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public List<JsonInformation> Items { get; set; }
        public TypeOfJsonElement Type { get; set; }
        public bool ShowDivider { get; set; }
    }
}