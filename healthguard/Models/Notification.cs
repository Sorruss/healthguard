namespace healthguard.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
