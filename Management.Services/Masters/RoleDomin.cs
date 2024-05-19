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
    public class RoleDomain : IRoleDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }

        private readonly GlobalUserID _gluID;

        public RoleDomain(IUnitOfWork unitOfWork, GlobalUserID globalUserID)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
            _gluID = globalUserID;

        }
        public async Task<List<GetAllRoleResponse>> GetAll()
        {
            var query = _unitOfWork.RoleRepository.AsQueryable().
                Select(r => new GetAllRoleResponse
                {
                    RoleIdPk = r.Id,
                    Description = r.Description,
                    RoleName = r.RoleName,
                    IsActive=r.IsActive


                }).ToList();
            return query;
        }
        public async Task<HashSet<string>> GetCheckPageAccess(int RoleID, int MenuId)
        {
            bool IsAccess = await _unitOfWork.RoleMappingepository.Any(x => x.RoleId == RoleID && x.SubMenuId == MenuId);
            if(IsAccess)
                validationMessage.Add("0");
             else
                validationMessage.Add("1");
            return validationMessage;
        }
        public async Task<GetAllRoleResponse> GetRoleById(GetRoleByIdRequestModel request)
        {
            var user = _unitOfWork.RoleRepository.AsQueryable()
                .Where(r => r.Id == request.Id)
                .Select(r => new GetAllRoleResponse
                {
                    RoleIdPk = r.Id,
                    Description = r.Description,
                    RoleName = r.RoleName,
                    IsActive=r.IsActive

                })
                .FirstOrDefault();

            return user;
        }
        public async Task<HashSet<string>> AddValidation(AddRoleRequestModel request)
        {
            bool isClientExists = await _unitOfWork.RoleRepository.Any(x => x.RoleName.ToLower().Trim() == request.RoleName.ToLower().Trim());
            if (isClientExists)
                validationMessage.Add("1");
            return validationMessage;
        }
        public async Task<HashSet<string>> UpdateValidation(UpdateRoleRequestModel request)
        {
            bool isClientExists = await _unitOfWork.RoleRepository.Any(x => x.Id == request.Id && x.Id != 0);


            if (isClientExists)
            {
                bool CheckRoleName = await _unitOfWork.RoleRepository.Any(x => x.Id == request.Id);
                if (!CheckRoleName)
                {
                    validationMessage.Add("1");
                }
                else
                    validationMessage.Add("0");
            }
            else
            {
                validationMessage.Add("2");
            }
            return validationMessage;

        }
        public async Task<RoleMaster> Add(AddRoleRequestModel request)
        {
            var newRole = new RoleMaster();

            newRole.RoleName = request.RoleName;
            newRole.Description = request.Description;
            newRole.IsActive = request.IsActive;
            newRole.CreatedBy = _gluID.GetUserID();
            newRole.CreatedDate = DateTime.Now;
            var response = await _unitOfWork.RoleRepository.Add(newRole);
            await _unitOfWork.Commit();
            return response;
        }

        public async Task<RoleMaster> Update(UpdateRoleRequestModel request)
        {
            var existingRole = await _unitOfWork.RoleRepository.GetById(request.Id);
            if (existingRole == null)
            {
                throw new Exception("Role not found");
            }
            existingRole.RoleName = request.RoleName;
            existingRole.Description = request.Description;
            existingRole.IsActive = request.IsActive;
            existingRole.ModifyBy = _gluID.GetUserID();
            existingRole.UpdatedAt = DateTime.Now;
            var response = await _unitOfWork.RoleRepository.Update(existingRole);
            await _unitOfWork.Commit();

            return response;
        }


        public async Task<HashSet<string>> DeleteValidation(GetRoleByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.RoleRepository.Any(x => x.Id == request.Id);
            if (IsRecord)
                validationMessage.Add("1");
            return validationMessage;
        }

        public async Task Delete(GetRoleByIdRequestModel id)
        {
            var RoleToDelete = await _unitOfWork.RoleRepository.GetById(id.Id);

            if (RoleToDelete != null)
            {
                _unitOfWork.RoleRepository.Delete(RoleToDelete);
                await _unitOfWork.Commit();
            }
        }

    }
    public interface IRoleDomain
    {
        Task<List<GetAllRoleResponse>> GetAll();
        Task<GetAllRoleResponse> GetRoleById(GetRoleByIdRequestModel request);
        Task<HashSet<string>> AddValidation(AddRoleRequestModel request);
        Task<HashSet<string>> UpdateValidation(UpdateRoleRequestModel request);
        Task<RoleMaster> Add(AddRoleRequestModel request);
        Task<RoleMaster> Update(UpdateRoleRequestModel request);
        Task<HashSet<string>> DeleteValidation(GetRoleByIdRequestModel id);
        Task Delete(GetRoleByIdRequestModel id);
        Task<HashSet<string>> GetCheckPageAccess(int RoleID, int MenuId);
    }
}
