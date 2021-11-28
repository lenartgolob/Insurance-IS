using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Insured
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstMidName { get; set; }
        public string? FullName { get; set; }

        public ICollection<InsurancePolicy>? InsurancePolicies { get; set; }
    }
}
