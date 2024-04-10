using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {

        SportsStoreEntities db = new SportsStoreEntities();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrdersView()
        {
            return View(db.Orders.ToList());
        }
    }
}