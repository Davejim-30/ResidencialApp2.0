using ResidencialApp.Model;
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
    public partial class ResidenInicio : ContentPage
    {
        
        public ResidenInicio()
        {
            InitializeComponent();
            this.BindingContext = new ResidenInicioViewModel();
            
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
        }
        private void RegistrarInccidente_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistroAccidente());
        }
        private void RegistrarUsuarios_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistroUsuario());
        }
        private void ConsultarAccidentes_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConsultarAccidente());
        }

    }

}