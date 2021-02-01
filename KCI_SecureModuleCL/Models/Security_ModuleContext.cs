using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KCI_SecureModuleCL.Models
{
    public partial class Security_ModuleContext : DbContext
    {
        public Security_ModuleContext()
        {
        }

        public Security_ModuleContext(DbContextOptions<Security_ModuleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SM_USER> SM_USER { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SM_USER>(entity =>
            {
                entity.HasKey(e => e.ID_User)
                    .HasName("PK_USUARIOS");

                entity.ToTable("SM_USER", "Security");

                entity.HasIndex(e => e.ID_User)
                    .HasName("IX_SM_USERS")
                    .IsUnique();

                entity.Property(e => e.TX_Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TX_FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.TX_SecondName)
                    .HasMaxLength(255)
                    .IsUnicode(false);                
                entity.Property(e => e.TX_LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);                
                entity.Property(e => e.TX_SecondLastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);               
                entity.Property(e => e.TX_Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false);


                entity.Property(e => e.TX_Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
