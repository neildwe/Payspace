using Payspace.Assesment.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Payspace.Assesment.Entities
{
    public class Calculations
    {
        [Key]
        public long CalculationID { get; set; }

        public double Rate { get; set; }

        public double From { get; set; }

        public double To { get; set; }
    }
}
