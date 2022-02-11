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
using System.Text.RegularExpressions;

namespace HeThongThueXe.Controllers
{
    public class AdminContractsController : Controller
    {
        private QLHeThongThueXeEntities2 db = new QLHeThongThueXeEntities2();

        // GET: AdminContracts
        public async Task<ActionResult> Index()
        {
            var hopDongThueXes = db.HopDongThueXes;
            return View(await hopDongThueXes.ToListAsync());
        }

        // GET: AdminContracts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HopDongThueXe hopDongThueXe = await db.HopDongThueXes.FindAsync(id);
            if (hopDongThueXe == null)
            {
                return HttpNotFound();
            }
            return View(hopDongThueXe);
        }

        // GET: AdminContracts/Create
        [HttpGet]
        public ActionResult Create(int idXe)
        {
            Session["IDxe"] = idXe;
            Session["HinhAnh"] = db.Xes.Where(x => x.ID == idXe).FirstOrDefault().HinhAnh;
            Session["BienSo"] = db.Xes.Where(x => x.ID == idXe).FirstOrDefault().BienSo;
            ViewBag.DDGiao = new SelectList(DDGX());
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string CMND, string TGGiao, string TGTra, string DDGiao)
        {

            if (ModelState.IsValid)
            {
                Xe xe = new Xe();
                CarViewModel car = new CarViewModel();
                HopDongThueXe hopDongThueXe = new HopDongThueXe();
                hopDongThueXe.IDKH = int.Parse(Session["IDUser"].ToString());
                hopDongThueXe.IDXe = int.Parse(Session["IDxe"].ToString());
                CMND = CMND.Trim();
                Regex trimmer = new Regex(@"\s\s+");
                CMND = trimmer.Replace(CMND, " ");
                hopDongThueXe.CMND = CMND;
                hopDongThueXe.TGGiaoXe = TGGiao;
                hopDongThueXe.TGTraXe = TGTra;
                hopDongThueXe.DiaDiemGiaoXe = DDGiao.Trim();
                hopDongThueXe.IDTrangThaiHD = 1;
                if (TinhSoNgay(hopDongThueXe.TGTraXe, hopDongThueXe.TGGiaoXe) < 0)
                {
                    string temp = hopDongThueXe.TGGiaoXe;
                    hopDongThueXe.TGGiaoXe = hopDongThueXe.TGTraXe;
                    hopDongThueXe.TGTraXe = temp;
                    hopDongThueXe.SoNgayThue = TinhSoNgay(hopDongThueXe.TGTraXe, hopDongThueXe.TGGiaoXe);
                }
                else if (TinhSoNgay(hopDongThueXe.TGTraXe, hopDongThueXe.TGGiaoXe) == 0)
                {
                    hopDongThueXe.SoNgayThue = 1;
                }
                else
                {
                    hopDongThueXe.SoNgayThue = TinhSoNgay(hopDongThueXe.TGTraXe, hopDongThueXe.TGGiaoXe);
                }

                xe = db.Xes.Where(x => x.ID == hopDongThueXe.IDXe).FirstOrDefault();
                car.GanGT(xe);

                hopDongThueXe.TienDuKien = car.Gia * hopDongThueXe.SoNgayThue;
                hopDongThueXe.ThanhToanTruoc = hopDongThueXe.TienDuKien / 2;
                hopDongThueXe.ConLai = hopDongThueXe.TienDuKien - hopDongThueXe.ThanhToanTruoc;

                db.Configuration.ValidateOnSaveEnabled = false;
                hopDongThueXe.IDKH = int.Parse(Session["IDUser"].ToString());
                hopDongThueXe.IDXe = int.Parse(Session["IDxe"].ToString());
                db.HopDongThueXes.Add(hopDongThueXe);
                xe.TinhTrang = false;
                db.Entry(xe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CheckOut_Success");
            }
            return View();
        }
        public ActionResult CheckOut_Success()
        {
            return View();
        }
        // POST: AdminContracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: AdminContracts/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HopDongThueXe hopDongThueXe = await db.HopDongThueXes.FindAsync(id);
        //    if (hopDongThueXe == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.IDKH = new SelectList(db.Users, "IDUser", "HoTen", hopDongThueXe.IDKH);
        //    ViewBag.IDXe = new SelectList(db.Xes, "ID", "HinhAnh", hopDongThueXe.IDXe);
        //    return View(hopDongThueXe);
        //}

        // POST: AdminContracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeState(HopDongThueXe hopDong)
        {
                HopDongThueXe hopDongThueXe = new HopDongThueXe();
                hopDongThueXe = db.HopDongThueXes.Where(hD => hD.ID == hopDong.ID).FirstOrDefault();
                if(hopDongThueXe.IDTrangThaiHD == 1)
                {
                    hopDongThueXe.IDTrangThaiHD = 2;
                }
                else
                {
                    hopDongThueXe.IDTrangThaiHD = 3;
                    Xe xe = new Xe();
                    xe = db.Xes.Where(x => x.ID == hopDongThueXe.IDXe).FirstOrDefault();
                    xe.TinhTrang = true;
                    CarViewModel car = new CarViewModel();
                    car.GanGT(xe);
                    SoXe soXe = new SoXe();
                    soXe.IDHD = hopDongThueXe.ID;
                    DateTime now = DateTime.Now.Date;
                    soXe.TGTraXeTT = now.ToString("dd/MM/yyyy");
                if(TinhSoNgay(now.ToString(), hopDongThueXe.TGTraXe)>=0)
                {
                    soXe.SoTienTraThem = db.ThamSoes.FirstOrDefault().TyLeTraThemKhiQuaNgay * (decimal)TinhSoNgay(now.ToString(), hopDongThueXe.TGTraXe) * car.Gia + hopDongThueXe.ConLai;
                    soXe.SoTienCanTra = soXe.SoTienTraThem + hopDongThueXe.ThanhToanTruoc;
                }    
                else
                {
                    soXe.SoTienTraThem = 0;
                    soXe.SoTienCanTra = soXe.SoTienTraThem + hopDongThueXe.ThanhToanTruoc;
                }    

                    db.SoXes.Add(soXe);
                    db.Entry(xe).State = EntityState.Modified;

                }
                if (ModelState.IsValid)
                {

                    db.Entry(hopDongThueXe).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            return View("Index");
        }
        // GET: AdminContracts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HopDongThueXe hopDongThueXe = await db.HopDongThueXes.FindAsync(id);
            if (hopDongThueXe == null)
            {
                return HttpNotFound();
            }
            return View(hopDongThueXe);
        }

        // POST: AdminContracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HopDongThueXe hopDongThueXe = await db.HopDongThueXes.FindAsync(id);
            db.HopDongThueXes.Remove(hopDongThueXe);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ConfirmContract(string CMND, string TGGiao, string TGTra, string DDGiao)
        {
            Xe xe = new Xe();
            CarViewModel car = new CarViewModel();
            HopDongThueXe hopDongThueXe = new HopDongThueXe();
            hopDongThueXe.IDKH = int.Parse(Session["IDUser"].ToString());
            hopDongThueXe.IDXe = int.Parse(Session["IDxe"].ToString());
            hopDongThueXe.CMND = CMND;
            hopDongThueXe.TGGiaoXe = TGGiao;
            hopDongThueXe.TGTraXe = TGTra;
            hopDongThueXe.DiaDiemGiaoXe = DDGiao;
            if (TinhSoNgay(hopDongThueXe.TGTraXe, hopDongThueXe.TGGiaoXe) < 0)
            {
                string temp = hopDongThueXe.TGGiaoXe;
                hopDongThueXe.TGGiaoXe = hopDongThueXe.TGTraXe;
                hopDongThueXe.TGTraXe = temp;
                hopDongThueXe.SoNgayThue = TinhSoNgay(hopDongThueXe.TGTraXe, hopDongThueXe.TGGiaoXe);
            }
            else if (TinhSoNgay(hopDongThueXe.TGTraXe, hopDongThueXe.TGGiaoXe) == 0)
            {
                hopDongThueXe.SoNgayThue = 1;
            }
            else
            {
                hopDongThueXe.SoNgayThue = TinhSoNgay(hopDongThueXe.TGTraXe, hopDongThueXe.TGGiaoXe);
            }

            xe = db.Xes.Where(x => x.ID == hopDongThueXe.IDXe).FirstOrDefault();
            car.GanGT(xe);

            hopDongThueXe.TienDuKien = car.Gia * hopDongThueXe.SoNgayThue;
            hopDongThueXe.ThanhToanTruoc = hopDongThueXe.TienDuKien / 2;
            hopDongThueXe.ConLai = hopDongThueXe.TienDuKien - hopDongThueXe.ThanhToanTruoc;

            return View(hopDongThueXe);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private int TinhSoNgay(string ngayTra, string ngayMuon)
        {
            int soNgay = 0;
            if (ngayMuon != null && ngayTra != null)
            {
                DateTime ngayM = DateTime.Parse(ngayMuon);
                DateTime ngayT = DateTime.Parse(ngayTra);
                TimeSpan Time = ngayT - ngayM;
                soNgay = Time.Days;
            }
            return soNgay;
        }
        private List<string> DDGX()
        {
            int idUser = int.Parse(Session["IDUser"].ToString());
            List<string> lstPlace = new List<string>();
            lstPlace.Add("SVH");
            string userAddress = db.Users.Where(u => u.IDUser == idUser).FirstOrDefault().DiaChi;
            lstPlace.Add(userAddress);
            return lstPlace;
        }
    }
}
