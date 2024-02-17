using Management.Model.RequestModel;
using Management.Model.ResponseModel;
using Management.Model.RMSEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoWayCommunication.Core.UnitOfWork;

namespace Management.Services.Masters
{
    public class PickListMasterDomain : IPickListDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        public PickListMasterDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
        }

        public async Task<IEnumerable<GetAllPickListResponseModel>> GetAll()
        {
            var query = _unitOfWork.PickListMasterRepository
                          .AsQueryable()
                             .Select(c => new GetAllPickListResponseModel
                             {
                                 Id = c.Id,
                                 LogoPath = c.LogoPath,
                                 TemplateId = c.TemplateId,
                                 Address = c.Address

                             }).ToList();

            return query;
        }


        public async Task<GetAllPickListResponseModel> GetPickListById(GetPickListByIdRequestModel request)
        {
            var user = _unitOfWork.PickListMasterRepository.AsQueryable()
                .Where(r => r.Id == request.Id)
                .Select(c => new GetAllPickListResponseModel
                {

                    Id = c.Id,
                    LogoPath = c.LogoPath,
                    TemplateId = c.TemplateId,
                    Address = c.Address

                })
                .FirstOrDefault();

            return user;
        }


        public async Task<HashSet<string>> AddValidation(AddPickListRequestModel request, string Chk)
        {
            validationMessage = new HashSet<string>();

            if (Chk == "PickList")
            {
                bool isUserExists = await _unitOfWork.PickListMasterRepository.Any(x => x.LogoPath.ToLower() == request.LogoPath.ToLower());
                if (!isUserExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            else
            {
                bool checkTemplate = await _unitOfWork.TemplateMasterRepository.Any(x => x.Id == request.TemplateId);
                if (checkTemplate)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            return validationMessage;
        }
        public async Task<PickListMaster> Add(AddPickListRequestModel request)
        {
            var pickList = new PickListMaster();

            pickList.LogoPath = request.LogoPath;
            pickList.TemplateId = request.TemplateId;
            pickList.Address = request.Address;

            // string  userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = await _unitOfWork.PickListMasterRepository.Add(pickList);
            await _unitOfWork.Commit();
            return response;
        }
        public async Task<HashSet<string>> UpdateValidation(UpdatePickListRequest request, string Chk)
        {

            validationMessage = new HashSet<string>();

            if (Chk == "PickList")
            {
                bool isUserExists = await _unitOfWork.PickListMasterRepository.Any(x => x.Id == request.Id);
                if (isUserExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            if (Chk == "LogoPath")
            {
                bool isUserExists = await _unitOfWork.PickListMasterRepository.Any(x => x.LogoPath.ToLower() == request.LogoPath.ToLower());
                if (!isUserExists)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            if (Chk == "Template")
            {
                bool checkTemplate = await _unitOfWork.TemplateMasterRepository.Any(x => x.Id == request.TemplateId);
                if (checkTemplate)
                    validationMessage.Add("0");
                else
                    validationMessage.Add("1");
            }
            return validationMessage;

        }
        public async Task<PickListMaster> Update(UpdatePickListRequest request)
        {
            var pickList = await _unitOfWork.PickListMasterRepository.GetById(request.Id);

            pickList.Id = request.Id;
            pickList.LogoPath = request.LogoPath;
            pickList.TemplateId = request.TemplateId;
            pickList.Address = request.Address;
            // branchMaster.UpdatedAt = DateTime.Now;
            var response = await _unitOfWork.PickListMasterRepository.Update(pickList);
            await _unitOfWork.Commit();

            return response;
        }

        public async Task<HashSet<string>> DeleteValidation(GetPickListByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.PickListMasterRepository.Any(x => x.Id == request.Id);
            if (!IsRecord)
                validationMessage.Add("Record not found for deletetion");
            return validationMessage;
        }

        public async Task Delete(GetPickListByIdRequestModel id)
        {
            var pickList = await _unitOfWork.PickListMasterRepository.GetById(id.Id);

            if (pickList != null)
            {
                _unitOfWork.PickListMasterRepository.Delete(pickList);
                await _unitOfWork.Commit();
            }
        }





    }
    public interface IPickListDomain
    {
        Task<IEnumerable<GetAllPickListResponseModel>> GetAll();
        Task<GetAllPickListResponseModel> GetPickListById(GetPickListByIdRequestModel request);
        Task<HashSet<string>> AddValidation(AddPickListRequestModel request, string Chk);
        Task<HashSet<string>> UpdateValidation(UpdatePickListRequest request, string Chk);
        Task<PickListMaster> Update(UpdatePickListRequest request);
        Task<HashSet<string>> DeleteValidation(GetPickListByIdRequestModel request);
        Task Delete(GetPickListByIdRequestModel id);
        Task<PickListMaster> Add(AddPickListRequestModel request);

    }
}
