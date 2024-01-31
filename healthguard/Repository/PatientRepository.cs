using healthguard.Data;
using healthguard.Interfaces;
using healthguard.Models;
using Microsoft.EntityFrameworkCore;

namespace healthguard.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DataContext _context;

        public PatientRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePatient(Patient patient)
        {
            _context.Add(patient);
            return Save();
        }

        public bool DeletePatient(Patient patient)
        {
            _context.Remove(patient);
            return Save();
        }

        public ICollection<Measurement> GetMeasurementsByPatient(int patientId)
        {
            return _context.Measurements.Where(e => e.PatientId == patientId).ToList();
        }

        public ICollection<MedicalRecord> GetMedicalRecordsByPatient(int patientId)
        {
            return _context.MedicalRecords.Where(e => e.PatientId == patientId).ToList();
        }

        public ICollection<Notification> GetNotificationsByPatient(int patientId)
        {
            return _context.Notifications.Where(e => e.PatientId == patientId).ToList();
        }

        public Patient GetPatient(int patientId)
        {
            return _context.Patients
                .Include(e => e.ApplicationUser)
                .Where(e => e.PatientId == patientId)
                .FirstOrDefault();
        }

        public ICollection<Patient> GetPatients()
        {
            return _context.Patients.OrderBy(e => e.PatientId).ToList();
        }

        public bool PatientExists(int patientId)
        {
            return _context.Patients.Any(e => e.PatientId == patientId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdatePatient(Patient patient)
        {
            _context.Update(patient);
            return Save();
        }
    }
}
