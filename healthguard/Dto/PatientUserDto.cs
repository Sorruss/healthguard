namespace healthguard.Dto
{
    public class PatientUserDto : ApplicationUserDto
    {
        public string Gender { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
    }
}
