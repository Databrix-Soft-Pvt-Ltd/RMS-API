using Management.Model.RequestModel;
using Management.Model.ResponseModel;
using Management.Model.RMSEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
    public class CheckListMasterDomain : ICheckListMasterDomain
    {
        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        private readonly GlobalUserID _gluID;
        public CheckListMasterDomain(IUnitOfWork unitOfWork, GlobalUserID globalUserID)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
            _gluID = globalUserID;
        }

        public async Task<List<GetAllCheckList1RequestModel>> GetAll()
        {
            var query = _unitOfWork.CheckList1Repository
                          .AsQueryable()
                             .Select(c => new GetAllCheckList1RequestModel
                             {
                                 id = c.Id,
                                 CheckListName1 = c.CheckListName1,

                             }).ToList();
            return query;
        }

        public async Task<CheckList1> Add(AddCheckList1 request)
        {
            var chklst = new CheckList1();
            chklst.CheckListName1 = request.CheckListName1;
            var response = await _unitOfWork.CheckList1Repository.Add(chklst);
            await _unitOfWork.Commit();
            return response;
        }

        public async Task<HashSet<string>> AddValidation(AddCheckList1 request)
        {
            bool ischkList = await _unitOfWork.CheckList1Repository.Any(x => x.CheckListName1.ToLower().Trim() == request.CheckListName1.ToLower().Trim());
            if (ischkList)
            {
                validationMessage.Add("Check List 1 Already Exists");
            }
            return validationMessage;
        }
        public async Task<HashSet<string>> UpdateValidation(AUpdateCheckList1 request)
        {
            bool ischkList = await _unitOfWork.CheckList1Repository.Any(x => x.Id != request.id && x.CheckListName1.ToLower().Trim() == request.CheckListName1.ToLower().Trim());
            if (ischkList)
            {
                validationMessage.Add("Check List 1 doesnot exits Exists");
            }
            return validationMessage;
        }
        public async Task<CheckList1> Update(AUpdateCheckList1 request)
        {
            var ChkList = await _unitOfWork.CheckList1Repository.GetById((int)request.id);
            if (ChkList == null)
            {
                throw new Exception("CheckList not found");
            }
            ChkList.CheckListName1 = request.CheckListName1;

            var response = await _unitOfWork.CheckList1Repository.Update(ChkList);
            await _unitOfWork.Commit();

            return response;
        }

        public async Task<HashSet<string>> DeleteValidation(GetChkListByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.CheckList1Repository.Any(x => x.Id == (int)request.id);
            if (!IsRecord)
                validationMessage.Add("Record not found for deletetion");
            return validationMessage;
        }

        public async Task Delete(GetChkListByIdRequestModel id)
        {
            var clientToDelete = await _unitOfWork.CheckList1Repository.GetById((int)id.id);

            if (clientToDelete != null)
            {
                _unitOfWork.CheckList1Repository.Delete(clientToDelete);
                await _unitOfWork.Commit();
            }
        }

        /// <summary>
        ///  CheckList 2
        /// </summary>

        public async Task<List<GetAllCheckList2RequestModel>> GetAllCheckList2()
        {
            var query = from C1 in _unitOfWork.CheckList2Repository.AsQueryable()
                        join C2 in _unitOfWork.CheckList1Repository.AsQueryable()
                        on C1.CheckListId1 equals C2.Id
                        select new GetAllCheckList2RequestModel
                        {
                            id = C1.Id,
                            CheckListName2 = C1.CheckListName2,
                            CheckListID1 = C1.CheckListId1,
                            CheckListName1 = C2.CheckListName1
                            // Include properties from checklist1 if needed
                            // For example: CheckListName1 = checklist1.CheckLi2stName1
                        };

            return await query.ToListAsync();
        }
        public async Task<List<GetAllCheckList2RequestModel>> GetAllCheckList2ByID(long id)
        {
            var query = _unitOfWork.CheckList2Repository
                          .AsQueryable()
                          .Where(c => c.CheckListId1 == id)  // Add the where condition here
                          .Select(c => new GetAllCheckList2RequestModel
                          {
                              id = c.Id,
                              CheckListName2 = c.CheckListName2
                          })
                          .ToList();
            return query;
        }

        public async Task<CheckList2> AddChkList2(AddCheckList2 request)
        {
            var chk = new CheckList2();
            chk.CheckListId1 = (int?)(request.CheckListID1);
            chk.CheckListName2 = request.CheckListName2;
            chk.CreatedDate = DateTime.Now;
            chk.CreatedBy = (int?)_gluID.GetUserID();
            var response = await _unitOfWork.CheckList2Repository.Add(chk);
            await _unitOfWork.Commit();
            return response;
        }

        public async Task<HashSet<string>> AddValidationChkList2(AddCheckList2 request)
        {
            bool ischkList = await _unitOfWork.CheckList2Repository.Any(x => x.CheckListName2.ToLower().Trim() == request.CheckListName2.ToLower().Trim() && x.CheckListId1 == request.CheckListID1);
            if (ischkList)
            {
                validationMessage.Add("Check List 2 Already Exists");
            }
            return validationMessage;
        }

        public async Task<HashSet<string>> UpdateChkList2Validation(UpdateCheckList2 request)
        {
            bool ischkList = await _unitOfWork.CheckList2Repository.Any(x => x.Id != request.id && x.CheckListName2.ToLower().Trim() == request.CheckListName2.ToLower().Trim());
            if (ischkList)
            {
                validationMessage.Add("Check List 2 doesnot exits Exists");
            }
            return validationMessage;
        }

        public async Task<CheckList2> UpdateChkList2(UpdateCheckList2 request)
        {
            var ChkList = await _unitOfWork.CheckList2Repository.GetById((int)request.id);
            if (ChkList == null)
            {
                throw new Exception("CheckList not found");
            }
            ChkList.CheckListName2 = request.CheckListName2;
            ChkList.CheckListId1 = (int?)request.CheckListID1;

            var response = await _unitOfWork.CheckList2Repository.Update(ChkList);
            await _unitOfWork.Commit();
            return response;
        }


        //DeleteValidationChkList2
        public async Task<HashSet<string>> DeleteValidationChkList2(GetChkListByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.CheckList2Repository.Any(x => x.Id == (int)request.id);
            if (!IsRecord)
                validationMessage.Add("Record not found for deletetion");
            return validationMessage;
        }

        public async Task DeleteChkList2(GetChkListByIdRequestModel id)
        {
            var clientToDelete = await _unitOfWork.CheckList2Repository.GetById((int)id.id);

            if (clientToDelete != null)
            {
                _unitOfWork.CheckList2Repository.Delete(clientToDelete);
                await _unitOfWork.Commit();
            }
        }



        // Check list -3 

        public async Task<List<GetAllCheckList3RequestModel>> GetAllCheckList3()
        {
            var query = from C1 in _unitOfWork.CheckList2Repository.AsQueryable()
                        join C2 in _unitOfWork.CheckList1Repository.AsQueryable() on C1.CheckListId1 equals C2.Id
                        join C3 in _unitOfWork.CheckList3Repository.AsQueryable() on C1.Id equals C3.CheckListId2 // Modify the join condition as needed
                        select new GetAllCheckList3RequestModel
                        {
                            id = C3.Id,
                            CheckListName2 = C1.CheckListName2,
                            CheckListName1 = C2.CheckListName1,
                            CheckListName3 = C3.CheckListName3,
                            CheckListID2 = C1.Id
                            // Include properties from CheckList3 if needed
                            // For example: CheckListName3 = C3.CheckListName3
                        };

            return await query.ToListAsync();
        }

        public async Task<List<GetAllCheckList3RequestModel>> GetAllCheckList3ByID(long id)
        {
            var query = from C1 in _unitOfWork.CheckList2Repository.AsQueryable()
                        join C2 in _unitOfWork.CheckList1Repository.AsQueryable() on C1.CheckListId1 equals C2.Id
                        join C3 in _unitOfWork.CheckList3Repository.AsQueryable() on C1.Id equals C3.CheckListId2 // Modify the join condition as needed
                        where C1.Id == id
                        select new GetAllCheckList3RequestModel
                        {
                            id = C3.Id,
                            CheckListName2 = C1.CheckListName2,
                            CheckListName1 = C2.CheckListName1,
                            CheckListName3 = C3.CheckListName3,
                            // Include properties from CheckList3 if needed
                            // For example: CheckListName3 = C3.CheckListName3
                        };

            return await query.ToListAsync();
        }


        public async Task<HashSet<string>> AddValidationChkList3(AddCheckList3 request)
        {
            bool ischkList = await _unitOfWork.CheckList3Repository.Any(x => x.CheckListName3.ToLower().Trim() == request.CheckListName3.ToLower().Trim() && x.CheckListId2 == request.CheckListID2);
            if (ischkList)
            {
                validationMessage.Add("Check List 3 Already Exists");
            }
            return validationMessage;
        }

        public async Task<CheckList3> AddChkList3(AddCheckList3 request)
        {
            var chk = new CheckList3();
            chk.CheckListId2 = (int?)(request.CheckListID2);
            chk.CheckListName3 = request.CheckListName3;
            chk.CreatedDate = DateTime.Now;
            chk.CreatedBy = (int?)_gluID.GetUserID();
            var response = await _unitOfWork.CheckList3Repository.Add(chk);
            await _unitOfWork.Commit();
            return response;
        }
        public async Task<HashSet<string>> DeleteValidationChkList3(GetChkListByIdRequestModel request)
        {
            bool IsRecord = await _unitOfWork.CheckList3Repository.Any(x => x.Id == request.id);
            if (!IsRecord)
                validationMessage.Add("Record not found for deletetion");
            return validationMessage;
        }

        public async Task DeleteChkList3(GetChkListByIdRequestModel id)
        {
            var clientToDelete = await _unitOfWork.CheckList3Repository.GetById((int)id.id);

            if (clientToDelete != null)
            {
                _unitOfWork.CheckList3Repository.Delete(clientToDelete);
                await _unitOfWork.Commit();
            }
        }


    }

    public interface ICheckListMasterDomain
    {
        Task<List<GetAllCheckList1RequestModel>> GetAll();
        Task<HashSet<string>> AddValidation(AddCheckList1 request);
        Task<CheckList1> Add(AddCheckList1 request);
        Task<HashSet<string>> UpdateValidation(AUpdateCheckList1 request);
        Task<CheckList1> Update(AUpdateCheckList1 request);
        Task<HashSet<string>> DeleteValidation(GetChkListByIdRequestModel id);
        Task Delete(GetChkListByIdRequestModel id);



        Task<List<GetAllCheckList2RequestModel>> GetAllCheckList2();
        Task<HashSet<string>> AddValidationChkList2(AddCheckList2 request);
        Task<CheckList2> AddChkList2(AddCheckList2 request);
        Task<HashSet<string>> UpdateChkList2Validation(UpdateCheckList2 request);
        Task<CheckList2> UpdateChkList2(UpdateCheckList2 request);

        Task<List<GetAllCheckList2RequestModel>> GetAllCheckList2ByID(long id);

        Task DeleteChkList2(GetChkListByIdRequestModel id);
        Task<HashSet<string>> DeleteValidationChkList2(GetChkListByIdRequestModel id);

        // Check List 3

        // GetAllCheckList3

        Task<List<GetAllCheckList3RequestModel>> GetAllCheckList3();
        Task<HashSet<string>> AddValidationChkList3(AddCheckList3 request);
        Task<CheckList3> AddChkList3(AddCheckList3 request);
        Task<List<GetAllCheckList3RequestModel>> GetAllCheckList3ByID(long id);
        Task<HashSet<string>> DeleteValidationChkList3(GetChkListByIdRequestModel id);
        Task DeleteChkList3(GetChkListByIdRequestModel id);

    }

}
