using System.Collections.Generic;
using System.Reflection.Emit;
using Class_Domain;
using Microsoft.EntityFrameworkCore;
namespace Class_Data
{
    public class PharmacyDbContext : DbContext
    {    
        public DbSet<Pharmacist> Pharmacists { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//PharmacyDB_EF التابع الخاص الذي سوف ينشئ لي قاعدة البيانات صاحبة الاسم 
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=PharmacyDB_EF");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pharmacist>()
                .HasOne(p => p.ContactInfo)
                .WithOne()
                .HasForeignKey<ContactInfo>(c => c.Id);

            modelBuilder.Entity<Prescription>()
                .HasMany(p => p.Medicines)
                .WithMany();

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Pharmacist)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(p => p.PharmacistId);
        }
    }

}
