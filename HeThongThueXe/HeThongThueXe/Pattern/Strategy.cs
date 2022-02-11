using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeThongThueXe.Pattern
{
    public abstract class Strategy
    {
        public abstract string ShowError();
    }
    public class LoginIncorrect : Strategy
    {
        public override string ShowError()
        {
            return "Login information is incorrect!!";
        }
    }
    public class InfoEmty : Strategy
    {
        public override string ShowError()
        {
            return "It's not empty!!";
        }
    }
    public class ErrorMess
    {
        Strategy strategy;
        public ErrorMess(Strategy strategy)
        {
            this.strategy = strategy;
        }
        public string ErrorMessIterface()
        {
            return strategy.ShowError();
        }
    }
}