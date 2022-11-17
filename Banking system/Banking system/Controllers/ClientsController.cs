using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Banking_system.Models;
using ClosedXML.Excel;

namespace Banking_system.Controllers
{
    public class ClientsController : Controller
    {
        private BankDBEntities db = new BankDBEntities();

        // GET: Clients
        public ActionResult Index(string sortOrder, string search)
        {
            ViewBag.Name = sortOrder == "Name_desc" ? "Name" : "Name_desc";
            var clients = from s in db.Clients
                           select s;
            switch (sortOrder)
            {
                case "Name":
                    clients = clients.OrderBy(s => s.Name);
                    break;
                case "Name_desc":
                    clients = clients.OrderByDescending(s => s.Name);
                    break;

            }

            if (!String.IsNullOrEmpty(search))
            {
                clients = clients.Where(s => s.Name.Contains(search)
                                       || s.Surname.Contains(search));
            }

            return View(clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,Patronymic,Salary,Phone,Photo")] Clients clients, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        clients.Photo= reader.ReadBytes(upload.ContentLength);
                    }
                }
                db.Clients.Add(clients);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(clients);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // POST: Clients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Patronymic,Salary,Phone,Photo")] Clients clients, HttpPostedFileBase upload)
        {
           
                if (ModelState.IsValid)
                {
                    db.Entry(clients).State = EntityState.Modified;
                    if (upload != null && upload.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            clients.Photo= reader.ReadBytes(upload.ContentLength);
                        }
                        db.SaveChanges();
                    }

                    else
                    {
                        db.Entry(clients).Property(m => m.Photo).IsModified = false;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }

                return View(clients);
            

            //if (ModelState.IsValid)
            //{
            //    db.Entry(clients).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(clients);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clients clients = db.Clients.Find(id);
            db.Clients.Remove(clients);
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
        public ActionResult DownloadFile()
        {
            List<Clients> clients = db.Clients.ToList();
                
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add("Clients");
                worksheet.ColumnWidth = 19;
                
                worksheet.Cell("A1").Value = "Id";
                worksheet.Cell("B1").Value = "Name";
                worksheet.Cell("C1").Value = "Surname";
                worksheet.Cell("D1").Value = "Patronymic";
                worksheet.Cell("E1").Value = "Salary";
                worksheet.Cell("F1").Value = "Phone";

                for (int j = 1; j <= 6; j++)
                {
                    worksheet.Cell(1, j).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(1, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(1, j).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(1, j).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                }

                for (int i = 1; i < clients.Count; i++)
                {
                    worksheet.Cell(i+1,1).Value = clients[i].Id;
                    worksheet.Cell(i + 1, 2).Value = clients[i].Name;
                    worksheet.Cell(i + 1, 3).Value = clients[i].Surname;
                    worksheet.Cell(i + 1, 4).Value = clients[i].Patronymic;
                    worksheet.Cell(i + 1, 5).Value = clients[i].Salary;
                    worksheet.Cell(i + 1, 6).Value = clients[i].Phone;
                    for (int j = 1; j <= 6; j++)
                    {
                        worksheet.Cell(i+1, j).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i+1, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i+1, j).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i+1, j).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    }

                }
                
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"Clients.xlsx"
                    };
                }

            }



        }


    }
}
