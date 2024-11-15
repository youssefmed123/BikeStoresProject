using System;
using System.Collections.Generic;
using BikeStores2.Data;
using BikeStores2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BikeStores2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			BikeStoreDbContext dbContext = new();

			//Retrieve all categories from the production.categories table.
			var categories = dbContext.Categories.ToList();
			foreach (var category in categories)
			{
				Console.WriteLine($"Category ID: {category.CategoryId}, Category Name: {category.CategoryName}");
			}


			//Retrieve the first product from the production.products table.
			var firstProduct = dbContext.Products.FirstOrDefault();
			Console.WriteLine($"Product ID: {firstProduct.ProductId}, Product Name: {firstProduct.ProductName}, Model Year: {firstProduct.ModelYear}");

			//Retrieve a specific product from the production.products table by ID

			var product = dbContext.Products.Find(6);
			Console.WriteLine($"Product ID: {product.ProductId}, Product Name: {product.ProductName}, Model Year: {product.ModelYear}");

			//Retrieve all products from the production.products table with a certain model year  
			var productsByYear = dbContext.Products.Where(p => p.ModelYear == 2016);
			foreach (var product in productsByYear)
			{
				Console.WriteLine($"Product ID: {product.ProductId}, Product Name: {product.ProductName}, Model Year: {product.ModelYear}");
			}
			//Retrieve a specific customer from the sales.customers table by ID.
			var customer = dbContext.Customers.Find(5);
			Console.WriteLine($"Customer ID: {customer.CustomerId}, Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}");


			//Retrieve a list of product names and their corresponding brand names.
			var productWithBrand = dbContext.Products.Select(p => new { p.ProductName, p.Brand.BrandName }).ToList();

			foreach (var item in productWithBrand)
			{
				Console.WriteLine($"Product Name: {item.ProductName}, Brand Name: {item.BrandName}");
			}

			//Count the number of products in a specific category.
			int categoryId = 3;
			int productCount = dbContext.Products.Count(p => p.CategoryId == categoryId);
			Console.WriteLine($"Number of products in Category {categoryId}: {productCount}");

			//Calculate the total list price of all products in a specific category.
			int categoryId = 3;
			decimal totalListPrice = dbContext.Products.Where(p => p.CategoryId == categoryId).Sum(p => p.ListPrice);
			Console.WriteLine($"Total list price of products in Category {categoryId}: {totalListPrice:C}");

			//Calculate the average list price of products.
			decimal averageListPrice = dbContext.Products.Average(p => p.ListPrice);
			Console.WriteLine($"Average list price of products: {averageListPrice:C}");

			//Retrieve orders that are completed.
			var completedOrders = dbContext.Orders.Where(e => e.OrderStatus == 1).ToList();

			foreach (var item in completedOrders)
			{
				Console.WriteLine($"OrderId: {item.OrderId}, CustomerId: {item.CustomerId}, Status: Completed");
			}
		}
	}
}


