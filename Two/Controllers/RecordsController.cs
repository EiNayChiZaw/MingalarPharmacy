using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Two;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Data.SqlClient;

namespace Two.Controllers
{
    public class RecordsController : Controller
    {
        private MVCThreeEntities db = new MVCThreeEntities();
        
        // GET: Records
        public ActionResult Index()
        {
            return View(db.Records.ToList());
        }

        public ActionResult Search(string searchString)
        {

            var medicine = from m in db.Records
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                medicine = medicine.Where(s => s.Bought_Medicine.Contains(searchString));
            }

            return View(medicine);

        }

        // GET: Records/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // GET: Records/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Records/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordID,PatientID,Date,Bought_Medicine,Buying_Reason,Comment")] Record record)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                db.Records.Add(record);
                db.SaveChanges();
               
                string connectionString = "Integrated Security=SSPI;Initial Catalog=MVCThree;Data Source=HISOKA";
                SqlConnection sql;
                sql = new SqlConnection(connectionString);

                sql.Open();
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("", connection))
                {
                    connection.Open();
                    command.CommandText = "update Record set PatientID = @Name where RecordID = (select MAX(RecordID) from Record)";
                    command.Parameters.AddWithValue("@Name", userId);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
               
                sql.Close();
                return RedirectToAction("Index");
            }

            return View(record);
        }

        // GET: Records/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // POST: Records/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Date,Bought_Medicine,Buying_Reason,Comment")] Record record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(record);
        }

        // GET: Records/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // POST: Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Record record = db.Records.Find(id);
            db.Records.Remove(record);
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
    }
}
