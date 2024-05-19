using Management.Model.RequestModel;
using Management.Model.ResponseModel;
using Management.Model.RMSEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoWayCommunication.Core.UnitOfWork;
using TwoWayCommunication.Model.Enums;

namespace Management.Services.Masters
{
    public class MenuMasterDomain : IMenuMasterDomain
    {

        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        private readonly GlobalUserID _gluID;
        public MenuMasterDomain(IUnitOfWork unitOfWork, GlobalUserID globalUserID)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
            _gluID = globalUserID;
        }

        public async Task<List<GetAllMenuResponseModel>> GetAll()
        {

            List<GetAllMenuResponseModel> resultList = new List<GetAllMenuResponseModel>();

            using (var rMS_2024Context = new RMS_2024Context())
            {
                resultList =  (from ms in rMS_2024Context.MenuMasters
                                    join sms in rMS_2024Context.SubMenuMasters on ms.Id equals  (int)sms.MainMenuId into joined
                                    from subMenu in joined.DefaultIfEmpty()
                                    select new GetAllMenuResponseModel
                                    {
                                        Id = ms.Id,
                                        SubMID = subMenu != null ? subMenu.SubMid : 0,
                                        MainMenu = ms.MainMenu,
                                        SubMenu = subMenu != null ? subMenu.SubMenu : null,
                                        isParent = ms.IsParentActive,
                                        isChild = subMenu != null ? subMenu.IsChildActive : false
                                    }).ToList();
            } 
            return resultList;

        }

        public async Task<GetAllMenuResponseModel> GetMenuById(GetMenuByIdRequestModel request)
        {
            var user = _unitOfWork.MenuMasterRepository.AsQueryable()
                .Where(r => r.Id == request.Id)
                .Select(c => new GetAllMenuResponseModel
                {
                    Id = c.Id,
                    MainMenu = c.MainMenu,
                    isParent = c.IsParentActive

                })
                .FirstOrDefault();

            return user;
        }

        public async Task<HashSet<string>> AddValidation(AddMenuRequestModel request)
        {
            bool isBranchExists = await _unitOfWork.MenuMasterRepository.Any(x => x.MainMenu.ToLower().Trim() == request.MainMenu.ToLower().Trim());
            if (isBranchExists)
            {
                validationMessage.Add("Main Menu Already Exists");
            }
            return validationMessage;
        }
        public async Task<HashSet<string>> Add(AddMenuRequestModel request)
        {
            var validationMessage = new HashSet<string>();

            object IsResult;

            IsResult = _unitOfWork.SubMenuMasterRepository.AsQueryable().FirstOrDefault(x => x.SubMenu == request.SubMenu);

            if (IsResult != null)
            {
                validationMessage.Add("Menu already exists");
            }
            else
            {
                object IsParents;

                IsParents = _unitOfWork.MenuMasterRepository.AsQueryable().FirstOrDefault(x => x.MainMenu == request.MainMenu);

                if (IsParents == null)
                {

                    var newMenu = new MenuMaster
                    {
                        MainMenu = request.MainMenu,
                        CreatedBy = _gluID.GetUserID(),
                        IsParentActive = request.IsParent,
                        CreatedDate = DateTime.Now
                    };
                    await _unitOfWork.MenuMasterRepository.Add(newMenu);
                    await _unitOfWork.Commit();

                    IsParents = _unitOfWork.MenuMasterRepository.AsQueryable().FirstOrDefault(x => x.MainMenu == request.MainMenu);

                }
                var newDetails = new SubMenuMaster
                {
                    SubMenu = request.SubMenu,
                    MainMenuId = (int)((MenuMaster)IsParents).Id,
                    IsChildActive = request.IsChild,
                    CreatedBy = (int)_gluID.GetUserID(),
                    CreatedDate = DateTime.Now
                };
                await _unitOfWork.SubMenuMasterRepository.Add(newDetails);
                await _unitOfWork.Commit();

            }
            return validationMessage;

        }
        public async Task<HashSet<string>> UpdateValidation(UpdateMenuRequestBody request)
        {
            bool isClientExists = await _unitOfWork.MenuMasterRepository.Any(x => x.Id != request.Id && x.MainMenu.ToLower().Trim() == request.MainMenu.ToLower().Trim());
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
            menuMaster.IsParentActive = request.IsParent;
            menuMaster.CreatedDate = DateTime.Now;
            menuMaster.CreatedBy = (int)_gluID.GetUserID();


            //nEED TO CHECK
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
        Task<List<GetAllMenuResponseModel>> GetAll();
        Task<GetAllMenuResponseModel> GetMenuById(GetMenuByIdRequestModel request);
        Task<HashSet<string>> AddValidation(AddMenuRequestModel request);
        Task<HashSet<string>> Add(AddMenuRequestModel request);
        Task<HashSet<string>> UpdateValidation(UpdateMenuRequestBody request);
        Task<MenuMaster> Update(UpdateMenuRequestBody request);
        Task<HashSet<string>> DeleteValidation(GetMenuByIdRequestModel request);
        Task Delete(GetMenuByIdRequestModel id);



    }
}
