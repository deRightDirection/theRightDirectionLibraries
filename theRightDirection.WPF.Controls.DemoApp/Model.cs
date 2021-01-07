using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.WPF.Controls.DemoApp
{
    internal class Model
    {
        public Model()
        {
            StringProperty = null;
        }

        public bool BooleanProperty { get; set; }
        public string StringProperty { get; set; }
    }
}