using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Data
{
    public class PhoneBookDbContext :DbContext 
    {
        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext>options
            ) : base(options) { }
        public DbSet<PhoneBookEntry> Entries { get; set; }
        public DbSet<PhoneBookUser> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entry = modelBuilder.Entity<PhoneBookEntry>();
            entry.ToTable("TM_PhoneBookEntry", "dbo");
            entry.HasKey(x => x.Id);
            entry.Property(x => x.Id).HasColumnName("PBE_Id");
            entry.Property(x => x.FirstName).HasColumnName("PBE_FirstName");
            entry.Property(x => x.LastName).HasColumnName("PBE_LastName");
            entry.Property(x => x.Unit).HasColumnName("PBE_Unit");
            entry.Property(x => x.InternalPhone).HasColumnName("PBE_InternalPhone");
            entry.Property(x => x.PhoneNumber).HasColumnName("PBE_PhoneNumber");
            entry.Property(x => x.MobileNumber).HasColumnName("PBE_MobileNumber");
            entry.Property(x => x.IsDeleted).HasColumnName("PBE_Is_deleted");
            entry.Property(x => x.Version).HasColumnName("PBE_Version");
            entry.Property(x => x.CreatedAt).HasColumnName("PBE_CreatedAt");
            entry.Property(x => x.CreatedByUserId).HasColumnName("PBE_CreatedByUserId_Fk");

            var user = modelBuilder.Entity<PhoneBookUser>();
            user.ToTable("AspNetUsers", "dbo");
            user.HasKey(x => x.Id);

            user.Property(x => x.Id).HasColumnName("Id");
            user.Property(X => X.FirstName).HasColumnName("FirstName");
            user.Property(x => x.LastName).HasColumnName("LastName");
            
        }
    }
}
