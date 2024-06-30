using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using Xunit;
using P3AddNewFunctionalityDotNetCore.Models;
using Moq;
using System;
using Xunit.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public void CheckMissingName()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {               
                Stock = "10",
                Price = "100.00"
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorMissingName");
        }

        [Fact]
        public void CheckMissingStock()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Name = "New Product",
                Price = "100.00"
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorMissingStock");
        }

        [Fact]
        public void CheckNumberInStock()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Name = "New Product",
                Price = "100.00",
                Stock = "a",
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorStockNotAnInteger");
        }

        [Fact]
        public void CheckIntInStock()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Name = "New Product",
                Price = "100.00",
                Stock = "2.3",
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorStockNotAnInteger");
        }

        [Fact]
        public void CheckRangeInStock()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Name = "New Product",
                Price = "100.00",
                Stock = "-1",
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorStockNotGreaterThanZero");
        }

        [Fact]
        public void CheckNotZeroInStock()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Name = "New Product",
                Price = "100.00",
                Stock = "0",
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorStockNotGreaterThanZero");
        }

        [Fact]
        public void CheckMissingPrice()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Name = "New Product",                
                Stock = "4",
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorMissingPrice");
        }

        [Fact]
        public void CheckNumberInPrice()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Name = "New Product",
                Price = "a",
                Stock = "3",
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorPriceNotANumber");
        }

        [Fact]
        public void CheckDecimalsInPrice()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Name = "New Product",
                Price = "4.239",
                Stock = "3",
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorPriceNotANumber");
        }

        [Fact]
        public void CheckNumberFormatInPrice()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Name = "New Product",
                Price = "4.",
                Stock = "3",
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorPriceNotANumber");
        }

        [Fact]
        public void CheckRangeInPrice()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Name = "New Product",
                Price = "-1",
                Stock = "3",
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorPriceNotGreaterThanZero");
        }

        [Fact]
        public void CheckNotZeroInPrice()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Name = "New Product",
                Price = "0",
                Stock = "3",
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorPriceNotGreaterThanZero");
        }
    }   
}