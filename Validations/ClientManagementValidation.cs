using AccountingSoftware.Data;
using AccountingSoftware.Data.Tables;
using AccountingSoftware.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;
using XAct.Users;

namespace AccountingSoftware.Validations
{
    public class ClientManagementValidation
    {
        public static void Validation(AccountingSoftwareContext dbContext, AddClientViewModel model, int userId)
        {
            bool isValid = true;
            model.IsEmptyFieldsMessageVisible = false;
            model.IsNotCorrectEmailMessageVisible = false;
            model.IsNotCorrectPhoneMessageVisible = false;
            model.IsClientAddedVisible = false;

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
                model.IsClientAddedVisible = true;

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

        public static void Validation(AccountingSoftwareContext dbContext, EditClientViewModel model, int userId, Customers selectedItem)
        {
            bool isValid = true;
            model.IsEmptyFieldsMessageVisible = false;
            model.IsNotCorrectEmailMessageVisible = false;
            model.IsNotCorrectPhoneMessageVisible = false;
            model.IsClientAddedVisible = false;

            if (string.IsNullOrEmpty(model.Address) || string.IsNullOrEmpty(model.Phone) || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Email))
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

            if (!Regex.IsMatch(model.Phone, "[0-9]"))
            {
                model.IsNotCorrectPhoneMessageVisible = true;
                isValid = false;
                return;
            }

            if (isValid)
            {
                

                foreach (var customer in dbContext.Customers)
                {
                    if(customer.UserId == userId && customer.Name == selectedItem.Name)
                    {
                        model.IsClientAddedVisible = true;
                        customer.Name = model.Name;
                        customer.Email = model.Email;
                        customer.Address = model.Address;
                        customer.Phone = model.Phone;
                    }
                }
                
                    
                
            }

        }
    }
}
