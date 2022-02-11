using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeThongThueXe.Models;

namespace HeThongThueXe.Pattern
{
    public sealed class BrandSingelton
    {
        public static BrandSingelton Instance { get; } = new BrandSingelton();
        public List<HangXe> listBrand { get; }=new List<HangXe>();
        private BrandSingelton(){}
        public void Init(QLHeThongThueXeEntities2 db)
        {
            if(listBrand.Count==0)
            {
                var brands = db.HangXes.ToList();
                foreach(var brand in brands)
                {
                    listBrand.Add(brand);
                }
            }
        }
        public void Update(QLHeThongThueXeEntities2 db)
        {
            listBrand.Clear();
            Init(db);
        }
    }
}