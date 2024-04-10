using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.Controllers
{
    public class SportsController : Controller
    {
        // GET: Sports
        private SportsStoreEntities db = new SportsStoreEntities();

        // GET: Sports
        public ActionResult Index()
        {
            if (AccountController.isLoggedIn == false || AccountController.isAdmin == false)
                return HttpNotFound();
            return View(db.sports.ToList());
        }

        public ActionResult UserView()
        {
            if (AccountController.isLoggedIn == false || AccountController.isUser == false)
                return HttpNotFound();

            return View(db.sports.ToList());
        }

        // GET: Sports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sport sportstore = db.sports.Find(id);
            if (sportstore == null)
            {
                return HttpNotFound();
            }
            return View(sportstore);
        }

        // GET: Sports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sports/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,Discount,Category,Quantity")] sport sportstore)
        {
            if (ModelState.IsValid)
            {
                db.sports.Add(sportstore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sportstore);
        }

        // GET: Sports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sport sportstore = db.sports.Find(id);
            if (sportstore == null)
            {
                return HttpNotFound();
            }
            return View(sportstore);
        }

        // POST: Sports/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,Discount,Category,Quantity")] sport sportstore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sportstore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sportstore);
        }

        // GET: Sports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sport sportstore = db.sports.Find(id);
            if (sportstore == null)
            {
                return HttpNotFound();
            }
            return View(sportstore);
        }

        // POST: Sports/Delete/5
        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sport sportstore = db.sports.Find(id);
            db.sports.Remove(sportstore);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
