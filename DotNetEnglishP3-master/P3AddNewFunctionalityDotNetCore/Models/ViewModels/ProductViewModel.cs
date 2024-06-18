using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "ErrorStockValue")]
        [RegularExpression(@"^\d+$", ErrorMessage = "ErrorStockValue")]
        [Range(1, int.MaxValue, ErrorMessage = "ErrorStockValue")]
        public string Stock { get; set; }

        [Required(ErrorMessage = "ErrorPriceValue")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "ErrorPriceValue")]
        [Range(0.01, double.MaxValue, ErrorMessage = "ErrorPriceValue")]
        public string Price { get; set; }
    }
}
