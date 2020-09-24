using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lab15._3DesignAPI2.Models;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Lab15._3DesignAPI2;

namespace Lab15._3DesignAPI2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IDbConnection _db;

        public HomeController(ILogger<HomeController> logger,IDbConnection db)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        //Returns All Products
        [HttpGet]
        public List<Product> ProductRead()
        {
            List<Product> result = Product.Read(_db);//_db.GetAll<Blog2>().ToList();

            return result;
        }



        //Returns products with <= Units in Stocks
        [HttpGet("Stock/{UnitsInStock}")]
        public List<Product> Stock(int UnitsInStock)
        {

            List<Product> products = Product.CheckStockUnder(_db, UnitsInStock);
            return products;

        }



        [HttpPost("Stock/UpdateStock/")]
        public List<Product> UpdateStock([FromBody]Product product)
        {

            List<Product> products = Product.UpdateStock(_db, product);
            return products;


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        //[HttpPost("Stock/AddProducts?productName=&itemCount=")]
        //public List<Product> AddProduct(string productName, int itemCount)
        //{

        //    List<Product> products = Product.AddProduct(_db, productName, itemCount);
        //    return products;

        //}



        //[HttpGet("ProductReadNames")]
        //public List<string> ProductReadNames()
        //{

        //    List<string> names = Product.ReadProductNames(_db);
        //    string names2 = names.ToString();
        //    return Content(names);

        //}

    }
}
