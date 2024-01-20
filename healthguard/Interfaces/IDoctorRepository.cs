using healthguard.Models;

namespace healthguard.Interfaces
{
    public interface IDoctorRepository
    {
        ICollection<Doctor> GetDoctors();
        Doctor GetDoctor(int doctorId);
        ICollection<MedicalRecord> GetMedicalRecordsByDoctor(int doctorId);
        bool DoctorExists(int doctorId);
        bool CreateDoctor(Doctor doctor);
        bool UpdateDoctor(Doctor doctor);
        bool DeleteDoctor(Doctor doctor);
        bool Save();
    }
}
