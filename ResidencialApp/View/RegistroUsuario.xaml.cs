using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ResidencialApp.ViewModel;
using Firebase.Database;
using Firebase.Database.Query;

namespace ResidencialApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroUsuario : ContentPage
    {
        private readonly RegistroUsuarioViewModel _viewModel;

        public RegistroUsuario()
        {
            InitializeComponent();
            _viewModel = new RegistroUsuarioViewModel();
            BindingContext = new RegistroUsuarioViewModel();
        }

        private async void Registrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var email = _viewModel.Email;
                var password = _viewModel.Password;
                var userType = _viewModel.IsOption1Checked ? "Residente" : _viewModel.IsOption2Checked ? "Personal de seguridad" : "";

                // Guardar usuario en Firebase Realtime Database
                var firebaseClient = new FirebaseClient("https://seguridadcondominio-b75ef-default-rtdb.firebaseio.com/");
                var newUser = new { Email = email, Password = password, UserType = userType };
                await firebaseClient
                    .Child("Users")
                    .PostAsync(newUser);

                await DisplayAlert("Éxito", "Usuario registrado con éxito", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
        private async void Autenticar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var email = _viewModel.Email;
                var password = _viewModel.Password;

                var auth = await _viewModel.AuthenticateUserAsync(email, password);

                // Aquí debes redirigir al usuario a la página correspondiente
                // Suponiendo que tienes una forma de identificar el tipo de usuario
                var userType = await _viewModel.GetUserTypeAsync(auth.User.LocalId);

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
