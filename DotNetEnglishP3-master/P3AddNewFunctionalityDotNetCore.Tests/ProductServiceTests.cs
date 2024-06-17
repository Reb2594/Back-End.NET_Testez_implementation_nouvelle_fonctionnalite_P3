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
        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>
        /// 
        private readonly ITestOutputHelper output;
        public ProductServiceTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public void ExampleMethod()
        {
            // Arrange

            // Act


            // Assert
            Assert.Equal(1, 1);
        }

        // TODO write test methods to ensure a correct coverage of all possibilities

        [Fact]
        public void CheckMissingName()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Description = "Description",
                Details = "Details",
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
                Description = "Description",
                Details = "Details",
                Name = "New Product",
                Price = "100.00"
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorStockValue");
        }

        [Fact]
        public void CheckNumberInStock()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Description = "Description",
                Details = "Details",
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
            Assert.Contains(results, v => v.ErrorMessage == "ErrorStockValue");
        }

        [Fact]
        public void CheckRangeInStock()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Description = "Description",
                Details = "Details",
                Name = "New Product",
                Price = "100.00",
                Stock = "-3",
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorStockValue");
        }

        [Fact]
        public void CheckIntInStock()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Description = "Description",
                Details = "Details",
                Name = "New Product",
                Price = "100.00",
                Stock = "2,3",
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorStockValue");
        }

        [Fact]
        public void CheckNotZeroInStock()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Description = "Description",
                Details = "Details",
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
            Assert.Contains(results, v => v.ErrorMessage == "ErrorStockValue");
        }

        [Fact]
        public void CheckMissingPrice()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Description = "Description",
                Details = "Details",
                Name = "New Product",                
                Stock = "4",
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorPriceValue");
        }

        [Fact]
        public void CheckNumberInPrice()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Description = "Description",
                Details = "Details",
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
            Assert.Contains(results, v => v.ErrorMessage == "ErrorPriceValue");
        }

        [Fact]
        public void CheckDecimalsInPrice()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Description = "Description",
                Details = "Details",
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
            Assert.Contains(results, v => v.ErrorMessage == "ErrorPriceValue");
        }

        [Fact]
        public void CheckNumberFormatInPrice()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Description = "Description",
                Details = "Details",
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
            Assert.Contains(results, v => v.ErrorMessage == "ErrorPriceValue");
        }

        [Fact]
        public void CheckRangeInPrice()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Description = "Description",
                Details = "Details",
                Name = "New Product",
                Price = "-4",
                Stock = "3",
            };

            var context = new ValidationContext(productViewModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(productViewModel, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.ErrorMessage == "ErrorPriceValue");
        }

        [Fact]
        public void CheckNumberFormatInPrice()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Description = "Description",
                Details = "Details",
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
            Assert.Contains(results, v => v.ErrorMessage == "ErrorPriceValue");
        }
    }


    
}