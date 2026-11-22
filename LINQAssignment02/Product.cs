using System;
using System.Collections.Generic;
using System.Text;
namespace LINQAssignment02
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
}