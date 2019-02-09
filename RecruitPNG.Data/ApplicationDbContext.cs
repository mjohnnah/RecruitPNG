using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RecruitPNG.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RecruitPNG.Models.ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<RecruitPNG.Models.City> Cities { get; set; }
        public DbSet<RecruitPNG.Models.Company> Companies { get; set; }
        public DbSet<RecruitPNG.Models.Country> Countries { get; set; }
        public DbSet<RecruitPNG.Models.County> Counties { get; set; }
        public DbSet<RecruitPNG.Models.Job> Jobs { get; set; }
        public DbSet<RecruitPNG.Models.JobApplication> JobApplications { get; set; }
        public DbSet<RecruitPNG.Models.Message> Messages { get; set; }
        public DbSet<RecruitPNG.Models.Occupation> Occupations { get; set; }
        public DbSet<RecruitPNG.Models.Resume> Resumes { get; set; }
        public DbSet<RecruitPNG.Models.ResumeFile> ResumeFiles { get; set; }
        public DbSet<RecruitPNG.Models.Sector> Sectors { get; set; }
        public DbSet<RecruitPNG.Models.Subscription> Subscription { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RecruitPNG.Models.Company>().HasOne(c => c.Country).WithMany(co => co.Companies).HasForeignKey(f => f.CountryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RecruitPNG.Models.Company>().HasOne(c => c.City).WithMany(co => co.Companies).HasForeignKey(f => f.CityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RecruitPNG.Models.Company>().HasOne(c => c.County).WithMany(co => co.Companies).HasForeignKey(f => f.CountyId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RecruitPNG.Models.Resume>().HasOne(c => c.Country).WithMany(co => co.Resumes).HasForeignKey(f => f.CountryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RecruitPNG.Models.Resume>().HasOne(c => c.City).WithMany(co => co.Resumes).HasForeignKey(f => f.CityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RecruitPNG.Models.Resume>().HasOne(c => c.County).WithMany(co => co.Resumes).HasForeignKey(f => f.CountyId).OnDelete(DeleteBehavior.Restrict);
            //mj added
            modelBuilder.Entity<RecruitPNG.Models.City>().HasOne(c => c.Country).WithMany(co => co.Cities).HasForeignKey(f => f.CountryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RecruitPNG.Models.County>().HasOne(c => c.City).WithMany(co => co.Counties).HasForeignKey(f => f.CityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RecruitPNG.Models.Job>().HasOne(c => c.Country).WithMany(co => co.Jobs).HasForeignKey(f => f.CountryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RecruitPNG.Models.Job>().HasOne(c => c.City).WithMany(co => co.Jobs).HasForeignKey(f => f.CityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RecruitPNG.Models.Job>().HasOne(c => c.County).WithMany(co => co.Jobs).HasForeignKey(f => f.CountyId).OnDelete(DeleteBehavior.Restrict);
            //create roles and users
            var adminRole = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", ConcurrencyStamp = "Admin", NormalizedName = "ADMIN" };
            var companyRole = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Company", ConcurrencyStamp = "Company", NormalizedName = "COMPANY" };
            var candidateRole = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Candidate", ConcurrencyStamp = "Candidate", NormalizedName = "CANDIDATE" };
            var adminUser = new RecruitPNG.Models.ApplicationUser { Id = Guid.NewGuid().ToString(), Email = "admin@bilisimegitim.com", NormalizedEmail = "ADMIN@BILISIMEGITIM.COM", UserName = "admin@bilisimegitim.com", NormalizedUserName = "ADMIN@BILISIMEGITIM.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEDvyQ5fzBhHIGG+XvlWnHOPQFj8vzwsWagAXr+MVJnsKsfr5OKE4nInSitz6gyg3cA==", SecurityStamp = "X4J75DO73ZV6MVW7DRVYPDOIUHHZQ7PM", ConcurrencyStamp = "1", AccessFailedCount = 0, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true };
            var companyUser = new RecruitPNG.Models.ApplicationUser { Id = Guid.NewGuid().ToString(), Email = "firma@bilisimegitim.com", NormalizedEmail = "FIRMA@BILISIMEGITIM.COM", UserName = "firma@bilisimegitim.com", NormalizedUserName = "FIRMA@BILISIMEGITIM.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEDvyQ5fzBhHIGG+XvlWnHOPQFj8vzwsWagAXr+MVJnsKsfr5OKE4nInSitz6gyg3cA==", SecurityStamp = "X4J75DO73ZV6MVW7DRVYPDOIUHHZQ7PM", ConcurrencyStamp = "1", AccessFailedCount = 0, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true };
            var candidateUser = new RecruitPNG.Models.ApplicationUser { Id = Guid.NewGuid().ToString(), Email = "aday@bilisimegitim.com", NormalizedEmail = "ADAY@BILISIMEGITIM.COM", UserName = "aday@bilisimegitim.com", NormalizedUserName = "ADAY@BILISIMEGITIM.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEDvyQ5fzBhHIGG+XvlWnHOPQFj8vzwsWagAXr+MVJnsKsfr5OKE4nInSitz6gyg3cA==", SecurityStamp = "X4J75DO73ZV6MVW7DRVYPDOIUHHZQ7PM", ConcurrencyStamp = "1", AccessFailedCount = 0, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true };

            // add roles
            modelBuilder.Entity<IdentityRole>().HasData(adminRole);
            modelBuilder.Entity<IdentityRole>().HasData(companyRole);
            modelBuilder.Entity<IdentityRole>().HasData(candidateRole);

            // add users
            modelBuilder.Entity<RecruitPNG.Models.ApplicationUser>().HasData(adminUser);
            modelBuilder.Entity<RecruitPNG.Models.ApplicationUser>().HasData(companyUser);
            modelBuilder.Entity<RecruitPNG.Models.ApplicationUser>().HasData(candidateUser);

            // add user roles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = adminUser.Id, RoleId = adminRole.Id });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = companyUser.Id, RoleId = companyRole.Id });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = candidateUser.Id, RoleId = candidateRole.Id });

            // add Eğitim sector
            modelBuilder.Entity<RecruitPNG.Models.Sector>().HasData(new RecruitPNG.Models.Sector { Id = "1", Name = "Shipping", CreateDate = DateTime.Now, CreatedBy = adminUser.UserName, UpdateDate = DateTime.Now, UpdatedBy = adminUser.UserName, IPAddress = "127.0.0.1" });

            // create sample country, city and county
            var turkeyCountry = new RecruitPNG.Models.Country { Id = Guid.NewGuid().ToString(), Name = "PNG", CreateDate = DateTime.Now, CreatedBy = adminUser.UserName, UpdateDate = DateTime.Now, UpdatedBy = adminUser.UserName, IPAddress = "127.0.0.1" };
            var istanbulCity = new RecruitPNG.Models.City { Id = Guid.NewGuid().ToString(), Name = "Morobe", CountryId = turkeyCountry.Id, CreateDate = DateTime.Now, CreatedBy = adminUser.UserName, UpdateDate = DateTime.Now, UpdatedBy = adminUser.UserName, IPAddress = "127.0.0.1" };
            var kadikoyCounty = new RecruitPNG.Models.County { Id = Guid.NewGuid().ToString(), Name = "Lae City", CityId = istanbulCity.Id, CountryId = turkeyCountry.Id, CreateDate = DateTime.Now, CreatedBy = adminUser.UserName, UpdateDate = DateTime.Now, UpdatedBy = adminUser.UserName, IPAddress = "127.0.0.1" };

            // add sample country, city and county
            modelBuilder.Entity<RecruitPNG.Models.Country>().HasData(turkeyCountry);
            modelBuilder.Entity<RecruitPNG.Models.City>().HasData(istanbulCity);
            modelBuilder.Entity<RecruitPNG.Models.County>().HasData(kadikoyCounty);

            var yazilimOccupucation = new RecruitPNG.Models.Occupation { Id = Guid.NewGuid().ToString(), Name = "Ship Captain", CreateDate = DateTime.Now, CreatedBy = adminUser.UserName, UpdateDate = DateTime.Now, UpdatedBy = adminUser.UserName, IPAddress = "127.0.0.1" };

            modelBuilder.Entity<RecruitPNG.Models.Occupation>().HasData(yazilimOccupucation);
        }
    }
}
