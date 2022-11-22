using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Banking_system.Models;

namespace Banking_system.Controllers
{
    public class WorkersController : Controller
    {

        BankDBEntities db = new BankDBEntities();
        // GET: Workers
        public ActionResult Index()
        {
            var workers = from s in db.Workers select s;

           
            return View(workers);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workers worker = db.Workers.Find(id);
            ViewData["account"] = db.Accounts.Find(worker.Id_account);
            if (worker == null)
            {
                return HttpNotFound();
            }
            return View(worker);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workers worker= db.Workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();

            }
            ViewData["account"] = db.Accounts.Find(worker.Id_account);
            return View(worker);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workers worker = db.Workers.Find(id);
            Accounts account = db.Accounts.Find(worker.Id_account);
            db.Workers.Remove(worker);
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Workers worker,string Login, string Password, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                Accounts account = new Accounts();
                int id = 0;
                foreach(var item in db.Workers.OrderBy(s=>s.Id))
                {
                    id = item.Id + 1;
                }
                worker.Id = id;

                foreach(var item in db.Accounts.OrderBy(s=>s.id))
                {
                    id = item.id + 1;
                }
                account.id = id;
                account.accout_type = "Worker";
                account.username = Login;
                account.pasword = Password;
                worker.Id_account = account.id;
                db.Accounts.Add(account);
                db.Workers.Add(worker);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(worker);
        }
    }
}