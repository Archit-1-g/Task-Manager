using Microsoft.EntityFrameworkCore;
using OritsoTaskManager.Models;
using System.Collections.Generic;

namespace OritsoTaskManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
