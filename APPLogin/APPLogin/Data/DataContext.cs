using APPLogin.Models;
using Microsoft.EntityFrameworkCore;

namespace APPLogin.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
