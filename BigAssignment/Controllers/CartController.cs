using BigAssignment.Helper;
using BigAssignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Drawing.Printing;
using System.Security.Cryptography;

namespace BigAssignment.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieWebContext _dbContext;
        private readonly IConfiguration _configuration;
        public CartController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _dbContext = new MovieWebContext();
            _configuration = configuration;
        }

        public IActionResult Cart()
        {
            List<CartViewModel> cart = HttpContext.Session.GetComplexData<List<CartViewModel>>("cart");
           
            return View(cart);
           
                       
        }

        public IActionResult Add(int idProduct, int quantityProduct)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Status == 1 && p.Id == idProduct);

            List<CartViewModel> cart = HttpContext.Session.GetComplexData<List<CartViewModel>>("cart");
            if (cart == null)
            {
                var list = new List<CartViewModel>();
                var newItem = new CartViewModel
                {
                    ProductID = product.Id,
                    Name = product.Name,
                    Slug = product.Slug,
                    Image = product.Image,
                    Quantity = 1,
                    Price = product.Price
                };

                list.Add(newItem);
                HttpContext.Session.SetComplexData("cart", list);
                HttpContext.Session.SetComplexData("cart_count", list.Count);
             
               
            }
            else
            {
                var list = (List<CartViewModel>)cart;

                if (list.Exists(m => m.ProductID == idProduct))
                {
                    foreach (var item in list)
                    {
                        if (item.ProductID == idProduct)
                            item.Quantity += quantityProduct;
                      
                    }
                }
                else
                {
                    var item = new CartViewModel();
                    item.ProductID = product.Id;
                    item.Name = product.Name;
                    item.Slug = product.Slug;
                    item.Image = product.Image;
                    item.Quantity = 1;
                    item.Price = product.Price;
                    list.Add(item);
                    HttpContext.Session.SetComplexData("cart", list);
                    HttpContext.Session.SetComplexData("cart_count", list.Count);
                }
            }


            return RedirectToAction("Index", "Home");

        }

        public IActionResult UpdateCart(int idProduct, string option)
        {
            var sCart = HttpContext.Session.GetComplexData<List<CartViewModel>>("cart");
            CartViewModel cartModel = sCart.First(m => m.ProductID == idProduct);

            if (cartModel != null)
            {
                switch (option)
                {
                    case "add":
                        cartModel.Quantity++;
                        HttpContext.Session.SetComplexData("cart", sCart);
                        return RedirectToAction("Cart");
                      
                    case "minus":
                        cartModel.Quantity--;
                        HttpContext.Session.SetComplexData("cart", sCart);
                        return RedirectToAction("Cart");

                    case "remove":
                        sCart.Remove(cartModel);
                        if (sCart.Count() == 0)
                        {
                            HttpContext.Session.SetComplexData("cart", sCart);
                        }
                        return RedirectToAction("Cart");

                    default:
                        break;

                }
            }
            return RedirectToAction("Cart");
        }

        public IActionResult RemoveFromCart(int idProduct)
        {
            List<CartViewModel> cart = HttpContext.Session.GetComplexData<List<CartViewModel>>("cart");
            var itemRemove = cart.FirstOrDefault(item => item.ProductID == idProduct);

            if (itemRemove != null)
            {
                cart.Remove(itemRemove);
                HttpContext.Session.SetComplexData("cart", cart);
            }

            return RedirectToAction("Cart");
        }

        public IActionResult CheckOut()
        {
            if (HttpContext.Session.GetString("Name") != null && HttpContext.Session.GetComplexData<List<CartViewModel>>("cart") != null)
            {
                int user_id = Convert.ToInt32(HttpContext.Session.GetString("user_id"));
                ViewBag.Info = _dbContext.Users.Where(m => m.Id == user_id).First();
            } else
            {
                return RedirectToAction("Cart", "Cart");
            }
            return View(HttpContext.Session.GetComplexData<List<CartViewModel>>("cart"));
        }

        [HttpPost]
        public IActionResult Payment(String Address, String FullName, String Phone, String Email)
        {
            var order = new Order();
            int user_id = Convert.ToInt32(HttpContext.Session.GetString("user_id"));
            order.Code = DateTime.Now.ToString("yyyyMMddhhMMss"); // yyyy-MM-dd hh:MM:ss
            order.UserId = user_id;
            order.CreateDate = DateTime.Now;
            order.DeliveryAddress = Address;
            order.DeliveryEmail = Email;
            order.DeliveryPhone = Phone;
            order.DeliveryName = FullName;
            order.Status = 1;
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            var OrderID = order.Id;

            foreach (var c in HttpContext.Session.GetComplexData<List<CartViewModel>>("cart"))
            {
                var orderdetails = new OrderDetail();
                orderdetails.OrderId = OrderID;
                orderdetails.ProductId = c.ProductID;
                orderdetails.Price = c.Price;
                orderdetails.Quantity = c.Quantity;
                orderdetails.Amount = c.Price * c.Quantity;
                _dbContext.OrderDetails.Add(orderdetails);
            }
            _dbContext.SaveChanges();

            HttpContext.Session.Remove("cart");
            HttpContext.Session.SetComplexData("cart_count", 0);

           
            return RedirectToAction("Index", "Home");

        }

        public JsonResult CheckAuth()
        {
            if (HttpContext.Session.GetString("Name") != null)
                return Json(1);
            return Json(0);
        }
    }
}
