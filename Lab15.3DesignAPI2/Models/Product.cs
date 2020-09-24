using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace Lab15._3DesignAPI2
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryID { get; set; }
        public int QuanitityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public int Discontinued { get; set; }


        public static List<Product> Read(IDbConnection _db)
        {
            List<Product> result = _db.GetAll<Product>().ToList();
            return result;
        }

        public static List<string> ReadProductNames(IDbConnection _db)
        { 
            List<Product> result = _db.GetAll<Product>().ToList();
            List<string> productnames = new List<string>();

            foreach (Product product in result)
            {
                productnames.Add(product.ProductName);
            }
            return productnames;
        }

        public static List<Product> CheckStockUnder(IDbConnection _db, int UnitsInStock)
        {

            List<Product> products = (List<Product>)_db.Query<Product>($"SELECT * FROM Products WHERE UnitsInStock <= {UnitsInStock}");
            return products;

        }
        public static List<Product> UpdateStock(IDbConnection _db, string productName, int itemCount)
        {
            List<Product> products = (List<Product>)_db.Query<Product>($"UPDATE Products SET UnitsInStock = {itemCount} WHERE ProductName = {productName}");
            return products;


        }



        //public static List<Product> AddProduct(IDbConnection _db, string ProductName, int itemCount)
        //{
        //    List<Product> products = (List<Product>)_db.Query<Product>($"INSERT INTO Products(ProductName, UnitsInStock)VALUES('{ProductName}', {itemCount})");
        //    return products;
        //}
    }
}
