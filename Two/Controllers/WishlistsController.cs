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
    public class WishlistsController : Controller
    {
        private MVCThreeEntities db = new MVCThreeEntities();

        // GET: Wishlists
        public ActionResult Index()
        {
            return View(db.Wishlists.ToList());
        }

        public ActionResult Search(string searchString)
        {

            var medicine = from m in db.Wishlists
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                medicine = medicine.Where(s => s.WishMedName.Contains(searchString));
            }

            return View(medicine);

        }
        // GET: Wishlists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wishlist wishlist = db.Wishlists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            return View(wishlist);
        }

        // GET: Wishlists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wishlists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WishID,WishMedName,RequestAmount,Comment,RequesterID")] Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                db.Wishlists.Add(wishlist);
                db.SaveChanges();
               

                string connectionString = "Integrated Security=SSPI;Initial Catalog=MVCThree;Data Source=HISOKA";
                SqlConnection sql;
                sql = new SqlConnection(connectionString);

                sql.Open();
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("", connection))
                {
                    connection.Open();
                    command.CommandText = "update Wishlist set RequesterID = @Name where WishID = (select MAX(WishID) from Wishlist)";
                    command.Parameters.AddWithValue("@Name", userId);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                sql.Close();
                return RedirectToAction("Index");
            }

            return View(wishlist);
        }

        // GET: Wishlists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wishlist wishlist = db.Wishlists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            return View(wishlist);
        }

        // POST: Wishlists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WishMedName,RequestAmount,Comment,RequesterID")] Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wishlist);
        }

        // GET: Wishlists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wishlist wishlist = db.Wishlists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            return View(wishlist);
        }

        // POST: Wishlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wishlist wishlist = db.Wishlists.Find(id);
            db.Wishlists.Remove(wishlist);
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
