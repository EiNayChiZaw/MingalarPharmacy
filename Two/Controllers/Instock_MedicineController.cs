using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Two;
using System.Data.SqlClient;
using System.IO;
namespace Two.Controllers
{
    public class Instock_MedicineController : Controller
    {
        private MVCThreeEntities db = new MVCThreeEntities();

        // GET: Instock_Medicine
        public ActionResult Index()
        {
            return View(db.Instock_Medicine.ToList());
        }

        public ActionResult Search(string searchString)
        {

            var medicine = from m in db.Instock_Medicine
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                medicine = medicine.Where(s => s.MedName.Contains(searchString));
            }

            return View(medicine);

            //if (searchby == "Name")
            //{
            //    return View(db.Instock_Medicine.Where(x => x.MedName == search || search == null).ToList());
            //}
            //else
            //{
            //    return View(db.Instock_Medicine.Where(x => x.MedID.Equals(search) || search == null).ToList());
            //}

        }


        // GET: Instock_Medicine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instock_Medicine instock_Medicine = db.Instock_Medicine.Find(id);
            if (instock_Medicine == null)
            {
                return HttpNotFound();
            }
            return View(instock_Medicine);
        }

        // GET: Instock_Medicine/Create
        public ActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(HttpPostedFileBase postedFile)
        //{
        //    byte[] bytes;
        //    using (BinaryReader br = new BinaryReader(postedFile.InputStream))
        //    {
        //        bytes = br.ReadBytes(postedFile.ContentLength);
        //    }
        //    MVCThreeEntities entities = new MVCThreeEntities();
        //    entities.Instock_Medicine.Add(new Instock_Medicine
        //    {
        //        Med_Img = bytes
        //    });
        //    entities.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MedID,MedName,Quantity,Price,SupplierID,Description,")] Instock_Medicine instock_Medicine, HttpPostedFileBase File1, HttpPostedFileBase File2)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //if (File1 != null && File1.ContentLength > 0 && File2 != null)
        //        //{
        //            instock_Medicine.Med_Img = new byte[File1.ContentLength];
        //            File1.InputStream.Read(instock_Medicine.Med_Img, 0, File1.ContentLength);
        //            string ImageName = System.IO.Path.GetFileName(File2.FileName);
        //            string physicalPath = Server.MapPath("~/img/" + ImageName);
        //            File2.SaveAs(physicalPath);
        //           instock_Medicine.Img_Path="img/" + ImageName;
        //            db.Instock_Medicine.Add(instock_Medicine);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //            //byte[] bytes;
        //            //using (BinaryReader br = new BinaryReader(postedFile.InputStream))
        //            //{
        //            //    bytes = br.ReadBytes(postedFile.ContentLength);
        //            //}
        //            //MVCThreeEntities entities = new MVCThreeEntities();
        //            //entities.Instock_Medicine.Add(new Instock_Medicine {

        //            //    Med_Img = bytes });
        //            //    entities.SaveChanges();



        //            //string connectionString = "Integrated Security=SSPI;Initial Catalog=MVCThree;Data Source=HISOKA";
        //            //SqlConnection sql;
        //            //sql = new SqlConnection(connectionString);

        //            //sql.Open();
        //            //using (SqlConnection connection = new SqlConnection(connectionString))
        //            //using (SqlCommand command = new SqlCommand("", connection))
        //            //{
        //            //    connection.Open();
        //            //    command.CommandText = "update Instock_Medicine set Med_Img = @Name where MedID = (select MAX(MedID) from Instock_Medicine)";
        //            //    command.Parameters.AddWithValue("@Name", bytes);
        //            //    command.ExecuteNonQuery();
        //            //    connection.Close();
        //            //}

        //            //sql.Close();
        //        //}
        //    }

        //    return View(instock_Medicine);
        //}]

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                MVCThreeEntities db = new MVCThreeEntities();
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Content/" + ImageName);
                file.SaveAs(physicalPath);
                Instock_Medicine med = new Instock_Medicine();
                //med.MedID = int.Parse(Request.Form["MedID"]);
                med.Med_Img = new byte[file.ContentLength];
                file.InputStream.Read(med.Med_Img, 0, file.ContentLength);
                med.MedName = Request.Form["MedName"];
                med.Quantity= int.Parse(Request.Form["Quantity"]);
                med.Price = int.Parse(Request.Form["Price"]);
                med.SupplierID = int.Parse(Request.Form["SupplierID"]);
                med.Description= Request.Form["Description"];
                med.Img_Path = ImageName;
                db.Instock_Medicine.Add(med);
                db.SaveChanges();

                string connectionString = "Integrated Security=SSPI;Initial Catalog=MVCThree;Data Source=HISOKA";
                SqlConnection sql;
                sql = new SqlConnection(connectionString);

                sql.Open();
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("", connection))
                {
                    connection.Open();
                    command.CommandText = "update Instock_Medicine set Instock_Date = GetDate() where MedID = (select MAX(MedID) from Instock_Medicine)";
                    
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                sql.Close();

            }
            return RedirectToAction("Index");
        }

        // GET: Instock_Medicine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instock_Medicine instock_Medicine = db.Instock_Medicine.Find(id);
            if (instock_Medicine == null)
            {
                return HttpNotFound();
            }
            return View(instock_Medicine);
        }

        // POST: Instock_Medicine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MedID,MedName,Quantity,Price,SupplierID")] Instock_Medicine instock_Medicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instock_Medicine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instock_Medicine);
        }

        // GET: Instock_Medicine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instock_Medicine instock_Medicine = db.Instock_Medicine.Find(id);
            if (instock_Medicine == null)
            {
                return HttpNotFound();
            }
            return View(instock_Medicine);
        }

        // POST: Instock_Medicine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instock_Medicine instock_Medicine = db.Instock_Medicine.Find(id);
            db.Instock_Medicine.Remove(instock_Medicine);
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
