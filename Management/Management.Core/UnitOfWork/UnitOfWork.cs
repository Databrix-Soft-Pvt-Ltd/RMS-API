using Management.Model.RMSEntity;
using TwoWayCommunication.Core.Repository;


namespace TwoWayCommunication.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected RMS_2024Context RepositoryContext { get; set; }  
        public IRepositoryBase<UserMaster> UserRepository { get; }  
        public IRepositoryBase<RoleMaster> RoleRepository { get; } 
        public IRepositoryBase<BranchMaster> BranchMasterRepository { get; } 
        public IRepositoryBase<ClientMaster> ClientMasterRepository { get; }

        public IRepositoryBase<CourierMaster> CourierMasterRepository { get; }

        public UnitOfWork(RMS_2024Context repositoryContext, 
            IRepositoryBase<ClientMaster> clientMasterRepository, 
            IRepositoryBase<BranchMaster> branchrepository, 
            IRepositoryBase<UserMaster> userRepository, 
            IRepositoryBase<RoleMaster> rolerepository,
            IRepositoryBase<CourierMaster> courierMasterRepository
            ) //IRepositoryBase<T2wcUserRoleMapping> userRoleRepository)
        {

            RepositoryContext = repositoryContext;
            UserRepository = userRepository;
            RoleRepository = rolerepository;
            BranchMasterRepository = branchrepository;
            ClientMasterRepository = clientMasterRepository;
            CourierMasterRepository = courierMasterRepository;
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
        IRepositoryBase<BranchMaster> BranchMasterRepository { get; } 
        IRepositoryBase<ClientMaster> ClientMasterRepository { get; }
        IRepositoryBase<CourierMaster> CourierMasterRepository { get; }
        Task Commit();
    }
}
