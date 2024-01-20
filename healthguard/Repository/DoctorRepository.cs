using healthguard.Data;
using healthguard.Interfaces;
using healthguard.Models;

namespace healthguard.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DataContext _context;

        public DoctorRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateDoctor(Doctor doctor)
        {
            _context.Add(doctor);
            return Save();
        }

        public bool DeleteDoctor(Doctor doctor)
        {
            _context.Remove(doctor);
            return Save();
        }

        public bool DoctorExists(int doctorId)
        {
            return _context.Doctors.Any(e => e.DoctorId == doctorId);
        }

        public Doctor GetDoctor(int doctorId)
        {
            return _context.Doctors.Where(e => e.DoctorId == doctorId).FirstOrDefault();
        }

        public ICollection<Doctor> GetDoctors()
        {
            return _context.Doctors.OrderBy(e => e.DoctorId).ToList();
        }

        public ICollection<MedicalRecord> GetMedicalRecordsByDoctor(int doctorId)
        {
            return _context.MedicalRecords.Where(e => e.DoctorId == doctorId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateDoctor(Doctor doctor)
        {
            _context.Update(doctor);
            return Save();
        }
    }
}
