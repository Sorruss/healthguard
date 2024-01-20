namespace healthguard.Models
{
    public class Doctor : BaseUser
    {
        public Doctor()
        {
            Role = "Doctor";
        }

        public int DoctorId { get; set; }
        public Specialization Specialization { get; set; }
        public ICollection<MedicalRecord> MedicalRecords { get; set; }
    }
}
