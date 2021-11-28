using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;


namespace web.Models
{
    public class InsuranceType
    {
        public int InsuranceTypeID { get; set; }
        public string Title { get; set; }
        public ApplicationUser? Owner { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateEdited { get; set; }

        // public ICollection<InsurancePolicy>? InsurancePolicies { get; set; }
    }
}
