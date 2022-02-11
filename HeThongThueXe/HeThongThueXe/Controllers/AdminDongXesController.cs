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
    public class AdminDongXesController : Controller
    {
        private QLHeThongThueXeEntities2 db = new QLHeThongThueXeEntities2();

        // GET: AdminDongXes
        public async Task<ActionResult> Index()
        {
            return View(await db.DongXes.ToListAsync());
        }

        // GET: AdminDongXes/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DongXe dongXe = await db.DongXes.FindAsync(id);
        //    if (dongXe == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dongXe);
        //}

        // GET: AdminDongXes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminDongXes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Ten,Gia,SoCho")] DongXe dongXe)
        {
            if (ModelState.IsValid)
            {
                db.DongXes.Add(dongXe);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(dongXe);
        }

        // GET: AdminDongXes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DongXe dongXe = await db.DongXes.FindAsync(id);
            if (dongXe == null)
            {
                return HttpNotFound();
            }
            return View(dongXe);
        }

        // POST: AdminDongXes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Ten,Gia,SoCho")] DongXe dongXe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dongXe).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dongXe);
        }

        // GET: AdminDongXes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DongXe dongXe = await db.DongXes.FindAsync(id);
            if (dongXe == null)
            {
                return HttpNotFound();
            }
            return View(dongXe);
        }

        // POST: AdminDongXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DongXe dongXe = await db.DongXes.FindAsync(id);
            db.DongXes.Remove(dongXe);
            await db.SaveChangesAsync();
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
