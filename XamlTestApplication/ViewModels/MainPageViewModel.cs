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
        private List<string> _emailadresses;
        private DelegateCommand<IList<object>> _selectionChangedCommand;

        public MainPageViewModel()
        {
            ImageName = TestEnum.StoreLogo;
            Amount = 12.345;
            EmailAddresses = new List<string>() { "info@test.nl", "test@test.nl" };
        }
        public TestEnum ImageName { get { return _imageName; }set { Set(ref _imageName, value); } }
        public double Amount { get { return _amount; } set { Set(ref _amount, value); } }  
        public IEnumerable<string> EmailAddresses { get { return _emailadresses; } set { Set<List<string>>(ref _emailadresses, value.ToList()); } }
        
    }
}

