using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongThueXe.Models;
using System.Web.UI;
using HeThongThueXe.Pattern;

namespace HeThongThueXe.Controllers
{
    public class LoginController : Controller
    {
        QLHeThongThueXeEntities2 db = new QLHeThongThueXeEntities2();
        // GET: Login
        public ActionResult Login()
        {
            if (Session["NameUser"] != null)
                Session.Clear();
            return View();
        }
        [HttpPost]
        public ActionResult LoginAccount(User user)
        {
            var check = db.Users.Where(s => s.Account == user.Account && s.PasswordUser == user.PasswordUser).FirstOrDefault();

            if(check!=null)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["IDUser"] = check.IDUser;
                Session["LoaiUser"] = check.IDLoaiUser;
                Session["NameUser"] = check.HoTen;
                Session["DateOfBirth"] = check.NgaySinh;
                Session["PhoneNumber"] = check.SDT;
                Session["Address"] = check.DiaChi;
                Session["CarRental"] = check.SoXeDangThue;
                Session["Account"] = check.Account;
                if (check.IDLoaiUser == 2)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "AdminCarViewModels");
                }
            }
            else
            {
                ErrorMess error = new ErrorMess(new LoginIncorrect());
                ViewBag.Message = error.ErrorMessIterface();
            }
            return View("Login");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(User _user)
        {
            if (ModelState.IsValid)
            {
                var check_ID = db.Users.Where(s => s.Account == _user.Account).FirstOrDefault();
                if (check_ID == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    _user.IDLoaiUser = 2;
                    db.Users.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.ErrorRegister = "This account is exist";
                    return View("Register");
                }
            }
            return View("Register");
        }
        public PartialViewResult AccountInfo()
        {
            return PartialView();
        }
    }
}