using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class InsurancePolicy
    {
        public int InsurancePolicyID { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal FinalSum { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        public string? OwnerID { get; set; }
        public int? InsuredID { get; set; }
        public virtual Insured? Insured { get; set; }
        public int? InsuranceSubjectID { get; set; }
        public virtual InsuranceSubject? InsuranceSubject { get; set; }
        public int? InsuranceSubtypeID { get; set; }
        public virtual InsuranceSubtype? InsuranceSubtype { get; set; }

    }
}