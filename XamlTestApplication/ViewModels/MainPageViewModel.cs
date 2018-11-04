using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using theRightDirection.UWP.CloudServices.Strava;

namespace XamlTestApplication.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private TestEnum _imageName;
        private double _amount;
        private string _textBoxText, _accessToken;
        private List<string> _emailadresses;
        private DelegateCommand<IList<object>> _selectionChangedCommand;
        public DelegateCommand StartStravaCommand { get; private set; }

        public MainPageViewModel()
        {
            StartStravaCommand = new DelegateCommand(() => StartStrava());
            ImageName = TestEnum.StoreLogo;
            Amount = 12.345;
            EmailAddresses = new List<string>() { "info@test.nl", "test@test.nl" };
            TextBoxText = "BasketBalNieuws";
        }
        public TestEnum ImageName { get { return _imageName; } set { Set(ref _imageName, value); } }
        public double Amount { get { return _amount; } set { Set(ref _amount, value); } }
        public IEnumerable<string> EmailAddresses { get { return _emailadresses; } set { Set<List<string>>(ref _emailadresses, value.ToList()); } }
        public string TextBoxText
        {
            get { return _textBoxText; }
            set { Set(ref _textBoxText, value); }
        }
        public string StravaAccessToken
        {
            get { return _accessToken; }
            set { Set(ref _accessToken, value); }
        }

        private async Task StartStrava()
        {
            var stravaConnector = new StravaServiceConnector("13576", "1f555d00a79b0869fb2cf3fc3639f71164e0b5a8");
            StravaAccessToken = await stravaConnector.GetAccessToken();
        }
    }
}