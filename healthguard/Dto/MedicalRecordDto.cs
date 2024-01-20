using healthguard.Models;

namespace healthguard.Dto
{
    public class MedicalRecordDto
    {
        public int MedicalRecordId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; }
        public string Medications { get; set; }
    }
}
