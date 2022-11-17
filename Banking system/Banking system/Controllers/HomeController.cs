using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Banking_system.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //try
            //{
            //    if (Session["Userid"] == null)
            //    {
            //            return RedirectToAction("Login", "Accounts");
            //    }
            //}
            //catch
            //{
            //    return RedirectToAction("Login", "Accounts");
            //}
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Calculate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Calculate(string Percent , string Period, string Summ)
        {
            try
            {
            int sum = Convert.ToInt32(Summ);
            int period = Convert.ToInt32(Period);
                double percent = Convert.ToDouble(Percent)/ Convert.ToDouble(12)/Convert.ToDouble(100);
                TempData["result"] = ((sum*percent) /(1 - Math.Pow(1+percent, -period))).ToString("C");
                return View();

            }
            catch
            {
                return View("Calculate");
            }
        }
    }
}