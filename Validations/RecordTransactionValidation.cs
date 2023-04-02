using AccountingSoftware.Data;
using AccountingSoftware.Data.Tables;
using AccountingSoftware.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAct;
using static iTextSharp.text.pdf.AcroFields;

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
                    if (model.VendorOrCustomerName.IsNullOrEmpty() || model.Price.IsNullOrEmpty() || model.Description.IsNullOrEmpty() || model.ItemName.IsNullOrEmpty() || model.Quantity.IsNullOrEmpty())
                    {
                        isValid = false;
                        model.IsEmptyFieldsMessageVisible = true;
                    }

                    if (double.Parse(model.Price) <= 0 || int.Parse(model.Quantity) <= 0)
                    {
                        isValid = false;
                        model.IsWrongAmountEnteredMessageVisible = true;
                    }

                    foreach (var transaction in dbContext.Transactions)
                    {
                        
                        if(transaction.CustomerName == model.VendorOrCustomerName && transaction.UserId == userId) //this validation is only for customers
                        {
                            isValid = false;
                            model.IsNonExistingItemMessageVisible = true;

                            foreach (var item in dbContext.Inventory)
                            {
                                if (item.ItemName == model.ItemName) //wanted item is in the inventory
                                {
                                    isValid = true;
                                    model.IsNonExistingItemMessageVisible = false;
                                    if (item.QuantityInStock < int.Parse(model.Quantity)) //if we don't have enough quantity
                                    {
                                        model.IsInvalidQuantityMessageVisible = true;
                                        isValid = false;
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    if (isValid)
                    {

                        switch (model.TransactionType)
                        {
                            case "Expense":

                                isValid = false;
                                model.IsNonExistingVendorMessageVisible = true;
                                foreach (var vendor in dbContext.Vendors) //find the wanted vendor
                                {
                                    if (vendor.Name == model.VendorOrCustomerName && vendor.UserId == userId)
                                    {
                                        isValid = true;
                                        model.IsNonExistingVendorMessageVisible = false;
                                        dbContext.Add(new Transactions()
                                        {
                                            TransactionDate = model.TransactionDate,
                                            TransactionType = model.TransactionType,
                                            ItemName = model.ItemName,
                                            Quantity = int.Parse(model.Quantity),
                                            Price = double.Parse(model.Price),
                                            Description = model.Description,
                                            CustomerName = null,
                                            VendorName = model.VendorOrCustomerName,
                                            UserId = userId
                                        });

                                        dbContext.Add(new Inventory()
                                        {
                                            ItemName = model.ItemName,
                                            QuantityInStock = int.Parse(model.Quantity),
                                            Cost = double.Parse(model.Price),
                                            SellingPrice = double.Parse(model.Price),
                                            Description = null,
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
                                    if (customer.Name == model.VendorOrCustomerName && customer.UserId == userId)
                                    {
                                        isValid = true;
                                        model.IsNonExistingCustomerMessageVisible = false;
                                        dbContext.Add(new Transactions()
                                        {
                                            TransactionDate = model.TransactionDate,
                                            TransactionType = model.TransactionType,
                                            ItemName = model.ItemName,
                                            Quantity = int.Parse(model.Quantity),
                                            Price = double.Parse(model.Price),
                                            Description = model.Description,
                                            CustomerName = model.VendorOrCustomerName,
                                            VendorName = null,
                                            UserId = userId
                                        });

                                        foreach (var item in dbContext.Inventory)
                                        {
                                            if (item.ItemName == model.ItemName)
                                            {
                                                if (item.QuantityInStock == int.Parse(model.Quantity))
                                                {
                                                    dbContext.Remove(item);
                                                }
                                                else
                                                {
                                                    item.QuantityInStock = item.QuantityInStock - int.Parse(model.Quantity);
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Input string was not in a correct format.")
                {
                    model.IsWrongAmountEnteredMessageVisible = true;
                    isValid = false;
                }
            }

            if (isValid)
            {
                model.IsSuccessfulTransactionMessageVisible = true;
            }
        }
    }
}
