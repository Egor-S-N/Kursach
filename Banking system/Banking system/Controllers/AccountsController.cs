using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Banking_system.Models;

namespace Banking_system.Controllers
{
    public class AccountsController : Controller
    {
        private BankDBEntities db = new BankDBEntities();
        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using(BankDBEntities db = new BankDBEntities())
            {
                var result = db.Accounts.Where(m=>m.username == user.Username && m.pasword == user.Password);
                
                if (result.Count() != 0)
                {
                    Session["Userid"] = user.Username;
                    Session["Usertype"] = user.UserType;


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["msg"] =$"Error" ;
                }
            }
                return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,username,pasword,accout_type")] Accounts ac, HttpPostedFileBase upload)
        {
            
            int id = 0;
            foreach (var item in db.Accounts)
            {
                id = item.id + 1;
            }
            ac.id = id;
            try
            {
                db.Accounts.Add(ac);
                db.SaveChanges();
                Session["Userid"] = ac.username;


            }
            catch
            {
            
            }
            return RedirectToAction("Index", "Home");
        }









        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
            //return View("Login");
        }
    }
}