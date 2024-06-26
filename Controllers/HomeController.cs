using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System;
using System.IO;
using System.Web;
using WebApplicationn.Database.Dreamy;
using WebApplicationn.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting.Server;

namespace WebApplicationn.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult Clients()
        {
            return View();
        }
        public IActionResult AddProduct(int productNo, Int64 price, int quantity, string productName, string category)
        {

            Cart cart = new Cart();

            cart.productNo = productNo;
            cart.price = price;
            cart.quantity = quantity;
            cart.productName = productName;
            cart.category = category;
            cart.finalPrice = price; //setting the initial value of finalPrice to the price of the product

            string output = Dreamy.AddToCart(cart);

            return MyCart();

        }
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
        public IActionResult TermsAndConditions()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult MyCart()
        {
            Int64 subtotal = Database.Dreamy.Dreamy.SubTotal();
            ViewBag.subtotal = subtotal;

            List<Cart> carts = Database.Dreamy.Dreamy.GetProducts();


            return View("~/views/home/cart.cshtml", carts);
        }

        [HttpGet]
        public IActionResult AddQuantity(int quantity, int prodNo, Int64 price)
        {

            quantity += 1;
            Int64 finalPrice = quantity * price;
            string str = Database.Dreamy.Dreamy.UpdateCart(quantity, prodNo, finalPrice);

            Int64 subtotal = Database.Dreamy.Dreamy.SubTotal();
            ViewBag.subtotal = subtotal;

            List<Cart> carts = Database.Dreamy.Dreamy.GetProducts();

            return PartialView("~/views/home/partialcart.cshtml", carts);
        }

        public IActionResult ReduceQuantity(int quantity, int prodNo, Int64 price)
        {
            quantity -= 1;
            if (quantity <= 0)
            {
                Database.Dreamy.Dreamy.RemoveFromCart(prodNo);

                Int64 subtotal = Database.Dreamy.Dreamy.SubTotal();
                ViewBag.subtotal = subtotal;
            }
            else
            {
                Int64 finalPrice = quantity * price;
                string str = Database.Dreamy.Dreamy.UpdateCart(quantity, prodNo, finalPrice);

                Int64 subtotal = Database.Dreamy.Dreamy.SubTotal();
                ViewBag.subtotal = subtotal;
            }

            List<Cart> carts = Database.Dreamy.Dreamy.GetProducts();

            return PartialView("~/views/home/partialcart.cshtml", carts);
        }
        [HttpGet]
        public IActionResult DeleteProduct(int prodNo)
        {

            Database.Dreamy.Dreamy.RemoveFromCart(prodNo);


            Int64 subtotal = Database.Dreamy.Dreamy.SubTotal();
            ViewBag.subtotal = subtotal;

            List<Cart> carts = Database.Dreamy.Dreamy.GetProducts();
            return PartialView("~/views/home/partialcart.cshtml", carts);

        }
        [HttpGet]
        public IActionResult PartialCart()
        {

            return PartialView("~/views/home/partialcart.cshtml");
        }

        [HttpGet]
        public IActionResult DisplayProduct(string tableName, int productNo)
        {
            WebApplicationn.Models.Products productDetails =  WebApplicationn.Database.Dreamy.Dreamy.GetProductDetails(tableName, productNo);

            return View(productDetails);
        }

        [HttpGet]
        public IActionResult AllProducts()
        {
            WebApplicationn.Models.VMAllProducts vMAllProducts = new WebApplicationn.Models.VMAllProducts();
            
            vMAllProducts.productsRow1 = WebApplicationn.Database.Dreamy.Dreamy.GetProductRows("furnitureRow1");
            vMAllProducts.productsRow2 = WebApplicationn.Database.Dreamy.Dreamy.GetProductRows("furnitureRow2");
            vMAllProducts.accessories = WebApplicationn.Database.Dreamy.Dreamy.GetProductRows("accessories");
            vMAllProducts.lightingsRow1 = WebApplicationn.Database.Dreamy.Dreamy.GetProductRows("lightingsRow1");
            vMAllProducts.lightingsRow2 = WebApplicationn.Database.Dreamy.Dreamy.GetProductRows("lightingsRow2");

            return View(vMAllProducts);
        }


    }

}