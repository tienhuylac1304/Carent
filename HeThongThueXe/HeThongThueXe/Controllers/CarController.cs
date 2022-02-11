using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HeThongThueXe.Models;
using PagedList;
using PagedList.Mvc;
using HeThongThueXe.Pattern;

namespace HeThongThueXe.Controllers
{
    public class CarController : Controller
    {
        QLHeThongThueXeEntities2 db = new QLHeThongThueXeEntities2();
        // GET: Car
        public ActionResult Index(int? page, int? hang,int? cho,string dong)
        {
            int pageNum = (page ?? 1);
            int pageSize = 6;
            //áp dụng singleton
            BrandSingelton.Instance.Init(db);
            List<HangXe> hangXes = BrandSingelton.Instance.listBrand;

            ViewBag.ListBrand = new SelectList(hangXes, "ID", "Ten");
            ViewBag.ListSeat = new SelectList(GetSeats());
            List<CarViewModel> cars = new List<CarViewModel>();
            if(hang==null&&cho==null&&dong==null)
            {
                var lstCar = db.Xes.ToList();
                // áp dụng iterator
                IIterator xeIterator = new XeIterator(lstCar);
                for(Xe i= xeIterator.First();!xeIterator.IsDone;i=xeIterator.Next())
                {
                    CarViewModel car = new CarViewModel();
                    car.GanGT(i);
                    cars.Add(car);
                }
                return View(cars.ToPagedList(pageNum, pageSize));
            }
           else if(hang == null && cho == null)
            {
                var lstCar = db.Xes.Where(x=>x.DongXe.Ten.Contains(dong)).ToList();
                //áp dụng iterator
                IIterator xeIterator = new XeIterator(lstCar);
                for (Xe i = xeIterator.First(); !xeIterator.IsDone; i = xeIterator.Next())
                {
                    CarViewModel car = new CarViewModel();
                    car.GanGT(i);
                    cars.Add(car);
                }
                return View(cars.ToPagedList(pageNum, pageSize));
            }
            else if (hang == null && dong == null)
            {
                var lstCar = db.Xes.Where(x => x.DongXe.SoCho==cho).ToList();
                //áp dụng iterator
                IIterator xeIterator = new XeIterator(lstCar);
                for (Xe i = xeIterator.First(); !xeIterator.IsDone; i = xeIterator.Next())
                {
                    CarViewModel car = new CarViewModel();
                    car.GanGT(i);
                    cars.Add(car);
                }
                return View(cars.ToPagedList(pageNum, pageSize));
            }
            else if (dong == null && cho == null)
            {
                var lstCar = db.Xes.Where(x => x.HangXe.ID==hang).ToList();
                //áp dụng iterator
                IIterator xeIterator = new XeIterator(lstCar);
                for (Xe i = xeIterator.First(); !xeIterator.IsDone; i = xeIterator.Next())
                {
                    CarViewModel car = new CarViewModel();
                    car.GanGT(i);
                    cars.Add(car);
                }
                return View(cars.ToPagedList(pageNum, pageSize));
            }
            else if (hang == null)
            {
                var lstCar = db.Xes.Where(x => x.DongXe.Ten.Contains(dong)&&x.DongXe.SoCho==cho).ToList();
                //áp dụng iterator
                IIterator xeIterator = new XeIterator(lstCar);
                for (Xe i = xeIterator.First(); !xeIterator.IsDone; i = xeIterator.Next())
                {
                    CarViewModel car = new CarViewModel();
                    car.GanGT(i);
                    cars.Add(car);
                }
                return View(cars.ToPagedList(pageNum, pageSize));
            }
            else if (dong == null)
            {
                var lstCar = db.Xes.Where(x => x.HangXe.ID==hang && x.DongXe.SoCho == cho).ToList();
                //áp dụng iterator
                IIterator xeIterator = new XeIterator(lstCar);
                for (Xe i = xeIterator.First(); !xeIterator.IsDone; i = xeIterator.Next())
                {
                    CarViewModel car = new CarViewModel();
                    car.GanGT(i);
                    cars.Add(car);
                }
                return View(cars.ToPagedList(pageNum, pageSize));
            }
            else if (cho == null)
            {
                var lstCar = db.Xes.Where(x => x.DongXe.Ten.Contains(dong) && x.HangXe.ID==hang).ToList();
                //áp dụng iterator
                IIterator xeIterator = new XeIterator(lstCar);
                for (Xe i = xeIterator.First(); !xeIterator.IsDone; i = xeIterator.Next())
                {
                    CarViewModel car = new CarViewModel();
                    car.GanGT(i);
                    cars.Add(car);
                }
                return View(cars.ToPagedList(pageNum, pageSize));
            }
            else
            {
                var lstCar = db.Xes.Where(x => x.DongXe.Ten.Contains(dong) && x.HangXe.ID==hang&&x.DongXe.SoCho==cho).ToList();
                //áp dụng iterator
                IIterator xeIterator = new XeIterator(lstCar);
                for (Xe i = xeIterator.First(); !xeIterator.IsDone; i = xeIterator.Next())
                {
                    CarViewModel car = new CarViewModel();
                    car.GanGT(i);
                    cars.Add(car);
                }
                return View(cars.ToPagedList(pageNum, pageSize));
            }    
        }
        public ActionResult CarDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            CarViewModel car = new CarViewModel();
            car.GanGT(xe);
            return View(car);
        }
        public List<int>GetSeats()
        {
            List<int> lstCho = new List<int>();
            List<int> lstSeat = new List<int>();
            List<DongXe> xes = db.DongXes.ToList();
            foreach(var xe in xes)
            {
                lstCho.Add(xe.SoCho);
            }
            lstSeat = lstCho.Distinct().ToList();
            return lstSeat;
        }
        public ActionResult BookCar(int IDXe)
        {
            SoDatXe soDatXe = new SoDatXe();
            soDatXe.IDKH = int.Parse(Session["IDUser"].ToString());
            soDatXe.IDXe = IDXe;
            soDatXe.NgayDat = DateTime.Now.Date.ToString("dd/MM/yyyy").Trim();
            if (ModelState.IsValid)
                {
                    db.SoDatXes.Add(soDatXe);
                    db.SaveChanges();
                    return RedirectToAction("CheckOut_Success", "AdminCarBookings");
                }
            return View();
        }
    }
    }