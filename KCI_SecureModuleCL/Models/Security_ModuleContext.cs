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

        public virtual DbSet<SM_ELEMENT> SM_ELEMENT { get; set; }
        public virtual DbSet<SM_ELEMENT_TYPE> SM_ELEMENT_TYPE { get; set; }
        public virtual DbSet<SM_ROLE> SM_ROLE { get; set; }
        public virtual DbSet<SM_ROLE_ELEMENT> SM_ROLE_ELEMENT { get; set; }
        public virtual DbSet<SM_ROLE_USER> SM_ROLE_USER { get; set; }
        public virtual DbSet<SM_USER> SM_USER { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SM_ELEMENT>(entity =>
            {
                entity.HasKey(e => e.ID_Element)
                    .HasName("PK_ELEMENTS");

                entity.ToTable("SM_ELEMENT", "Security");

                entity.Property(e => e.TX_Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TX_Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TX_Url)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.ID_TypeNavigation)
                    .WithMany(p => p.SM_ELEMENT)
                    .HasForeignKey(d => d.ID_Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_ELEMENT_SM_ELEMENT_TYPE");
            });

            modelBuilder.Entity<SM_ELEMENT_TYPE>(entity =>
            {
                entity.HasKey(e => e.ID_Type)
                    .HasName("PK_SM_ELEMENTS_TYPES");

                entity.ToTable("SM_ELEMENT_TYPE", "Security");

                entity.Property(e => e.TX_Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SM_ROLE>(entity =>
            {
                entity.HasKey(e => e.ID_Role)
                    .HasName("PK_SM_ROLES");

                entity.ToTable("SM_ROLE", "Security");

                entity.Property(e => e.TX_Description).IsUnicode(false);

                entity.Property(e => e.BO_VisibleCliente).IsUnicode(false);

                entity.Property(e => e.TX_Role)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.ID_ElementNavigation)
                    .WithMany(p => p.SM_ROLE)
                    .HasForeignKey(d => d.ID_Element)
                    .HasConstraintName("FK_SM_ROLE_SM_ELEMENT");
            });

            modelBuilder.Entity<SM_ROLE_ELEMENT>(entity =>
            {
                entity.HasKey(e => e.ID_RoleElement)
                    .HasName("PK_LINK_ROLES_ELEMENTS");

                entity.ToTable("SM_ROLE_ELEMENT", "Security");

                entity.HasOne(d => d.ID_ElementNavigation)
                    .WithMany(p => p.SM_ROLE_ELEMENT)
                    .HasForeignKey(d => d.ID_Element)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_ROLE_ELEMENT_SM_ELEMENT");

                entity.HasOne(d => d.ID_RoleNavigation)
                    .WithMany(p => p.SM_ROLE_ELEMENT)
                    .HasForeignKey(d => d.ID_Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_ROLE_ELEMENT_SM_ROLE");
            });

            modelBuilder.Entity<SM_ROLE_USER>(entity =>
            {
                entity.HasKey(e => e.ID_UserRoleApplication)
                    .HasName("PK_SM_ROLES_USER_ELEMENTS");

                entity.ToTable("SM_ROLE_USER", "Security");

                entity.HasOne(d => d.ID_RoleNavigation)
                    .WithMany(p => p.SM_ROLE_USER)
                    .HasForeignKey(d => d.ID_Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_ROLE_USER_SM_ROLE");

                entity.HasOne(d => d.ID_UserNavigation)
                    .WithMany(p => p.SM_ROLE_USER)
                    .HasForeignKey(d => d.ID_User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SM_ROLE_USER_SM_USER");
            });

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

                entity.Property(e => e.TX_Link)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TX_Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DT_ValidDatePasswordRecoveryLink)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
