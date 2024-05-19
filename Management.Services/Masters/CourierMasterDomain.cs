using Management.Model.RequestModel;
using Management.Model.ResponseModel;
using Management.Model.RMSEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TwoWayCommunication.Core.UnitOfWork;
using TwoWayCommunication.Model.Enums;

namespace Management.Services.Masters
{

    public class CourierMasterDomain : ICourierMasterDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        private readonly GlobalUserID _gluID;
        public CourierMasterDomain(IUnitOfWork unitOfWork, GlobalUserID globalUserID)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
            _gluID = globalUserID;
        }

        public async Task<List<GetAllCourierResposneModel>> GetAll()
        {
            var query = _unitOfWork.CourierMasterRepository
                          .AsQueryable()
                             .Select(c => new GetAllCourierResposneModel
                             {
                                 Id = c.Id,
                                 CourierName = c.CourierName,
                                 IsActive=c.IsActive


                             }).ToList();

            return query;
        }

        public async Task<List<GetAllCourierResposneModel>> GetAllIsActiveCourier()
        {
            var query = _unitOfWork.CourierMasterRepository
                          .AsQueryable().Where(x => x.IsActive == true)
                             .Select(c => new GetAllCourierResposneModel
                             {
                                 Id = c.Id,
                                 CourierName = c.CourierName,
                                 IsActive = c.IsActive


                             }).ToList();

            return query;
        }

        public async Task<GetAllCourierResposneModel> GetUserById(GetCourierByIdRequestModel request)
        {
            var user = _unitOfWork.CourierMasterRepository.AsQueryable()
                .Where(r => r.Id == request.Id)
                .Select(c => new GetAllCourierResposneModel
                {
                    Id = c.Id,
                    CourierName = c.CourierName,
                    IsActive=c.IsActive
                })
                .FirstOrDefault();

            return user;
        }


        public async Task<HashSet<string>> AddValidation(AddCourierRequestModel request)
        {
            bool isCourierExists = await _unitOfWork.CourierMasterRepository.Any(x => x.CourierName.ToLower().Trim() == request.CourierName.ToLower().Trim());
            if (isCourierExists)
            {
                validationMessage.Add("Courier Already Exists");
            }
            return validationMessage;
        }


        public async Task<CourierMaster> Add(AddCourierRequestModel request)
        {
            var courier = new CourierMaster();

            courier.CourierName = request.CourierName;
            courier.IsActive = request.IsActive;
            courier.CreatedBy = _gluID.GetUserID();
            courier.CreatedDate = DateTime.Now;
            var response = await _unitOfWork.CourierMasterRepository.Add(courier);
            await _unitOfWork.Commit();
            return response;
        }

        public async Task<HashSet<string>> UpdateValidation(UpdateCourierRequestModel request)
        {
            bool isCourierExists = await _unitOfWork.CourierMasterRepository.Any(x => x.Id == request.Id);
            if (isCourierExists)
                validationMessage.Add("0");
            else
                validationMessage.Add("1");  
            return validationMessage;
        }

        public async Task<CourierMaster> Update(UpdateCourierRequestModel request)
        {
            var existingClient = await _unitOfWork.CourierMasterRepository.GetById(request.Id);
            if (existingClient == null)
            {
                throw new Exception("courier not found");
            }

            existingClient.Id = request.Id;
            existingClient.CourierName = request.CourierName;
            existingClient.IsActive = request.IsActive;
            existingClient.UpdatedBy = _gluID.GetUserID();
            existingClient.UpdatedAt = DateTime.Now;
            var response = await _unitOfWork.CourierMasterRepository.Update(existingClient);
            await _unitOfWork.Commit();

            return response;
        }



        public async Task<HashSet<string>> DeleteValidation(GetCourierByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.CourierMasterRepository.Any(x => x.Id == request.Id);
            if (!IsRecord)
                validationMessage.Add("Record not found for deletetion");
            return validationMessage;
        }

        public async Task Delete(GetCourierByIdRequestModel id)
        {
            var clientToDelete = await _unitOfWork.CourierMasterRepository.GetById(id.Id);

            if (clientToDelete != null)
            {
                _unitOfWork.CourierMasterRepository.Delete(clientToDelete);
                await _unitOfWork.Commit();
            }
        }


    }
    public interface ICourierMasterDomain
    {
        Task<List<GetAllCourierResposneModel>> GetAllIsActiveCourier();
        Task<List<GetAllCourierResposneModel>> GetAll();
        Task<GetAllCourierResposneModel> GetUserById(GetCourierByIdRequestModel request);
        Task<HashSet<string>> AddValidation(AddCourierRequestModel request);
        Task<HashSet<string>> UpdateValidation(UpdateCourierRequestModel request);
        Task<CourierMaster> Add(AddCourierRequestModel request);
        Task<CourierMaster> Update(UpdateCourierRequestModel request);
        Task<HashSet<string>> DeleteValidation(GetCourierByIdRequestModel id);
        Task Delete(GetCourierByIdRequestModel id);
    }
}

