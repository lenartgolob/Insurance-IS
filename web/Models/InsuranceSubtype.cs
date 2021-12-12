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
        public decimal Rate { get; set; }
        [Required]
        public string Description { get; set; }
        public int? InsuranceTypeID { get; set; }
        public virtual InsuranceType? InsuranceType { get; set; }
        public ICollection<InsurancePolicy>? InsurancePolicies { get; set; }


    }
}
