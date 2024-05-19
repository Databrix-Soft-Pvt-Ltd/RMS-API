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
using Microsoft.Data.SqlClient;
using Dapper;
using TwoWayCommunication.Model.Enums;

namespace Management.Services.Masters
{
    public class Refilling_RequestDomain : IRefilling_RequestDomain
    {


        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        private RefillingTransaction RetTransaction { get; set; }
        private RetrievalTranHistory RrnHist { get; set; }
        public List<AddRefilling_RequestRequestModel> DumpLIstData { get; set; }

        private List<TemplateMaster> ListTemp { get; set; }

        private readonly RMS_2024Context rMS_2024Context;


        private readonly GlobalUserID _gluid;

        public Refilling_RequestDomain(IUnitOfWork unitOfWork, RMS_2024Context rMS_2024Context, GlobalUserID globalUserID)
        {
            _unitOfWork = unitOfWork;
            this.rMS_2024Context = rMS_2024Context;
            validationMessage = new HashSet<string>();
            this._gluid = globalUserID;

        }
        public async Task<HashSet<string>> AddValidation_GenRR(Refilling_ReqCheck ReqNo)
        {
            var validationMessage = new HashSet<string>();

            var isRetrievalRequest = _unitOfWork.RefillingRequestGenRepository
                      .AsQueryable().Where(rrg => rrg.IsCreatedBy == ReqNo.IsCreatedBy)
                      .OrderByDescending(rrg => rrg.Id)
                      .FirstOrDefault();

            if (isRetrievalRequest != null && !string.IsNullOrWhiteSpace(isRetrievalRequest.RefNumber))
            {
                if (isRetrievalRequest.IsClosedby == null && isRetrievalRequest.IsClosedDate == null && isRetrievalRequest.Status == "N")
                {
                    validationMessage.Add(isRetrievalRequest.RefNumber);
                }
                else
                {
                    var maxKVALoad = _unitOfWork.RefillingRequestGenRepository
                        .AsQueryable()
                        .OrderByDescending(l => l.RefNumber)
                        .FirstOrDefault();

                    if (maxKVALoad != null)
                    {
                        var parts = maxKVALoad.RefNumber.Split("-");

                        if (parts.Length > 1 && int.TryParse(parts[1], out int number))
                        {
                            var newNumber = string.Format("REF-{0:0000}", number + 1);
                            validationMessage.Add(newNumber);
                        }
                    }
                }
            }
            else
            {
                var maxKVALoad = _unitOfWork.RefillingRequestGenRepository
                    .AsQueryable()
                    .OrderByDescending(l => l.RefNumber)
                    .FirstOrDefault();

                if (maxKVALoad != null)
                {
                    var parts = maxKVALoad.RefNumber.Split("-");
                    if (parts.Length > 1 && int.TryParse(parts[1], out int number))
                    {
                        var newNumber = string.Format("REF-{0:0000}", number + 1);
                        validationMessage.Add(newNumber);
                    }
                }
                else
                {
                    validationMessage.Add("REF-0001");
                }
            }

            return validationMessage;
        }
        public async Task<RefillingData> AddRefilling(Refilling_ReqCheck ReqCheck)
        {
            var response = new RefillingData();

            bool isRetrivel_Request = await _unitOfWork.RefillingRequestGenRepository.Any(x => x.RefNumber == ReqCheck.REF_Number);

            if (!isRetrivel_Request)
            {
                var newRefillingRequestGen = new RefillingRequestGen();

                newRefillingRequestGen.RefNumber = ReqCheck.REF_Number;
                newRefillingRequestGen.IsCreatedBy = (int)_gluid.GetUserID();//ReqCheck.IsCreatedBy;
                newRefillingRequestGen.IsCreatedDate = DateTime.Now;
                newRefillingRequestGen.Status = "N";

                await _unitOfWork.RefillingRequestGenRepository.Add(newRefillingRequestGen);
                await _unitOfWork.Commit();
                response.REF_Number = ReqCheck.REF_Number;
            }
            else
            {
                response.REF_Number = ReqCheck.REF_Number;

            }
            return response;
        }
        public async Task<List<GetAllRefillingRequestModel>> GetRefillingDetailsByReqNumber(string REF_No)
        {
            RMS_2024Context dbContext = new RMS_2024Context();

            var result = (from a in dbContext.RefillingTransactions
                          join b in dbContext.DumpUploads on a.Ref1 equals b.Ref1
                          join c in dbContext.UserMasters on a.RefillingBy equals (int)c.UserId
                          where a.RefNumber == REF_No
                          select new GetAllRefillingRequestModel
                          {
                              REF_Number = a.RefNumber,
                              RefillingDate = Convert.ToDateTime(a.RefillingDate),
                              Refilling_By = c.UserName,
                              File_Status = a.ItemSatus,
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
        public async Task<List<DumpUpload>> GetRefillingDetailsByLenNumber(string Ref1)
        {
            RMS_2024Context dbContext = new RMS_2024Context();

            var result = (from b in dbContext.DumpUploads
                          where b.Ref1 == Ref1 && b.ItemStatus == "OUT"
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
        public async Task<List<GetAllRefillingAllModel>> GetRefillingAllByRefNumber(string RefNumber)
        {
            RMS_2024Context dbContext = new RMS_2024Context();

            var result = (from a in dbContext.RefillingTransactions
                          join b in dbContext.StatusMasters on a.FileStatus equals b.Id
                          join c in dbContext.UserMasters on a.RefillingBy equals (int)c.UserId
                          where a.RefNumber == RefNumber
                          select new GetAllRefillingAllModel
                          {
                              REF_Number = a.RefNumber,
                              Ref1 = a.Ref1,
                              File_Status = b.Status,
                              Status = a.ItemSatus,
                              Remarks = a.Remarks,
                              Refilling_Date = a.RefillingDate,
                              RefillingBy = c.UserName

                          }).ToList();
            return result;

        }
        public async Task<List<SP_GetALLReffiling>> GetALLReffilBefore_ACK(string UserID)
        {

            string connectionString = rMS_2024Context.Database.GetDbConnection().ConnectionString;

            List<SP_GetALLReffiling> result;

            string sqlQuery = "EXEC SP_GetALLReffiling_Details @UserID";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = connection.Query<SP_GetALLReffiling>(sqlQuery,
                    new
                    {
                        UserID = UserID ?? (object)DBNull.Value

                    }).ToList();
            }
            return result;

        }
        public async Task<HashSet<string>> CheckRefillingValidationAndInsert(RequestRefillingTransaction objRefillingTran)
        {
            var validationMessage = new HashSet<string>();

            try
            {
                if (objRefillingTran.REF_Number != null)
                {
                    bool isRefNumberExists = await _unitOfWork.RefillingRequestGenRepository.Any(x => x.RefNumber == objRefillingTran.REF_Number && x.Status != "Y");

                    if (isRefNumberExists)
                    {
                        foreach (var ref1 in objRefillingTran.ListRef1)
                        {
                            var checkValidRef1 = _unitOfWork.DumpUploadMasterRepository
                                .AsQueryable()
                                .FirstOrDefault(x => x.Ref1 == ref1 && x.ItemStatus.TrimEnd() == "OUT");

                            if (checkValidRef1 != null)
                            {
                                bool isValidationCheck = await _unitOfWork.RefillingTransactionRepository.Any(x => x.Ref1 == ref1 && x.RefNumber == objRefillingTran.REF_Number);

                                if (!isValidationCheck)
                                {
                                    var GetRetrivelID = 55847;

                                    var Get_Check = _unitOfWork.RefillingTransactionRepository.AsQueryable().Where(x => x.Ref1 == ref1 && x.ItemSatus == "OUT").FirstOrDefault();

                                    // GetRetrivelID.Id = 254;
                                    // Add Retrieval...
                                    if (GetRetrivelID != null && Get_Check == null)
                                    {
                                        var newRefillingTransaction = new RefillingTransaction
                                        {
                                            RefNumber = objRefillingTran.REF_Number,
                                            Ref1 = ref1,
                                            //RefillingType = requestRetrievalTransaction.RetrievalType,
                                            //RetrievalRegion = requestRetrievalTransaction.RetrievalRegion,
                                            Remarks = objRefillingTran.Remarks,
                                            FileStatus = 10,
                                            Status = "Partial Refilling",
                                            ItemSatus = "OUT",
                                            RefillingDate = DateTime.Now,
                                            RefillingBy = (int)_gluid.GetUserID(),
                                            //RefillingAckDate = DateTime.Now,
                                            //RefillingAckBy = 558, 
                                            RetrievalId = GetRetrivelID,


                                        };
                                        await _unitOfWork.RefillingTransactionRepository.Add(newRefillingTransaction);

                                        // Update History Table
                                        var objRetrievalTranHistory = new RetrievalTranHistory
                                        {
                                            Type = "Refilling",
                                            ReqNumber = objRefillingTran.REF_Number,
                                            Ref1 = ref1,
                                            FileStatus = "10",
                                            Status = "Refilling",
                                            ProcessDate = DateTime.Now,
                                            ProcessBy = (int)_gluid.GetUserID(),
                                            // Remarks = requestRetrievalTransaction.Remarks
                                        };


                                        await _unitOfWork.RetrievalRetrievalTranHistory.Add(objRetrievalTranHistory);

                                        await _unitOfWork.Commit();
                                    }
                                    else
                                    {
                                        validationMessage.Add($"Refilling Entry not done for Ref1 is not OUT");
                                    }
                                    validationMessage.Add($"Refilling saved successfully!");
                                }
                                else
                                {
                                    validationMessage.Add($"Record alredy exists!");
                                }
                            }
                            else
                            {
                                validationMessage.Add($"Ref1 {ref1} not found for Refilling transaction or item is not 'OUT'");
                            }
                        }
                    }
                    else
                    {
                        validationMessage.Add("Invalid RefNumber for Refilling transaction");
                    }
                }
                else
                {
                    validationMessage.Add("REFNumber is null for Refilling transaction");
                }
            }
            catch (Exception ex)
            {
                validationMessage.Add($"Error: {ex.Message}");
            }
            return validationMessage;
        }
        public async Task<HashSet<string>> Refilling_Process(RefillingProcess_Model RP_Model)
        {
            HashSet<string> validationMessage = new HashSet<string>();
            bool IsValid = false;

            if (!string.IsNullOrEmpty(RP_Model.Process_Type) && RP_Model.Process_Type != "string")
            {
                if (RP_Model.Process_Type != "Closed" && RP_Model.Process_Type != "Ack")
                {
                    if (!string.IsNullOrEmpty(RP_Model.REFNumber) && RP_Model.REFNumber != "string")
                    {
                        var updateData = _unitOfWork.RefillingTransactionRepository
                                             .AsQueryable()
                                             .Where(r => r.RefNumber == RP_Model.REFNumber)
                                             .AsEnumerable() // Switch to LINQ to Objects
                                             .Where(r => RP_Model.ListRef1.Any(item => r.Ref1.Contains(item)))
                                             .ToList();

                        if (updateData != null && updateData.Count > 0)
                        {
                            int Check_Count = 0;
                            int IsRecievd = 0;

                            foreach (var item in updateData)
                            {
                                switch (RP_Model.Process_Type)
                                {
                                    case "Received":


                                        var RefillingTran = _unitOfWork.RefillingTransactionRepository
                                            .AsQueryable()
                                            .Where(r => r.Ref1 == item.Ref1 && r.RefNumber == RP_Model.REFNumber)
                                            .FirstOrDefault(); 


                                        if (RP_Model.ListRef1.Contains(item.Ref1) && RefillingTran.FileStatus != 9)
                                        {
                                            Check_Count++;
                                            RefillingTran.FileStatus = 9;
                                            RefillingTran.ItemSatus = "IN";
                                            RefillingTran.RefillingReceivedDate = DateTime.Now;
                                            RefillingTran.RefillingReceivedBy = (int)_gluid.GetUserID();//RP_Model.IsProcessBy;
                                            _unitOfWork.RefillingTransactionRepository.Update(RefillingTran);

                                            var RrnHist = new RetrievalTranHistory()
                                            {
                                                ReqNumber = item.RefNumber,
                                                Ref1 = item.Ref1,
                                                Type = "Refilling",
                                                FileStatus = "9",
                                                Status = "Received",
                                                ProcessDate = DateTime.Now,
                                                ProcessBy = (int)_gluid.GetUserID(),//Convert.ToInt32(RP_Model.IsProcessBy),
                                                Remarks = item.Remarks
                                            };
                                            _unitOfWork.RetrievalRetrievalTranHistory.Add(RrnHist);
                                            IsValid = true;

                                            var EntryDumpUpload = _unitOfWork.DumpUploadMasterRepository.AsQueryable().Where(x => x.Ref1 == item.Ref1).FirstOrDefault();
                                            if (EntryDumpUpload != null)
                                            {
                                                EntryDumpUpload.ItemStatus = "IN";
                                                _unitOfWork.DumpUploadMasterRepository.Update(EntryDumpUpload);
                                            }
                                        }
                                        break;
                                    default:
                                        validationMessage.Add("Invalid Process Types value. Please provide a valid Process Types");
                                        break;
                                }
                            }
                            if (IsValid)
                            {
                                if (RP_Model.Process_Type == "Received" && Check_Count > 0)
                                {
                                    var TotalCount = _unitOfWork.RefillingTransactionRepository
                                        .AsQueryable()
                                        .Count(r => r.RefNumber == RP_Model.REFNumber && r.Status == "Partial-Received" && r.FileStatus == 9);

                                    IsRecievd = TotalCount > 0 ? TotalCount : IsRecievd;

                                    var RefillingTranGen = _unitOfWork.RefillingTransactionRepository
                                        .AsQueryable()
                                        .Where(r => r.RefNumber == RP_Model.REFNumber)
                                        .ToList();

                                    if (IsRecievd > 0)
                                        Check_Count += IsRecievd;

                                    foreach (var item2 in RefillingTranGen)
                                    {
                                        item2.Status = (RefillingTranGen.Count == Check_Count) ? "Completely-Received" : "Partial-Received";
                                        _unitOfWork.RefillingTransactionRepository.Update(item2);
                                    }

                                    _unitOfWork.Commit();
                                    validationMessage.Add("Refilling's :" + RP_Model.Process_Type + " Successfully..");
                                }

                            }
                            else
                            {
                                validationMessage.Add("Refilling transaction not found for the specified Ref1 and RefNumber");
                            }
                        }
                        else
                        {
                            validationMessage.Add("Please enter the Valid REF_NUMBER");
                        }
                    }
                    else
                    {
                        validationMessage.Add("Please enter the Valid REF_Number Type.");
                    }
                }
                else
                {
                    var RefillingTran = _unitOfWork.RefillingTransactionRepository.AsQueryable().Where(r => r.RefNumber == RP_Model.REFNumber).ToList();

                    switch (RP_Model.Process_Type)
                    {
                        case "Closed":
                            foreach (var item in RefillingTran)
                            {
                                item.Status = "Refilling Request";
                                item.RefillingClosedDate = DateTime.Now;
                                item.RefillingClosedBy = (int)_gluid.GetUserID();
                                item.DispatchDate = DateTime.Now;
                                item.DispatchAddress = RP_Model.Dispatch_Address;
                                item.CourierId = RP_Model.Courier_ID;
                                item.ConsignmentsNumber = RP_Model.Consignments_Number;
                                _unitOfWork.RefillingTransactionRepository.Update(item);
                            }

                            var RetrivelTranHistory = new RetrievalTranHistory()
                            {
                                ReqNumber = RP_Model.REFNumber,
                                Ref1 = "All",
                                Type = "Refilling",
                                FileStatus = "1",
                                Status = "Refilling Request",
                                ProcessDate = DateTime.Now,
                                ProcessBy = (int)_gluid.GetUserID(),//Convert.ToInt32(RP_Model.IsProcessBy),
                                Remarks = "Refilling has been Closed"
                            };
                            _unitOfWork.RetrievalRetrievalTranHistory.Add(RetrivelTranHistory);

                            if (RP_Model.Process_Type == "Closed")
                            {
                                var RefillingTranGen = _unitOfWork.RefillingRequestGenRepository
                                    .AsQueryable()
                                    .Where(r => r.RefNumber == RP_Model.REFNumber)
                                    .FirstOrDefault();

                                if (RefillingTranGen != null)
                                {
                                    RefillingTranGen.IsClosedDate = DateTime.Now;
                                    RefillingTranGen.Status = "Y";
                                    RefillingTranGen.IsClosedby = (int)_gluid.GetUserID();
                                    _unitOfWork.RefillingRequestGenRepository.Update(RefillingTranGen);
                                }
                                else
                                {
                                    validationMessage.Add("Refilling's Generation Not closed due to not found a values.");
                                }

                            }
                            validationMessage.Add("Refilling's Closed Successfully..");
                            break;

                        case "Ack":
                            foreach (var item in RefillingTran)
                            {
                                item.Status = "Refilling Ack";
                                item.RefillingAckDate = DateTime.Now;
                                item.RefillingAckBy = (int)_gluid.GetUserID();//Convert.ToInt32(RP_Model.IsProcessBy);
                                _unitOfWork.RefillingTransactionRepository.Update(item);
                            }
                            var RetrivelTranHistory1 = new RetrievalTranHistory()
                            {
                                ReqNumber = RP_Model.REFNumber,
                                Ref1 = "All",
                                Type = "Refilling",
                                FileStatus = "9",
                                Status = "Refilling Ack",
                                ProcessDate = DateTime.Now,
                                ProcessBy = (int)_gluid.GetUserID(),//Convert.ToInt32(RP_Model.IsProcessBy),
                                Remarks = "Refilling has been Ack"
                            };
                            _unitOfWork.RetrievalRetrievalTranHistory.Add(RetrivelTranHistory1);

                            validationMessage.Add("Refilling's Ack Successfully..");
                            break;
                        default:
                            validationMessage.Add("Invalid Process Types value. Please provide a valid Process Types");
                            break;
                    }
                    _unitOfWork.Commit();
                }
            }
            else
            {
                validationMessage.Add("Invalid Process Type. Please provide a valid Process Type.");
            }
            return validationMessage;
        }
        public async Task<List<GetAllRefillingModels_Closed>> GetAllRefilling_Closed()
        {
            using (RMS_2024Context dbContext = new RMS_2024Context())
            {
                var result = (from b in dbContext.RefillingTransactions
                              join a in dbContext.UserMasters on (long)b.RefillingClosedBy equals a.UserId
                              join c in dbContext.DumpUploads on b.Ref1 equals c.Ref1
                              join D in dbContext.CourierMasters on b.CourierId equals (int)D.Id
                              where b.RefillingClosedDate != null
                              select new
                              {
                                  b.RefNumber,
                                  b.Status,
                                  b.RefillingClosedDate,
                                  a.UserName,
                                  D.CourierName,
                                  b.ConsignmentsNumber,
                              })
                              .AsEnumerable()
                            .Select(item => new GetAllRefillingModels_Closed
                            {
                                Ref_Number = item.RefNumber,
                                Status = item.Status,
                                Closed_Date = item.RefillingClosedDate.HasValue ? item.RefillingClosedDate.Value.Date : default(DateTime),
                                UserName = item.UserName,
                                Courier_Name = item.CourierName,
                                Consignments_Number = item.ConsignmentsNumber,

                            })
                            .GroupBy(item => new
                            {
                                item.Ref_Number,
                                item.Status,
                                item.Closed_Date,
                                item.UserName,
                                item.Courier_Name,
                                item.Consignments_Number
                            })
                            .Select(group => new GetAllRefillingModels_Closed
                            {
                                Ref_Number = group.Key.Ref_Number,
                                Status = group.Key.Status,
                                Closed_Date = group.Key.Closed_Date,
                                UserName = group.Key.UserName,
                                Courier_Name = group.Key.Courier_Name,
                                Consignments_Number = group.Key.Consignments_Number
                            }).ToList();

                return result;
            }
        }

        public async Task<List<GetAllParentsAck>> GetAllParent_Retrive1_Ack()
        {

            string connectionString = rMS_2024Context.Database.GetDbConnection().ConnectionString;

            List<GetAllParentsAck> result;

            string sqlQuery = "EXEC Usp_GetAllParents_ACK";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = connection.Query<GetAllParentsAck>(sqlQuery).ToList();
            }
            return result;


            //using (RMS_2024Context dbContext = new RMS_2024Context())
            //{
            //    var result = (from b in dbContext.RefillingTransactions
            //                  join d in dbContext.UserMasters on (long)b.RefillingClosedBy equals d.UserId
            //                  join D in dbContext.CourierMasters on b.CourierId equals (int)D.Id
            //                  join a in dbContext.UserMasters on (long)b.RefillingAckBy equals a.UserId into a_join
            //                  from a in a_join.DefaultIfEmpty()
            //                  where b.FileStatus == 10
            //                  select new
            //                  {
            //                      b.RefNumber,
            //                      b.Status,
            //                      b.RefillingClosedDate,
            //                      RefillingClosedBy = d.UserName,
            //                      RefillingAckDate = b.RefillingAckDate,
            //                      RefillingAck = a != null ? a.UserName : null,
            //                      D.CourierName,
            //                      b.ConsignmentsNumber,
            //                  })
            //   .AsEnumerable()
            //   .Select(item => new GetAllRetrivelModels_Ack
            //   {
            //       Ref_Number = item.RefNumber,
            //       Status = item.Status,
            //       Closed_Date = item.RefillingClosedDate.HasValue ? item.RefillingClosedDate.Value.Date : default(DateTime),
            //       Closed_by = item.RefillingClosedBy,
            //       Ack_Date = item.RefillingAckDate.HasValue ? item.RefillingAckDate.Value.Date : default(DateTime),
            //       Ack_by = item.RefillingAck,
            //       Courier_Name = item.CourierName,
            //       Consignments_Number = item.ConsignmentsNumber,
            //   })
            //   .GroupBy(item => new
            //   {
            //       item.Ref_Number,
            //       item.Status,
            //       item.Closed_Date,
            //       item.Closed_by,
            //       item.Ack_Date,
            //       item.Ack_by,
            //       item.Courier_Name,
            //       item.Consignments_Number
            //   })
            //   .Select(group => new GetAllRetrivelModels_Ack
            //   {
            //       Ref_Number = group.Key.Ref_Number,
            //       Status = group.Key.Status,
            //       Closed_Date = group.Key.Closed_Date,
            //       Closed_by = group.Key.Closed_by,
            //       Ack_Date = group.Key.Ack_Date,
            //       Ack_by = group.Key.Ack_by,
            //       Courier_Name = group.Key.Courier_Name,
            //       Consignments_Number = group.Key.Consignments_Number,
            //   }).ToList();


            //    return result;
            //  }
        }


    }
    public class RefillingData
    {
        public string REF_Number { get; set; }
        public string Status { get; set; }
    }
    public interface IRefilling_RequestDomain
    {
        Task<RefillingData> AddRefilling(Refilling_ReqCheck ReqCheck);
        Task<HashSet<string>> AddValidation_GenRR(Refilling_ReqCheck request);
        Task<List<GetAllRefillingRequestModel>> GetRefillingDetailsByReqNumber(string REF_No);
        Task<HashSet<string>> CheckRefillingValidationAndInsert(RequestRefillingTransaction objRefillingTran);
        Task<List<DumpUpload>> GetRefillingDetailsByLenNumber(string Len_No);
        Task<HashSet<string>> Refilling_Process(RefillingProcess_Model RP_Model);

        Task<List<GetAllRefillingAllModel>> GetRefillingAllByRefNumber(string RefNumber);
        Task<List<SP_GetALLReffiling>> GetALLReffilBefore_ACK(string UserID);
        Task<List<GetAllRefillingModels_Closed>> GetAllRefilling_Closed();
        Task<List<GetAllParentsAck>> GetAllParent_Retrive1_Ack();
    }

}
