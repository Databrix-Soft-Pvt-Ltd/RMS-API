using ExcelDataReader;
using Management.Model.RequestModel;
using Management.Model.ResponseModel;
using Management.Model.RMSEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TwoWayCommunication.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using TwoWayCommunication.Model.Enums;

namespace Management.Services.Masters
{
    public class Retrivel_RequestDomain : IRetrivel_RequestDomain
    {


        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        private RetrievalTransaction RetTransaction { get; set; }
        private RetrievalTranHistory RrnHist { get; set; }
        public List<AddRetrivel_RequestRequestModel> DumpLIstData { get; set; }

        private List<TemplateMaster> ListTemp { get; set; }

        private readonly GlobalUserID _gluid;
         

        public Retrivel_RequestDomain(IUnitOfWork unitOfWork, GlobalUserID globalUserID)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
            this._gluid = globalUserID;

        }
        public async Task<HashSet<string>> AddValidation_GenRR()
        {
            var validationMessage = new HashSet<string>();

            var isRetrievalRequest = _unitOfWork.RetrievalRequestGenRepository
            .AsQueryable()
            .Where(x => x.IsCreatedBy == _gluid.GetUserID())
            .OrderByDescending(rrg => rrg.Id) // Assuming there's an Id field that uniquely identifies records
            .FirstOrDefault();

            if (isRetrievalRequest != null && !string.IsNullOrWhiteSpace(isRetrievalRequest.ReqNumber))
            {
                if (isRetrievalRequest.Status == "N")
                {
                    validationMessage.Add(isRetrievalRequest.ReqNumber);
                }
                else
                {
                    var maxKVALoad = _unitOfWork.RetrievalRequestGenRepository
                        .AsQueryable()
                        .OrderByDescending(l => l.ReqNumber)
                        .FirstOrDefault();

                    if (maxKVALoad != null)
                    {
                        var parts = maxKVALoad.ReqNumber.Split("-");

                        if (parts.Length > 1 && int.TryParse(parts[1], out int number))
                        {
                            var newNumber = string.Format("REQ-{0:0000}", number + 1);
                            validationMessage.Add(newNumber);
                        }
                    }
                }
            }
            else
            {
                var maxKVALoad = _unitOfWork.RetrievalRequestGenRepository
                    .AsQueryable()
                    .OrderByDescending(l => l.ReqNumber)
                    .FirstOrDefault();

                if (maxKVALoad != null)
                {
                    var parts = maxKVALoad.ReqNumber.Split("-");
                    if (parts.Length > 1 && int.TryParse(parts[1], out int number))
                    {
                        var newNumber = string.Format("REQ-{0:0000}", number + 1);
                        validationMessage.Add(newNumber);
                    }
                }
                else
                {
                    validationMessage.Add("REQ-0001");
                }
            }

            return validationMessage;
        }
        public async Task<List<GetAllRetrivelRequestModel>> GetRetrivelDetailsByReqNumber(string ReqNo)
        {
            RMS_2024Context dbContext = new RMS_2024Context();

            var result = (from a in dbContext.RetrievalTransactions
                          join b in dbContext.DumpUploads on a.Ref1 equals b.Ref1
                          join c in dbContext.StatusMasters on a.FileStatus equals c.Id
                          where a.ReqNumber == ReqNo
                          select new GetAllRetrivelRequestModel
                          {
                              ReqNumber = a.ReqNumber,
                              Retrieval_Type = a.RetrievalType,
                              Retrieval_region = a.RetrievalRegion,
                              File_Status = c.Status,
                              Ref1 = a.Ref1,
                              Ref2 = b.Ref2,
                              Ref3 = b.Ref3,
                              Ref4 = b.Ref4,
                              Ref5 = b.Ref5,
                              Ref6 = b.Ref6,
                              Ref7 = b.Ref7,
                              Ref8 = b.Ref8,
                              Ref9 = b.Ref9,
                              Ref10 = b.Ref10,
                              Ref11 = b.Ref11,
                              Ref12 = b.Ref12,
                              Ref13 = b.Ref13,
                              Ref14 = b.Ref14,
                              Ref15 = b.Ref15,
                              Ref16 = b.Ref16,
                              Ref17 = b.Ref17,
                              Ref18 = b.Ref18,
                              Ref19 = b.Ref19,
                              Ref20 = b.Ref20,
                              Ref21 = b.Ref21,
                              Ref22 = b.Ref22,
                              Ref23 = b.Ref23,  
                              Ref24 = b.Ref24,
                              Ref25 = b.Ref25,
                              Ref26 = b.Ref26,
                              Ref27 = b.Ref27,
                              Ref28 = b.Ref28,
                              Ref29 = b.Ref29,
                              Ref30 = b.Ref30,
                              remarks=a.Remarks,
                          }).ToList();

            return result;

        }

        public async Task<List<GetAllRetrivelRequestModel>> GetRetrivelDetails_For_Dispath_ByReqNumber(string ReqNo)
        {
            RMS_2024Context dbContext = new RMS_2024Context();

            var result = (from a in dbContext.RetrievalTransactions
                          join b in dbContext.DumpUploads on a.Ref1 equals b.Ref1
                          where a.ReqNumber == ReqNo && a.ApprovalDate != null
                          select new GetAllRetrivelRequestModel
                          {
                              ReqNumber = a.ReqNumber,
                              Retrieval_Type = a.RetrievalType,
                              Retrieval_region = a.RetrievalRegion,
                              File_Status = a.ItemStatus,
                              Ref1 = a.Ref1,
                              Ref2 = b.Ref2,
                              Ref3 = b.Ref3,
                              Ref4 = b.Ref4,
                              Ref5 = b.Ref5,
                              Ref6 = b.Ref6,
                              Ref7 = b.Ref7,
                              Ref8 = b.Ref8,
                              Ref9 = b.Ref9,
                              Ref10 = b.Ref10,
                              Ref11 = b.Ref11,
                              Ref12 = b.Ref12,
                              Ref13 = b.Ref13,
                              Ref14 = b.Ref14,
                              Ref15 = b.Ref15,
                              Ref16 = b.Ref16,
                              Ref17 = b.Ref17,
                              Ref18 = b.Ref18,
                              Ref19 = b.Ref19,
                              Ref20 = b.Ref20,
                              Ref21 = b.Ref21,
                              Ref22 = b.Ref22,
                              Ref23 = b.Ref23,
                              Ref24 = b.Ref24,
                              Ref25 = b.Ref25,
                              Ref26 = b.Ref26,
                              Ref27 = b.Ref27,
                              Ref28 = b.Ref28,
                              Ref29 = b.Ref29,
                              Ref30 = b.Ref30
                          }).ToList();

            return result;

        }

        public async Task<List<DumpUpload>> GetRetrivelDetailsByLenNumber(string Ref1)
        {
            RMS_2024Context dbContext = new RMS_2024Context();

            var result = (
                          from b in dbContext.DumpUploads
                          where b.Ref1 == Ref1 && b.ItemStatus == "IN"
                          select new DumpUpload
                          {

                              Ref1 = b.Ref1,
                              Ref2 = b.Ref2,
                              Ref3 = b.Ref3,
                              Ref4 = b.Ref4,
                              Ref5 = b.Ref5,
                              Ref6 = b.Ref6,
                              Ref7 = b.Ref7,
                              Ref8 = b.Ref8,
                              Ref9 = b.Ref9,
                              Ref10 = b.Ref10,
                              Ref11 = b.Ref11,
                              Ref12 = b.Ref12,
                              Ref13 = b.Ref13,
                              Ref14 = b.Ref14,
                              Ref15 = b.Ref15,
                              Ref16 = b.Ref16,
                              Ref17 = b.Ref17,
                              Ref18 = b.Ref18,
                              Ref19 = b.Ref19,
                              Ref20 = b.Ref20,
                              Ref21 = b.Ref21,
                              Ref22 = b.Ref22,
                              Ref23 = b.Ref23,
                              Ref24 = b.Ref24,
                              Ref25 = b.Ref25,
                              Ref26 = b.Ref26,
                              Ref27 = b.Ref27,
                              Ref28 = b.Ref28,
                              Ref29 = b.Ref29,
                              Ref30 = b.Ref30,
                              Status = b.Status,
                              UploadBy = b.UploadBy,
                              ItemStatus = b.ItemStatus

                          }).ToList();

            return result;

        }

        public async Task<HashSet<string>> CheckRetrievalValidationAndInsert(RequestRetrivelTransaction requestRetrievalTransaction)
        {
            var validationMessage = new HashSet<string>();

            try
            {
                if (requestRetrievalTransaction.ReqNumber != null)
                {
                    bool isReqNumberExists = await _unitOfWork.RetrievalRequestGenRepository.Any(x => x.ReqNumber == requestRetrievalTransaction.ReqNumber);

                    if (isReqNumberExists)
                    {
                        if (requestRetrievalTransaction.ListRef1.Length > 0)
                        {
                            foreach (var ref1 in requestRetrievalTransaction.ListRef1)
                            {
                                var checkValidRef1 = _unitOfWork.DumpUploadMasterRepository
                                    .AsQueryable()
                                    .FirstOrDefault(x => x.Ref1 == ref1 && x.ItemStatus.TrimEnd() == "IN");

                                if (checkValidRef1 != null)
                                {
                                    bool isValidationCheck = await _unitOfWork.RetrievalRetrievalTransactionRepository.Any(x => x.Ref1 == ref1 && x.ReqNumber == requestRetrievalTransaction.ReqNumber);

                                    if (!isValidationCheck)
                                    {
                                        // Add Retrieval...
                                        var newRetrievalTransaction = new RetrievalTransaction
                                        {
                                            ReqNumber = requestRetrievalTransaction.ReqNumber,
                                            Ref1 = ref1,
                                            RetrievalType = requestRetrievalTransaction.RetrievalType,
                                            RetrievalRegion = requestRetrievalTransaction.RetrievalRegion,
                                            Remarks = requestRetrievalTransaction.Remarks,
                                            FileStatus = 1,
                                            Status = "Partial Retrieval",
                                            ItemStatus = "IN",
                                            RequestDate = DateTime.Now,
                                            RequestBy = (int)_gluid.GetUserID(),
                                            //ClosedDateTime = DateTime.Now,
                                            //ClosedBy = 246412

                                        };
                                        await _unitOfWork.RetrievalRetrievalTransactionRepository.Add(newRetrievalTransaction);

                                        // Update History Table
                                        var objRetrievalTranHistory = new RetrievalTranHistory
                                        {
                                            Type = "Retrieval",
                                            ReqNumber = requestRetrievalTransaction.ReqNumber,
                                            Ref1 = ref1,
                                            FileStatus = "1",
                                            Status = "Partial Retrieval",
                                            ProcessDate = DateTime.Now,
                                            ProcessBy = (int)_gluid.GetUserID(),
                                            Remarks = requestRetrievalTransaction.Remarks
                                        };

                                        var RetrivelTranGen = _unitOfWork.RetrievalRequestGenRepository
                                       .AsQueryable()
                                       .Where(r => r.ReqNumber == requestRetrievalTransaction.ReqNumber)
                                       .FirstOrDefault();

                                        //if (RetrivelTranGen != null)
                                        //{
                                        //    RetrivelTranGen.IsClosedDate = DateTime.Now;
                                        //    RetrivelTranGen.Status = "Y";
                                        //    RetrivelTranGen.IsClosedby = 5487;
                                        //    _unitOfWork.RetrievalRequestGenRepository.Update(RetrivelTranGen);
                                        //}
                                        //await _unitOfWork.RetrievalRetrievalTranHistory.Add(objRetrievalTranHistory);

                                        await _unitOfWork.Commit();

                                        validationMessage.Add($"Retrieval saved successfully!");
                                    }
                                }
                                else
                                {
                                    validationMessage.Add($"Ref1 {ref1} not found for Retrieval transaction or item is not 'IN'");
                                }
                            }
                        }
                        else
                        {
                            validationMessage.Add("List of Ref1 are not found");
                        }
                    }
                    else
                    {
                        validationMessage.Add("Invalid ReqNumber for Retrieval transaction");
                    }
                }
                else
                {
                    validationMessage.Add("REQNumber is null for Retrieval transaction");
                }
            }
            catch (Exception ex)
            {
                validationMessage.Add($"Error: {ex.Message}");
            }
            return validationMessage;
        }

        public async Task<HashSet<string>> Retrivel_Process(RetrivelProcess_Model RP_Model)
        {
            HashSet<string> validationMessage = new HashSet<string>();
            bool IsValid = false;

            if (!string.IsNullOrEmpty(RP_Model.Process_Type) && RP_Model.Process_Type != "string")
            {

                if (RP_Model.Process_Type != "Closed")
                {
                    if (!string.IsNullOrEmpty(RP_Model.REQNumber) && RP_Model.REQNumber != "string")
                    {

                        var updateData = _unitOfWork.RetrievalRetrievalTransactionRepository
                                                    .AsQueryable()
                                                    .Where(r => r.ReqNumber == RP_Model.REQNumber)
                                                    .AsEnumerable() // Switch to LINQ to Objects
                                                    .Where(r => RP_Model.Ref1.Any(item => r.Ref1.Contains(item)))
                                                    .ToList();

                        if (updateData != null && updateData.Count > 0)
                        {
                            int C_Count = 0;
                            int Check_Count = 0;
                            int IsDispatch = 0;
                            int IsApproved = 0;

                            foreach (var item in updateData)
                            {
                                bool IsCheckClosed = await _unitOfWork.RetrievalRetrievalTransactionRepository
                                    .Any(r => r.ReqNumber == RP_Model.REQNumber && r.Ref1 == item.Ref1 && r.ClosedDateTime != null && item.Ref1 == r.Ref1);

                                C_Count = updateData.Count;

                                switch (RP_Model.Process_Type)
                                {
                                    case "Approved":
                                        if (IsCheckClosed)
                                        {
                                            var RetrivelTran = _unitOfWork.RetrievalRetrievalTransactionRepository
                                                .AsQueryable()
                                                .Where(r => r.Ref1 == item.Ref1 && r.ReqNumber == RP_Model.REQNumber && item.ApprovalDate == null && item.Ref1 == r.Ref1)
                                                .FirstOrDefault();

                                            if (RetrivelTran != null && RetrivelTran.FileStatus != 2)
                                            {
                                                Check_Count++;

                                                RetrivelTran.FileStatus = 2;
                                                //RetrivelTran.Status = "Approved";
                                                RetrivelTran.ApprovalDate = DateTime.Now;
                                                RetrivelTran.ApprovalBy = (int)_gluid.GetUserID(); //Convert.ToInt32(RP_Model.IsProcessBy);
                                                _unitOfWork.RetrievalRetrievalTransactionRepository.Update(RetrivelTran);

                                                // Update History-Updated for Approval
                                                var RetTranHisty1 = new RetrievalTranHistory()
                                                {
                                                    ReqNumber = item.ReqNumber,
                                                    Ref1 = item.Ref1,
                                                    Type = "Retrivel",
                                                    FileStatus = "2",
                                                    Status = "Approved",
                                                    ProcessDate = DateTime.Now,
                                                    ProcessBy = (int)_gluid.GetUserID(),//Convert.ToInt32(RP_Model.IsProcessBy),
                                                    Remarks = item.Remarks
                                                };
                                                _unitOfWork.RetrievalRetrievalTranHistory.Add(RetTranHisty1);

                                                IsValid = true;
                                            }
                                            else
                                            {
                                                IsValid = false;
                                                // validationMessage.Add("Retrivel's is already " + RP_Model.Process_Type);
                                            }
                                        }
                                        else
                                        {
                                            IsValid = false;
                                            validationMessage.Add("Retrivel's cannot be closed as it is already closed.");
                                        }
                                        break;
                                    case "Rejected":
                                        if (IsCheckClosed)
                                        {
                                            var RetrivelTran = _unitOfWork.RetrievalRetrievalTransactionRepository
                                                .AsQueryable()
                                                .Where(r => r.Ref1 == item.Ref1 && r.ReqNumber == RP_Model.REQNumber && r.ApprovalDate == null && r.DispatchDate == null)
                                                .FirstOrDefault();

                                            if (RetrivelTran != null)
                                            {
                                                RetrivelTran.FileStatus = 3;
                                                //  RetrivelTran.Status = "Rejected";
                                                RetrivelTran.RejectDate = DateTime.Now;
                                                RetrivelTran.RejectBy = (int)_gluid.GetUserID();//Convert.ToInt32(RP_Model.IsProcessBy);
                                                _unitOfWork.RetrievalRetrievalTransactionRepository.Update(RetrivelTran);

                                                // Update History-Updated for Rejection
                                                var RetTranHisty1 = new RetrievalTranHistory()
                                                {
                                                    ReqNumber = item.ReqNumber,
                                                    Ref1 = item.Ref1,
                                                    Type = "Retrivel",
                                                    FileStatus = "3",
                                                    Status = "Rejected",
                                                    ProcessDate = DateTime.Now,
                                                    ProcessBy = (int)_gluid.GetUserID(),//Convert.ToInt32(RP_Model.IsProcessBy),
                                                    Remarks = item.Remarks
                                                };
                                                _unitOfWork.RetrievalRetrievalTranHistory.Add(RetTranHisty1);

                                                IsValid = true;
                                            }
                                            else
                                            {
                                                IsValid = false;
                                                validationMessage.Add("Retrivel's has already been" + RP_Model.Process_Type + " Or  Approved or Dispatch");
                                            }
                                        }
                                        else
                                        {
                                            IsValid = false;
                                            validationMessage.Add("Retrivel's is not " + RP_Model.Process_Type + " due to not Closed");
                                        }
                                        break;
                                    case "Closed":
                                        if (!IsCheckClosed)
                                        {
                                            var RetrivelTran = _unitOfWork.RetrievalRetrievalTransactionRepository
                                                .AsQueryable()
                                                .Where(r => r.Ref1 == item.Ref1 && r.ReqNumber == RP_Model.REQNumber)
                                                .FirstOrDefault();

                                            if (RetrivelTran != null)
                                            {

                                                RetrivelTran.Status = "Retrieval Request";
                                                RetrivelTran.ClosedDateTime = DateTime.Now;
                                                RetrivelTran.ClosedBy = (int)_gluid.GetUserID();  //Convert.ToInt32(RP_Model.IsProcessBy);
                                                _unitOfWork.RetrievalRetrievalTransactionRepository.Update(RetrivelTran);

                                                // Update History-Updated for Closure
                                                var RetrivelTranHistory = new RetrievalTranHistory()
                                                {
                                                    ReqNumber = item.ReqNumber,
                                                    Ref1 = item.Ref1,
                                                    Type = "Retrivel",
                                                    FileStatus = "1",
                                                    Status = "Retrieval Request",
                                                    ProcessDate = DateTime.Now,
                                                    ProcessBy = (int)_gluid.GetUserID(),//Convert.ToInt32(RP_Model.IsProcessBy),
                                                    Remarks = item.Remarks
                                                };
                                                _unitOfWork.RetrievalRetrievalTranHistory.Add(RetrivelTranHistory);



                                                IsValid = true;
                                            }
                                            else
                                            {
                                                IsValid = false; // Considered valid if already closed
                                                validationMessage.Add("Retrivel's is already Closed");
                                            }
                                        }
                                        else
                                        {
                                            IsValid = false;
                                            validationMessage.Add("Retrivel's cannot be closed as it is already closed.");
                                        }
                                        break;

                                    case "Dispatch":

                                        bool IsCheckApproval = await _unitOfWork.RetrievalRetrievalTransactionRepository
                                            .Any(r => r.ReqNumber == RP_Model.REQNumber && r.Ref1 == item.Ref1 && r.ApprovalDate != null && r.RejectDate == null);

                                        if (IsCheckApproval && IsCheckClosed)
                                        {
                                            var RetrivelTran = _unitOfWork.RetrievalRetrievalTransactionRepository
                                                .AsQueryable()
                                                .Where(r => r.Ref1 == item.Ref1 && r.ReqNumber == RP_Model.REQNumber)
                                                .FirstOrDefault();


                                            if (RP_Model.Ref1.Contains(item.Ref1) && RetrivelTran.FileStatus != 4)
                                            {
                                                if (RP_Model.CourierId != 0 && !string.IsNullOrEmpty(RP_Model.ConsignmentsNumber) && RP_Model.ConsignmentsNumber != "string")
                                                {
                                                    Check_Count++;

                                                    RetrivelTran.FileStatus = 4;
                                                    RetrivelTran.ItemStatus = "OUT";
                                                    RetrivelTran.DispatchDate = DateTime.Now;
                                                    RetrivelTran.CourierId = RP_Model.CourierId;
                                                    RetrivelTran.ConsignmentsNumber = RP_Model.ConsignmentsNumber;
                                                    //RetrivelTran.CourierAckDate = DateTime.Now;
                                                    //RetrivelTran.CourierAckBy = RP_Model.CourierId;

                                                    _unitOfWork.RetrievalRetrievalTransactionRepository.Update(RetrivelTran);

                                                    var RrnHist = new RetrievalTranHistory()
                                                    {
                                                        ReqNumber = item.ReqNumber,
                                                        Ref1 = item.Ref1,
                                                        Type = "Retrivel",
                                                        FileStatus = "4",
                                                        Status = "Dispatch",
                                                        ProcessDate = DateTime.Now,
                                                        ProcessBy = (int)_gluid.GetUserID(), //Convert.ToInt32(RP_Model.IsProcessBy),
                                                        Remarks = item.Remarks
                                                    };
                                                    _unitOfWork.RetrievalRetrievalTranHistory.Add(RrnHist);

                                                    IsValid = true;

                                                    var EntryDumpUpload = _unitOfWork.DumpUploadMasterRepository.AsQueryable().Where(x => x.Ref1 == item.Ref1).FirstOrDefault();
                                                    if (EntryDumpUpload != null)
                                                    {
                                                        EntryDumpUpload.ItemStatus = "OUT";
                                                        _unitOfWork.DumpUploadMasterRepository.Update(EntryDumpUpload);
                                                    }
                                                }
                                                else
                                                {
                                                    IsValid = false;
                                                    validationMessage.Add("Retrivel's is not Dispatched due to CourierId or ConsignmentsNumber not found.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            IsValid = false;
                                            validationMessage.Add("Retrivel's has either been rejected or not approved for Dispatch.");
                                        }
                                        break;
                                    default:
                                        validationMessage.Add("Invalid Process Types value. Please provide a valid Process Types");
                                        break;
                                }
                            }
                            if (IsValid)
                            {
                                if (RP_Model.Process_Type == "Dispatch" && Check_Count > 0)
                                {
                                    var TotalCount = _unitOfWork.RetrievalRetrievalTransactionRepository
                                         .AsQueryable().Where(r => r.ReqNumber == RP_Model.REQNumber && r.Status == "Partial-Dispatch" && r.FileStatus == 4)
                                         .ToList().Count();

                                    if (TotalCount > 0)
                                        IsDispatch = TotalCount;

                                    var updateData1 = _unitOfWork.RetrievalRetrievalTransactionRepository
                                       .AsQueryable().Where(r => r.ReqNumber == RP_Model.REQNumber)
                                       .ToList();

                                    if (IsDispatch > 0)
                                    {
                                        Check_Count += IsDispatch;
                                    }
                                    foreach (var item2 in updateData1)
                                    {
                                        item2.Status = (updateData1.Count() == Check_Count) ? "Retrieval-Dispatch" : "Partial-Dispatch";
                                        _unitOfWork.RetrievalRetrievalTransactionRepository.Update(item2);
                                    }
                                }

                                if (RP_Model.Process_Type == "Approved" && Check_Count > 0)
                                {
                                    var TotalCount = _unitOfWork.RetrievalRetrievalTransactionRepository
                                        .AsQueryable().Where(r => r.ReqNumber == RP_Model.REQNumber && r.Status == "Partial-Approved" && r.FileStatus == 2)
                                        .ToList().Count();

                                    if (TotalCount > 0)
                                        IsApproved = TotalCount;

                                    var updateData1 = _unitOfWork.RetrievalRetrievalTransactionRepository
                                       .AsQueryable().Where(r => r.ReqNumber == RP_Model.REQNumber)
                                       .ToList();

                                    if (IsApproved > 0)
                                    {
                                        Check_Count += IsApproved;
                                    }

                                    foreach (var item2 in updateData1)
                                    {
                                        item2.Status = (updateData1.Count() == Check_Count) ? "Retrieval-Approved" : "Partial-Approved";

                                        _unitOfWork.RetrievalRetrievalTransactionRepository.Update(item2);
                                    }
                                    //  _unitOfWork.Commit(); // Commit changes after each operation
                                }
                                _unitOfWork.Commit();
                                validationMessage.Add("Retrivel's :" + RP_Model.Process_Type + " Successfully..");
                            }
                            else
                            {
                                validationMessage.Add($"Retrieval transaction {RP_Model.Process_Type}  already completed with the specified Ref1 and ReqNumber");
                            }

                        }
                        else
                        {
                            validationMessage.Add("Please enter the Valid REQ_NUMBER");
                        }
                    }
                    else
                    {
                        validationMessage.Add("Please enter the Valid Process Type.");
                    }
                }
                else
                {
                    var RetrivelTran = _unitOfWork.RetrievalRetrievalTransactionRepository.AsQueryable().Where(r => r.ReqNumber == RP_Model.REQNumber).ToList();

                    foreach (var item in RetrivelTran)
                    {
                        item.Status = "Retrieval Request";
                        item.ClosedDateTime = DateTime.Now;
                        item.ClosedBy = (int)_gluid.GetUserID();//Convert.ToInt32(RP_Model.IsProcessBy);
                        _unitOfWork.RetrievalRetrievalTransactionRepository.Update(item);
                    }
                    // Update History-Updated for Closure
                    var RetrivelTranHistory = new RetrievalTranHistory()
                    {
                        ReqNumber = RP_Model.REQNumber,
                        Ref1 = "All",
                        Type = "Retrivel",
                        FileStatus = "1",
                        Status = "Retrieval Request",
                        ProcessDate = DateTime.Now,
                        ProcessBy = (int)_gluid.GetUserID(), //Convert.ToInt32(RP_Model.IsProcessBy),
                        Remarks = "Request has been Closed"
                    };
                    _unitOfWork.RetrievalRetrievalTranHistory.Add(RetrivelTranHistory);

                    if (RP_Model.Process_Type == "Closed")
                    {
                        var RetrivelTranGen = _unitOfWork.RetrievalRequestGenRepository
                            .AsQueryable()
                            .Where(r => r.ReqNumber == RP_Model.REQNumber)
                            .FirstOrDefault();

                        if (RetrivelTranGen != null)
                        {
                            RetrivelTranGen.IsClosedDate = DateTime.Now;
                            RetrivelTranGen.Status = "Y";
                            RetrivelTranGen.IsClosedby = (int)_gluid.GetUserID();
                            _unitOfWork.RetrievalRequestGenRepository.Update(RetrivelTranGen);
                        }
                        else
                        {
                            validationMessage.Add("Retrivel's Generation Not closed due to not found a values.");
                        }

                    }
                    validationMessage.Add("Retrivel's Closed Successfully..");
                    _unitOfWork.Commit(); // Commit changes after each operation


                }
            }
            else
            {
                validationMessage.Add("Invalid Process Type. Please provide a valid Process Type.");
            }
            return validationMessage;
        }

        public async Task<RetrivelData> AddRetrivel(string ReqNumber)
        {
            var response = new RetrivelData();

            bool isRetrivel_Request = await _unitOfWork.RetrievalRequestGenRepository.Any(x => x.ReqNumber == ReqNumber);

            if (!isRetrivel_Request)
            {
                var newRetrievalRequestGen = new RetrievalRequestGen();

                newRetrievalRequestGen.ReqNumber = ReqNumber;
                newRetrievalRequestGen.IsCreatedBy = (int)_gluid.GetUserID();
                newRetrievalRequestGen.IsCreatedDate = DateTime.Now;
                newRetrievalRequestGen.Status = "N";

                await _unitOfWork.RetrievalRequestGenRepository.Add(newRetrievalRequestGen);
                await _unitOfWork.Commit();
                response.REQ_Number = ReqNumber;
            }
            else
            {
                response.REQ_Number = ReqNumber;

            }
            return response;
        }
        public async Task<List<GetAllRetrivelModels>> GetAllRetrivel_Closed()
        {
            using (RMS_2024Context dbContext = new RMS_2024Context())
            {
                var result = (from b in dbContext.RetrievalTransactions
                              join a in dbContext.UserMasters on (long)b.ClosedBy equals a.UserId
                              join c in dbContext.DumpUploads on b.Ref1 equals c.Ref1
                              where b.ClosedDateTime != null
                              select new GetAllRetrivelModels
                              {
                                  ReqNumber = b.ReqNumber,
                                  Status = b.Status,
                                  Closed_Date = b.ClosedDateTime,
                                  UserName = a.UserName

                              })
                            .GroupBy(item => new
                            {
                                item.ReqNumber,
                                item.Status,
                                item.Closed_Date,
                                item.UserName
                            })
                            .Select(group => new GetAllRetrivelModels
                            {
                                ReqNumber = group.Key.ReqNumber,
                                Status = group.Key.Status,
                                Closed_Date = group.Key.Closed_Date,
                                UserName = group.Key.UserName,
                            }).ToList();

                return result;
            }
        }
        public async Task<List<GetAllRetrivelModels_Approval>> GetAllRetrive1_Approval()
        {

            using (RMS_2024Context dbContext = new RMS_2024Context())
            {
                var result = (from b in dbContext.RetrievalTransactions
                              join a in dbContext.UserMasters on (long)b.ClosedBy equals a.UserId
                              join c in dbContext.DumpUploads on b.Ref1 equals c.Ref1
                              where b.FileStatus == 1
                              select new GetAllRetrivelModels_Approval
                              {
                                  ReqNumber = b.ReqNumber,
                                  Status = b.Status,
                                  Closed_Date = b.ClosedDateTime,
                                  UserName = a.UserName
                              })
                            .GroupBy(item => new
                            {
                                item.ReqNumber,
                                item.Status,
                                item.Closed_Date,
                                item.UserName
                            })
                            .Select(group => new GetAllRetrivelModels_Approval
                            {
                                ReqNumber = group.Key.ReqNumber,
                                Status = group.Key.Status,
                                Closed_Date = group.Key.Closed_Date,
                                UserName = group.Key.UserName,
                            }).ToList();

                return result;
            }
        }

        #region Dispath Patents & Child GetAll Section.. 

        public async Task<List<GetAllRetrivelModels_Dispatch>> GetAllParent_Retrive1_Dispatch()
        {
            using (RMS_2024Context dbContext = new RMS_2024Context())
            {
                var result = (from b in dbContext.RetrievalTransactions
                              join a in dbContext.UserMasters on (long)b.ApprovalBy equals a.UserId
                              join d in dbContext.UserMasters on (long)b.ClosedBy equals d.UserId
                              join c in dbContext.DumpUploads on b.Ref1 equals c.Ref1
                              where b.FileStatus == 2
                              select new
                              {
                                  b.ReqNumber,
                                  b.Status,
                                  b.ClosedDateTime,
                                  b.ApprovalDate,
                                  ApprovalUser  = a.UserName ,
                                  ClosedUser = d.UserName,
                              })
                            .AsEnumerable()
                            .Select(item => new GetAllRetrivelModels_Dispatch
                            {
                                ReqNumber = item.ReqNumber,
                                Status = item.Status,
                                Closed_Date = item.ClosedDateTime.HasValue ? item.ClosedDateTime.Value.Date : default(DateTime),
                                Approval_Date = item.ApprovalDate.HasValue ? item.ApprovalDate.Value.Date : default(DateTime),
                                UserName = item.ApprovalUser,
                                ClosedName = item.ClosedUser,
                            })
                            .GroupBy(item => new
                            {
                                item.ReqNumber,
                                item.Status,
                                item.Closed_Date,
                                item.Approval_Date,
                                item.UserName,
                                item.ClosedName
                                 

                            })
                            .Select(group => new GetAllRetrivelModels_Dispatch
                            {
                                ReqNumber = group.Key.ReqNumber,
                                Status = group.Key.Status,
                                Closed_Date = group.Key.Closed_Date,
                                Approval_Date = group.Key.Approval_Date,
                                UserName = group.Key.UserName,
                                ClosedName = group.Key.ClosedName,
                            }).ToList();

                return result;
            }
        }
        #endregion
        public async Task<List<GetAllRetrivelModels_Ref>> GetAllRetrive1_ByRef(string ReqNumber)
        {
            using (RMS_2024Context dbContext = new RMS_2024Context())
            {
                List<int?> FilterFileStatus = new List<int?> { 2, 4};

                var result = (from a in dbContext.UserMasters
                              join b in dbContext.RetrievalTransactions on a.UserId equals (long)b.RequestBy
                              join c in dbContext.DumpUploads on b.Ref1 equals c.Ref1
                              join d in dbContext.StatusMasters on b.FileStatus equals d.Id
                              where FilterFileStatus.Contains(b.FileStatus) && b.ReqNumber == ReqNumber
                              select new GetAllRetrivelModels_Ref
                              {
                                  Ref = b.Ref1,
                                  ReqNumber = b.ReqNumber,
                                  FileStatus = d.Status,
                                  RequestDate = b.RequestDate,
                                  ApprovalBy = b.ApprovalBy,
                                  Approval_Date = b.ApprovalDate,
                                  UserName = a.UserName,
                                  retrieval_reason=b.RetrievalRegion,
                                  retrieval_type=b.RetrievalType,
                                  remarks=b.Remarks

                              }).ToList();

                return result;
            }
        }

    }
    public class RetrivelData
    {
        public string REQ_Number { get; set; }
        public string Status { get; set; }
    }
    public interface IRetrivel_RequestDomain
    {
        Task<HashSet<string>> AddValidation_GenRR();
        Task<RetrivelData> AddRetrivel(string ReqNumber);
        Task<List<GetAllRetrivelRequestModel>> GetRetrivelDetailsByReqNumber(string ReqNo);
        Task<HashSet<string>> CheckRetrievalValidationAndInsert(RequestRetrivelTransaction requestRetrivelTransaction);
        Task<List<DumpUpload>> GetRetrivelDetailsByLenNumber(string Len_No);
        Task<HashSet<string>> Retrivel_Process(RetrivelProcess_Model RP_Model);
        Task<List<GetAllRetrivelModels>> GetAllRetrivel_Closed();
        Task<List<GetAllRetrivelModels_Approval>> GetAllRetrive1_Approval();
        Task<List<GetAllRetrivelModels_Ref>> GetAllRetrive1_ByRef(string ReqNumber);
        Task<List<GetAllRetrivelModels_Dispatch>> GetAllParent_Retrive1_Dispatch();

    }

}
