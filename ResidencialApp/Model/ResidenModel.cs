using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Firebase.Database;
using Firebase.Database.Query;

namespace ResidencialApp.Model
{
    public class ResidenModel : INotifyPropertyChanged
    {
        private string currentDate;
        private string currentTime;
        private Timer timer;

        public event PropertyChangedEventHandler PropertyChanged;

        public string UserId { get; set; }
        public string UserType { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CurrentTime
        {
            get => currentTime;
            set
            {
                if (currentTime != value)
                {
                    currentTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CurrentDate
        {
            get => currentDate;
            set
            {
                if (currentDate != value)
                {
                    currentDate = value;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task CreateUserInFirebase()
        {
            var firebase = new FirebaseClient("https://seguridadcondominio-b75ef-default-rtdb.firebaseio.com/");

            var user = new
            {
                UserId = Guid.NewGuid().ToString(),
                UserType = this.UserType,
                Email = this.Email,
                DateCreated = DateTime.UtcNow
            };

            await firebase
              .Child("Users")
              .PostAsync(user);
        }
    }
}

