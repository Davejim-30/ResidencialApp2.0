using ResidencialApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResidencialApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroAccidente : ContentPage
    {
        public RegistroAccidente()
        {
            InitializeComponent();
            this.BindingContext = new ResidenInicioViewModel();
        }
    }
}