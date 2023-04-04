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
    public class VendorManagementValidation
    {
        public static void Validation(AccountingSoftwareContext dbContext, AddVendorViewModel model, int userId)
        {
            bool isValid = true;
            model.IsEmptyFieldsMessageVisible = false;
            model.IsNotCorrectEmailMessageVisible = false;
            model.IsNotCorrectPhoneMessageVisible = false;
            model.IsAddedVendorVisible = false;

            if (string.IsNullOrEmpty(model.Address) || string.IsNullOrEmpty(model.PhoneNumber) || string.IsNullOrEmpty(model.VendorName) || string.IsNullOrEmpty(model.Email))
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

            if (isValid)
            {
                model.IsAddedVendorVisible = true;
                dbContext.Add(new Vendors()
                {
                    Name = model.VendorName,
                    Email = model.Email,
                    Address = model.Address,
                    Phone = model.PhoneNumber,
                    UserId = userId
                });
            }
        }


        public static void Validation(AccountingSoftwareContext dbContext, EditVendorViewModel model, int userId, Vendors selectedItem)
        {
            bool isValid = true;
            model.IsEmptyFieldsMessageVisible = false;
            model.IsNotCorrectEmailMessageVisible = false;
            model.IsNotCorrectPhoneMessageVisible = false;
            model.IsAddedVendorVisible = false;

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
                

                foreach (var vendor in dbContext.Vendors)
                {
                    if (vendor.UserId == userId && vendor.Name == selectedItem.Name)
                    {
                        model.IsAddedVendorVisible = true;
                        //vendor.Name = model.Name;
                        vendor.Email = model.Email;
                        vendor.Address = model.Address;
                        vendor.Phone = model.Phone;
                    }
                }
            }
        }
    }
}
