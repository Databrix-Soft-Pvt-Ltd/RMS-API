using Management.Model.RequestModel;
using Management.Model.ResponseModel;
using Management.Model.RMSEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoWayCommunication.Core.UnitOfWork;
using TwoWayCommunication.Model.Enums;

namespace Management.Services.Masters
{
    public class TemplateMasterDomain : ITemplateMasterDomain
    {


        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        private readonly GlobalUserID _gluID;

        public TemplateMasterDomain(IUnitOfWork unitOfWork, GlobalUserID globalUserID)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
            _gluID = globalUserID;

        }
        public async Task<List<GetAllTemplateResponse>> GetAll()
        {
            var result = (from templateDetail in _unitOfWork.TemplateDetailRepository.AsQueryable()
                          join templateMaster in _unitOfWork.TemplateMasterRepository.AsQueryable()
                          on templateDetail.TempId equals templateMaster.TempId
                          select new GetAllTemplateResponse
                          {
                              Id = templateDetail.Id,
                              TempID = templateDetail.TempId,
                              DisplayName = templateDetail.DisplayName,
                              DatabaseName = templateDetail.DatabaseName,
                              DataType = templateDetail.DataType,
                              MasterValue = templateDetail.MasterValue,
                              MaxLength = templateDetail.MaxLength,
                              IsMandatory = templateDetail.IsMandatory,
                              IsUnique = templateDetail.IsUnique,
                              ControlType = templateDetail.ControlType,
                              DateFormat = templateDetail.DateFormat,
                              Validation = templateDetail.Validation,
                              TempName = templateMaster.TempName,
                              TempDescription = templateMaster.TempDescription
                          }).ToList();

            return result;
        }

        public async Task<List<GetAllTemplateResponse>> GetTemplateById(GEtTemplateByIdRequest request)
        {
            var result = (from templateDetail in _unitOfWork.TemplateDetailRepository.AsQueryable()
                          join templateMaster in _unitOfWork.TemplateMasterRepository.AsQueryable()
                          on templateDetail.TempId equals templateMaster.TempId
                          where templateMaster.TempId == request.Id
                          select new GetAllTemplateResponse
                          {
                              Id = templateDetail.Id,
                              DisplayName = templateDetail.DisplayName,
                              TempID = templateMaster.TempId,
                              DatabaseName = templateDetail.DatabaseName,
                              DataType = templateDetail.DataType,
                              MasterValue = templateDetail.MasterValue,
                              MaxLength = templateDetail.MaxLength,
                              IsMandatory = templateDetail.IsMandatory,
                              IsUnique = templateDetail.IsUnique,
                              ControlType = templateDetail.ControlType,
                              DateFormat = templateDetail.DateFormat,
                              Validation = templateDetail.Validation,
                              TempName = templateMaster.TempName,
                              TempDescription = templateMaster.TempDescription
                          }).ToList();

            return result;
        }

        public async Task<HashSet<string>> AddValidation(GetTemplateName request)
        {
            bool isTemplateExists = await _unitOfWork.TemplateMasterRepository.Any(x => x.TempName.ToLower().Trim() == request.TemplateName.ToLower().Trim());
            if (isTemplateExists)
            {
                validationMessage.Add("Template Already Exists");
            }
            return validationMessage;
        }

        public async Task<HashSet<string>> Add(GetTemplateName request)
        {
            var validationMessage = new HashSet<string>();

            var newTempMst = new TemplateMaster
            {
                TempName = request.TemplateName,
                TempDescription = request.TemplateDescription,
                IsActive = request.IsActive,
                IsCreatedBy = (int)_gluID.GetUserID(),
                IsCreatedDate = DateTime.Now
                
            }; 
            await _unitOfWork.TemplateMasterRepository.Add(newTempMst);
            await _unitOfWork.Commit();

            var IsResult = _unitOfWork.TemplateMasterRepository.AsQueryable().Where(x => x.TempName == request.TemplateName).FirstOrDefault();


            if (IsResult != null)
            {
                int index = 1;
                foreach (var item in request.TemplateDetails)
                {
                    var Ref = "Ref" + index;
                    var newDetails = new TemplateDetail
                    {
                        TempId = IsResult.TempId,  
                        DisplayName = item.DisplayName,
                        DatabaseName = item.DatabaseName,
                        DataType = item.DataType,
                        MaxLength = item.MaxLength,
                        IsMandatory = item.IsMandatory,
                        IsUnique = item.IsUnique,
                        MasterValue = item.MasterValue,
                        ControlType = item.ControlType,
                        DateFormat = item.DateFormat,
                        Validation = item.Validation,
                        IsActive = item.IsActive
                    };
                    await _unitOfWork.TemplateDetailRepository.Add(newDetails);

                    index++;
                }
            }
            else
            {
                validationMessage.Add("Template not found");
            } 
            await _unitOfWork.Commit(); 

            return validationMessage;
        }
        public async Task<HashSet<string>> UpdateValidation(UpdateTemplateRequest request)
        {
            bool isTemplate = await _unitOfWork.TemplateMasterRepository.Any(x => x.TempId == request.Id);
            if (isTemplate)
                validationMessage.Add("0");
            else
                validationMessage.Add("1");

            return validationMessage;
        }

        public async Task<bool> DeleteByTempId(int tempId)
        {
            using (RMS_2024Context dbContext = new RMS_2024Context())
            {
                var detailsToDelete = await dbContext.TemplateDetails.Where(td => td.TempId == tempId).ToListAsync();
                if (detailsToDelete.Any())
                {
                    dbContext.TemplateDetails.RemoveRange(detailsToDelete);
                    await dbContext.SaveChangesAsync();
                }
                return detailsToDelete.Any();
            }
        } 
        public async Task<HashSet<string>> Update(UpdateTemplateRequest request)
        { 
            bool IsDelete = await DeleteByTempId((int)request.Id);

            if (IsDelete)
            {
                var newTempMst = new TemplateMaster
                {
                    TempId = (int)request.Id,
                    TempName = request.TemplateName,
                    TempDescription = request.TemplateDescription,
                    IsActive = request.IsActive,
                    IsCreatedDate = DateTime.Now,
                    IsCreatedBy = (int)_gluID.GetUserID(),
                };
                await _unitOfWork.TemplateMasterRepository.Update(newTempMst);

                foreach (var item in request.TemplateDetails)
                {
                    var newDetails = new TemplateDetail
                    { 
                        TempId = (int)request.Id,
                        DisplayName = item.DisplayName,
                        DatabaseName = item.DatabaseName,
                        DataType = item.DataType,
                        MaxLength = item.MaxLength,
                        IsMandatory = item.IsMandatory,
                        IsUnique = item.IsUnique,
                        MasterValue = item.MasterValue,
                        ControlType = item.ControlType,
                        DateFormat = item.DateFormat,
                        Validation = item.Validation,
                        IsActive = item.IsActive
                    };
                    await _unitOfWork.TemplateDetailRepository.Add(newDetails);
                }
                await _unitOfWork.Commit();

                validationMessage.Add("Template Save successfully");
            }
            else
            { 
                validationMessage.Add("Failed to update template. Template not found or unable to delete existing details.");
            } 
            return validationMessage;
        } 
        public async Task<HashSet<string>> DeleteValidation(GEtTemplateByIdRequest request)
        {
            bool IsRecord = await _unitOfWork.TemplateDetailRepository.Any(x => x.Id == request.Id);
            if (IsRecord)
                validationMessage.Add("0");
            else
                validationMessage.Add("1");
            return validationMessage;
        }

        public async Task<HashSet<string>> Delete(GEtTemplateByIdRequest request)
        {
            

            var templateDetailsToDelete = await _unitOfWork.TemplateDetailRepository.GetById(request.TempDetailsID);

            if (templateDetailsToDelete != null && templateDetailsToDelete.TempId == request.Id)
            {
                _unitOfWork.TemplateDetailRepository.Delete(templateDetailsToDelete);
                await _unitOfWork.Commit();
                validationMessage.Add("Template detail deleted successfully.");
            }
            else
            {
                validationMessage.Add("Failed to delete template detail. Detail not found or does not belong to the specified template.");
            }

            var remainingDetails = await _unitOfWork.TemplateDetailRepository.AsQueryable().Where(td => td.TempId == request.Id).ToListAsync();

            if (!remainingDetails.Any())
            {
                var templateToDelete = await _unitOfWork.TemplateMasterRepository.GetById(request.Id);

                if (templateToDelete != null)
                {
                    _unitOfWork.TemplateMasterRepository.Delete(templateToDelete);
                    await _unitOfWork.Commit();
                    validationMessage.Add("Template master deleted successfully.");
                }
                else
                {
                    validationMessage.Add("Failed to delete template master. Master not found.");
                }
            }
            else
            {
                validationMessage.Add("Details still remain associated with the template. Master not deleted.");
            }

            return validationMessage;
        }

    }
    public interface ITemplateMasterDomain
    {
        Task<List<GetAllTemplateResponse>> GetAll();
        Task<List<GetAllTemplateResponse>> GetTemplateById(GEtTemplateByIdRequest request);
        Task<HashSet<string>> AddValidation(GetTemplateName request);
        Task<HashSet<string>> Add(GetTemplateName request);
        Task<HashSet<string>> UpdateValidation(UpdateTemplateRequest request);
        Task<HashSet<string>> Update(UpdateTemplateRequest request);
        Task<HashSet<string>> DeleteValidation(GEtTemplateByIdRequest request);
        Task<HashSet<string>> Delete(GEtTemplateByIdRequest request);
        Task<bool> DeleteByTempId(int tempId);



    }
}
