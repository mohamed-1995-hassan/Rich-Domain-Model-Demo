using DomainLogic.Courses.Entities;
using DomainLogic.Courses.Enums;
using DomainLogic.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLogic.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("FirstConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .OwnsOne(u=>u.Email)
                .Property(u => u.Value)
                .HasColumnName("Email");

            modelBuilder.Entity<User>()
                .OwnsOne(u=>u.Name)
                .Property(u => u.Value)
                .HasColumnName("Name");

            modelBuilder.Entity<User>()
                .OwnsOne(u => u.MoneySpent)
                .Property(u => u.Value)
                .HasColumnName("MoneySpent");

            modelBuilder.Entity<User>()
                .OwnsOne(u => u.Status, statusNavigationBuilder =>
                {
                    statusNavigationBuilder
                    .Property(p=>p.Type)
                    .HasColumnName("Type");

                    statusNavigationBuilder
                    .OwnsOne(u=>u.ExpirationDate)
                    .Property(p => p.Date)
                    .HasColumnName("ExpirationDate");
                });

            modelBuilder.Entity<UserCourseEnrolment>()
                .OwnsOne(u => u.Price)
                .Property(u => u.Value)
                .HasColumnName("Price");

            modelBuilder.Entity<UserCourseEnrolment>()
                .OwnsOne(u => u.StatusExpirationDate)
                .Property(u => u.Date)
                .HasColumnName("StatusExpirationDate");

            modelBuilder.Entity<Course>()
                .HasDiscriminator<int>(x => x.LicensingModel)
                .HasValue<ShortTimeCourse>((int)LicensingModel.ShortTime)
                .HasValue<LongTimeCourse>((int)LicensingModel.LongTime);

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
