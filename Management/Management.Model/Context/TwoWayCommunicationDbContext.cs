using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TwoWayCommunication.Model.DBModels
{
    public partial class TwoWayCommunicationDbContext : DbContext
    {
        public TwoWayCommunicationDbContext()
        {
        }

        public TwoWayCommunicationDbContext(DbContextOptions<TwoWayCommunicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BranchMaster> BranchMasters { get; set; } = null!;
        public virtual DbSet<T2wcRole> T2wcRoles { get; set; } = null!;
        public virtual DbSet<T2wcUser> T2wcUsers { get; set; } = null!;
        public virtual DbSet<T2wcUserRoleMapping> T2wcUserRoleMappings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-TPNR0GU\\SQLEXPRESS2022;Initial Catalog=Sample_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=true;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T2wcRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_Roles");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<T2wcUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_Users");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<T2wcUserRoleMapping>(entity =>
            {
                entity.HasKey(e => e.UserRoleMappingId)
                    .HasName("PK_2WC_UserRoleMappings");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.T2wcUserRoleMappings)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_2WC_UserRoleMappings_Roles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.T2wcUserRoleMappings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_2WC_UserRoleMappings_2WC_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
