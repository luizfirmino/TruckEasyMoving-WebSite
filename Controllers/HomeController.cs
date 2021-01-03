using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Web.Configuration;
using TruckEasyWebSite.Models;
using Newtonsoft.Json.Linq;

namespace TruckEasyWebSite.Controllers
{
    public class HomeController : Controller
    {

        private WebSiteDBContext db = new WebSiteDBContext();
        Notification email = new Notification();

        public ActionResult Index()
        {
            WebSite Model = new WebSite();
            Model.comboServices = new SelectList(db.OrderServices.Where(i => i.active == 1).OrderBy(i => i.service).ToList(), "orderServiceId","service");
            Model.comboSources = new SelectList(db.OrderSources.OrderBy(i => i.name).ToList(), "orderSourceId", "name");
            Model.reviews = db.OrderReviews.ToList();
            return View(Model);
        }

        [HttpPost]
        public ActionResult Index(WebSite Model)
        {

            Model.comboServices = new SelectList(db.OrderServices.Where(i => i.active == 1).OrderBy(i => i.service).ToList(), "orderServiceId", "service");
            Model.comboSources = new SelectList(db.OrderSources.OrderBy(i => i.name).ToList(), "orderSourceId", "name");
            Model.reviews = db.OrderReviews.ToList();

            if (!(IsReCaptchValid())) {
                ModelState.AddModelError(string.Empty, "Google reCAPTCHA validation failed");
            }
            else { 

                if (ModelState.IsValid) {
                
                    string bodyEmail = "";
                    bodyEmail = "Name: " + Model.firstName + " " + Model.lastName + "<br>";
                    bodyEmail = bodyEmail + "Phone: " + Model.phoneNumber + "<br>";
                    bodyEmail = bodyEmail + "E-mail: " + Model.email + "<br>";
                    bodyEmail = bodyEmail + "Service: " + Model.comboServices.FirstOrDefault(d => d.Value == Model.orderServiceId).Text + "<br>";
                    bodyEmail = bodyEmail + "Date: " + Model.dateSchedule + "<br>";
                    bodyEmail = bodyEmail + "Source: " + Model.comboSources.FirstOrDefault(d => d.Value == Model.orderSourceId).Text + "<br>";
                    bodyEmail = bodyEmail + "Details: " + Model.notes + "<br>";
                    email.SendEmail(Model.email, "info@truckeasymoving.com", "Request from TruckEasyMoving.com", bodyEmail);
                    
                    try { 
                        db.CreateOrder(Model);
                    } catch (Exception e)
                    {
                        email.SendEmail("noreply@truckeasymoving.com", "getquotemoving@gmail.com", "Error executing AddOrderWebSite", e.ToString());
                    }             

                }
            }
            return View(Model);

        }


        public bool IsReCaptchValid()
        {
            var result = false;
            var captchaResponse = Request.Form["g-recaptcha-response"];
            var secretKey = WebConfigurationManager.AppSettings["Google-SecretKey"];
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }
            return result;
        }

    }
}