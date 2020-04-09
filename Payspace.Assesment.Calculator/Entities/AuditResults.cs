using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Payspace.Assesment.Entities
{
    public class AuditResults
    {
        [Key]
        public long AuditID { get; set; }
        public string PostalCode { get; set; }
        public double AnnualIncome { get; set; }
        public DateTime RequestedDate { get; set; }
        public double Tax { get; set; }
    }
}
