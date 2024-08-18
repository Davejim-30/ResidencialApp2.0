using System;
using System.Collections.Generic;
using System.Text;

namespace ResidencialApp.Model
{
    public class Usuario

    {
        public string UserId { get; set; }
        public string UserType { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
    }
}