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

    public class BranchMappingDomin : IBranchMappingDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }

        public BranchMappingDomin(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();

        }
        public async Task<IEnumerable<GetAllBranchMappingResponse>> GetAllBranchMap()
        {
            var query = _unitOfWork.BranchMappingepository.AsQueryable().
                Select(r => new GetAllBranchMappingResponse
                {
                    BranchId = r.BranchId,
                    UserId = r.UserId, 

                }).ToList();
            return query;
        }
        public async Task<GetAllBranchMappingResponse> GetBranchMapById(GetBranchMapByIdRequestModel request)
        {
            var ResultMap = _unitOfWork.BranchMappingepository.AsQueryable().Where(r => r.Id == request.BranchMapId).
                 Select(r => new GetAllBranchMappingResponse
                 {
                    BranchId = r.BranchId,
                    UserId = r.UserId

                 })
                .FirstOrDefault();

            return ResultMap;
        }
        public async Task<HashSet<string>> AddBranchMapValidation(AddBranchMapRequestModel request,string Check)
        {
            HashSet<string> validationMessage = new HashSet<string>();

            if (Check == "BranchMap")
            {
                bool isRoleMapExists = await _unitOfWork.BranchMappingepository.Any(x => x.BranchId == request.BranchId && x.UserId == request.UserId);
                if (isRoleMapExists)
                    validationMessage.Add("1");
                else
                    validationMessage.Add("0");
            }
            if (Check == "Branch")
            {
                bool isRoleExists = await Task.Run(() => _unitOfWork.BranchMasterRepository.Any(x => x.Id == request.BranchId));
              // bool isRoleExists = await _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId);
                if (isRoleExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("2");
            }
            if (Check == "User")
            {
                bool isMenuExists = await _unitOfWork.UserRepository.Any(x => x.UserId == request.UserId);
                if (isMenuExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("3");
            } 
            return validationMessage;
        }
        public async Task<HashSet<string>> UpdateBranchMapValidation(UpdateBranchMappingResponse request ,string Check)
        {
            HashSet<string> validationMessage = new HashSet<string>();

            if (Check == "BranchMap")
            {
                bool isRoleMapExists = await _unitOfWork.BranchMappingepository.Any(x => x.BranchId == request.BranchId && x.UserId == request.UserId);
                if (!isRoleMapExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            if (Check == "Branch")
            {
                //bool isRoleExists = await Task.Run(() => _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId));
                bool isRoleExists = await Task.Run(() => _unitOfWork.BranchMasterRepository.Any(x => x.Id == request.BranchId));
                if (isRoleExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("2");
            }
            if (Check == "User")
            {
                bool isMenuExists = await _unitOfWork.UserRepository.Any(x => x.UserId == request.UserId);
                if (isMenuExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("3");
            } 
            return validationMessage;
        }
        public async Task<BranchMapping> Add(AddBranchMapRequestModel r)
        {
            var newBranchFeature = new BranchMapping();

            newBranchFeature.BranchId = r.BranchId;
            newBranchFeature.UserId = r.UserId;
            newBranchFeature.CreatedDate = DateTime.Now;

            var response = await _unitOfWork.BranchMappingepository.Add(newBranchFeature);
            await _unitOfWork.Commit();
            return response;
        }
        public async Task<BranchMapping> Update(UpdateBranchMappingResponse request)
        {
            var existingBranchMap = await _unitOfWork.BranchMappingepository.GetById(request.Id);
            if (existingBranchMap == null)
            { 
                throw new Exception("Branch Mapping not found");
            }
            existingBranchMap.BranchId = request.BranchId;
            existingBranchMap.UserId = request.UserId;
           
            // existingRoleMap.Cre = DateTime.Now; 

            var response = await _unitOfWork.BranchMappingepository.Update(existingBranchMap);
            await _unitOfWork.Commit();

            return response;
        }
        public async Task<HashSet<string>> DeleteValidation(GetBranchMapByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.BranchMappingepository.Any(x => x.Id == request.BranchMapId);
            if (IsRecord)
                validationMessage.Add("1");
            return validationMessage;
        }
        public async Task Delete(GetBranchMapByIdRequestModel id)
        {
            var RoleToDelete = await _unitOfWork.BranchMappingepository.GetById(id.BranchMapId);

            if (RoleToDelete != null)
            {
                _unitOfWork.BranchMappingepository.Delete(RoleToDelete);
                await _unitOfWork.Commit();
            }
        }

    }
    public interface IBranchMappingDomain
    {
        Task<IEnumerable<GetAllBranchMappingResponse>> GetAllBranchMap();
        Task<GetAllBranchMappingResponse> GetBranchMapById(GetBranchMapByIdRequestModel request);
        Task<HashSet<string>> AddBranchMapValidation(AddBranchMapRequestModel request, string Check);
        Task<HashSet<string>> UpdateBranchMapValidation(UpdateBranchMappingResponse request, string Check);
        Task<BranchMapping> Add(AddBranchMapRequestModel r);
        Task<BranchMapping> Update(UpdateBranchMappingResponse request);
        Task<HashSet<string>> DeleteValidation(GetBranchMapByIdRequestModel request);
        Task Delete(GetBranchMapByIdRequestModel id);
    }

}
