using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoWayCommunication.Core.UnitOfWork;
using Management.Model.RMSEntity;
using Management.Model.RequestModel;
using Management.Model.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TwoWayCommunication.Model.Enums;

namespace Management.Services.Masters
{

    public class RoleMappingDomain : IRoleMapDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }

        private readonly GlobalUserID _gluID;

        public RoleMappingDomain(IUnitOfWork unitOfWork, GlobalUserID globalUserID)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
            _gluID = globalUserID;

        }
        public async Task<List<GetAllRoleMappingResponse>> GetAllRoleMap()
        {

            List<GetAllRoleMappingResponse> resultList = new List<GetAllRoleMappingResponse>();
            using (var rMS_2024Context = new RMS_2024Context())
            {
                resultList = await (from s in rMS_2024Context.RoleFeatures1
                                    join b in rMS_2024Context.RoleMasters on s.RoleId equals b.Id
                                    join c in rMS_2024Context.SubMenuMasters on s.SubMenuId equals c.SubMid
                                    join m in rMS_2024Context.MenuMasters on (long)c.MainMenuId equals (long)m.Id
                                    select new GetAllRoleMappingResponse
                                    {
                                        Id = s.Id,
                                        RoleId = s.RoleId,
                                        RoleName = b.RoleName,
                                        MenuId = m.Id,
                                        MenuName = m.MainMenu,
                                        SubMenuId = c.SubMid,
                                        SubMenuName = c.SubMenu,
                                        ViewRights = s.ViewRights,
                                        AddRights = s.AddRights,
                                        ModifyRights = s.ModifyRights,
                                        DeleteRights = s.DeleteRights,
                                        DownloadRights = s.DownloadRights,
                                        ApprovalRights = s.ApprovalRights

                                    }).ToListAsync();
            }
            return resultList;

        }

        public async Task<List<object>> GetAllMenuForAll_RoleMapping()
        {
            using (var rMS_2024Context = new RMS_2024Context())
            {
                var result = await (from s in rMS_2024Context.SubMenuMasters
                                    join b in rMS_2024Context.MenuMasters on (int)s.MainMenuId equals b.Id
                                    select new
                                    {
                                        MainMenuId = b.Id,
                                        MainMenuName = b.MainMenu,
                                        SubMenuId = s.SubMid,
                                        SubMenuName = s.SubMenu
                                    }).ToListAsync();

                var mainMenuGroups = result.GroupBy(r => new { r.MainMenuId, r.MainMenuName })
                                            .Select(g => new MainMenu
                                            {
                                                Id = g.Key.MainMenuId,
                                                ParentName = g.Key.MainMenuName,
                                                SubMenu = g.Select(s => new SubMainMenu
                                                {
                                                    Id = s.SubMenuId,
                                                    ChildName = s.SubMenuName
                                                }).ToArray()
                                            }).ToArray();

                var response = new GetAllMenuFor_RoleMappingResponse
                {
                    mainMenu = mainMenuGroups
                };
                return new List<object> { response };
            }
        }

        public async Task<object> GetRoleMapById(GetRoleMapByIdRequestModel request)
        {
            using (var context = new RMS_2024Context())
            {
                var query = (from s in context.SubMenuMasters
                             join mm in context.MenuMasters on (long)s.MainMenuId equals mm.Id into mmGroup
                             from mm in mmGroup.DefaultIfEmpty()
                             join rf in context.RoleFeatures1 on s.SubMid equals rf.SubMenuId into rfGroup
                             from rf in rfGroup.DefaultIfEmpty()
                             where rf.RoleId == request.RoleId || rf.RoleId == null
                             select new
                             {
                                 RoleId = request.RoleId,
                                 SubMenuId = s.SubMid,
                                 MainMenuId = s.MainMenuId,
                                 MainMenu = mm.MainMenu,
                                 SubMenu = s.SubMenu,
                                 ISViewRights = rf.ViewRights ?? false,
                                 IsAddRights = rf.AddRights ?? false,
                                 IsModifyRights = rf.ModifyRights ?? false,
                                 IsDeleteRights = rf.DeleteRights ?? false,
                                 IsDownloadRights = rf.DownloadRights ?? false,
                                 IsApprovalRights = rf.ApprovalRights ?? false
                             }).ToList();

                var resultList = query
                                    .GroupBy(x => x.MainMenuId)
                                    .Select(g => new
                                    {
                                        mainId = g.Key,
                                        parentName = g.First().MainMenu,
                                        isSeleted = true,
                                        subMenu = g.Select(sub => new
                                        {
                                            subMenuID = sub.SubMenuId,
                                            childName = sub.SubMenu,
                                            subIsSelected = true,
                                            isViewRights = sub.ISViewRights,
                                            isAddRights = sub.IsAddRights,
                                            isModifyRights = sub.IsModifyRights,
                                            isDeleteRights = sub.IsDeleteRights,
                                            isDownloadRights = sub.IsDownloadRights,
                                            isApprovalRights = sub.IsApprovalRights
                                        }).Distinct().ToList()
                                    }).ToList();

                var response = new
                {
                    response_Code = 200,
                    response_Message = "Record fetched Successfully....",
                    reponseData = resultList
                };

                return response;
            }
        }
        public async Task<HashSet<string>> AddRoleMapValidation(AddRoleMapRequestModel request, string Check)
        {
            //HashSet<string> validationMessage = new HashSet<string>();

            //if (request..Length > 0)
            //{
            //    foreach (var rolID in request.AddRoleRights)
            //    {
            //        if (Check == "RoleMap")
            //        {
            //            bool isRoleMapExists = await _unitOfWork.RoleMappingepository.Any(x => x.SubMenuId == rolID.SubMenuId && x.RoleId == request.RoleId);
            //            if (isRoleMapExists)
            //                validationMessage.Add("1");
            //            else
            //                validationMessage.Add("0");
            //        }
            //        if (Check == "Role")
            //        {
            //            bool isRoleExists = await Task.Run(() => _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId));
            //            // bool isRoleExists = await _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId);
            //            if (isRoleExists)
            //                validationMessage.Add("0");
            //            else
            //                validationMessage.Add("2");
            //        }
            //        if (Check == "Menu")
            //        {
            //            bool isMenuExists = await _unitOfWork.MenuMasterRepository.Any(x => x.Id == rolID.SubMenuId);
            //            if (isMenuExists)
            //                validationMessage.Add("0");
            //            else
            //                validationMessage.Add("3");
            //        }
            //    }
            //}
            return validationMessage;
        }
        public async Task<HashSet<string>> UpdateRoleMapValidation(UpdateRoleMappingResponse request, string Check)
        {
            //HashSet<string> validationMessage = new HashSet<string>();

            //   if (Check == "RoleMap")
            //    {
            //        bool isRoleMapExists = await _unitOfWork.RoleMappingepository.Any(x => x.MenuId == request.MenuId && x.RoleId == request.RoleId);
            //        if (!isRoleMapExists)
            //            validationMessage.Add("0");
            //        else
            //            validationMessage.Add("1");
            //    }
            //    if (Check == "Role")
            //    {
            //        //bool isRoleExists = await Task.Run(() => _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId));
            //        bool isRoleExists = await _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId);
            //        if (isRoleExists)
            //            validationMessage.Add("0");
            //        else
            //            validationMessage.Add("2");
            //    }
            //    if (Check == "Menu")
            //    {
            //        bool isMenuExists = await _unitOfWork.MenuMasterRepository.Any(x => x.Id == request.MenuId);
            //        if (isMenuExists)
            //            validationMessage.Add("0");
            //        else
            //            validationMessage.Add("3");
            //    } 

            return validationMessage;
        }
        public async Task<HashSet<string>> Add(AddRoleMapRequestModel r)
        {

            bool IsExists = await _unitOfWork.RoleMappingepository.Any(x => x.RoleId == r.Role_ID);
            if (IsExists)
            {
                validationMessage.Add("Role already exists");
            }
            else

                using (var context = new RMS_2024Context())
                {
                    foreach (var request in r.mainMenu)
                    {
                        //******************************* Check if mainMenu is selected
                        if (request.IsSeleted == false)
                        {
                            var roleFeaturesToDelete = from rf in context.RoleFeatures1
                                                       join sm in context.SubMenuMasters on rf.SubMenuId equals sm.SubMid
                                                       where sm.MainMenuId == request.MainId && rf.RoleId == r.Role_ID
                                                       select rf;

                            context.RoleFeatures1.RemoveRange(roleFeaturesToDelete);
                        }
                        else
                        {
                            //************************** If mainMenu is selected, delete existing RoleMappings
                            var existingRoleMappings = context.RoleFeatures1.Where(rm => rm.RoleId == r.Role_ID && rm.SubMenuId == request.MainId);
                            context.RoleFeatures1.RemoveRange(existingRoleMappings);

                            //***************************** Add new RoleMappings for subMenu items
                            foreach (var subMenu1 in request.SubMenu)
                            {
                                // Check if subMenu is selected
                                if (subMenu1.SubIsSelected == true)
                                {
                                    // Add new RoleMapping for the selected subMenu
                                    var newRoleMapping = new RoleFeature1
                                    {
                                        RoleId = r.Role_ID,
                                        SubMenuId = subMenu1.SubMenuID,
                                        ViewRights = subMenu1.ISViewRights,
                                        AddRights = subMenu1.IsAddRights,
                                        ModifyRights = subMenu1.IsModifyRights,
                                        DeleteRights = subMenu1.IsDeleteRights,
                                        DownloadRights = subMenu1.IsDownloadRights,
                                        ApprovalRights = subMenu1.IsApprovalRights,
                                        CreatedBy = (int)_gluID.GetUserID(),
                                        CreatedDate = DateTime.Now

                                    };
                                    context.RoleFeatures1.Add(newRoleMapping);
                                }
                                else
                                {
                                    // If subMenu is not selected, delete RoleMapping based on id:5 & Role_ID: 25
                                    var roleMappingToDelete = context.RoleFeatures1
                                        .FirstOrDefault(rm => rm.SubMenuId == subMenu1.SubMenuID && rm.RoleId == r.Role_ID);
                                    if (roleMappingToDelete != null)
                                    {
                                        context.RoleFeatures1.Remove(roleMappingToDelete);
                                    }
                                }
                            }
                        }
                    }
                    // Move SaveChangesAsync() outside of the loop
                    await context.SaveChangesAsync();
                    validationMessage.Add("RoleMappinig Added Successfully");
                }
            return validationMessage;
        }
        public async Task<HashSet<string>> Update(AddRoleMapRequestModel r)
        {
            bool IsExists = await _unitOfWork.RoleMappingepository.Any(x => x.RoleId == r.Role_ID);
            if (!IsExists)
            {
                validationMessage.Add("Role not exists");
            }
            else

                using (var context = new RMS_2024Context())
                {
                    foreach (var request in r.mainMenu)
                    {
                        //******************************* Check if mainMenu is selected
                        if (request.IsSeleted == false)
                        {
                            var roleFeaturesToDelete = from rf in context.RoleFeatures1
                                                       join sm in context.SubMenuMasters on rf.SubMenuId equals sm.SubMid
                                                       where sm.MainMenuId == request.MainId && rf.RoleId == r.Role_ID
                                                       select rf;

                            context.RoleFeatures1.RemoveRange(roleFeaturesToDelete);
                        }
                        else
                        {
                            //************************** If mainMenu is selected, delete existing RoleMappings
                            var roleFeaturesToDelete = from rf in context.RoleFeatures1 
                                                       join sm in context.SubMenuMasters on rf.SubMenuId equals sm.SubMid
                                                       join mm in context.MenuMasters on sm.MainMenuId equals  (int)mm.Id
                                                       where (int)mm.Id == request.MainId && rf.RoleId == r.Role_ID
                                                       select rf;

                            context.RoleFeatures1.RemoveRange(roleFeaturesToDelete);
                            //***************************** Add new RoleMappings for subMenu items
                            await context.SaveChangesAsync();
                            foreach (var subMenu1 in request.SubMenu)
                            {
 
                                // Check if subMenu is selected
                                if (subMenu1.SubIsSelected == true)
                                {
                                    // Add new RoleMapping for the selected subMenu
                                    var newRoleMapping = new RoleFeature1
                                    {
                                        RoleId = r.Role_ID,
                                        SubMenuId = subMenu1.SubMenuID,
                                        ViewRights = subMenu1.ISViewRights,
                                        AddRights = subMenu1.IsAddRights,
                                        ModifyRights = subMenu1.IsModifyRights,
                                        DeleteRights = subMenu1.IsDeleteRights,
                                        DownloadRights = subMenu1.IsDownloadRights,
                                        ApprovalRights = subMenu1.IsApprovalRights,
                                        CreatedBy = (int)_gluID.GetUserID(),
                                        CreatedDate = DateTime.Now

                                    };
                                    context.RoleFeatures1.Add(newRoleMapping);
                                }
                                else
                                {
                                    // If subMenu is not selected, delete RoleMapping based on id:5 & Role_ID: 25
                                    var roleMappingToDelete = context.RoleFeatures1
                                        .FirstOrDefault(rm => rm.SubMenuId == subMenu1.SubMenuID && rm.RoleId == r.Role_ID);
                                    if (roleMappingToDelete != null)
                                    {
                                        context.RoleFeatures1.Remove(roleMappingToDelete);
                                    }
                                }
                            }
                        }
                    }
                    // Move SaveChangesAsync() outside of the loop
                    await context.SaveChangesAsync();
                    validationMessage.Add("RoleMappinig Added Successfully");
                }
            return validationMessage;
        }
        public async Task<HashSet<string>> Update_Rights(UpdateRoleRight_by_Role_ID request)
        {
            RoleFeature1 newRoleFeature;

            var IRoleData = _unitOfWork.RoleMappingepository.AsQueryable().Where(x => x.Id == request.RoleId).FirstOrDefault();

            IRoleData.Id = request.RoleId;
            IRoleData.ViewRights = request.ViewRights;
            IRoleData.AddRights = request.AddRights;
            IRoleData.ModifyRights = request.ModifyRights;
            IRoleData.DeleteRights = request.DeleteRights;
            IRoleData.DownloadRights = request.DownloadRights;
            IRoleData.ApprovalRights = request.ApprovalRights;
            IRoleData.CreatedBy = _gluID.GetUserID();
            IRoleData.CreatedDate = DateTime.Now;
            IRoleData = await _unitOfWork.RoleMappingepository.Update(IRoleData);

            await _unitOfWork.Commit();


            return validationMessage;
        }

        public async Task<HashSet<string>> DeleteValidation(GetRoleMapByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.RoleMappingepository.Any(x => x.Id == request.RoleId);
            if (IsRecord)
                validationMessage.Add("1");
            return validationMessage;
        }
        public async Task Delete(GetRoleMapByIdRequestModel id)
        {
            var RoleToDelete = _unitOfWork.RoleMappingepository.AsQueryable().Where(x =>  x.Id == id.RoleId).FirstOrDefault();

            if (RoleToDelete != null)
            {
                _unitOfWork.RoleMappingepository.Delete(RoleToDelete);
                await _unitOfWork.Commit();
            }
        }

    }
    public interface IRoleMapDomain
    {
        Task<List<GetAllRoleMappingResponse>> GetAllRoleMap();
        Task<object> GetRoleMapById(GetRoleMapByIdRequestModel request);
        //Task<GetAllRoleMappingResponse> GetRoleMapById(GetRoleMapByIdRequestModel request);
        Task<HashSet<string>> AddRoleMapValidation(AddRoleMapRequestModel request, string Check);
        Task<HashSet<string>> UpdateRoleMapValidation(UpdateRoleMappingResponse request, string Check);
        Task<HashSet<string>> Add(AddRoleMapRequestModel r);
        Task<HashSet<string>> Update(AddRoleMapRequestModel r);
        Task<HashSet<string>> DeleteValidation(GetRoleMapByIdRequestModel request);
        Task Delete(GetRoleMapByIdRequestModel id);
        Task<HashSet<string>> Update_Rights(UpdateRoleRight_by_Role_ID request);
        Task<List<object>> GetAllMenuForAll_RoleMapping();
    }

}
