using healthguard.Models;

namespace healthguard.Interfaces
{
    public interface IPatientRepository
    {
        ICollection<Patient> GetPatients();
        Patient GetPatient(int patientId);
        ICollection<Notification> GetNotificationsByPatient(int patientId);
        ICollection<Measurement> GetMeasurementsByPatient(int patientId);
        ICollection<MedicalRecord> GetMedicalRecordsByPatient(int patientId);
        bool PatientExists(int patientId);
        bool CreatePatient(Patient patient);
        bool UpdatePatient(Patient patient);
        bool DeletePatient(Patient patient);
        bool Save();
    }
}
