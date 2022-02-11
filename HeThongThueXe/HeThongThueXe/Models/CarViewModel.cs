using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HeThongThueXe.Models
{
    public class CarViewModel
    {
        QLHeThongThueXeEntities2 db = new QLHeThongThueXeEntities2();
        public int ID { get; set; }
        [Required(ErrorMessage = "It's not empty")]
        [Display(Name ="Image")]
        public string Image { get; set; }
        [Required(ErrorMessage = "It's not empty")]
        [Display(Name ="Status")]
        public string Tinhtrang { get; set; }
        [Required(ErrorMessage = "It's not empty")]
        [Display(Name ="Car name")]
        public string DongXe { get; set; }
        [Required(ErrorMessage = "It's not empty")]
        [Display(Name ="Brand")]
        public string HangXe { get; set; }
        [Required(ErrorMessage = "It's not empty")]
        [Display(Name = "Year of manufacture")]
        public int NSX { get; set; }
        [Required(ErrorMessage = "It's not empty")]
        [Display(Name = "License plate")]
        public string BienSo { get; set; }
        [Required(ErrorMessage = "It's not empty")]
        [Display(Name ="Seat")]
        public int SoCho { get; set; }
        [Required(ErrorMessage = "It's not empty")]
        [Display(Name ="Price")]
        public decimal Gia { get; set; }
        [Display(Name = "Note")]
        public string GhiChu { get; set; }
        public Xe Xe { get; set; }

        public void GanGT(Xe x)
        {
            var dong = db.DongXes.Where(i => i.ID == x.IDDongXe).FirstOrDefault();
            var hang = db.HangXes.Where(i => i.ID == x.IDHangXe).FirstOrDefault();
            this.ID = x.ID;
            this.DongXe = dong.Ten;
            this.HangXe = hang.Ten;
            this.NSX = x.NamSX;
            this.Image = x.HinhAnh;
            this.SoCho = dong.SoCho;
            this.BienSo = x.BienSo;
            this.GhiChu = x.GhiChu;
            this.Gia = x.TyLeGia * dong.Gia;
            this.Xe = x;
            if (x.TinhTrang == true)
            {
                this.Tinhtrang = "Stock";
            }
            else
            {
                this.Tinhtrang = "Out of stock";
            }
        }
    }
}