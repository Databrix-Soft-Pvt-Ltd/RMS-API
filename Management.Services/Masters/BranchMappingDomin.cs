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

    public class BranchMappingDomin : IBranchMappingDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        private readonly GlobalUserID _gluID;

        public BranchMappingDomin(IUnitOfWork unitOfWork, GlobalUserID globalUserID)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
            _gluID = globalUserID;
        }
        public async Task<List<GetAllBranchMappingResponse>> GetAllBranchMap()
        {
            List<GetAllBranchMappingResponse> resultList = new List<GetAllBranchMappingResponse>();
            using (var rMS_2024Context = new RMS_2024Context())
            {
                resultList = await (from s in rMS_2024Context.BranchMappings
                                    join b in rMS_2024Context.BranchMasters on s.BranchId equals b.Id
                                    join c in rMS_2024Context.UserMasters on s.UserId equals c.UserId
                                    select new GetAllBranchMappingResponse
                                    {
                                        Id = s.Id,
                                        BranchName = b.BranchName,
                                        UserName = c.UserName,
                                        userId = c.UserId,
                                        BranchId = b.Id

                                    }).ToListAsync();
            }
            return resultList;
        }
        public async Task<List<GetAllBranchMappingResponse>> GetBranchMapById(GetBranchMapByIdRequestModel request)
        {
            List<GetAllBranchMappingResponse> resultList = new List<GetAllBranchMappingResponse>();
            using (var rMS_2024Context = new RMS_2024Context())
            {
                resultList = await (from s in rMS_2024Context.BranchMappings
                                    join b in rMS_2024Context.BranchMasters on s.BranchId equals b.Id
                                    join c in rMS_2024Context.UserMasters on s.UserId equals c.UserId
                                    where s.UserId == request.UserID
                                    select new GetAllBranchMappingResponse
                                    {
                                        Id = b.Id,
                                        BranchName = b.BranchName,
                                        UserName = c.UserName,
                                        userId = c.UserId,
                                        BranchId = b.Id

                                    }).ToListAsync();
            }
            return resultList;

        }
        public async Task<HashSet<string>> AddBranchMapValidation(AddBranchMapRequestModel request, string Check)
        {
            HashSet<string> validationMessage = new HashSet<string>();

            if (request.BranchId.Length > 0)
            {
                foreach (var brnID in request.BranchId)
                {

                    if (Check == "BranchMap")
                    {
                        bool isRoleMapExists = await _unitOfWork.BranchMappingepository.Any(x => x.BranchId == brnID && x.UserId == request.UserId);
                        if (isRoleMapExists)
                            validationMessage.Add("1");
                        else
                            validationMessage.Add("0");
                    }
                    if (Check == "Branch")
                    {
                        bool isRoleExists = await Task.Run(() => _unitOfWork.BranchMasterRepository.Any(x => x.Id == brnID));
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
                }
            }
            return validationMessage;
        }
        public async Task<HashSet<string>> UpdateBranchMapValidation(UpdateBranchMappingResponse request, string Check)
        {
            HashSet<string> validationMessage = new HashSet<string>();
            foreach (var item in request.BranchId)
            {
                if (Check == "BranchMap")
                {

                    bool isRoleMapExists = await _unitOfWork.BranchMappingepository.Any(x => x.BranchId == item && x.UserId == request.UserId);
                    if (!isRoleMapExists)
                        validationMessage.Add("0");
                    else
                        validationMessage.Add("1");

                }
                if (Check == "Branch")
                {
                    //bool isRoleExists = await Task.Run(() => _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId));
                    bool isRoleExists = await Task.Run(() => _unitOfWork.BranchMasterRepository.Any(x => x.Id == item));
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
            }
            return validationMessage;
        }
        public async Task<BranchMapping> Add(AddBranchMapRequestModel request)
        {
            var newBranchFeature1 = new BranchMapping();

            foreach (var item in request.BranchId)
            {
                bool IsExists = await _unitOfWork.BranchMappingepository.Any(x => x.BranchId == item && x.UserId == request.UserId);

                if (!IsExists)
                {
                    var newBranchFeature = new BranchMapping();
                    newBranchFeature.BranchId = item;
                    newBranchFeature.UserId = request.UserId;
                    newBranchFeature.CreatedBy = _gluID.GetUserID();
                    newBranchFeature.CreatedDate = DateTime.Now;
                    newBranchFeature = await _unitOfWork.BranchMappingepository.Add(newBranchFeature);
                    await _unitOfWork.Commit();
                }
            }
            return newBranchFeature1;
        }
        public async Task<BranchMapping> Update(UpdateBranchMappingResponse request)
        {
            var objectBranchMapping = new BranchMapping();

            if (request.BranchId != null && request.BranchId.Length > 0)
            {
                var branchMappingsToDelete = _unitOfWork.BranchMappingepository.AsQueryable()
                                                .Where(x => x.UserId == request.UserId)
                                                .ToList();

                if (branchMappingsToDelete != null && branchMappingsToDelete.Any())
                {
                    foreach (var mappingToDelete in branchMappingsToDelete)
                    {
                        _unitOfWork.BranchMappingepository.Delete(mappingToDelete);
                    }
                    await _unitOfWork.Commit();


                    foreach (var item in request.BranchId)
                    {
                        var newBranchFeature = new BranchMapping();
                        newBranchFeature.BranchId = item;
                        newBranchFeature.UserId = request.UserId;
                        newBranchFeature.CreatedDate = DateTime.Now;

                        newBranchFeature = await _unitOfWork.BranchMappingepository.Add(newBranchFeature);
                    }
                    await _unitOfWork.Commit();
                }
            }
            else
            {
                throw new Exception("Branch Ids not provided");
            }
            return objectBranchMapping;
        }

        public async Task<HashSet<string>> DeleteValidation(GetBranchMapByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.BranchMappingepository.Any(x => x.UserId == request.UserID && x.BranchId == request.BranchID);
            if (IsRecord)
                validationMessage.Add("1");
            return validationMessage;
        }
        public async Task Delete(GetBranchMapByIdRequestModel id)
        {
            var RoleToDelete = _unitOfWork.BranchMappingepository.AsQueryable().Where(x => x.UserId == id.UserID && x.BranchId == id.BranchID).FirstOrDefault();

            if (RoleToDelete != null)
            {
                _unitOfWork.BranchMappingepository.Delete(RoleToDelete);
                await _unitOfWork.Commit();
            }
        }

        public async Task UpdateBranchMapIsActive(BranchMap_IsAction branchMapList)
        {
            
            var GlobalIsActive = _unitOfWork.BranchMappingepository.AsQueryable().Where(x => x.UserId == branchMapList.UserID && x.BranchId ==  branchMapList.BranchID).FirstOrDefault();
            if (GlobalIsActive != null)
            {
                GlobalIsActive.IsActive = branchMapList.IsActive; 
                _unitOfWork.BranchMappingepository.Update(GlobalIsActive);
                await _unitOfWork.Commit();
                validationMessage.Add("1");
            }  
        }

    }
    public interface IBranchMappingDomain
    {
        Task<List<GetAllBranchMappingResponse>> GetAllBranchMap();
        Task<List<GetAllBranchMappingResponse>> GetBranchMapById(GetBranchMapByIdRequestModel request);
        Task<HashSet<string>> AddBranchMapValidation(AddBranchMapRequestModel request, string Check);
        Task<HashSet<string>> UpdateBranchMapValidation(UpdateBranchMappingResponse request, string Check);
        Task<BranchMapping> Add(AddBranchMapRequestModel r);
        Task<BranchMapping> Update(UpdateBranchMappingResponse request);
        Task<HashSet<string>> DeleteValidation(GetBranchMapByIdRequestModel request);
        Task Delete(GetBranchMapByIdRequestModel id);
        Task UpdateBranchMapIsActive(BranchMap_IsAction branchMapList);
    }

}
