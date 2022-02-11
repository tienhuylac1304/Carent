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
using HeThongThueXe.Pattern;

namespace HeThongThueXe.Controllers
{
    public class AdminHangXesController : Controller
    {
        private QLHeThongThueXeEntities2 db = new QLHeThongThueXeEntities2();

        // GET: AdminHangXes
        public async Task<ActionResult> Index()
        {
            BrandSingelton.Instance.Init(db);
            return View(BrandSingelton.Instance.listBrand);
        }

        // GET: AdminHangXes/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HangXe hangXe = await db.HangXes.FindAsync(id);
        //    if (hangXe == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hangXe);
        //}

        // GET: AdminHangXes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminHangXes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Ten")] HangXe hangXe)
        {
            if (ModelState.IsValid)
            {
                db.HangXes.Add(hangXe);
                await db.SaveChangesAsync();
                BrandSingelton.Instance.Update(db);
                return RedirectToAction("Index");
            }

            return View(hangXe);
        }

        // GET: AdminHangXes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<HangXe> hangXes = BrandSingelton.Instance.listBrand;
            HangXe hangXe = new HangXe();
            foreach(HangXe brand in hangXes)
            {
                if(brand.ID==id)
                {
                    hangXe = brand;
                }    
            }
            if (hangXe == null)
            {
                return HttpNotFound();
            }
            return View(hangXe);
        }

        // POST: AdminHangXes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Ten")] HangXe hangXe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangXe).State = EntityState.Modified;
                await db.SaveChangesAsync();
                BrandSingelton.Instance.Update(db);
                return RedirectToAction("Index");
            }
            return View(hangXe);
        }

        // GET: AdminHangXes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<HangXe> hangXes = BrandSingelton.Instance.listBrand;
            HangXe hangXe = new HangXe();
            foreach (HangXe brand in hangXes)
            {
                if (brand.ID == id)
                {
                    hangXe = brand;
                }
            }
            if (hangXe == null)
            {
                return HttpNotFound();
            }
            return View(hangXe);
        }

        // POST: AdminHangXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HangXe hangXe = await db.HangXes.FindAsync(id);
            db.HangXes.Remove(hangXe);
            await db.SaveChangesAsync();
            BrandSingelton.Instance.Update(db);
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
