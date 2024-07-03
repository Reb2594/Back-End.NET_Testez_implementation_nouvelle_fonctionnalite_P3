using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Moq;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceIntegrationTests
    {


        private readonly DbContextOptions<P3Referential> _options;        
        private readonly P3Referential _context;
        private Cart _cart;        
        private readonly ProductService _productService;
       

        public ProductServiceIntegrationTests()
        {
            var _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var _connection = _configuration.GetConnectionString("P3Referential");

            var _optionsBuilder = new DbContextOptionsBuilder<P3Referential>();

            _optionsBuilder.UseSqlServer(_connection);

            _options = (DbContextOptions<P3Referential>)_optionsBuilder.Options;

            _context = new P3Referential(_options, _configuration);

            var _productRepository = new ProductRepository(_context);

            var _orderRepository = new OrderRepository(_context);

            var _localizer = new Mock<IStringLocalizer<ProductService>>();

            _cart = new Cart();

            _productService = new ProductService(_cart, _productRepository, _orderRepository, _localizer.Object);
        }


        [Fact]
        public void SaveNewProductToDB()
        {
            //Arrange
            ProductViewModel _productTest1 = new ProductViewModel
            {
                Name = "Product to Add",
                Price = "30.00",
                Stock = "45",
            };

            //Act
            _productService.SaveProduct(_productTest1);


            //Assert
            Product addedTest1 = _context.Product.FirstOrDefault(p => p.Name == "Product to Add");
            Assert.NotNull(addedTest1);
            Assert.Equal(30.00, addedTest1.Price);
            Assert.Equal("Product to Add", addedTest1.Name);
            Assert.Equal(45, addedTest1.Quantity);
            _productService.DeleteProduct(addedTest1.Id);
        }

        [Fact]
        public void DeleteProductFromDB()
        {
            //Arrange
            ProductViewModel _productTest2 = new ProductViewModel
            {
                Name = "Product to Delete",
                Price = "20.99",
                Stock = "10",
            };

            //Act
            _productService.SaveProduct(_productTest2);
            Product addedTest2 = _context.Product.FirstOrDefault(p => p.Name == "Product to Delete");
            Assert.NotNull(addedTest2);

            _productService.DeleteProduct(addedTest2.Id);

            //Assert
            Product deletedTest2 = _context.Product.FirstOrDefault(p => p.Id == addedTest2.Id);
            Assert.Null(deletedTest2);

        }

        [Fact]
        public void UpdatedQuantitiesInDB()
        {
            //Arrange
            ProductViewModel _productTest3 = new ProductViewModel
            {
                Name = "Product with updated quantities",
                Price = "20",
                Stock = "10",
            };
            
            _productService.SaveProduct(_productTest3);
            Product addedTest3 = _context.Product.FirstOrDefault(p => p.Name == _productTest3.Name);
            Assert.NotNull(addedTest3);

            _cart.AddItem(new Product { Id = addedTest3.Id, Name = addedTest3.Name }, 3);

            // Act
            _productService.UpdateProductQuantities();

            // Assert
            var updatedProduct = _context.Product.FirstOrDefault(p => p.Name == "Product with updated quantities");
            Assert.NotNull(updatedProduct);
            Assert.Equal(7, updatedProduct.Quantity); // 10 - 3 = 7
            _productService.DeleteProduct(updatedProduct.Id);

        }

        [Fact]
        public void GetProductByIdDB()
        {
            //Arrange
            ProductViewModel _productTest4 = new ProductViewModel
            {
                Name = "Product with Id",
                Price = "30",
                Stock = "10",
            };

            _productService.SaveProduct(_productTest4);
            Product addedTest4 = _context.Product.FirstOrDefault(p => p.Name == "Product with Id");
            Assert.NotNull(addedTest4);

            //Act
            var productById = _productService.GetProductById(addedTest4.Id);


            //Assert
            Assert.NotNull(productById);
            Assert.Equal("Product with Id", productById.Name);
            Assert.Equal(30, productById.Price);
            Assert.Equal(10, productById.Quantity);
            _productService.DeleteProduct(productById.Id);
        }
    }
}
