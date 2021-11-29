using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class InsuranceSubtype
    {
        public int InsuranceSubtypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Rate { get; set; }
        public int? InsuranceTypeID;
        public InsuranceType? InsuranceType { get; set; }
        public ICollection<InsurancePolicy>? InsurancePolicies { get; set; }


    }
}
