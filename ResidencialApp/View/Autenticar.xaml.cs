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
    public partial class Autenticar : ContentPage
    {
        private readonly RegistroUsuarioViewModel _userService;

        public Autenticar()
        {
            InitializeComponent();
            _userService = new RegistroUsuarioViewModel();
        }

        private async void IniciarSesion_Clicked(object sender, EventArgs e)
        {
            try
            {
                var email = "correo@example.com"; // Captura este dato del formulario
                var password = "contraseña"; // Captura este dato del formulario

                // Usa el método correcto AuthenticateUserAsync
                var auth = await _userService.AuthenticateUserAsync(email, password);
                var userType = await _userService.GetUserTypeAsync(auth.User.LocalId);

                switch (userType)
                {
                    case "Admin":
                        await Navigation.PushAsync(new AdminInicio());
                        break;
                    case "Guarda":
                        await Navigation.PushAsync(new GuardaInicio());
                        break;
                    case "Residente":
                        await Navigation.PushAsync(new ResidenInicio());
                        break;
                    default:
                        await DisplayAlert("Error", "Tipo de usuario no reconocido", "OK");
                        break;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
