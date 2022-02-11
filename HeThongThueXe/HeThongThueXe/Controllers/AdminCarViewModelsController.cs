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
    public class AdminCarViewModelsController : Controller
    {
        private QLHeThongThueXeEntities2 db = new QLHeThongThueXeEntities2();

        // GET: AdminCarViewModels
        public ActionResult Index()
        {
            List<Xe> xes = db.Xes.ToList();
            List<CarViewModel> cars = new List<CarViewModel>();
            //áp dụng iterator
            IIterator xeIterator = new XeIterator(xes);
            for (Xe i = xeIterator.First(); !xeIterator.IsDone; i = xeIterator.Next())
            {
                CarViewModel car = new CarViewModel();
                car.GanGT(i);
                cars.Add(car);
            }
            return View(cars);
        }

        // GET: AdminCarViewModels/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Xe xe = await db.Xes.FindAsync(id);
        //    if (xe == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(xe);
        //}

        // GET: AdminCarViewModels/Create
        public ActionResult Create()
        {
            BrandSingelton.Instance.Init(db);
            ViewBag.IDDongXe = new SelectList(BrandSingelton.Instance.listBrand, "ID", "Ten");
            ViewBag.IDHangXe = new SelectList(BrandSingelton.Instance.listBrand, "ID", "Ten");
            return View();
        }

        // POST: AdminCarViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,HinhAnh,TinhTrang,BienSo,IDDongXe,IDHangXe,NamSX,TyLeGia,GhiChu")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                db.Xes.Add(xe);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDDongXe = new SelectList(db.DongXes, "ID", "Ten", xe.IDDongXe);
            ViewBag.IDHangXe = new SelectList(db.HangXes, "ID", "Ten", xe.IDHangXe);
            return View(xe);
        }
        public async Task<ActionResult> Duplicate(int id)
        {
            ConcreteprototypeXe p1 = new ConcreteprototypeXe(db.Xes.Find(id));
            ConcreteprototypeXe c1 =(ConcreteprototypeXe) p1.Clone();
            Xe xe = c1.Xe;
            if (ModelState.IsValid)
            {
                db.Xes.Add(xe);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDDongXe = new SelectList(db.DongXes, "ID", "Ten", xe.IDDongXe);
            ViewBag.IDHangXe = new SelectList(db.HangXes, "ID", "Ten", xe.IDHangXe);
            return View(xe);
        }

        // GET: AdminCarViewModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = await db.Xes.FindAsync(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDongXe = new SelectList(db.DongXes, "ID", "Ten", xe.IDDongXe);
            BrandSingelton.Instance.Init(db);
            ViewBag.IDHangXe = new SelectList(BrandSingelton.Instance.listBrand, "ID", "Ten", xe.IDHangXe);
            return View(xe);
        }

        // POST: AdminCarViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,HinhAnh,TinhTrang,BienSo,IDDongXe,IDHangXe,NamSX,TyLeGia,GhiChu")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xe).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDDongXe = new SelectList(db.DongXes, "ID", "Ten", xe.IDDongXe);
            BrandSingelton.Instance.Init(db);
            ViewBag.IDHangXe = new SelectList(BrandSingelton.Instance.listBrand, "ID", "Ten", xe.IDHangXe);
            return View(xe);
        }

        // GET: AdminCarViewModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = await db.Xes.FindAsync(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            return View(xe);
        }

        // POST: AdminCarViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Xe xe = await db.Xes.FindAsync(id);
            db.Xes.Remove(xe);
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
