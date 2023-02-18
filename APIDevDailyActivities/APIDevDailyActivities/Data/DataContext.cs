using APIDevDailyActivities.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDevDailyActivities.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {  }
        public DbSet<DailyActivity> DailyActivities { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Labor> Labors { get; set; }
    }
}
