using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongThueXe.Models;

namespace HeThongThueXe.Controllers
{
    public class HomeController : Controller
    {
        QLHeThongThueXeEntities2 db = new QLHeThongThueXeEntities2();
        public ActionResult Index()
        {
            var lstXe = db.Xes;
            List<CarViewModel> lstC = new List<CarViewModel>();
            foreach(var xe in lstXe)
            {
                CarViewModel car = new CarViewModel();
                car.GanGT(xe);
                lstC.Add(car);
            }
            return View(lstC);
        }
    }
}