using Management.Model.RequestModel;
using Management.Model.ResponseModel;
using Management.Model.RMSEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoWayCommunication.Core.UnitOfWork;

namespace Management.Services.Masters
{
    public class MenuMasterDomain : IMenuMasterDomain
    {

        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        public MenuMasterDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
        }

        public async Task<IEnumerable<GetAllMenuResponseModel>> GetAll()
        {
            var query = _unitOfWork.MenuMasterRepository
                          .AsQueryable()
                             .Select(c => new GetAllMenuResponseModel
                             {
                                 Id = c.Id,
                                 MainMenu = c.MainMenu,
                                 SubMenu = c.SubMenu


                             }).ToList();

            return query;
        }

        public async Task<GetAllMenuResponseModel> GetMenuById(GetMenuByIdRequestModel request)
        {
            var user = _unitOfWork.MenuMasterRepository.AsQueryable()
                .Where(r => r.Id == request.Id)
                .Select(c => new GetAllMenuResponseModel
                {
                    Id = c.Id,
                    MainMenu = c.MainMenu,
                    SubMenu = c.SubMenu

                })
                .FirstOrDefault();

            return user;
        }

        public async Task<HashSet<string>> AddValidation(AddMenuRequestModel request)
        {
            bool isBranchExists = await _unitOfWork.MenuMasterRepository.Any(x => x.MainMenu.ToLower().Trim() == request.MainMenu.ToLower().Trim()  && x.SubMenu.ToLower().Trim() == request.SubMenu.ToLower().Trim());
            if (isBranchExists)
            {
                validationMessage.Add("Main Menu Already Exists");
            }
            return validationMessage;
        }
        public async Task<MenuMaster> Add(AddMenuRequestModel request)
        {
            var menu = new MenuMaster();
            menu.MainMenu = request.MainMenu;
            menu.SubMenu = request.SubMenu;
            menu.IsParent = request.IsParent;
            menu.IsChild = request.IsChild;
            menu.IsVisible = request.IsVisible;
            // menu.CreatedBy = 213123;
            menu.CreatedDate = DateTime.Now;
            var response = await _unitOfWork.MenuMasterRepository.Add(menu);
            await _unitOfWork.Commit();
            return response;
        }
        public async Task<HashSet<string>> UpdateValidation(UpdateMenuRequestBody request)
        {
            bool isClientExists = await _unitOfWork.MenuMasterRepository.Any(x => x.Id != request.Id && x.MainMenu.ToLower().Trim() == request.MainMenu.ToLower().Trim() && x.SubMenu.ToLower().Trim() == request.SubMenu.ToLower().Trim());
            if (!isClientExists) 
                validationMessage.Add("0");
            else
            validationMessage.Add("1");

            return validationMessage;
        }

        public async Task<MenuMaster> Update(UpdateMenuRequestBody request)
        {
            var menuMaster = await _unitOfWork.MenuMasterRepository.GetById(request.Id);
            if (menuMaster == null)
            {
                throw new Exception("Client not found");
            }
            menuMaster.MainMenu = request.MainMenu;
            menuMaster.SubMenu = request.SubMenu;
            //branchMaster.Address = request.Address;
            //  branchMaster.UpdatedAt = DateTime.Now;
            var response = await _unitOfWork.MenuMasterRepository.Update(menuMaster);
            await _unitOfWork.Commit();

            return response;
        }
        public async Task<HashSet<string>> DeleteValidation(GetMenuByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.MenuMasterRepository.Any(x => x.Id == request.Id);
            if (!IsRecord)
                validationMessage.Add("Record not found for deletetion");
            return validationMessage;
        }

        public async Task Delete(GetMenuByIdRequestModel id)
        {
            var clientToDelete = await _unitOfWork.MenuMasterRepository.GetById(id.Id);

            if (clientToDelete != null)
            {
                _unitOfWork.MenuMasterRepository.Delete(clientToDelete);
                await _unitOfWork.Commit();
            }
        }
    }
    public interface IMenuMasterDomain
    {
        Task<IEnumerable<GetAllMenuResponseModel>> GetAll();
        Task<GetAllMenuResponseModel> GetMenuById(GetMenuByIdRequestModel request);
        Task<HashSet<string>> AddValidation(AddMenuRequestModel request);
        Task<MenuMaster> Add(AddMenuRequestModel request);
        Task<HashSet<string>> UpdateValidation(UpdateMenuRequestBody request);
        Task<MenuMaster> Update(UpdateMenuRequestBody request);
        Task<HashSet<string>> DeleteValidation(GetMenuByIdRequestModel request);
        Task Delete(GetMenuByIdRequestModel id);



    }
}
