using Management.Model.RMSEntity;
using TwoWayCommunication.Core.Repository;


namespace TwoWayCommunication.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected RMS_2024Context RepositoryContext { get; set; }
        public IRepositoryBase<UserMaster> UserRepository { get; }
        public IRepositoryBase<RoleMaster> RoleRepository { get; }
        public IRepositoryBase<RoleFeature1> RoleMappingepository { get; }
        public IRepositoryBase<ClientMapping> ClientMappingepository { get; }
        public IRepositoryBase<BranchMapping> BranchMappingepository { get; }
        public IRepositoryBase<BranchMaster> BranchMasterRepository { get; }
        public IRepositoryBase<ClientMaster> ClientMasterRepository { get; }
        public IRepositoryBase<CourierMaster> CourierMasterRepository { get; }
        public IRepositoryBase<ProjectMaster> ProjectMasterRepository { get; }
        public IRepositoryBase<PickListMaster> PickListMasterRepository { get; }
        public IRepositoryBase<MenuMaster> MenuMasterRepository { get; } 
        public IRepositoryBase<SubMenuMaster> SubMenuMasterRepository { get; }
        public IRepositoryBase<TemplateMaster> TemplateMasterRepository { get; }
        public IRepositoryBase<TemplateDetail> TemplateDetailRepository { get; }
        public IRepositoryBase<DumpUpload> DumpUploadMasterRepository { get; }
        public IRepositoryBase<RetrievalRequestGen> RetrievalRequestGenRepository { get; } 
        public IRepositoryBase<RefillingRequestGen> RefillingRequestGenRepository { get; } 
        public IRepositoryBase<RetrievalTransaction> RetrievalRetrievalTransactionRepository { get; } 
        public IRepositoryBase<RetrievalTranHistory> RetrievalRetrievalTranHistory { get; } 
        public IRepositoryBase<RefillingTransaction> RefillingTransactionRepository { get; }

        public IRepositoryBase<CheckList1> CheckList1Repository { get; }
        public IRepositoryBase<CheckList2> CheckList2Repository { get; }
        public IRepositoryBase<CheckList3> CheckList3Repository { get; }

        public UnitOfWork(RMS_2024Context repositoryContext,
            IRepositoryBase<ClientMaster> clientMasterRepository,
            IRepositoryBase<BranchMapping> branchMappingepository,
            IRepositoryBase<BranchMaster> branchrepository,
            IRepositoryBase<UserMaster> userRepository,
            IRepositoryBase<RoleMaster> rolerepository,
            IRepositoryBase<CourierMaster> courierMasterRepository,
            IRepositoryBase<ProjectMaster> projectMasterRepository,
            IRepositoryBase<PickListMaster> pickListMasterRepository,
            IRepositoryBase<MenuMaster> menuMasterRepository,
            IRepositoryBase<TemplateMaster> templateMasterRepository,
            IRepositoryBase<RoleFeature1> roleRMappingepository,
            IRepositoryBase<ClientMapping> clientMappingepository,
            IRepositoryBase<DumpUpload> dumpUploadMasterRepository,
            IRepositoryBase<RetrievalRequestGen> retrievalRequestGenRepository,
            IRepositoryBase<RefillingRequestGen> refillingRequestGenRepository,
            IRepositoryBase<RetrievalTransaction> retrievalRetrievalTransactionRepository,
            IRepositoryBase<RetrievalTranHistory> retrievalRetrievalTranHistory,
            IRepositoryBase<RefillingTransaction> refillingTransactionRepository,
            IRepositoryBase<TemplateDetail> templateDetailRepository,
            IRepositoryBase<SubMenuMaster> subMenuMasterRepository,
             IRepositoryBase<CheckList1> checkList1Repository,
              IRepositoryBase<CheckList2> checkList2Repository,
             IRepositoryBase<CheckList3> checkList3Repository
            )
        {
            RepositoryContext = repositoryContext;
            UserRepository = userRepository;
            RoleRepository = rolerepository;
            RoleMappingepository = roleRMappingepository;
            ClientMappingepository = clientMappingepository;
            BranchMappingepository = branchMappingepository;
            BranchMasterRepository = branchrepository;
            ClientMasterRepository = clientMasterRepository;
            CourierMasterRepository = courierMasterRepository;
            ProjectMasterRepository = projectMasterRepository;
            PickListMasterRepository = pickListMasterRepository;
            MenuMasterRepository = menuMasterRepository;
            SubMenuMasterRepository = subMenuMasterRepository;
            TemplateMasterRepository = templateMasterRepository;
            DumpUploadMasterRepository = dumpUploadMasterRepository;
            RetrievalRequestGenRepository = retrievalRequestGenRepository;
            RefillingRequestGenRepository = refillingRequestGenRepository;
            RetrievalRetrievalTransactionRepository = retrievalRetrievalTransactionRepository;
            RetrievalRetrievalTranHistory = retrievalRetrievalTranHistory;
            RefillingTransactionRepository = refillingTransactionRepository;
            TemplateDetailRepository = templateDetailRepository;
            CheckList1Repository = checkList1Repository;
            CheckList2Repository = checkList2Repository;
            CheckList3Repository = checkList3Repository;
        }
        public async Task Commit()
        {
            await RepositoryContext.SaveChangesAsync();
        }

    }

    public interface IUnitOfWork
    {
        IRepositoryBase<UserMaster> UserRepository { get; }
        IRepositoryBase<RoleMaster> RoleRepository { get; }
        IRepositoryBase<RoleFeature1> RoleMappingepository { get; }
        IRepositoryBase<ClientMapping> ClientMappingepository { get; }
        IRepositoryBase<BranchMapping> BranchMappingepository { get; }
        IRepositoryBase<BranchMaster> BranchMasterRepository { get; }
        IRepositoryBase<ClientMaster> ClientMasterRepository { get; }
        IRepositoryBase<CourierMaster> CourierMasterRepository { get; }
        IRepositoryBase<ProjectMaster> ProjectMasterRepository { get; }
        IRepositoryBase<PickListMaster> PickListMasterRepository { get; }
        IRepositoryBase<MenuMaster> MenuMasterRepository { get; }
        IRepositoryBase<SubMenuMaster> SubMenuMasterRepository { get; }
        IRepositoryBase<TemplateMaster> TemplateMasterRepository { get; }
        IRepositoryBase<TemplateDetail> TemplateDetailRepository { get; }
        IRepositoryBase<DumpUpload> DumpUploadMasterRepository { get; }
        IRepositoryBase<RetrievalRequestGen> RetrievalRequestGenRepository { get; }
        IRepositoryBase<RefillingRequestGen> RefillingRequestGenRepository { get; }
        IRepositoryBase<RetrievalTransaction> RetrievalRetrievalTransactionRepository { get; }
        IRepositoryBase<RefillingTransaction> RefillingTransactionRepository { get; }
        IRepositoryBase<RetrievalTranHistory> RetrievalRetrievalTranHistory { get; }
        IRepositoryBase<CheckList1> CheckList1Repository { get; }
        IRepositoryBase<CheckList2> CheckList2Repository { get; }
        IRepositoryBase<CheckList3> CheckList3Repository { get; }
        Task Commit();
    }
}
