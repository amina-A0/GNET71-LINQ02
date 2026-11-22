using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQAssignment02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Products Data

            List<Product> products = new List<Product>
            {
                new Product { ProductID = 1, ProductName = "Fish", Category = "Seafood", UnitPrice = 8.50m, UnitsInStock = 20 },
                new Product { ProductID = 2, ProductName = "Salmon", Category = "Seafood", UnitPrice = 12m, UnitsInStock = 15 },
                new Product { ProductID = 3, ProductName = "Shrimp", Category = "Seafood", UnitPrice = 15m, UnitsInStock = 10 },
                new Product { ProductID = 4, ProductName = "Crab", Category = "Seafood", UnitPrice = 18m, UnitsInStock = 8 },
                new Product { ProductID = 5, ProductName = "Juice", Category = "Beverages", UnitPrice = 22m, UnitsInStock = 30 },
                new Product { ProductID = 6, ProductName = "Tea", Category = "Beverages", UnitPrice = 27m, UnitsInStock = 25 },
                new Product { ProductID = 7, ProductName = "Olive Oil", Category = "Condiments", UnitPrice = 32m, UnitsInStock = 12 },
                new Product { ProductID = 8, ProductName = "Vinegar", Category = "Condiments", UnitPrice = 38m, UnitsInStock = 18 },
                new Product { ProductID = 9, ProductName = "Carrot", Category = "Produce", UnitPrice = 45m, UnitsInStock = 40 },
                new Product { ProductID = 13, ProductName = "Apple", Category = "Produce", UnitPrice = 60m, UnitsInStock = 35 },
                new Product { ProductID = 18, ProductName = "Mango", Category = "Produce", UnitPrice = 85m, UnitsInStock = 22 },
                new Product { ProductID = 20, ProductName = "Potato", Category = "Produce", UnitPrice = 120m, UnitsInStock = 50 }
            };

            #endregion

            #region Customers Data

            List<Customer> customers = new List<Customer>
            {
                new Customer { CustomerID = 1, CompanyName = "Alpha Company", Country = "Germany" },
                new Customer { CustomerID = 2, CompanyName = "Beta Company", Country = "France" },
                new Customer { CustomerID = 3, CompanyName = "Gamma Company", Country = "Germany" },
                new Customer { CustomerID = 4, CompanyName = "Delta Company", Country = "UK" },
                new Customer { CustomerID = 5, CompanyName = "Omega Company", Country = "France" }
            };

            #endregion

            #region Orders Data

            List<Order> orders = new List<Order>
            {
                new Order { OrderID = 101, CustomerID = 1, TotalOrderValue = 1200m },
                new Order { OrderID = 102, CustomerID = 1, TotalOrderValue = 800m },
                new Order { OrderID = 103, CustomerID = 2, TotalOrderValue = 500m },
                new Order { OrderID = 104, CustomerID = 3, TotalOrderValue = 1500m },
                new Order { OrderID = 105, CustomerID = 4, TotalOrderValue = 700m },
                new Order { OrderID = 106, CustomerID = 5, TotalOrderValue = 900m },
                new Order { OrderID = 107, CustomerID = 2, TotalOrderValue = 300m }
            };

            #endregion


            #region Q1 - Get Top 3 Most Expensive Products

            var top3Expensive = products
                .OrderByDescending(p => p.UnitPrice)
                .Take(3);

            Console.WriteLine("Top 3 Most Expensive Products:");

            foreach (var product in top3Expensive)
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }

            #endregion


            #region Q2 - Show Page 2 of Products (Page Size = 5)


            int pageSize = 5;
            int pageNumber = 2;

            var page2 = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            Console.WriteLine("\nPage 2 Products:");

            foreach (var product in page2)
            {
                Console.WriteLine(product.ProductName);
            }

            #endregion


            #region Q3 - Take Products While UnitPrice < 25


            var cheapProducts = products
                .TakeWhile(p => p.UnitPrice < 25);

            Console.WriteLine("\nProducts With Price Less Than 25:");

            foreach (var product in cheapProducts)
            {
                Console.WriteLine($"{product.ProductName} - {product.UnitPrice}");
            }

            #endregion


            #region Q4 - Check If All Seafood Products Are In Stock

            bool allSeafoodInStock = products
                .Where(p => p.Category == "Seafood")
                .All(p => p.UnitsInStock > 0);

            Console.WriteLine("\nAre All Seafood Products In Stock?");
            Console.WriteLine(allSeafoodInStock);

            #endregion


            #region Q5 - Check If ID List Contains 9

            int[] ids = { 3, 9, 13, 18 };

            bool contains9 = ids.Contains(9);

            Console.WriteLine("\nDoes ID List Contain 9?");
            Console.WriteLine(contains9);

            #endregion


            #region Q6 - Group Products By Category And Print Count

            var productsByCategory = products
                .GroupBy(p => p.Category);

            Console.WriteLine("\nProducts Count By Category:");

            foreach (var group in productsByCategory)
            {
                Console.WriteLine($"{group.Key}: {group.Count()} products");
            }

            #endregion


            #region Q7 - Group Products By Category And Select Product Names

            var productNamesByCategory = products
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    ProductNames = g.Select(p => p.ProductName)
                });

            Console.WriteLine("\nProduct Names By Category:");

            foreach (var group in productNamesByCategory)
            {
                Console.WriteLine(group.Category + ":");

                foreach (var name in group.ProductNames)
                {
                    Console.WriteLine(name);
                }
            }

            #endregion


            #region Q8 - Find Categories That Have More Than 3 Products

            var categories = products
                .GroupBy(p => p.Category)
                .Where(g => g.Count() > 3)
                .Select(g => g.Key);

            Console.WriteLine("\nCategories With More Than 3 Products:");

            foreach (var category in categories)
            {
                Console.WriteLine(category);
            }

            #endregion


            #region Q9 - Group Customers By Country Using Query Syntax

            var countryStatistics =
                from c in customers
                join o in orders
                on c.CustomerID equals o.CustomerID
                group o by c.Country into g
                select new
                {
                    Country = g.Key,
                    Count = g.Count(),
                    TotalOrderValue = g.Sum(o => o.TotalOrderValue)
                };

            Console.WriteLine("\nCustomers Statistics By Country:");

            foreach (var item in countryStatistics)
            {
                Console.WriteLine(
                    $"Country: {item.Country}, " +
                    $"Count: {item.Count}, " +
                    $"Total Order Value: {item.TotalOrderValue}");
            }

            #endregion


            #region Q10 - Calculate Total Units In Stock

            int totalUnits = products.Sum(p => p.UnitsInStock);

            Console.WriteLine("\nTotal Units In Stock:");
            Console.WriteLine(totalUnits);

            #endregion


            #region Q11 - Find Cheapest And Most Expensive Product Prices

            decimal cheapestPrice = products.Min(p => p.UnitPrice);

            decimal mostExpensivePrice = products.Max(p => p.UnitPrice);

            Console.WriteLine("\nCheapest Product Price:");
            Console.WriteLine(cheapestPrice);

            Console.WriteLine("Most Expensive Product Price:");
            Console.WriteLine(mostExpensivePrice);

            #endregion


            #region Q12 - Get Distinct Product Categories

            var distinctCategories = products
                .Select(p => p.Category)
                .Distinct();

            Console.WriteLine("\nDistinct Product Categories:");

            foreach (var category in distinctCategories)
            {
                Console.WriteLine(category);
            }

            #endregion


            #region Q13 - Find IDs In SetA But Not In SetB

            int[] setA = { 1, 3, 5, 7, 9, 11, 13 };

            int[] setB = { 3, 6, 9, 12, 15, 13 };

            var result = setA.Except(setB);

            Console.WriteLine("\nIDs In SetA But Not In SetB:");

            foreach (var id in result)
            {
                Console.WriteLine(id);
            }

            #endregion


            #region Q14 - Find Countries In List1 But Not In List2 (Case-Insensitive)

            string[] list1 = { "Germany", "France", "UK", "Spain" };

            string[] list2 = { "france", "SPAIN", "Italy" };

            var countries = list1.Except(
                list2,
                StringComparer.OrdinalIgnoreCase
            );

            Console.WriteLine("\nCountries In List1 But Not In List2:");

            foreach (var country in countries)
            {
                Console.WriteLine(country);
            }

            #endregion


            #region Q15 - Build Dictionary And Retrieve Product With ID 18

            Dictionary<int, Product> productsDictionary =
                products.ToDictionary(p => p.ProductID);

            Product product18 = productsDictionary[18];

            Console.WriteLine("\nProduct With ID 18:");
            Console.WriteLine(
                $"{product18.ProductName} - {product18.Category} - {product18.UnitPrice}");

            #endregion


            #region Q16 - Get First Product With Price Greater Than 50

            var firstExpensiveProduct = products
                .First(p => p.UnitPrice > 50);

            Console.WriteLine("\nFirst Product With Price Greater Than 50:");

            Console.WriteLine(
                $"{firstExpensiveProduct.ProductName} - {firstExpensiveProduct.UnitPrice}");

            #endregion


            #region Q17 - Get First Product With Price Greater Than 500 Without Exception

            Product? productOver500 = products
                .FirstOrDefault(p => p.UnitPrice > 500);

            Console.WriteLine("\nFirst Product With Price Greater Than 500:");

            if (productOver500 != null)
            {
                Console.WriteLine(productOver500.ProductName);
            }
            else
            {
                Console.WriteLine("No Product Found");
            }

            #endregion


            #region Q18 - Generate Multiplication Table Row For 7

            var multiplicationTable = Enumerable.Range(1, 10)
                .Select(n => 7 * n);

            Console.WriteLine("\nMultiplication Table Row For 7:");

            foreach (var number in multiplicationTable)
            {
                Console.Write(number + " ");
            }

            Console.WriteLine();

            #endregion


            #region Q19 - Generate Even Numbers Between 1 And 30

            var evenNumbers = Enumerable.Range(1, 30)
                .Where(n => n % 2 == 0);

            Console.WriteLine("\nEven Numbers Between 1 And 30:");

            foreach (var number in evenNumbers)
            {
                Console.Write(number + " ");
            }

            Console.WriteLine();

            #endregion


            #region Q20 - Concatenate First 3 Product Names With First 3 Customer Company Names

            var firstProductNames = products
                .Take(3)
                .Select(p => p.ProductName);

            var firstCustomerNames = customers
                .Take(3)
                .Select(c => c.CompanyName);

            var allNames = firstProductNames
                .Concat(firstCustomerNames);

            Console.WriteLine("\nProducts And Customer Company Names:");

            foreach (var name in allNames)
            {
                Console.WriteLine(name);
            }

            #endregion


            #region Q21 - Pair Each Product With A Customer Using Zip

            var productCustomerPairs = products
                .Zip(
                    customers,
                    (p, c) => $"{p.ProductName} sold to {c.CompanyName}"
                );

            Console.WriteLine("\nProducts Sold To Customers:");

            foreach (var pair in productCustomerPairs)
            {
                Console.WriteLine(pair);
            }

            #endregion


        }
    }
}