using healthguard.Models;

namespace healthguard.Dto
{
    public class MeasurementDto
    {
        public int MeasurementId { get; set; }
        public float MeasurementValue { get; set; }
        public DateTime MeasurementDate { get; set; }
    }
}
