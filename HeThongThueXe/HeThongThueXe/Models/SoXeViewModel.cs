using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HeThongThueXe.Models
{
    public class SoXeViewModel
    {
        QLHeThongThueXeEntities2 db = new QLHeThongThueXeEntities2();
        public int ID { get; set; }
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "It's not empty!!")]
        public string HoTen { get; set; }
        [Display(Name = "Car")]
        [Required(ErrorMessage = "It's not empty!!")]
        public string Xe { get; set; }
        [Display(Name = "Number of rental days")]
        [Required(ErrorMessage = "It's not empty!!")]
        public int SoNgayThue { get; set; }
        [Display(Name = "Days late")]
        [Required(ErrorMessage = "It's not empty!!")]
        public int SoNgayTraTre { get; set; }
        [Display(Name = "Final total")]
        [Required(ErrorMessage = "It's not empty!!")]
        public decimal? TongTien { get; set; }
        [Display(Name = "Done")]
        [Required(ErrorMessage = "It's not empty!!")]
        public string DaHoanThanh { get; set; }
        public HopDongThueXe HopDongThueXe { get; set; }
        public void GanGT( SoXe soXe)
        {
            this.ID = soXe.ID;
            this.HoTen = db.Users.Where(u => u.IDUser == soXe.HopDongThueXe.IDKH).FirstOrDefault().HoTen;
            this.Xe = db.Xes.Where(x => x.ID == soXe.HopDongThueXe.IDXe).FirstOrDefault().HinhAnh;
            this.SoNgayTraTre = TinhTG(soXe.TGTraXeTT, db.HopDongThueXes.Where(h => h.ID == soXe.IDHD).FirstOrDefault().TGTraXe);
            this.SoNgayThue = db.HopDongThueXes.Where(h => h.ID == soXe.IDHD).FirstOrDefault().SoNgayThue + this.SoNgayTraTre;
            this.TongTien = soXe.SoTienCanTra;
            this.DaHoanThanh = TinhTG(soXe.TGTraXeTT, null).ToString();
            this.HopDongThueXe = soXe.HopDongThueXe;
        }
        public int TinhTG (string tgTra,string tgMuon)
        {
            if(tgMuon==null)
            {
                DateTime tgT = DateTime.ParseExact(tgTra, "dd/MM/yyyy", null);
                TimeSpan timer = DateTime.Now.Date - tgT;
                int soNgay = timer.Days;
                return soNgay;
            }
            else
            {
                DateTime tgT = DateTime.ParseExact(tgTra,"dd/MM/yyyy",null);
                DateTime tgM = DateTime.Parse(tgMuon);
                TimeSpan timer = tgM - tgT;
                int soNgay = timer.Days;
                return soNgay;
            }
           
        }
    }
}