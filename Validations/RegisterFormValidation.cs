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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XAct.Users;
using XSystem.Security.Cryptography;

namespace AccountingSoftware.Validations
{
    public class RegisterFormValidation
    {
        
        public static void Validation(AccountingSoftwareContext dbContext, AddUsersViewModel model)
        {
            bool isValid = true;
            model.IsInvalidEntriesMessageVisible = false;
            model.IsNotMatchingPasswordsMessageVisible = false;
            model.IsNotCorrectEmailMessageVisible = false;

            // Check if any required field is empty
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.PasswordEntry) || string.IsNullOrEmpty(model.FirstName)
            || string.IsNullOrEmpty(model.LastName)
            || string.IsNullOrEmpty(model.Email))
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

            //Check if email is correct
            if (!Regex.IsMatch(model.Email, "^[a-zA-Z0-9.!#$%&'*+\\/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$"))
            {
                model.IsNotCorrectEmailMessageVisible = true;
                isValid = false;
                return;
            }

            if (isValid)
            {
                //hash the password with md5 encryption
                var md5 = new MD5CryptoServiceProvider();
                byte[] Password = Encoding.ASCII.GetBytes(model.PasswordEntry);
                model.PasswordHash = Convert.ToBase64String(md5.ComputeHash(Password));

                //after validation we add the elements to the db
                dbContext.Add(new Users()
                {
                    Username = model.Username,
                    PasswordHash = model.PasswordHash,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    LastName = model.LastName
                });

            }
            

        }
    }
}
