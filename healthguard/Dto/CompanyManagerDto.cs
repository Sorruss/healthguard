using healthguard.Models;

namespace healthguard.Dto
{
    public class CompanyManagerDto
    {
        public int CompanyManagerId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
    }
}
