using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class InsuranceType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InsuranceTypeID { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }

        public ICollection<InsurancePolicy>? InsurancePolicies { get; set; }
    }
}
