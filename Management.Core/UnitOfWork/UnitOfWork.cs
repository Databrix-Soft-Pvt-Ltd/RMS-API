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
        public IRepositoryBase<TemplateMaster> TemplateMasterRepository { get; }

        public IRepositoryBase<DumpUpload> DumpUploadMasterRepository { get; }

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
            IRepositoryBase<DumpUpload> dumpUploadMasterRepository
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
            TemplateMasterRepository = templateMasterRepository;
            DumpUploadMasterRepository = dumpUploadMasterRepository;


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
        IRepositoryBase<TemplateMaster> TemplateMasterRepository { get; }
        IRepositoryBase<DumpUpload> DumpUploadMasterRepository { get; }
        Task Commit();
    }
}
