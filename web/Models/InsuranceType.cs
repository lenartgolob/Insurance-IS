using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace web.Models
{
    public class InsuranceType
    {
        public int InsuranceTypeID { get; set; }
        [Required]
        public string Title { get; set; }
        // public ApplicationUser? Owner { get; set; }
        public ICollection<InsuranceSubtype>? InsuranceSubtypes { get; set; }
    }
}
