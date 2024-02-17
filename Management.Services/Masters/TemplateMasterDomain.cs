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
    public class TemplateMasterDomain : ITemplateMasterDomain
    {


        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }

        public TemplateMasterDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();

        }
        public async Task<IEnumerable<GetAllTemplateResponse>> GetAll()
        {
            var query = _unitOfWork.TemplateMasterRepository.AsQueryable().
                Select(c => new GetAllTemplateResponse
                {
                    Id = c.Id,
                    DisplayName = c.DisplayName,
                    DatabaseName = c.DatabaseName,
                    DataType = c.DataType,
                    MasterValue = c.MasterValue,
                    MaxLength = c.MaxLength,


                }).ToList();
            return query;
        }

        public async Task<GetAllTemplateResponse> GetTemplateById(GEtTemplateByIdRequest request)
        {
            var user = _unitOfWork.TemplateMasterRepository.AsQueryable()
                .Where(r => r.Id == request.Id)
                .Select(c => new GetAllTemplateResponse
                {
                    Id = c.Id,
                    DisplayName = c.DisplayName,
                    DatabaseName = c.DatabaseName,
                    DataType = c.DataType,
                    MasterValue = c.MasterValue,
                    MaxLength = c.MaxLength,
                })
                .FirstOrDefault();

            return user;
        }

        public async Task<HashSet<string>> AddValidation(AddTemplateRequestModel request)
        {
            bool isTemplateExists = await _unitOfWork.TemplateMasterRepository.Any(x => x.DisplayName.ToLower().Trim() == request.DisplayName.ToLower().Trim());
            if (isTemplateExists)
            {
                validationMessage.Add("Template Already Exists");
            }
            return validationMessage;
        }

        public async Task<TemplateMaster> Add(AddTemplateRequestModel request)
        {
            var newTemplate = new TemplateMaster();

            newTemplate.DisplayName = request.DisplayName;
            newTemplate.DatabaseName = request.DatabaseName;
            newTemplate.DataType = request.DataType;
            newTemplate.MaxLength = request.MaxLength;
            newTemplate.IsMandatory = request.IsMandatory;
            newTemplate.IsUnique = request.IsUnique;
            newTemplate.MasterValue = request.MasterValue;
            newTemplate.ControlType = request.ControlType;
            newTemplate.DateFormat = request.DateFormat;
            newTemplate.Validation = request.Validation;
            // newTemplate.CreatedDate = DateTime.Now;
            var response = await _unitOfWork.TemplateMasterRepository.Add(newTemplate);
            await _unitOfWork.Commit();
            return response;
        }
        public async Task<HashSet<string>> UpdateValidation(UpdateTemplateRequest request)
        {
            bool isTemplate = await _unitOfWork.TemplateMasterRepository.Any(x => x.Id != request.Id);
            if (isTemplate)
            {
                validationMessage.Add("Template doesnot exits Exists");
            }
            return validationMessage;
        }

        public async Task<TemplateMaster> Update(UpdateTemplateRequest request)
        {
            var existingTemplate = await _unitOfWork.TemplateMasterRepository.GetById(request.Id);

            existingTemplate.DisplayName = request.DisplayName;
            existingTemplate.DatabaseName = request.DatabaseName;
            existingTemplate.DataType = request.DataType;
            existingTemplate.MaxLength = request.MaxLength;
            existingTemplate.IsMandatory = request.IsMandatory;
            existingTemplate.IsUnique = request.IsUnique;
            existingTemplate.MasterValue = request.MasterValue;
            existingTemplate.ControlType = request.ControlType;
            existingTemplate.DateFormat = request.DateFormat;
            existingTemplate.Validation = request.Validation;
            var response = await _unitOfWork.TemplateMasterRepository.Update(existingTemplate);
            await _unitOfWork.Commit();

            return response;
        }

        public async Task<HashSet<string>> DeleteValidation(GEtTemplateByIdRequest request)
        {
            bool IsRecord = await _unitOfWork.TemplateMasterRepository.Any(x => x.Id == request.Id);
            if (!IsRecord)
                validationMessage.Add("Record not found for deletetion");
            return validationMessage;
        }

        public async Task Delete(GEtTemplateByIdRequest id)
        {
            var templateToDelete = await _unitOfWork.TemplateMasterRepository.GetById(id.Id);

            if (templateToDelete != null)
            {
                _unitOfWork.TemplateMasterRepository.Delete(templateToDelete);
                await _unitOfWork.Commit();
            }
        }

    }


    public interface ITemplateMasterDomain
    {
        Task<IEnumerable<GetAllTemplateResponse>> GetAll();
        Task<GetAllTemplateResponse> GetTemplateById(GEtTemplateByIdRequest request);
        Task<HashSet<string>> AddValidation(AddTemplateRequestModel request);
        Task<TemplateMaster> Add(AddTemplateRequestModel request);
        Task<HashSet<string>> UpdateValidation(UpdateTemplateRequest request);
        Task<TemplateMaster> Update(UpdateTemplateRequest request);
        Task<HashSet<string>> DeleteValidation(GEtTemplateByIdRequest request);
        Task Delete(GEtTemplateByIdRequest id);


    }
}
