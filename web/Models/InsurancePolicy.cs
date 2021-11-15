namespace web.Models
{
    public class InsurancePolicy
    {
        public int InsurancePolicyID { get; set; }
        public int InsuranceTypeID { get; set; }
        public int InsuredID { get; set; }
        public string InsuranceObject;
        public int ObjectPrice;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public Insured Insured { get; set; }
        public InsuranceType InsuranceType { get; set; }
    }
}