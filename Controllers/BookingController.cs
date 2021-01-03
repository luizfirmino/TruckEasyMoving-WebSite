using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruckEasyWebSite.Models;

namespace TruckEasyWebSite.Controllers
{
    public class BookingController : Controller
    {

        private WebSiteDBContext db = new WebSiteDBContext();
        Notification email = new Notification();

        // GET: Booking
        public ActionResult Index()
        {
            WebSite Model = new WebSite();
            Model.comboServices = new SelectList(db.OrderServices.Where(i => i.active == 1).OrderBy(i => i.service).ToList(), "orderServiceId", "service");
            Model.comboSources = new SelectList(db.OrderSources.OrderBy(i => i.name).ToList(), "orderSourceId", "name");
            Model.reviews = db.OrderReviews.ToList();
            return View(Model);
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Booking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
