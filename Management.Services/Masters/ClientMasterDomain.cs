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
    public class ClientMasterDomain : IClientMasterDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }

        public ClientMasterDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();

        }
        public async Task<IEnumerable<GetAllClientResponseModel>> GetAll()
        {
            var query = _unitOfWork.ClientMasterRepository.AsQueryable().
                Select(client => new GetAllClientResponseModel
                {
                    Id = client.Id,
                    ClientName = client.ClientName,
                    ClientCode = client.ClientCode,

                }).ToList();
            return query;
        }

        public async Task<GetAllClientResponseModel> GetClientById(GetClientByIdRequestModel request)
        {
            var user = _unitOfWork.ClientMasterRepository.AsQueryable()
                .Where(r => r.Id == request.Id)
                .Select(client => new GetAllClientResponseModel
                {
                    Id = client.Id,
                    ClientName = client.ClientName,
                    ClientCode = client.ClientCode,

                })
                .FirstOrDefault();

            return user;
        }
        public async Task<HashSet<string>> AddValidation(AddClientRequest request)
        {
            bool isClientExists = await _unitOfWork.ClientMasterRepository.Any(x => x.ClientCode.ToLower().Trim() == request.ClientCode.ToLower().Trim());
            if (isClientExists)
            {
                validationMessage.Add("Client Already Exists");
            }
            return validationMessage;
        }
        public async Task<HashSet<string>> UpdateValidation(UpdateClientRequestModel request)
        {
            bool isClientExists = await _unitOfWork.ClientMasterRepository.Any(x => x.Id != request.Id && x.ClientCode.ToLower().Trim() == request.ClientCode.ToLower().Trim());
            if (isClientExists)
            {
                validationMessage.Add("Client doesnot exits Exists");
            }
            return validationMessage;
        }


        public async Task<ClientMaster> Add(AddClientRequest request)
        {
            var newClient = new ClientMaster();

            newClient.ClientName = request.ClientName;
            newClient.ClientCode = request.ClientCode;
            newClient.CreatedDate = DateTime.Now;
            var response = await _unitOfWork.ClientMasterRepository.Add(newClient);
            await _unitOfWork.Commit();
            return response;
        }

        public async Task<ClientMaster> Update(UpdateClientRequestModel request)
        {
            var existingClient = await _unitOfWork.ClientMasterRepository.GetById(request.Id);
            if (existingClient == null)
            {
                throw new Exception("Client not found");
            }
            existingClient.ClientCode = request.ClientCode;
            existingClient.ClientName = request.ClientName;
            existingClient.UpdatedDate = DateTime.Now;
            var response = await _unitOfWork.ClientMasterRepository.Update(existingClient);
            await _unitOfWork.Commit();

            return response;
        }


        public async Task<HashSet<string>> DeleteValidation(GetClientByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.ClientMasterRepository.Any(x => x.Id == request.Id);
            if (!IsRecord)
                validationMessage.Add("Record not found for deletetion");
            return validationMessage;
        }

        public async Task Delete(GetClientByIdRequestModel id)
        {
            var clientToDelete = await _unitOfWork.ClientMasterRepository.GetById(id.Id);

            if (clientToDelete != null)
            {
                _unitOfWork.ClientMasterRepository.Delete(clientToDelete);
                await _unitOfWork.Commit();
            }
        }

    }
    public interface IClientMasterDomain
    {
        Task<IEnumerable<GetAllClientResponseModel>> GetAll();
        Task<GetAllClientResponseModel> GetClientById(GetClientByIdRequestModel request);
        Task<HashSet<string>> AddValidation(AddClientRequest request);
        Task<HashSet<string>> UpdateValidation(UpdateClientRequestModel request);
        Task<ClientMaster> Add(AddClientRequest request);
        Task<ClientMaster> Update(UpdateClientRequestModel request);
        Task<HashSet<string>> DeleteValidation(GetClientByIdRequestModel id);
        Task Delete(GetClientByIdRequestModel id);
    }
}
