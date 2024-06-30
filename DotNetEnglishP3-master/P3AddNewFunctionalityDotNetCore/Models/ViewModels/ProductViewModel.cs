using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "ErrorMissingName")]
        public string Name { get; set; }
                
        public string Description { get; set; }
                
        public string Details { get; set; }

        [Required(ErrorMessage = "ErrorMissingStock")]
        [RegularExpression(@"^\d+$", ErrorMessage = "ErrorStockNotAnInteger")]
        [Range(1, int.MaxValue, ErrorMessage = "ErrorStockNotGreaterThanZero")]
        public string Stock { get; set; }

        [Required(ErrorMessage = "ErrorMissingPrice")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "ErrorPriceNotANumber")]
        [Range(0.01, double.MaxValue, ErrorMessage = "ErrorPriceNotGreaterThanZero")]
        public string Price { get; set; }

        public double IntPrice()
        {
            if (double.TryParse(Price, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }
            throw new FormatException("Price is not in a correct format.");
        }

        public string PriceViewMoney()
        {
            return CultureInfo.CurrentCulture.Name == "fr"
                ? IntPrice().ToString("C", CultureInfo.CreateSpecificCulture("fr-FR")).Replace("¤", "€")
                : IntPrice().ToString("C", CultureInfo.CreateSpecificCulture("en-US")).Replace("¤", "$");
        }
    }
}
