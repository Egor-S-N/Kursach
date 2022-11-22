using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
                    var correct_account = (from s in db.Accounts  where s.username == user.Username & s.pasword == user.Password select  s).First();
                    var correct_client = (from s in db.Clients where s.Id_account == correct_account.id select s);
                    if(correct_client.Count() != 0)
                    {
                        var client = correct_client.First();
                        Session["UserSurname"] = client.Name;
                        Session["Userid"] = client.Id;
                    }
                    else
                    {
                        var correct_worker = (from s in db.Workers where s.Id_account == correct_account.id select s).First();
                        Session["UserSurname"] = correct_worker.Name;
                        Session["Userid"] = correct_worker.Id;

                    }
                    
                    
                    
                    

                    Session["Usertype"] = correct_account.accout_type;
                    TempData["msg"] = null;


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["msg"] =$"Error";
                }
            }
                return View();
        }

        public ActionResult Register()
        {
            return View();
        }

       
        [HttpPost]
        public ActionResult Register( Register ac, HttpPostedFileBase upload)
        {
            try
            {


                int id = 0;
                foreach (var item in db.Accounts)
                {
                    id = item.id + 1;
                }
                byte[] a;
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    a = reader.ReadBytes(upload.ContentLength);
                }

                ac.Id = id;
                ac.UserType = "Client";
                int temp = id;

                db.Accounts.Add(new Accounts
                {
                    id = id,
                    username = ac.Username,
                    pasword = ac.Password,
                    accout_type = "Client"
                });
                db.SaveChanges();

                foreach (var item in db.Clients)
                {
                    id = item.Id + 1;
                }


                db.Clients.Add(new Clients
                {
                    Id = id,
                    Name = ac.Name,
                    Surname = ac.Surname,
                    Patronymic = ac.Patronymic,
                    Salary = ac.Salary,
                    Phone = ac.Phone,
                    Photo = a,
                    Id_account = temp
                });

                db.SaveChanges();
                return RedirectToAction("Login", "Accounts");
            }
            catch
            {
                return View();
            }
            
        }


        
        public ActionResult Profile(int? id)
        {

            id = Convert.ToInt32(Session["Userid"]);
           
            Workers workers = db.Workers.Find(id);
            ViewData["worker"] = workers;
            
            Clients clients = db.Clients.Find(id);
            ViewData["client"] = clients;


            try
            {


                var credits = (from cred in db.Credits
                               from req in db.Requests
                               where req.Id_client == id && cred.Id_request == req.Id
                               select cred).ToList();

                ViewData["credits"] = credits;
            }
            catch
            {
                ViewData["credits"] = null;
            }





            return View();
        }











        public ActionResult Logout()
        {
            
            Session.Clear();
            TempData.Clear();
            return RedirectToAction("Login", "Accounts");
        }
        
    }
}