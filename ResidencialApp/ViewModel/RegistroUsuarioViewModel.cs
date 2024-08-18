using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using ResidencialApp.Model;
using Xamarin.Forms;

namespace ResidencialApp.ViewModel
{
    public class RegistroUsuarioViewModel : INotifyPropertyChanged
    {
        private string email;
        private string password;
        private bool isOption1Checked;
        private bool isOption2Checked;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public bool IsOption1Checked
        {
            get => isOption1Checked;
            set
            {
                isOption1Checked = value;
                OnPropertyChanged();
            }
        }

        public bool IsOption2Checked
        {
            get => isOption2Checked;
            set
            {
                isOption2Checked = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand => new Command(async () => await RegisterUserAsync());


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly FirebaseAuthProvider _authProvider;
        private readonly FirebaseClient _firebaseClient;

        public RegistroUsuarioViewModel()
        {
            _authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAhdo9XJLRE4BvvrznS0xZNjov_NRY56OY")); 
            _firebaseClient = new FirebaseClient("https://seguridadcondominio-b75ef-default-rtdb.firebaseio.com");
        }

        public async Task RegisterUserAsync()
        {
            try
            {
                var user = new Usuario
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserType = IsOption1Checked ? "Residente" : IsOption2Checked ? "Personal de seguridad" : "",
                    Email = this.Email,
                    Password = this.Password, 
                    DateCreated = DateTime.UtcNow
                };

                await _firebaseClient
                    .Child("Users")
                    .PostAsync(user);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al registrar el usuario: " + ex.Message);
            }
        }

        public async Task<FirebaseAuthLink> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                return await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al autenticar el usuario: " + ex.Message);
            }
        }

        public async Task<string> GetUserTypeAsync(string userId)
        {
            try
            {
                var user = await _firebaseClient
                    .Child("Users")
                    .OrderBy("UserId")
                    .EqualTo(userId)
                    .OnceSingleAsync<Usuario>();

                return user?.UserType ?? "Unknown";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el tipo de usuario: " + ex.Message);
            }
        }
    }
}
