using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Management.Model.RMSEntity
{
    public partial class RMS_2024Context : DbContext
    {
        public RMS_2024Context()
        {
        }

        public RMS_2024Context(DbContextOptions<RMS_2024Context> options)
            : base(options)
        {
        }

        public virtual DbSet<BranchMapping> BranchMappings { get; set; } = null!;
        public virtual DbSet<BranchMaster> BranchMasters { get; set; } = null!;
        public virtual DbSet<ClientMapping> ClientMappings { get; set; } = null!;
        public virtual DbSet<ClientMaster> ClientMasters { get; set; } = null!;
        public virtual DbSet<CourierMaster> CourierMasters { get; set; } = null!;
        public virtual DbSet<DumpUpload> DumpUploads { get; set; } = null!;
        public virtual DbSet<Feature> Features { get; set; } = null!;
        public virtual DbSet<FileInfo> FileInfos { get; set; } = null!;
        public virtual DbSet<MenuMaster> MenuMasters { get; set; } = null!;
        public virtual DbSet<PickListMaster> PickListMasters { get; set; } = null!;
        public virtual DbSet<ProjectMaster> ProjectMasters { get; set; } = null!;
        public virtual DbSet<RetrievalType> RetrievalTypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleFeature> RoleFeatures { get; set; } = null!;
        public virtual DbSet<RoleFeature1> RoleFeatures1 { get; set; } = null!;
        public virtual DbSet<RoleMaster> RoleMasters { get; set; } = null!;
        public virtual DbSet<TemplateMaster> TemplateMasters { get; set; } = null!;
        public virtual DbSet<UserMaster> UserMasters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                 optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Initial Catalog=RMS_2024;Persist Security Info=True;User ID=sa;Password=balaji@123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BranchMapping>(entity =>
            {
                entity.ToTable("BranchMapping");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<BranchMaster>(entity =>
            {
                entity.ToTable("BranchMaster");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");
            });

            modelBuilder.Entity<ClientMapping>(entity =>
            {
                entity.ToTable("ClientMapping");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ClientMaster>(entity =>
            {
                entity.ToTable("ClientMaster");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");
            });

            modelBuilder.Entity<CourierMaster>(entity =>
            {
                entity.ToTable("CourierMaster");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourierName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasColumnName("isActive");
            });

            modelBuilder.Entity<DumpUpload>(entity =>
            {
                entity.ToTable("DumpUpload");

                entity.Property(e => e.Ref1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref10)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref11)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref12)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref13)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref14)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref15)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref16)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref17)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref18)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref19)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref20)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref21)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref22)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref23)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref24)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref25)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref26)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref27)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref28)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref29)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref30)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref4)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref5)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref6)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref7)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref8)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ref9)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UploadDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.HasKey(e => e.FeatureIdPk);

                entity.ToTable("feature");

                entity.Property(e => e.FeatureIdPk).HasColumnName("feature_id_pk");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.FeatureName)
                    .HasMaxLength(50)
                    .HasColumnName("feature_name");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<FileInfo>(entity =>
            {
                entity.ToTable("FileInfo");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.DispatchAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DispatchDate).HasColumnType("datetime");

                entity.Property(e => e.FileStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NewPodNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PodAckDate).HasColumnType("datetime");

                entity.Property(e => e.PodNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RefillingAckDate).HasColumnType("datetime");

                entity.Property(e => e.RefillingCartonNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RefillingDate).HasColumnType("datetime");

                entity.Property(e => e.RefillingRequestNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RejectDate).HasColumnType("datetime");

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UniqueId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MenuMaster>(entity =>
            {
                entity.ToTable("MenuMaster");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsChild).HasColumnName("isChild");

                entity.Property(e => e.IsParent).HasColumnName("isParent");

                entity.Property(e => e.IsVisible).HasColumnName("isVisible");

                entity.Property(e => e.MainMenu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubMenu)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PickListMaster>(entity =>
            {
                entity.ToTable("PickListMaster");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LogoPath)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Logo_Path");

                entity.Property(e => e.TemplateId).HasColumnName("Template_Id");
            });

            modelBuilder.Entity<ProjectMaster>(entity =>
            {
                entity.ToTable("ProjectMaster");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HeaderPageLogo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("HeaderPage_Logo");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.LoginPageLogo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("LoginPage_Logo");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RetrievalType>(entity =>
            {
                entity.ToTable("RetrievalType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.RetrievalType1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RetrievalType");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleIdPk);

                entity.ToTable("role");

                entity.Property(e => e.RoleIdPk).HasColumnName("role_id_pk");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });

            modelBuilder.Entity<RoleFeature>(entity =>
            {
                entity.HasKey(e => new { e.RoleIdFk, e.FeatureIdFk });

                entity.ToTable("role_feature");

                entity.Property(e => e.RoleIdFk).HasColumnName("role_id_fk");

                entity.Property(e => e.FeatureIdFk).HasColumnName("feature_id_fk");

                entity.Property(e => e.AddPerm).HasColumnName("add_perm");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.DeletePerm).HasColumnName("delete_perm");

                entity.Property(e => e.EditPerm).HasColumnName("edit_perm");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.ViewPerm).HasColumnName("view_perm");

                entity.HasOne(d => d.FeatureIdFkNavigation)
                    .WithMany(p => p.RoleFeatures)
                    .HasForeignKey(d => d.FeatureIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.RoleIdFkNavigation)
                    .WithMany(p => p.RoleFeatures)
                    .HasForeignKey(d => d.RoleIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<RoleFeature1>(entity =>
            {
                entity.ToTable("RoleFeatures");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.ToTable("RoleMaster");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.ModifyBy).HasColumnName("modifyBy");

                entity.Property(e => e.ModifyDate).HasColumnName("modifyDate");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TemplateMaster>(entity =>
            {
                entity.ToTable("TemplateMaster");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ControlType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DataType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DatabaseName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateFormat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsMandatory).HasColumnName("isMandatory");

                entity.Property(e => e.IsUnique).HasColumnName("isUnique");

                entity.Property(e => e.MasterValue)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Validation)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("UserMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.Ipaddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IPAddress");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
