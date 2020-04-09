using Payspace.Assesment.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Payspace.Assesment.Entities
{
    public class PostalCode
    {
        [Key]
        [Column(TypeName = "varchar(50)")]
        public string Code { get; set; }

        [Column(TypeName = "bigint")]
        public ECalculationType CalcType { get; set; }
    }
}
