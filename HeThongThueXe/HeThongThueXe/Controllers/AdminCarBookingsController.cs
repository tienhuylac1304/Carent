using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeThongThueXe.Models;

namespace HeThongThueXe.Controllers
{
    public class AdminCarBookingsController : Controller
    {
        private QLHeThongThueXeEntities2 db = new QLHeThongThueXeEntities2();

        // GET: AdminCarBookings
        public async Task<ActionResult> Index()
        {
            var soDatXes = db.SoDatXes.Include(s => s.User).Include(s => s.Xe);
            return View(await soDatXes.ToListAsync());
        }

        // GET: AdminCarBookings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoDatXe soDatXe = await db.SoDatXes.FindAsync(id);
            if (soDatXe == null)
            {
                return HttpNotFound();
            }
            return View(soDatXe);
        }

        // GET: AdminCarBookings/Create
        public ActionResult Create(int IDXe)
        {
            SoDatXe soDatXe = new SoDatXe();
            var idKH = Session["IDUser"];
            soDatXe.IDKH = (int)idKH;
            soDatXe.IDXe = IDXe;
            return View(soDatXe);
        }

        // POST: AdminCarBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BookCar([Bind(Include = "ID,IDKH,IDXe")] SoDatXe soDatXe)
        {
            soDatXe.IDKH = int.Parse(Session["IDUser"].ToString());
            if (db.SoDatXes.Where(model => model.IDKH == int.Parse(Session["IDUser"].ToString()) && model.IDXe == soDatXe.IDXe) == null)
            {
                soDatXe.NgayDat = DateTime.Now.Date.ToString();
                db.SoDatXes.Add(soDatXe);
                await db.SaveChangesAsync();
                return RedirectToAction("CheckOut_Success");
            }
            else
            {
                if (ModelState.IsValid)
                {     
                    ViewBag.BookingError = "This car is being booked!!";
                }
            }
            return View("CheckOut_Success");
        }

        // GET: AdminCarBookings/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SoDatXe soDatXe = await db.SoDatXes.FindAsync(id);
        //    if (soDatXe == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.IDDongXe = new SelectList(db.DongXes, "ID", "Ten", soDatXe.IDDongXe);
        //    ViewBag.IDKH = new SelectList(db.Users, "IDUser", "HoTen", soDatXe.IDKH);
        //    ViewBag.IDXe = new SelectList(db.Xes, "ID", "HinhAnh", soDatXe.IDXe);
        //    return View(soDatXe);
        //}

        // POST: AdminCarBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "ID,IDKH,IDXe,IDDongXe")] SoDatXe soDatXe)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(soDatXe).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.IDDongXe = new SelectList(db.DongXes, "ID", "Ten", soDatXe.IDDongXe);
        //    ViewBag.IDKH = new SelectList(db.Users, "IDUser", "HoTen", soDatXe.IDKH);
        //    ViewBag.IDXe = new SelectList(db.Xes, "ID", "HinhAnh", soDatXe.IDXe);
        //    return View(soDatXe);
        //}

        // GET: AdminCarBookings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoDatXe soDatXe = await db.SoDatXes.FindAsync(id);
            if (soDatXe == null)
            {
                return HttpNotFound();
            }
            return View(soDatXe);
        }

        // POST: AdminCarBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SoDatXe soDatXe = await db.SoDatXes.FindAsync(id);
            db.SoDatXes.Remove(soDatXe);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> CheckOut_Success()
        {
            return View();
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
