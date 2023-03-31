using AccountingSoftware.Data;
using AccountingSoftware.Data.Tables;
using AccountingSoftware.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAct;

namespace AccountingSoftware.Validations
{
    public class RecordTransactionValidation
    {
        public static void Validation(AccountingSoftwareContext dbContext, AddTransactionViewModel model, int userId)
        {
            bool isValid = true;

            try
            {
                if (model.TransactionType.IsNullOrEmpty())
                {
                    isValid = false;
                    model.IsChosenTransactionTypeMessageVisible = true;
                }
                else
                {
                    if (model.VendorOrCustomerName.IsNullOrEmpty() || model.AmountEntry.IsNullOrEmpty() || model.Description.IsNullOrEmpty())
                    {
                        isValid = false;
                        model.IsEmptyFieldsMessageVisible = true;
                    }

                    if (double.Parse(model.AmountEntry) <= 0)
                    {
                        isValid = false;
                        model.IsWrongAmountEnteredMessageVisible = true;
                    }

                    switch (model.TransactionType)
                    {
                        case "Expense":

                            isValid = false;
                            model.IsNonExistingVendorMessageVisible = true;
                            foreach (var vendor in dbContext.Vendors)
                            {
                                if (vendor.Name == model.VendorOrCustomerName && vendor.UserId == userId)
                                {
                                    isValid = true;
                                    model.IsNonExistingVendorMessageVisible = false;
                                    dbContext.Add(new Transactions()
                                    {
                                        TransactionDate = model.TransactionDate,
                                        Amount = double.Parse(model.AmountEntry),
                                        Description = model.Description,
                                        CustomerName = null,
                                        VendorName = model.VendorOrCustomerName,
                                        UserId = userId
                                    });
                                }
                            }

                            break;

                        case "Income":

                            isValid = false;
                            model.IsNonExistingCustomerMessageVisible = true;
                            foreach (var customer in dbContext.Customers)
                            {
                                if (customer.Name == model.VendorOrCustomerName)
                                {
                                    isValid = true;
                                    model.IsNonExistingCustomerMessageVisible = false;
                                    dbContext.Add(new Transactions()
                                    {
                                        TransactionDate = model.TransactionDate,
                                        TransactionType = model.TransactionType,
                                        Amount = double.Parse(model.AmountEntry),
                                        Description = model.Description,
                                        CustomerName = model.VendorOrCustomerName,
                                        VendorName = null,
                                        UserId = userId
                                    });
                                }
                            }

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Input string was not in a correct format.")
                {
                    model.IsWrongAmountEnteredMessageVisible = true;
                }

                /*
                if(ex.InnerException is FormatException)
                {
                    model.IsWrongAmountEnteredMessageVisible = true;
                }*/
            }

            if (isValid)
            {
                model.IsSuccessfulTransactionMessageVisible = true;
            }
        }
    }
}
