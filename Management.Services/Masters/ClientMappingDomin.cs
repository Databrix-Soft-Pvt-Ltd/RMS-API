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

    public class ClientMappingDomain : IClientMappingDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        private readonly GlobalUserID _gluID;

        public ClientMappingDomain(IUnitOfWork unitOfWork, GlobalUserID globalUserID)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
            _gluID = globalUserID;

        }
        public async Task<List<GetAllClientMappingResponse>> GetAllClientMap()
        {
            List<GetAllClientMappingResponse> resultList = new List<GetAllClientMappingResponse>();
            using (var rMS_2024Context = new RMS_2024Context())
            {
                resultList = await (from s in rMS_2024Context.ClientMappings
                                    join b in rMS_2024Context.ClientMasters on s.ClientId equals b.Id
                                    join c in rMS_2024Context.UserMasters on s.UserId equals c.UserId
                                    select new GetAllClientMappingResponse
                                    {
                                        Id = s.Id,
                                        ClinetName = b.ClientName,
                                        UserName = c.UserName,
                                        ClientId = b.Id,
                                        userId = c.UserId


                                    }).ToListAsync();
            }
            return resultList; 
 
        }
        public async Task<List<GetAllClientMappingResponse>> GetClientMapById(GetClientMapByIdRequestModel request)
        {

            List<GetAllClientMappingResponse> resultList = new List<GetAllClientMappingResponse>();
            using (var rMS_2024Context = new RMS_2024Context())
            {
                resultList = await (from s in rMS_2024Context.ClientMappings
                                    join b in rMS_2024Context.ClientMasters on s.ClientId equals b.Id
                                    join c in rMS_2024Context.UserMasters on s.UserId equals c.UserId
                                    where s.UserId == request.UserID
                                    select new GetAllClientMappingResponse
                                    {
                                        Id = s.Id,
                                        ClinetName = b.ClientName,
                                        UserName = c.UserName,
                                        ClientId = b.Id,
                                        userId = c.UserId

                                    }).ToListAsync();
            }
            return resultList;
 
        }
        public async Task<HashSet<string>> AddClientMapValidation(AddClientMapRequestModel request,string Check)
        {
            HashSet<string> validationMessage = new HashSet<string>();

            if(request.ClientId.Length > 0)
            {
                foreach(var clinID in request.ClientId)
                {
                    if (Check == "ClientMap")
                    {
                        bool isRoleMapExists = await _unitOfWork.ClientMappingepository.Any(x => x.ClientId == clinID && x.UserId == request.UserId);
                        if (isRoleMapExists)
                            validationMessage.Add("1");
                        else
                            validationMessage.Add("0");
                    }
                    if (Check == "Client")
                    {
                        bool isRoleExists = await Task.Run(() => _unitOfWork.ClientMasterRepository.Any(x => x.Id == clinID));
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
        public async Task<HashSet<string>> UpdatClientMapValidation(UpdateClientMappingResponse request ,string Check)
        {
            HashSet<string> validationMessage = new HashSet<string>();

            foreach(var item in request.ClientId)
            {
                if (Check == "ClientMap")
                {
                    bool isRoleMapExists = await _unitOfWork.ClientMappingepository.Any(x => x.ClientId == item && x.UserId == request.UserId);
                    if (!isRoleMapExists)
                        validationMessage.Add("0");
                    else
                        validationMessage.Add("1");
                }
                if (Check == "Client")
                {
                    //bool isRoleExists = await Task.Run(() => _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId));
                    bool isRoleExists = await Task.Run(() => _unitOfWork.ClientMasterRepository.Any(x => x.Id == item));
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
        public async Task<ClientMapping> Add(AddClientMapRequestModel r)
        {
            var newClientFeature1 = new ClientMapping();

            foreach(var item in r.ClientId)
            {
                bool IsExists = await _unitOfWork.ClientMappingepository.Any(x => x.ClientId == item && x.UserId == r.UserId);
                if (!IsExists)
                {
                    var newClientFeature = new ClientMapping();
                    newClientFeature.ClientId = item;
                    newClientFeature.UserId = r.UserId;
                    newClientFeature.CreatedBy = _gluID.GetUserID();
                    newClientFeature.CreatedDate = DateTime.Now;
                    newClientFeature = await _unitOfWork.ClientMappingepository.Add(newClientFeature);
                    await _unitOfWork.Commit();
                }

            }

            return newClientFeature1;
        }
        public async Task<ClientMapping> Update(UpdateClientMappingResponse request)
        {
            //var existingRoleMap = await _unitOfWork.ClientMappingepository.GetById(request.Id);
            //if (existingRoleMap == null)
            //{ 
            //    throw new Exception("ClienteMapping not found");
            //}
            //existingRoleMap.ClientId = request.ClientId;
            //existingRoleMap.UserId = request.UserId;

            //// existingRoleMap.Cre = DateTime.Now; 

            //var response = await _unitOfWork.ClientMappingepository.Update(existingRoleMap);
            //await _unitOfWork.Commit();

            //return response;

            var objectClientMapping = new ClientMapping();

            if (request.ClientId != null && request.ClientId.Length > 0)
            {
                var clientMappingsToDelete = _unitOfWork.ClientMappingepository.AsQueryable()
                                                .Where(x => x.UserId == request.UserId)
                                                .ToList();

                if (clientMappingsToDelete != null && clientMappingsToDelete.Any())
                {
                    foreach (var mappingToDelete in clientMappingsToDelete)
                    {
                        _unitOfWork.ClientMappingepository.Delete(mappingToDelete);
                    }
                    await _unitOfWork.Commit();


                    foreach (var item in request.ClientId)
                    {
                        var newClientFeature = new ClientMapping();
                        newClientFeature.ClientId = item;
                        newClientFeature.UserId = request.UserId;
                        newClientFeature.CreatedBy = _gluID.GetUserID();
                        newClientFeature.CreatedDate = DateTime.Now;

                        newClientFeature = await _unitOfWork.ClientMappingepository.Add(newClientFeature);
                    }
                    await _unitOfWork.Commit();
                }
            }
            else
            {
                throw new Exception("Client Ids not provided");
            }
            return objectClientMapping;
        }
        public async Task<HashSet<string>> DeleteValidation(GetClientMapByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.ClientMappingepository.Any(x => x.UserId == request.UserID  && x.ClientId == request.ClientId);
            if (IsRecord)
                validationMessage.Add("1");
            return validationMessage;
        }
        public async Task Delete(GetClientMapByIdRequestModel id)
        {
            var RoleToDelete = _unitOfWork.ClientMappingepository.AsQueryable().Where(x => x.UserId == id.UserID && x.ClientId == id.ClientId).FirstOrDefault();

            if (RoleToDelete != null)
            {
                _unitOfWork.ClientMappingepository.Delete(RoleToDelete);
                await _unitOfWork.Commit();
            }
        }
        public async Task UpdateUserIsActive(Client_IsAction branchMapList)
        {

            var GlobalIsActive = _unitOfWork.ClientMappingepository.AsQueryable().Where(x => x.UserId == branchMapList.UserID).FirstOrDefault();
            if (GlobalIsActive != null)
            {
                GlobalIsActive.IsActive = branchMapList.IsActive;
                _unitOfWork.ClientMappingepository.Update(GlobalIsActive);
                await _unitOfWork.Commit();
                validationMessage.Add("1");
            }
        }

    }
    public interface IClientMappingDomain
    {
        Task<List<GetAllClientMappingResponse>> GetAllClientMap();
        Task<List<GetAllClientMappingResponse>> GetClientMapById(GetClientMapByIdRequestModel request);
        Task<HashSet<string>> AddClientMapValidation(AddClientMapRequestModel request, string Check);
        Task<HashSet<string>> UpdatClientMapValidation(UpdateClientMappingResponse request, string Check);
        Task<ClientMapping> Add(AddClientMapRequestModel r);
        Task<ClientMapping> Update(UpdateClientMappingResponse request);
        Task<HashSet<string>> DeleteValidation(GetClientMapByIdRequestModel request);
        Task Delete(GetClientMapByIdRequestModel id);
        Task UpdateUserIsActive(Client_IsAction branchMapList);
    }

}
