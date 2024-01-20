using healthguard.Models;

namespace healthguard.Interfaces
{
    public interface INotificationRepository
    {
        ICollection<Notification> GetNotifications();
        Notification GetNotification(int notifId);
        ICollection<Notification> GetNotificationsByPatient(int patientId);
        bool NotificationExists(int notifId);
        bool CreateNotification(Notification notification);
        bool UpdateNotification(Notification notification);
        bool DeleteNotification(Notification notification);
        bool Save();
    }
}
