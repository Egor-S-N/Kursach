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
    public class HomeController : Controller
    {
        BankDBEntities db = new BankDBEntities();
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

        public ActionResult Requests()
        {
            if (Session["Userid"]!= null)
            {
                if ((string)Session["Usertype"] == "Client")
                {
                    int id = Convert.ToInt32(Session["Userid"]);
                    var requests = from s in db.Requests where s.Id_client == id select s;
                    return View(requests);

                }
                else
                {
                    var requests = from s in db.Requests  select s;
                    return View(requests);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Details(int? id)
        {
            ViewData.Clear();

            
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }   
            Requests requests = db.Requests.Find(id);
            if (requests == null)
            {
                return HttpNotFound();
            }
            
            var client = (from s in db.Clients where s.Id == requests.Id_client select s).First();
            var creditType = (from s in db.CreditTypes where s.Id == requests.Id_credit_type select s).First();
            ViewData["surname"] = client.Name;
            ViewData["clients"] = client;
            ViewData["credittype"] = creditType;
            return View(requests);

        }

        [HttpGet]
        public ActionResult Create()
        {
            if ((string)Session["Usertype"] == "Client")
            {
                SelectList names = new SelectList(db.Requests, "Name", "Id_credit_type");

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            //ViewBag.Names = names;

            return View();
        }


        [HttpPost]
        public ActionResult Create(string Summ)
        {
            try
            {

                string name_of_credit = Request.Form["Credit"].ToString();
                int credit_id = Convert.ToInt32((from s in db.CreditTypes where name_of_credit == s.Name select s.Id).First());
                Requests request = new Requests();
                request.Result = false;
                request.Id_credit_type = credit_id;

                int id = 0;
                foreach (var item in db.Requests.OrderBy(s => s.Id))
                {
                    id = item.Id + 1;
                }

                request.Id_client = Convert.ToInt32(Session["Userid"]);
                request.Id = id;
                db.Requests.Add(request);
                

                Credits credit = new Credits();

                foreach(var item in db.Credits.OrderBy(s=>s.Id))
                {
                    id = item.Id + 1;
                }


                credit.Id = id;
                Random rand = new Random();
                var workers = (from s in db.Workers where s.Post == "Менеджер" select s).ToList();
                var all_workers = (from s in db.Workers select s).ToList();
                int i = 0;
                while(true)
                {
                    int temp = rand.Next(1,all_workers.Count());
                    if (temp == workers[i].Id)
                    {
                        credit.Id_worker = temp;
                        break;
                    }
                    else
                    {
                        i++;

                    }
                }
                

                foreach(var item in db.Requests.OrderBy(s=>s.Id))
                {
                    id = item.Id + 1;
                }

                credit.Id_request = id;
                credit.Credit_sum = Convert.ToInt32(Summ);
                credit.Date_of_issue = DateTime.Today;

                db.Credits.Add(credit);

                db.SaveChanges();
                return RedirectToAction("Index","Home");


            }
            catch
            {
            }
            return View();
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requests requests = db.Requests.Find(id);
            if (requests == null)
            {
                return HttpNotFound();
            }

            var client = (from s in db.Clients where s.Id == requests.Id_client select s).First();
            var creditType = (from s in db.CreditTypes where s.Id == requests.Id_credit_type select s).First();
            ViewData["clients"] = client;
            ViewData["credittype"] = creditType;
            return View(requests);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Requests requests = db.Requests.Find(id);
            db.Requests.Remove(requests);
            db.SaveChanges();
            return RedirectToAction("Requests");
        }
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requests requests = db.Requests.Find(id);
            var creditType = (from s in db.CreditTypes where s.Id == requests.Id_credit_type select s).First();
            var client = (from s in db.Clients where s.Id == requests.Id_client select s).First();

            ViewData["client"] = client;
            ViewData["credit_type"] =creditType;
            Session["req_id_client"] = requests.Id_client;
            Session["req_id_credit_type"] = requests.Id_credit_type;
            if (requests == null)
            {
                return HttpNotFound();
            }
            return View(requests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve([Bind(Include = "Id,Id_credit_type,Id_client,Result")] Requests requests, HttpPostedFileBase upload)
        {

            
                db.Entry(requests).State = EntityState.Modified;
                
                    requests.Result = true;
                    requests.Id_client = Convert.ToInt32(Session["req_id_client"]);
                    requests.Id_credit_type = Convert.ToInt32(Session["req_id_credit_type"]);
                    db.SaveChanges();
               
                return RedirectToAction("Requests");
        }


        

    }
}