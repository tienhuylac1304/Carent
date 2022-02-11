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
    public class SoXesController : Controller
    {
        private QLHeThongThueXeEntities2 db = new QLHeThongThueXeEntities2();

        // GET: SoXes
        public ActionResult Index()
        {
            List<SoXe> soXes = db.SoXes.Include(s => s.HopDongThueXe).ToList();
            List<SoXeViewModel> lstSo = new List<SoXeViewModel>();
            foreach(SoXe s in soXes)
            {
                SoXeViewModel so = new SoXeViewModel();
                so.GanGT(s);
                lstSo.Add(so);
            }
            return View(lstSo);
        }

        // GET: SoXes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoXe soXe = await db.SoXes.FindAsync(id);
            
            if (soXe == null)
            {
                return HttpNotFound();
            }
            SoXeViewModel so = new SoXeViewModel();
            so.GanGT(soXe);
            return View(so);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoXe soXe = await db.SoXes.FindAsync(id);
            
            if (soXe == null)
            {
                return HttpNotFound();
            }
            SoXeViewModel so = new SoXeViewModel();
            so.GanGT(soXe);
            return View(so);
        }

        // POST: SoXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SoXe soXe = await db.SoXes.FindAsync(id);
            HopDongThueXe hopDongThueXe = await db.HopDongThueXes.FindAsync(soXe.IDHD);
            db.SoXes.Remove(soXe);
            db.HopDongThueXes.Remove(hopDongThueXe);
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
