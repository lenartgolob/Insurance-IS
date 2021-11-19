using System;
using System.Collections.Generic;

namespace web.Models
{
    public class Insured
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }


        public ICollection<InsurancePolicy>? InsurancePolicies { get; set; }
    }
}
