using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class InsuranceSubject
    {
        public int InsuranceSubjectID { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal EstimatedValue { get; set; }
        public int? InsuranceSubjectTypeID { get; set; }
        public InsuranceSubjectType? InsuranceSubjectType { get; set; }
    }
}
