using AccountingSoftware.Data;
using AccountingSoftware.Data.Tables;
using AccountingSoftware.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccountingSoftware.Validations
{
    public class ClientManagementValidation
    {
        public static void Validation(AccountingSoftwareContext dbContext, AddClientViewModel model, int userId)
        {
            bool isValid = true;

            if(string.IsNullOrEmpty(model.Address) || string.IsNullOrEmpty(model.PhoneNumber) || string.IsNullOrEmpty(model.ClientName) || string.IsNullOrEmpty(model.Email))
            {
                model.IsEmptyFieldsMessageVisible = true;
                isValid = false;
                return;
            }

            if (!Regex.IsMatch(model.Email, "^[a-zA-Z0-9.!#$%&'*+\\/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$"))
            {
                model.IsNotCorrectEmailMessageVisible = true;
                isValid = false;
                return;
            }

            if (!Regex.IsMatch(model.PhoneNumber, "[0-9]"))
            {
                model.IsNotCorrectPhoneMessageVisible = true;
                isValid = false; 
                return;    
            }

            if(isValid)
            {
                dbContext.Add(new Customers()
                {
                    Name = model.ClientName,
                    Email = model.Email,
                    Address = model.Address,
                    Phone = model.PhoneNumber,
                    UserId = userId
                });
            }

        }
    }
}
