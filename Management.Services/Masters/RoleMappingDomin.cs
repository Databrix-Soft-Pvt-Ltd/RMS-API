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

namespace Management.Services.Masters
{

    public class RoleMappingDomain : IRoleMapDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }

        public RoleMappingDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();

        }
        public async Task<IEnumerable<GetAllRoleMappingResponse>> GetAllRoleMap()
        {
            var query = _unitOfWork.RoleMappingepository.AsQueryable().
                Select(r => new GetAllRoleMappingResponse
                {
                    RoleId = r.RoleId,
                    MenuId = r.MenuId,
                    ViewRights = r.ViewRights,
                    AddRights = r.AddRights,
                    ModifyRights = r.ModifyRights,
                    DeleteRights = r.DeleteRights,
                    DownloadRights = r.DownloadRights,
                    ApprovalRights = r.ApprovalRights

                }).ToList();
            return query;
        }
        public async Task<GetAllRoleMappingResponse> GetRoleMapById(GetRoleMapByIdRequestModel request)
        {
            var ResultMap = _unitOfWork.RoleMappingepository.AsQueryable().Where(r => r.Id == request.RoleMapID).
                 Select(r => new GetAllRoleMappingResponse
                 {
                     RoleId = r.RoleId,
                     MenuId = r.MenuId,
                     ViewRights = r.ViewRights,
                     AddRights = r.AddRights,
                     ModifyRights = r.ModifyRights,
                     DeleteRights = r.DeleteRights,
                     DownloadRights = r.DownloadRights,
                     ApprovalRights = r.ApprovalRights

                 })
                .FirstOrDefault();

            return ResultMap;
        }
        public async Task<HashSet<string>> AddRoleMapValidation(AddRoleMapRequestModel request,string Check)
        {
            HashSet<string> validationMessage = new HashSet<string>();

            if (Check == "RoleMap")
            {
                bool isRoleMapExists = await _unitOfWork.RoleMappingepository.Any(x => x.RoleId == request.RoleId && x.MenuId == request.MenuId);
                if (isRoleMapExists)
                    validationMessage.Add("1");
                else
                    validationMessage.Add("0");
            }
            if (Check == "Role")
            {
                bool isRoleExists = await Task.Run(() => _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId));
              // bool isRoleExists = await _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId);
                if (isRoleExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("2");
            }
            if (Check == "Menu")
            {
                bool isMenuExists = await _unitOfWork.MenuMasterRepository.Any(x => x.Id == request.MenuId);
                if (isMenuExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("3");
            } 
            return validationMessage;
        }
        public async Task<HashSet<string>> UpdateRoleMapValidation(UpdateRoleMappingResponse request ,string Check)
        {
            HashSet<string> validationMessage = new HashSet<string>();

            if (Check == "RoleMap")
            {
                bool isRoleMapExists = await _unitOfWork.RoleMappingepository.Any(x => x.RoleId == request.RoleId && x.MenuId == request.MenuId);
                if (!isRoleMapExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            if (Check == "Role")
            {
                //bool isRoleExists = await Task.Run(() => _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId));
                 bool isRoleExists = await _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId);
                if (isRoleExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("2");
            }
            if (Check == "Menu")
            {
                bool isMenuExists = await _unitOfWork.MenuMasterRepository.Any(x => x.Id == request.MenuId);
                if (isMenuExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("3");
            } 
            return validationMessage;
        }
        public async Task<RoleFeature1> Add(AddRoleMapRequestModel r)
        {
            var newRoleFeature = new RoleFeature1();

            newRoleFeature.RoleId = r.RoleId;
            newRoleFeature.MenuId = r.MenuId;
            newRoleFeature.ViewRights = r.ViewRights;
            newRoleFeature.AddRights = r.AddRights;
            newRoleFeature.ModifyRights = r.ModifyRights;
            newRoleFeature.DeleteRights = r.DeleteRights;
            newRoleFeature.DownloadRights = r.DownloadRights;
            newRoleFeature.ApprovalRights = r.ApprovalRights;
            newRoleFeature.CreatedDate = DateTime.Now;

            var response = await _unitOfWork.RoleMappingepository.Add(newRoleFeature);
            await _unitOfWork.Commit();
            return response;
        }
        public async Task<RoleFeature1> Update(UpdateRoleMappingResponse request)
        {
            var existingRoleMap = await _unitOfWork.RoleMappingepository.GetById(request.RoleMapId);
            if (existingRoleMap == null)
            {

                throw new Exception("RoleMapping not found");
            }
            existingRoleMap.RoleId = request.RoleId;
            existingRoleMap.MenuId = request.MenuId;
            existingRoleMap.ViewRights = request.ViewRights;
            existingRoleMap.AddRights = request.AddRights;
            existingRoleMap.ModifyRights = request.ModifyRights;
            existingRoleMap.DeleteRights = request.DeleteRights;
            existingRoleMap.DownloadRights = request.DownloadRights;
            existingRoleMap.ApprovalRights = request.ApprovalRights;
            // existingRoleMap.Cre = DateTime.Now; 

            var response = await _unitOfWork.RoleMappingepository.Update(existingRoleMap);
            await _unitOfWork.Commit();

            return response;
        }
        public async Task<HashSet<string>> DeleteValidation(GetRoleMapByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.RoleMappingepository.Any(x => x.Id == request.RoleMapID);
            if (IsRecord)
                validationMessage.Add("1");
            return validationMessage;
        }
        public async Task Delete(GetRoleMapByIdRequestModel id)
        {
            var RoleToDelete = await _unitOfWork.RoleMappingepository.GetById(id.RoleMapID);

            if (RoleToDelete != null)
            {
                _unitOfWork.RoleMappingepository.Delete(RoleToDelete);
                await _unitOfWork.Commit();
            }
        }

    }
    public interface IRoleMapDomain
    {
        Task<IEnumerable<GetAllRoleMappingResponse>> GetAllRoleMap();
        Task<GetAllRoleMappingResponse> GetRoleMapById(GetRoleMapByIdRequestModel request);
        Task<HashSet<string>> AddRoleMapValidation(AddRoleMapRequestModel request, string Check);
        Task<HashSet<string>> UpdateRoleMapValidation(UpdateRoleMappingResponse request, string Check);
        Task<RoleFeature1> Add(AddRoleMapRequestModel r);
        Task<RoleFeature1> Update(UpdateRoleMappingResponse request);
        Task<HashSet<string>> DeleteValidation(GetRoleMapByIdRequestModel request);
        Task Delete(GetRoleMapByIdRequestModel id);
    }

}
