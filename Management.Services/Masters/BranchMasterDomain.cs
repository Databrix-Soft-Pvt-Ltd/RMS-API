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

    public class BranchMasterDomain : IBranchMasterDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        private readonly GlobalUserID _gluID;
        public BranchMasterDomain(IUnitOfWork unitOfWork,GlobalUserID globalUserID)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
            _gluID = globalUserID;
        }

        public async Task<List<GetAllBranchResponseModel>> GetAll()
        {
            var query = _unitOfWork.BranchMasterRepository
                          .AsQueryable()
                             .Select(c => new GetAllBranchResponseModel
                             {
                                 Id = c.Id,
                                 BranchCode = c.BranchCode,
                                 BranchName = c.BranchName,
                                 Address = c.Address,
                                 IsActive=c.IsActive


                             }).ToList();

            return query;
        }

        public async Task<List<GetAllBranchResponseModel>> GetAllIsActiveBranch()
        {
            var query = _unitOfWork.BranchMasterRepository
                          .AsQueryable().Where(x => x.IsActive == true)
                             .Select(c => new GetAllBranchResponseModel
                             {
                                 Id = c.Id,
                                 BranchCode = c.BranchCode,
                                 BranchName = c.BranchName,
                                 Address = c.Address,
                                 IsActive = c.IsActive


                             }).ToList();

            return query;
        }

        public async Task<GetAllBranchResponseModel> GetBranchById(GetBranchByIdRequestModel request)
        {
            var user = _unitOfWork.BranchMasterRepository.AsQueryable()
                .Where(r => r.Id == request.Id)
                .Select(c => new GetAllBranchResponseModel
                {
                    Id = c.Id,
                    BranchCode = c.BranchCode,
                    BranchName = c.BranchName,
                    Address = c.Address,
                    IsActive = c.IsActive

                })
                .FirstOrDefault();

            return user;
        }



        public async Task<HashSet<string>> AddValidation(AddBranchRequestModel request)
        {
            bool isBranchExists = await _unitOfWork.BranchMasterRepository.Any(x => x.BranchName.ToLower().Trim() == request.BranchName.ToLower().Trim());
            if (isBranchExists)
            {
                validationMessage.Add("Branch Already Exists");
            }
            return validationMessage;
        }
        public async Task<HashSet<string>> UpdateValidation(UpdateBranchRequestModel request)
        {
            bool isClientExists = await _unitOfWork.BranchMasterRepository.Any(x => x.Id != request.Id && x.BranchName.ToLower().Trim() == request.BranchName.ToLower().Trim());
            if (isClientExists)
            {
                validationMessage.Add("Branch doesnot exits Exists");
            }
            return validationMessage;
        }

        public async Task<BranchMaster> Add(AddBranchRequestModel request)
        {
            var branch = new BranchMaster();
            branch.BranchName = request.BranchName;
            branch.BranchCode = request.BranchCode;
            branch.Address = request.Address;
            branch.IsActive = request.IsActive;
            branch.CreatedDate = DateTime.Now;
            branch.CreatedBy = _gluID.GetUserID();
            var response = await _unitOfWork.BranchMasterRepository.Add(branch);
            await _unitOfWork.Commit();
            return response;
        }


        public async Task<BranchMaster> Update(UpdateBranchRequestModel request)
        {
            var branchMaster = await _unitOfWork.BranchMasterRepository.GetById(request.Id);
            if (branchMaster == null)
            {
                throw new Exception("Client not found");
            }
            branchMaster.Id = request.Id;
            branchMaster.BranchName = request.BranchName;
            branchMaster.BranchCode = request.BranchCode;
            branchMaster.Address = request.Address;
            branchMaster.IsActive = request.IsActive;
            branchMaster.UpdatedDate = DateTime.Now;
            branchMaster.UpdatedBy = _gluID.GetUserID();

            var response = await _unitOfWork.BranchMasterRepository.Update(branchMaster);
            await _unitOfWork.Commit();

            return response;
        }


        public async Task<HashSet<string>> DeleteValidation(GetBranchByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.BranchMasterRepository.Any(x => x.Id == request.Id);
            if (!IsRecord)
                validationMessage.Add("Record not found for deletetion");
            return validationMessage;
        }

        public async Task Delete(GetBranchByIdRequestModel id)
        {
            var clientToDelete = await _unitOfWork.BranchMasterRepository.GetById(id.Id);

            if (clientToDelete != null)
            {
                _unitOfWork.BranchMasterRepository.Delete(clientToDelete);
                await _unitOfWork.Commit();
            }
        }

    }
    public interface IBranchMasterDomain
    {
        Task<List<GetAllBranchResponseModel>> GetAll();
        Task<List<GetAllBranchResponseModel>> GetAllIsActiveBranch();
        Task<GetAllBranchResponseModel> GetBranchById(GetBranchByIdRequestModel request);
        Task<HashSet<string>> AddValidation(AddBranchRequestModel request);
        Task<HashSet<string>> UpdateValidation(UpdateBranchRequestModel request);
        Task<BranchMaster> Add(AddBranchRequestModel request);
        Task<BranchMaster> Update(UpdateBranchRequestModel request);
        Task<HashSet<string>> DeleteValidation(GetBranchByIdRequestModel id);
        Task Delete(GetBranchByIdRequestModel id);
    }

}

