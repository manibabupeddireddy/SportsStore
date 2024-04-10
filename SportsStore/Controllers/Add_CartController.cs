using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.Controllers
{
    public class Add_CartController : Controller
    {
        
        // GET: Add_Cart
        SportsStoreEntities db = new SportsStoreEntities();

        // GET: MyCart


        public ActionResult CartView()

        {
            List<Add_Cart> cart = Session["Cart"] as List<Add_Cart>;
            if (cart == null)
            {
                cart = new List<Add_Cart>();

                Session["Cart"] = cart;

            }

            return View(cart);

        }


        // GET: Products/AddToCart

        [HttpPost]

        public ActionResult AddToCart(int id, int price, int quantity = 1)

        {
            List<Add_Cart> cart = Session["Cart"] as List<Add_Cart> ?? new List<Add_Cart>();
            Add_Cart existingItem = cart.FirstOrDefault(item => item.CartID == id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;

            }

            else

            {

                cart.Add(new Add_Cart { CartID = id, Price = price, Quantity = quantity });
            }

            Session["Cart"] = cart;

            return RedirectToAction("CartView");

        }

        public ActionResult RemoveFromCart(int id)

        {

            List<Add_Cart> cart = Session["Cart"] as List<Add_Cart>;
            if (cart != null)
            {

                cart.RemoveAll(item => item.CartID == id);

                Session["Cart"] = cart;

            }

            return RedirectToAction("CartView");

        }

        public ActionResult ClearCart()

        {

            Session.Clear();

            Session.Remove("Cart");

            return Redirect("~/Sports/UserView");

        }
        private int GetCurrentUserId()
        {
            return 1;
        }

        public ActionResult Checkout()
        {
            List<Add_Cart> cartItems = Session["Cart"] as List<Add_Cart>;
            if (cartItems == null || cartItems.Count == 0)
            {
                return RedirectToAction("EmptyCart");
            }
            int totalAmount = (int)cartItems.Sum(item => item.Price * item.Quantity);

            var order = new Order
            {
                UserId = GetCurrentUserId(),
                TotalAmount = totalAmount,
                OrderDate = DateTime.Now,
                OrderStatus = "Paid"
            };

            db.Orders.Add(order);
            db.SaveChanges();

            Session["Cart"] = null;

            return RedirectToAction("PaidSuccessfully", new { orderId = order.OrderID });

        }
        public ActionResult PaidSuccessfully()
        {
            return View();
        }
    }
}