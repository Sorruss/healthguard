using healthguard.Models;

namespace healthguard.Dto
{
    public class PatientDto
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
    }
}
