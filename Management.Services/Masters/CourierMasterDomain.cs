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

namespace Management.Services.Masters
{

    public class CourierMasterDomain : ICourierMasterDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        public CourierMasterDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
        }

        public async Task<IEnumerable<GetAllCourierResposneModel>> GetAll()
        {
            var query = _unitOfWork.CourierMasterRepository
                          .AsQueryable()
                             .Select(c => new GetAllCourierResposneModel
                             {
                                 Id = c.Id,
                                 CourierName = c.CourierName,


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
            courier.CreatedDate = DateTime.Now;
            var response = await _unitOfWork.CourierMasterRepository.Add(courier);
            await _unitOfWork.Commit();
            return response;
        }

        public async Task<HashSet<string>> UpdateValidation(UpdateCourierRequestModel request)
        {
            bool isClientExists = await _unitOfWork.CourierMasterRepository.Any(x => x.Id != request.Id);
            return validationMessage;
        }

        public async Task<CourierMaster> Update(UpdateCourierRequestModel request)
        {
            var existingClient = new CourierMaster();

            existingClient.Id = request.Id;
            existingClient.CourierName = request.CourierName;
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
        Task<IEnumerable<GetAllCourierResposneModel>> GetAll();
        Task<GetAllCourierResposneModel> GetUserById(GetCourierByIdRequestModel request);
        Task<HashSet<string>> AddValidation(AddCourierRequestModel request);
        Task<HashSet<string>> UpdateValidation(UpdateCourierRequestModel request);
        Task<CourierMaster> Add(AddCourierRequestModel request);
        Task<CourierMaster> Update(UpdateCourierRequestModel request);
        Task<HashSet<string>> DeleteValidation(GetCourierByIdRequestModel id);
        Task Delete(GetCourierByIdRequestModel id);
    }
}

