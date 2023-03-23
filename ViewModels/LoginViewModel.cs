using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSoftware.ViewModels
{
    public class LoginViewModel : BindableObject
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
