using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeThongThueXe.Models;

namespace HeThongThueXe.Pattern
{
    public abstract class PrototypeXe
    {
        Xe xe;
        public PrototypeXe(Xe xe)
        {
            this.xe = xe;
        }
        // Gets id
        public Xe Xe
        {
            get { return xe; }
        }
        public abstract PrototypeXe Clone();
    }
    /// <summary>
    /// A 'ConcretePrototype' class 
    /// </summary>
    public class ConcreteprototypeXe : PrototypeXe
    {
        // Constructor
        public ConcreteprototypeXe(Xe xe)
            : base(xe)
        {
        }
        // Returns a shallow copy
        public override PrototypeXe Clone()
        {
            return (PrototypeXe)this.MemberwiseClone();
        }
    }
}