using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Banking_system.Models;

namespace Banking_system.Controllers
{
    public class CreditsController : Controller
    {
        BankDBEntities db = new BankDBEntities();
        // GET: Credits
        public ActionResult Index(int? id)
        {
            id = Convert.ToInt32(Session["Userid"]);
            if ((string)Session["Usertype"] == "Manager")
            {

                var credits = from s in db.Credits where s.Id_worker == id select s;
                return View(credits);
            }
            else if ((string)Session["Usertype"] == "Client")
            {
                var credits = (from cred in db.Credits
                               from req in db.Requests
                               where req.Id_client == id && cred.Id_request == req.Id
                               select cred).ToList();
                return View(credits);
            }
            else if((string)Session["Usertype"] == "Admin")
            {
                var credits = from s in db.Credits select s;
                
                return View(credits);
            }

            
            else
            {
                return HttpNotFound();
            }
        }


        public ActionResult Details(int? id)
        {
            ViewData.Clear();




            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Credits credit= db.Credits.Find(id);

            Workers worker = db.Workers.Find(credit.Id_worker);
            Requests request = db.Requests.Find(credit.Id_request);
            Clients client = db.Clients.Find(request.Id_client);
            CreditTypes creditType = db.CreditTypes.Find(request.Id_credit_type);

            ViewData["worker"] = worker;
            ViewData["client"] = client;
            ViewData["request"] = request;
            ViewData["credit_type"] = creditType;
            ViewData["credit"] = credit;




            if (credit == null)
            {
                return HttpNotFound();
            }

           
            return View(credit);

        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credits credit = db.Credits.Find(id);
            if (credit == null)
            {
                return HttpNotFound();
            }
            Workers worker = db.Workers.Find(credit.Id_worker);
            Requests request = db.Requests.Find(credit.Id_request);
            Clients client = db.Clients.Find(request.Id_client);
            CreditTypes creditType = db.CreditTypes.Find(request.Id_credit_type);

            ViewData["worker"] = worker;
            ViewData["client"] = client;
            ViewData["request"] = request;
            ViewData["credit_type"] = creditType;
            ViewData["credit"] = credit;


            //var client = (from s in db.Clients where s.Id == requests.Id_client select s).First();
            //var creditType = (from s in db.CreditTypes where s.Id == requests.Id_credit_type select s).First();
            //ViewData["clients"] = client;
            //ViewData["credittype"] = creditType;
            return View(credit);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Credits credits = db.Credits.Find(id);
            db.Credits.Remove(credits);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credits credit = db.Credits.Find(id);
            if (credit == null)
            {
                return HttpNotFound();
            }
            return View(credit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_worker,Id_request,Credit_sum,Date_of_issue")] Credits credits, HttpPostedFileBase upload)
        {

            if (ModelState.IsValid)
            {
                db.Entry(credits).State = EntityState.Modified;
                
                    db.SaveChanges();

                

                return RedirectToAction("Index");
            }

            return View(credits);


            //if (ModelState.IsValid)
            //{
            //    db.Entry(clients).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(clients);
        }

    }
}