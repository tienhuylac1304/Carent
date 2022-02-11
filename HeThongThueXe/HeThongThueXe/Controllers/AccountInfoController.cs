using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HeThongThueXe.Models;

namespace HeThongThueXe.Controllers
{
    public class AccountInfoController : Controller
    {
        QLHeThongThueXeEntities2 db = new QLHeThongThueXeEntities2();
        // GET: AccountInfo
        public ActionResult Index()
        {
            int id = int.Parse(Session["IDUser"].ToString());
            User user = db.Users.Where(x => x.IDUser == id).FirstOrDefault();
            return View(user);
        }
        public ActionResult Edit(int id)
        {
            User user = db.Users.Where(x => x.IDUser == id).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> Edit( User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.EditError = "!!!";
            }
            return View(user);
        }
        public ActionResult CarRentaling(int id)
        {
            var hD = db.Users.Where(u => u.IDUser == id).FirstOrDefault().HopDongThueXes;
            return View(hD.ToList());
        }
        public ActionResult CarBooking(int id)
        {
            var hD = db.Users.Where(u => u.IDUser == id).FirstOrDefault().SoDatXes;
            return View(hD.ToList());
        }
        public async Task<ActionResult> CarBookingDelete(int? id)
        {
            SoDatXe soDatXe = await db.SoDatXes.FindAsync(id);
            if (soDatXe == null)
            {
                return HttpNotFound();
            }
            return View(soDatXe);
        }

        // POST: AdminCarBookings/Delete/5
        [HttpPost, ActionName("CarBookingDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SoDatXe soDatXe = await db.SoDatXes.FindAsync(id);
            db.SoDatXes.Remove(soDatXe);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}