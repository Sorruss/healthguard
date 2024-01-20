namespace healthguard.Models
{
    public class CompanyManager : BaseUser
    {
        public CompanyManager()
        {
            Role = "CompanyManager";
        }

        public int CompanyManagerId { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
