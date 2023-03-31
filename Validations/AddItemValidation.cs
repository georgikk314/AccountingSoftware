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
    public class AddItemValidation
    {
        public static void Validation(AccountingSoftwareContext dbContext, AddItemViewModel model, int userId)
        {
            bool isValid = true;
            try
            {
                if (model.ItemName.IsNullOrEmpty() || model.Description.IsNullOrEmpty() || model.Cost.IsNullOrEmpty() || model.QuantityInStock.IsNullOrEmpty() || model.SellingPrice.IsNullOrEmpty())
                {
                    model.IsEmptyFieldsMessageVisible = true;
                    isValid = false;
                }

                if (double.Parse(model.Cost) <= 0 || int.Parse(model.QuantityInStock) <= 0 || double.Parse(model.SellingPrice) <= 0)
                {
                    model.IsWrongAmountEnteredMessageVisible = true;
                    isValid = false;
                }

                if (isValid)
                {
                    model.IsSuccessfulItemAddedMessageVisible = true;
                    dbContext.Add(new Inventory
                    {
                        ItemName = model.ItemName,
                        Description = model.Description,
                        Cost = double.Parse(model.Cost),
                        SellingPrice = double.Parse(model.SellingPrice),
                        QuantityInStock = int.Parse(model.QuantityInStock),
                        UserId = userId
                    });
                }
            }
            catch (Exception ex)
            {
                if(ex.Message == "Input string was not in a correct format.")
                {
                    model.IsWrongAmountEnteredMessageVisible = true;
                }

                /*
                if(ex.InnerException is FormatException)
                {
                    model.IsWrongAmountEnteredMessageVisible = true;
                }*/
            }
        }
    }
}
