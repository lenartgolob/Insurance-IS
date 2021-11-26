using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class InsurancePolicy
    {
        public int InsurancePolicyID { get; set; }
        // public int InsuranceTypeID { get; set; }
        // public int InsuredID { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal FinalSum { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public Insured Insured { get; set; }
        public ICollection<InsuranceSubtype>? InsuranceSubtypes { get; set; }
        public InsuranceSubject InsuranceSubject { get; set; }

    }
}