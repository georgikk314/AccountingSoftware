using AccountingSoftware.Data;
using AccountingSoftware.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;
using Microsoft.Maui.Controls;

namespace AccountingSoftware.Validations
{
    public class LoginValidation
    {
        public static int Validation(AccountingSoftwareContext dbContext, LoginViewModel model)
        {
            foreach (var user in dbContext.Users)
            {
                if (user.Username == model.Username)
                {
                    //check hashed password from the database with the entered password which we now hash, i.e we compare hashes
                    var md5 = new MD5CryptoServiceProvider();
                                byte[] Password =   Encoding.ASCII.GetBytes(model.Password);
                                var CheckPassword = Convert.ToBase64String(md5.ComputeHash(Password));

                    if(user.PasswordHash == CheckPassword)
                    {
                        return user.UserId;
                    }
                }
            }
            return -1;
            
        }
 
    }
}
