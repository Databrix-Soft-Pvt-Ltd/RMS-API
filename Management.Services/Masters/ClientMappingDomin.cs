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

    public class ClientMappingDomain : IClientMappingDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }

        public ClientMappingDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();

        }
        public async Task<IEnumerable<GetAllClientMappingResponse>> GetAllClientMap()
        {
            var query = _unitOfWork.RoleMappingepository.AsQueryable().
                Select(r => new GetAllClientMappingResponse
                {
                    ClientId = r.RoleId,
                    UserId = r.MenuId, 

                }).ToList();
            return query;
        }
        public async Task<GetAllClientMappingResponse> GetClientMapById(GetClientMapByIdRequestModel request)
        {
            var ResultMap = _unitOfWork.ClientMappingepository.AsQueryable().Where(r => r.Id == request.ClientMapId).
                 Select(r => new GetAllClientMappingResponse
                 {
                    ClientId = r.ClientId,
                    UserId = r.UserId

                 })
                .FirstOrDefault();

            return ResultMap;
        }
        public async Task<HashSet<string>> AddClientMapValidation(AddClientMapRequestModel request,string Check)
        {
            HashSet<string> validationMessage = new HashSet<string>();

            if (Check == "ClientMap")
            {
                bool isRoleMapExists = await _unitOfWork.ClientMappingepository.Any(x => x.ClientId == request.ClientId && x.UserId == request.UserId);
                if (isRoleMapExists)
                    validationMessage.Add("1");
                else
                    validationMessage.Add("0");
            }
            if (Check == "Client")
            {
                bool isRoleExists = await Task.Run(() => _unitOfWork.ClientMasterRepository.Any(x => x.Id == request.ClientId));
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
        public async Task<HashSet<string>> UpdatClientMapValidation(UpdateClientMappingResponse request ,string Check)
        {
            HashSet<string> validationMessage = new HashSet<string>();

            if (Check == "ClientMap")
            {
                bool isRoleMapExists = await _unitOfWork.ClientMappingepository.Any(x => x.ClientId == request.ClientId && x.UserId == request.UserId);
                if (!isRoleMapExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            if (Check == "Client")
            {
                //bool isRoleExists = await Task.Run(() => _unitOfWork.RoleRepository.Any(x => x.Id == request.RoleId));
                bool isRoleExists = await Task.Run(() => _unitOfWork.ClientMasterRepository.Any(x => x.Id == request.ClientId));
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
        public async Task<ClientMapping> Add(AddClientMapRequestModel r)
        {
            var newClientFeature = new ClientMapping();

            newClientFeature.ClientId = r.ClientId;
            newClientFeature.UserId = r.UserId;
            newClientFeature.CreatedDate = DateTime.Now;

            var response = await _unitOfWork.ClientMappingepository.Add(newClientFeature);
            await _unitOfWork.Commit();
            return response;
        }
        public async Task<ClientMapping> Update(UpdateClientMappingResponse request)
        {
            var existingRoleMap = await _unitOfWork.ClientMappingepository.GetById(request.Id);
            if (existingRoleMap == null)
            { 
                throw new Exception("ClienteMapping not found");
            }
            existingRoleMap.ClientId = request.ClientId;
            existingRoleMap.UserId = request.UserId;
           
            // existingRoleMap.Cre = DateTime.Now; 

            var response = await _unitOfWork.ClientMappingepository.Update(existingRoleMap);
            await _unitOfWork.Commit();

            return response;
        }
        public async Task<HashSet<string>> DeleteValidation(GetClientMapByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.ClientMappingepository.Any(x => x.Id == request.ClientMapId);
            if (IsRecord)
                validationMessage.Add("1");
            return validationMessage;
        }
        public async Task Delete(GetClientMapByIdRequestModel id)
        {
            var RoleToDelete = await _unitOfWork.ClientMappingepository.GetById(id.ClientMapId);

            if (RoleToDelete != null)
            {
                _unitOfWork.ClientMappingepository.Delete(RoleToDelete);
                await _unitOfWork.Commit();
            }
        }

    }
    public interface IClientMappingDomain
    {
        Task<IEnumerable<GetAllClientMappingResponse>> GetAllClientMap();
        Task<GetAllClientMappingResponse> GetClientMapById(GetClientMapByIdRequestModel request);
        Task<HashSet<string>> AddClientMapValidation(AddClientMapRequestModel request, string Check);
        Task<HashSet<string>> UpdatClientMapValidation(UpdateClientMappingResponse request, string Check);
        Task<ClientMapping> Add(AddClientMapRequestModel r);
        Task<ClientMapping> Update(UpdateClientMappingResponse request);
        Task<HashSet<string>> DeleteValidation(GetClientMapByIdRequestModel request);
        Task Delete(GetClientMapByIdRequestModel id);
    }

}
