using healthguard.Models;

namespace healthguard.POST
{
    public class PatientUPDATE
    {
        public int PatientId { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int CompanyId { get; set; }
    }
}
