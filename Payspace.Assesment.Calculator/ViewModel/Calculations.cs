using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payspace.Assesment.ViewModel
{
    public class Calculations
    {
        public double Rate { get; set; }
        public double From { get; set; }
        public double To { get; set; }
        public double Tax { get; internal set; }
    }
}
