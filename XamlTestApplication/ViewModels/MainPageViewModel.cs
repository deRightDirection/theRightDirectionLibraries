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
        private double _amount;

        public MainPageViewModel()
        {
            ImageName = TestEnum.StoreLogo;
            Amount = 12.345;
        }
        public TestEnum ImageName { get { return _imageName; }set { Set(ref _imageName, value); } }
        public Double Amount { get { return _amount; } set { Set(ref _amount, value); } }

    }
}

