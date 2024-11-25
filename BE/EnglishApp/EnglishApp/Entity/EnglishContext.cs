using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;

namespace EnglishApp.Entity
{
    public class EnglishContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

                var connectionString = configuration.GetConnectionString("EnglishApp");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LessonEntity>().HasData(new LessonEntity { Id = 1, Name = "Sáng: 8h-10h từ T2-T7", Time = "8h-10h", From = 2, To = 7 });
            modelBuilder.Entity<LessonEntity>().HasData(new LessonEntity { Id = 2, Name = "Sáng: 10h-12h từ T2-T7", Time = "10h-12h", From = 2, To = 7});
            modelBuilder.Entity<LessonEntity>().HasData(new LessonEntity { Id = 3, Name = "Trưa: 13h-15h từ T2-T7", Time = "13h-15h", From = 2, To = 7});
            modelBuilder.Entity<LessonEntity>().HasData(new LessonEntity { Id = 4, Name = "Trưa: 15h-17h từ T2-T7", Time = "15h-17h", From = 2, To = 7});
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<ClassEntity> Classes { get; set; }
        public DbSet<DocumentEntity> Documents { get; set; }
        public DbSet<LessonEntity> Lessons { get; set; }
        public DbSet<RevenueEntity> Revenues { get; set; }
    }
}
