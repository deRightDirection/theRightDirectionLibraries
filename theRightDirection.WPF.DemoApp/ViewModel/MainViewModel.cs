using System;
using System.Net;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace theRightDirection.WPF.DemoApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _json;
        public RelayCommand LoadJsonCommand { get; private set; }

        public MainViewModel()
        {
            LoadJsonCommand = new RelayCommand(LoadJson);
        }

        private void LoadJson()
        {
            var client = new WebClient { Proxy = null };
            client.DownloadStringCompleted += delegate (object sender, DownloadStringCompletedEventArgs args)
            {
                Json = args.Result;
            };
            client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/users"));
        }

        public string Json
        {
            get { return _json; }
            set { Set(ref _json, value); }
        }

        public bool Visible
        {
            get { return false; }
        }
    }
}