using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace theRightDirection.WPF.DemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new WebClient { Proxy = null };
            client.DownloadStringCompleted += delegate (object sender2, DownloadStringCompletedEventArgs args)
            {
                //JsonViewer.Load(args.Result);
            };
            client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/users"));
        }
    }
}
