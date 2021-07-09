using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace theRightDirection.WPF.Controls.DemoApp
{
    internal class ViewModel : MvxViewModel
    {
        private Model _someModel, _someModel2;

        public MvxCommand ChangeStringPropertyCommand { get; }

        public ViewModel()
        {
            ChangeStringPropertyCommand = new MvxCommand(ChangeStringProperty);
            SomeModel = new Model();
        }

        private void ChangeStringProperty()
        {
            if (SomeModel.StringProperty == null)
            {
                SomeModel = new Model() { StringProperty = string.Empty };
                return;
            }
            if (SomeModel.StringProperty.Equals(string.Empty))
            {
                SomeModel = new Model() { StringProperty = "mannus" };
                return;
            }
            if (SomeModel.StringProperty.Equals("mannus"))
            {
                SomeModel = new Model() { StringProperty = null };
            }
        }

        public Model SomeModel
        {
            get => _someModel;
            set => SetProperty(ref _someModel, value);
        }
        public Model SomeModel2
        {
            get => _someModel2;
            set => SetProperty(ref _someModel2, value);
        }
    }
}