using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class InsuranceSubjectType
    {
        public int InsuranceSubjectTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public ICollection<InsuranceSubject>? InsuranceSubjects { get; set; }

    }
}
