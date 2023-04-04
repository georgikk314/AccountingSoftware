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
        public static void Validation(AccountingSoftwareContext dbContext, AddItemViewModel model, int userId, Inventory selectedItem)
        {
            bool isValid = true;
            try
            {
                if (model.ItemName.IsNullOrEmpty() || model.Description.IsNullOrEmpty() || model.SellingPrice.IsNullOrEmpty())
                {
                    model.IsEmptyFieldsMessageVisible = true;
                    isValid = false;
                }

                if (double.Parse(model.SellingPrice) <= 0)
                {
                    model.IsWrongAmountEnteredMessageVisible = true;
                    isValid = false;
                }

                if (isValid)
                {
                    model.IsSuccessfulItemAddedMessageVisible = true;

                    foreach (var item in dbContext.Inventory)
                    {
                        if(item.ItemId == selectedItem.ItemId)
                        {
                            item.ItemName = model.ItemName;
                            item.Description = model.Description;
                            item.SellingPrice = double.Parse(model.SellingPrice);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if(ex.Message == "Input string was not in a correct format.")
                {
                    model.IsWrongAmountEnteredMessageVisible = true;
                }

            }
        }
    }
}
