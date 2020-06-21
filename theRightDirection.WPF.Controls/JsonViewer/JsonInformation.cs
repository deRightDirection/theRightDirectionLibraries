using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.WPF.Xaml.Controls.JsonViewer
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