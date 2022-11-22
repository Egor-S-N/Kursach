using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Banking_system.Models;
namespace Banking_system.Controllers
{
    public class CreditTypesController : Controller
    {
        BankDBEntities db = new BankDBEntities();
        // GET: CreditTypes
        public ActionResult Index()
        {
            var creditTypes = from s in db.CreditTypes select s;
            return View(creditTypes);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Percent,MonthPeriod")] CreditTypes creditTypes, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                   
                }
                db.CreditTypes.Add(creditTypes);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(creditTypes);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditTypes creditTypes = db.CreditTypes.Find(id);
            if (creditTypes == null)
            {
                return HttpNotFound();
            }
            return View(creditTypes);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditTypes creditTypes = db.CreditTypes.Find(id);
            db.CreditTypes.Remove(creditTypes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}