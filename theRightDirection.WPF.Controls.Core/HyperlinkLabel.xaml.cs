using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace theRightDirection.WPF.Controls
{
    public partial class HyperlinkLabel : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string),
            typeof(HyperlinkLabel), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty UrlProperty = DependencyProperty.Register("Url", typeof(Uri),
            typeof(HyperlinkLabel), new PropertyMetadata(default(Uri)));

        public HyperlinkLabel()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public Uri Url
        {
            get { return (Uri)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            e.Uri.OpenInBrowser();
        }
    }
}