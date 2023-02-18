using System.ComponentModel.DataAnnotations;

namespace APIDevDailyActivities.Models
{
    public class DailyActivity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ActivityId { get; set; }
        public int LaborId { get; set; }
        public DateTime WorkDay { get; set; }
        public int DurationLabor { get; set; }
        [StringLength(200)]
        public string? Comments { get; set; }

    }
}
