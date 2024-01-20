using healthguard.Models;

namespace healthguard.Interfaces
{
    public interface IMedicalDeviceRepository
    {
        ICollection<MedicalDevice> GetMedicalDevices();
        MedicalDevice GetMedicalDevice(int mdeviceId);
        bool MedicalDeviceExists(int mdeviceId);
        bool CreateMedicalDevice(MedicalDevice medicalDevice);
        bool UpdateMedicalDevice(MedicalDevice medicalDevice);
        bool DeleteMedicalDevice(MedicalDevice medicalDevice);
        bool Save();
    }
}
