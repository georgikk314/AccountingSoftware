using AccountingSoftware.Data;
using AccountingSoftware.Data.Tables;
using AccountingSoftware.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XAct.Users;
using XSystem.Security.Cryptography;

namespace AccountingSoftware.Validations
{
    public class RegisterFormValidation
    {
        
        public static void Validation(AccountingSoftwareContext dbContext, AddUsersViewModel model)
        {
            /*var md5 = new MD5CryptoServiceProvider();
            byte[] Password = Encoding.ASCII.GetBytes(model.PasswordHash);
            model.PasswordHash = Convert.ToBase64String(md5.ComputeHash(Password));*/

            bool isValid = true;
            model.IsInvalidEntriesMessageVisible = false;
            model.IsNotMatchingPasswordsMessageVisible = false;

            // Check if any required field is empty
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.PasswordEntry) || string.IsNullOrEmpty(model.FirstName)
            || string.IsNullOrEmpty(model.LastName))
            {
                model.IsInvalidEntriesMessageVisible = true;
                isValid = false;
                return;
            }

            // Check if the username starts with an alphabetical symbol
            if (!char.IsLetter(model.Username[0]))
            {
                model.IsInvalidEntriesMessageVisible = true;
                isValid = false;
                return;
            }

            // Check if the password and confirm password fields match
            if (model.PasswordEntry != model.ConfirmPasswordEntry)
            {
                
                model.IsNotMatchingPasswordsMessageVisible = true;
                isValid = false;
                return;
            }

            if (isValid)
            {
                var md5 = new MD5CryptoServiceProvider();
                byte[] Password = Encoding.ASCII.GetBytes(model.PasswordHash);
                model.PasswordHash = Convert.ToBase64String(md5.ComputeHash(Password));

                dbContext.Add(new Users()
                {
                    Username = model.Username,
                    PasswordHash = model.PasswordHash,
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    LastName = model.LastName
                });

            }
            

        }
    }
}
