using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace XamlTestApplication.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private TestEnum _imageName;
        public MainPageViewModel()
        {
            ImageName = TestEnum.StoreLogo;
        }
        public TestEnum ImageName { get { return _imageName; }set { Set(ref _imageName, value); } }
    }
}

