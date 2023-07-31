using Microsoft.EntityFrameworkCore;
using Notification_Api.Models;

namespace Notification_Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Notification> Notifications { get; set; }
    }
}
