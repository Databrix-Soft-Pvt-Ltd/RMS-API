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

        public virtual DbSet<BatchMaster> BatchMasters { get; set; } = null!;
        public virtual DbSet<BranchMapping> BranchMappings { get; set; } = null!;
        public virtual DbSet<BranchMaster> BranchMasters { get; set; } = null!;
        public virtual DbSet<CheckList1> CheckList1s { get; set; } = null!;
        public virtual DbSet<CheckList2> CheckList2s { get; set; } = null!;
        public virtual DbSet<CheckList3> CheckList3s { get; set; } = null!;
        public virtual DbSet<ClientMapping> ClientMappings { get; set; } = null!;
        public virtual DbSet<ClientMaster> ClientMasters { get; set; } = null!;
        public virtual DbSet<CourierMaster> CourierMasters { get; set; } = null!;
        public virtual DbSet<DocumentInward> DocumentInwards { get; set; } = null!;
        public virtual DbSet<DumpUpload> DumpUploads { get; set; } = null!;
        public virtual DbSet<Feature> Features { get; set; } = null!;
        public virtual DbSet<FileInfo> FileInfos { get; set; } = null!;
        public virtual DbSet<FileInward> FileInwards { get; set; } = null!;
        public virtual DbSet<MenuMaster> MenuMasters { get; set; } = null!;
        public virtual DbSet<PickListMaster> PickListMasters { get; set; } = null!;
        public virtual DbSet<ProjectMaster> ProjectMasters { get; set; } = null!;
        public virtual DbSet<RefillingRequestGen> RefillingRequestGens { get; set; } = null!;
        public virtual DbSet<RefillingTransaction> RefillingTransactions { get; set; } = null!;
        public virtual DbSet<RetrievalRequestGen> RetrievalRequestGens { get; set; } = null!;
        public virtual DbSet<RetrievalTranHistory> RetrievalTranHistories { get; set; } = null!;
        public virtual DbSet<RetrievalTransaction> RetrievalTransactions { get; set; } = null!;
        public virtual DbSet<RetrievalType> RetrievalTypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleFeature> RoleFeatures { get; set; } = null!;
        public virtual DbSet<RoleFeature1> RoleFeatures1 { get; set; } = null!;
        public virtual DbSet<RoleMaster> RoleMasters { get; set; } = null!;
        public virtual DbSet<StatusMaster> StatusMasters { get; set; } = null!;
        public virtual DbSet<SubMenuMaster> SubMenuMasters { get; set; } = null!;
        public virtual DbSet<TemplateDetail> TemplateDetails { get; set; } = null!;
        public virtual DbSet<TemplateMaster> TemplateMasters { get; set; } = null!;
        public virtual DbSet<UserMaster> UserMasters { get; set; } = null!;
        public virtual DbSet<VQuickSearch> VQuickSearches { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-C2H4MN0;Initial Catalog=RMS_20241;Persist Security Info=True;User ID=sa;Password=Admin@1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BatchMaster>(entity =>
            {
                entity.ToTable("Batch_Master");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BatchCloseBy).HasColumnName("batch_close_by");

                entity.Property(e => e.BatchCloseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("batch_close_date");

                entity.Property(e => e.BatchId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("batch_id");

                entity.Property(e => e.ConsignmentNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("consignment_number");

                entity.Property(e => e.CourierAckBy).HasColumnName("courier_ack_by");

                entity.Property(e => e.CourierAckDate)
                    .HasColumnType("datetime")
                    .HasColumnName("courier_ack_date");

                entity.Property(e => e.CourierId).HasColumnName("courier_id");

                entity.Property(e => e.CourierRemark)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("courier_remark");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.FilePath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("file_path");

                entity.Property(e => e.OldBatchId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("old_batch_id");

                entity.Property(e => e.RequestStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("request_status");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");
            });

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

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CheckList1>(entity =>
            {
                entity.ToTable("CheckList1");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CheckListName1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CheckList2>(entity =>
            {
                entity.ToTable("CheckList2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CheckListId1).HasColumnName("CheckListID1");

                entity.Property(e => e.CheckListName2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CheckList3>(entity =>
            {
                entity.ToTable("CheckList3");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CheckListId2).HasColumnName("CheckListID2");

                entity.Property(e => e.CheckListName3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
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

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CourierMaster>(entity =>
            {
                entity.ToTable("CourierMaster");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourierName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<DocumentInward>(entity =>
            {
                entity.ToTable("document_inward");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CheckListId1).HasColumnName("checkList_id1");

                entity.Property(e => e.CheckListId2).HasColumnName("checkList_id2");

                entity.Property(e => e.CheckListId3).HasColumnName("checkList_id3");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_date");

                entity.Property(e => e.DocumentStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("document_status");

                entity.Property(e => e.FileInwardId).HasColumnName("file_inward_id");

                entity.Property(e => e.ScrutinyBy).HasColumnName("scrutiny_by");

                entity.Property(e => e.ScrutinyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("scrutiny_date");
            });

            modelBuilder.Entity<DumpUpload>(entity =>
            {
                entity.ToTable("DumpUpload");

                entity.Property(e => e.ItemStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Item_Status")
                    .IsFixedLength();

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

            modelBuilder.Entity<FileInward>(entity =>
            {
                entity.ToTable("FileInward");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BatchId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("batch_id");

                entity.Property(e => e.BranchInwardBy).HasColumnName("branch_inward_by");

                entity.Property(e => e.BranchInwardDate)
                    .HasColumnType("datetime")
                    .HasColumnName("branch_inward_date");

                entity.Property(e => e.CartonNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("carton_no");

                entity.Property(e => e.DocumentType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("document_type");

                entity.Property(e => e.FileAckBy).HasColumnName("file_ack_by");

                entity.Property(e => e.FileAckDate)
                    .HasColumnType("datetime")
                    .HasColumnName("file_ack_date");

                entity.Property(e => e.FileNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("file_no");

                entity.Property(e => e.FileStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("file_status");

                entity.Property(e => e.Ref1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<MenuMaster>(entity =>
            {
                entity.ToTable("MenuMaster");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.MainMenu)
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

            modelBuilder.Entity<RefillingRequestGen>(entity =>
            {
                entity.ToTable("Refilling_Request_Gen");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsClosedDate).HasColumnType("datetime");

                entity.Property(e => e.IsCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.RefNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("REF_Number");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RefillingTransaction>(entity =>
            {
                entity.ToTable("Refilling_Transaction");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConsignmentsNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Consignments_Number");

                entity.Property(e => e.CourierId).HasColumnName("Courier_ID");

                entity.Property(e => e.DispatchAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Dispatch_Address");

                entity.Property(e => e.DispatchDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Dispatch_Date");

                entity.Property(e => e.FileStatus).HasColumnName("File_Status");

                entity.Property(e => e.ItemSatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Item_Satus");

                entity.Property(e => e.PickupDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Pickup_Date");

                entity.Property(e => e.Ref1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RefNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("REF_Number");

                entity.Property(e => e.RefillingAckBy).HasColumnName("Refilling_Ack_By");

                entity.Property(e => e.RefillingAckDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Refilling_Ack_Date");

                entity.Property(e => e.RefillingBy).HasColumnName("Refilling_By");

                entity.Property(e => e.RefillingClosedBy).HasColumnName("Refilling_Closed_By");

                entity.Property(e => e.RefillingClosedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Refilling_Closed_Date");

                entity.Property(e => e.RefillingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Refilling_Date");

                entity.Property(e => e.RefillingReceivedBy).HasColumnName("Refilling_Received_By");

                entity.Property(e => e.RefillingReceivedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Refilling_Received_Date");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.RetrievalId).HasColumnName("Retrieval_ID");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RetrievalRequestGen>(entity =>
            {
                entity.ToTable("Retrieval_Request_Gen");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsClosedDate).HasColumnType("datetime");

                entity.Property(e => e.IsCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ReqNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("REQ_Number");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RetrievalTranHistory>(entity =>
            {
                entity.ToTable("Retrieval_Tran_History");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("File_Status");

                entity.Property(e => e.ProcessBy).HasColumnName("Process_By");

                entity.Property(e => e.ProcessDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Process_Date");

                entity.Property(e => e.Ref1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ReqNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Req_Number");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RetrievalTransaction>(entity =>
            {
                entity.ToTable("Retrieval_Transactions");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovalBy).HasColumnName("Approval_By");

                entity.Property(e => e.ApprovalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Approval_Date");

                entity.Property(e => e.ClosedBy).HasColumnName("Closed_By");

                entity.Property(e => e.ClosedDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Closed_DateTime");

                entity.Property(e => e.ConsignmentsNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Consignments_Number");

                entity.Property(e => e.CourierAckBy).HasColumnName("Courier_Ack_By");

                entity.Property(e => e.CourierAckDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Courier_Ack_date");

                entity.Property(e => e.CourierId).HasColumnName("Courier_ID");

                entity.Property(e => e.DispatchBy).HasColumnName("Dispatch_By");

                entity.Property(e => e.DispatchDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Dispatch_Date");

                entity.Property(e => e.FileStatus).HasColumnName("File_Status");

                entity.Property(e => e.ItemStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Item_Status");

                entity.Property(e => e.Ref1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RejectBy).HasColumnName("Reject_By");

                entity.Property(e => e.RejectDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Reject_Date");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ReqNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Req_Number");

                entity.Property(e => e.RequestBy).HasColumnName("Request_By");

                entity.Property(e => e.RequestDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Request_Date");

                entity.Property(e => e.RetrievalRegion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Retrieval_region");

                entity.Property(e => e.RetrievalType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Retrieval_Type");

                entity.Property(e => e.Status)
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

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<StatusMaster>(entity =>
            {
                entity.ToTable("Status_Master");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubMenuMaster>(entity =>
            {
                entity.HasKey(e => e.SubMid);

                entity.ToTable("SubMenu_Master");

                entity.Property(e => e.SubMid).HasColumnName("SubMID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.MainMenuId).HasColumnName("MainMenu_ID");

                entity.Property(e => e.SubMenu)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TemplateDetail>(entity =>
            {
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

                entity.Property(e => e.TempId).HasColumnName("TempID");

                entity.Property(e => e.Validation)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TemplateMaster>(entity =>
            {
                entity.HasKey(e => e.TempId);

                entity.ToTable("Template_Master");

                entity.Property(e => e.TempId).HasColumnName("TempID");

                entity.Property(e => e.IsCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TempDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TempName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("UserMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
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

                entity.Property(e => e.Password).HasMaxLength(500);

                entity.Property(e => e.PasswordExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VQuickSearch>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Quick_Search");

                entity.Property(e => e.ConsignmentsNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Consignments_Number");

                entity.Property(e => e.CourierName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FileStatus).HasColumnName("File_Status");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ItemStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Item_Status")
                    .IsFixedLength();

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

                entity.Property(e => e.ReqNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Req_Number");

                entity.Property(e => e.RetrievalRegion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Retrieval_region");

                entity.Property(e => e.RetrievalType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Retrieval_Type");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UploadBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UploadDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
