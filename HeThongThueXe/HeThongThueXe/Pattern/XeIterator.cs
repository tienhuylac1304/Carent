using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeThongThueXe.Models;

namespace HeThongThueXe.Pattern
{
    public interface IIterator
    {
        Xe First();
        Xe Next();
        Xe CurrentItem { get; }
        bool IsDone { get; }
    }
    public class XeIterator : IIterator
    {
        List<Xe> lstXe;
        int current = 0;
        int step = 1;
        public XeIterator(List<Xe> xes)
        {
            lstXe = xes;
        }
        public Xe CurrentItem => lstXe[current];

        public bool IsDone { get { return current >= lstXe.Count; } }

        public int Step { get => step; set => step = value; }

        public Xe First()
        {
            current = 0;
            if (lstXe.Count > 0)
                return lstXe[current];
            return null;
        }
        public Xe Next()
        {
            current += step;
            if (!IsDone)
                return lstXe[current];
            else
                return null;
        }
    }
}
