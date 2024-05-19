using System;
using System.Collections.Generic;
using System.Linq;
using Management.Model.RMSEntity;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Management.Model.RequestModel;
using Dapper;
using TwoWayCommunication.Model.Enums;
using System.Data;
using Management.Model.ResponseModel;

namespace Management.Services.Masters
{
    public interface IBranchInwardDomain
    {
        Task<List<BatchIDRequest>> getBatchID();
        Task<List<getBatchDetailsResponseModel>> getBatchDetails(string BatchID);
        Task<List<getBranchInwardDetailsResponsemodel>> getRef1DetailsBranchInward(string Ref1);
        Task<List<getBIPendingDetailsResponseModel>> getBatchInwardPendingDetails();
        Task<List<getCourierAckPendingResponseModel>> getCourierAckPending();
        Task<string> AddBranchInward(AddBIDetailsRequestModel request);

        Task<string> UpdateAckFile(UpdateFileACKRequestModel request);


        

        Task<string> UpdatePODNumber(UpdatePODNumberRequestModel request);
        Task<string> CourierACk(CourierACKRequestModel request);
        Task<List<getFileAckPendingResponseModel>> getFileAckPending();
        Task<List<getLanDetailsByBatchNoResponseModel>> getLanDetailsByBatchNo(string BatchNo);
        Task<List<getDocumentDetailsByLanNoResponseModel>> getDocumentDetailsByLanNo(string Ref1);
        Task<List<getackDetailsByLanAndBatchNoRM>> getackDetailsByLanAndBatchNo(string BatchNo, string Ref1);
        Task<string> UpdateBatchStatusAck(string BatchNo);


        

    }
    public class BranchInwardDomain : IBranchInwardDomain
    {
        readonly RMS_2024Context _dbContext;
        private HashSet<string> validationMessage { get; set; }
        private readonly GlobalUserID _gluID;
        public BranchInwardDomain(RMS_2024Context _dbContext,GlobalUserID globalUserID)
        {
            this._dbContext = _dbContext;
            validationMessage = new HashSet<string>();
            _gluID = globalUserID;
        }

        public async Task<List<BatchIDRequest>> getBatchID()
        {
            string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            List<BatchIDRequest> result;

            string sqlQuery = "EXEC sp_Get_Batch_Id_By_User @UserID";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = connection.Query<BatchIDRequest>(sqlQuery,
                    new
                    {
                        UserID = _gluID.GetUserID()

                    }).ToList();
            }
            return result;
        }
        public async Task<List<getBatchDetailsResponseModel>> getBatchDetails(string bathcID)
        {

            string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { BatchID = bathcID };

                var result = connection.Query<getBatchDetailsResponseModel>("sp_getBatchDetails", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }


            //string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            //List<BatchIDRequest> result;

            //string sqlQuery = "EXEC sp_Get_Batch_Id_By_User @BatchID";

            //using (var connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    result = connection.Query<BatchIDRequest>(sqlQuery,
            //        new
            //        {
            //            BatchID = _gluID.GetUserID()

            //        }).ToList();
            //}
            //return result;
        }
        public async Task<List<getBranchInwardDetailsResponsemodel>> getRef1DetailsBranchInward(string ref1)
        {
            string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new { Ref1 = ref1 };
                var result = connection.Query<getBranchInwardDetailsResponsemodel>("sp_getRef1DetailsBranchInward", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }

        }
        public async Task<string> AddBranchInward(AddBIDetailsRequestModel request)
        {
            string responseMessage = "";

            try
            {
                if (request.CheckListID3.Length > 0)
                {
                    for (int i = 0; i < request.CheckListID3.Length; i++)
                    {

                        var responseMessageParam = new SqlParameter("@Response_Message", SqlDbType.NVarChar, 100);
                        responseMessageParam.Direction = ParameterDirection.Output;

                        var result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[SP_AddBranchInward] @BatchID,@Ref1,@FileNo,@CartonNo,@CheckListID3,@ChkListType,@UserId,@Response_Message OUTPUT",
                            new SqlParameter("@BatchID", request.BatchID),
                            new SqlParameter("@Ref1", request.Ref1),
                            new SqlParameter("@FileNo", request.file_number),
                            new SqlParameter("@CartonNo", request.carton_number),                          
                            new SqlParameter("@CheckListID3", request.CheckListID3[i]),
                              new SqlParameter("@ChkListType", "3"),
                            new SqlParameter("@UserId", _gluID.GetUserID()),
                            responseMessageParam);
                            responseMessage = responseMessageParam.Value.ToString();
                    }
                    return responseMessage;
                }
                else if (request.CheckListID2.Length > 0)
                {
                    for (int i = 0; i < request.CheckListID2.Length; i++)
                    {

                        var responseMessageParam = new SqlParameter("@Response_Message", SqlDbType.NVarChar, 100);
                        responseMessageParam.Direction = ParameterDirection.Output;

                        var result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[SP_AddBranchInward] @BatchID,@Ref1,@FileNo,@CartonNo,@CheckListID1,@CheckListID2,@CheckListID3,@UserId,@Response_Message OUTPUT",
                            new SqlParameter("@BatchID", request.BatchID),
                            new SqlParameter("@Ref1", request.Ref1),
                             new SqlParameter("@FileNo", request.file_number),
                            new SqlParameter("@CartonNo", request.carton_number),
                            new SqlParameter("@CheckListID2", request.CheckListID2[i]),
                            new SqlParameter("@ChkListType", "2"),
                            new SqlParameter("@UserId", _gluID.GetUserID()), responseMessageParam);
                        responseMessage = responseMessageParam.Value.ToString();
                    }
                    return responseMessage;
                }
                else if (request.CheckListID1.Length > 0)
                {
                    for (int i = 0; i < request.CheckListID1.Length; i++)
                    {

                        var responseMessageParam = new SqlParameter("@Response_Message", SqlDbType.NVarChar, 100);
                        responseMessageParam.Direction = ParameterDirection.Output;

                        var result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[SP_AddBranchInward] @BatchID,@Ref1,@FileNo,@CartonNo,@CheckListID1,@CheckListID2,@CheckListID3,@UserId,@Response_Message OUTPUT",
                            new SqlParameter("@BatchID", request.BatchID),
                            new SqlParameter("@Ref1", request.Ref1),
                            new SqlParameter("@FileNo", request.file_number),
                            new SqlParameter("@CartonNo", request.carton_number),
                            new SqlParameter("@CheckListID", request.CheckListID1[i]),
                            new SqlParameter("@ChkListType", "1"),
                            new SqlParameter("@UserId", _gluID.GetUserID()), responseMessageParam);
                        responseMessage = responseMessageParam.Value.ToString();
                    }
                    return responseMessage;
                }
                else
                {
                    var responseMessageParam = new SqlParameter("@Response_Message", SqlDbType.NVarChar, 100);
                    responseMessageParam.Direction = ParameterDirection.Output;

                    var result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[SP_AddBranchInward] @BatchID,@Ref1,@FileNo,@CartonNo,@CheckListID1,@CheckListID2,@CheckListID3,@UserId,@Response_Message OUTPUT",
                        new SqlParameter("@BatchID", request.BatchID),
                        new SqlParameter("@Ref1", request.Ref1),
                      new SqlParameter("@FileNo", request.file_number),
                            new SqlParameter("@CartonNo", request.carton_number),
                        new SqlParameter("@CheckListID1", request.CheckListID1),
                        new SqlParameter("@CheckListID2", request.CheckListID2),
                        new SqlParameter("@CheckListID3", request.CheckListID3),
                        new SqlParameter("@UserId", _gluID.GetUserID()), responseMessageParam);

                    return responseMessage = responseMessageParam.Value.ToString();
                }



                //Console.WriteLine("Response Message: " + responseMessage);
              
                
                return responseMessage;




            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("An error occurred: " + ex.Message);
                return "An error occurred while processing your request.";
            }

        }
        public async Task<string> UpdatePODNumber(UpdatePODNumberRequestModel request)
        {
            string responseMessage = "";

            try
            {
                var responseMessageParam = new SqlParameter("@Response_Message", SqlDbType.NVarChar, 100);
                responseMessageParam.Direction = ParameterDirection.Output;

                var result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].[SP_UpdatePODNumber] @BatchID,@CourierId,@ConsignmentNumber,@DispatchAddress,@UserId,@Response_Message OUTPUT",
                    new SqlParameter("@BatchID", request.BatchID),
                    new SqlParameter("@CourierId", request.CourierId),
                    new SqlParameter("@ConsignmentNumber", request.ConsignmentNumber),
                    new SqlParameter("@DispatchAddress", request.DispatchAddress),  
                    new SqlParameter("@UserId", _gluID.GetUserID()), responseMessageParam);
                    responseMessage = responseMessageParam.Value.ToString();
                    return responseMessage;
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("An error occurred: " + ex.Message);
                return "An error occurred while processing your request.";
            }

        }

        public async Task<List<getBIPendingDetailsResponseModel>> getBatchInwardPendingDetails()
        {
            string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            List<getBIPendingDetailsResponseModel> result;

            string sqlQuery = "EXEC sp_getBatchInwardPendingDetails @UserID";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = connection.Query<getBIPendingDetailsResponseModel>(sqlQuery,
                    new
                    {
                        UserID = _gluID.GetUserID()

                    }).ToList();
            }
            return result;
        }

        public async Task<List<getCourierAckPendingResponseModel>> getCourierAckPending()
        {
            string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            List<getCourierAckPendingResponseModel> result;

            string sqlQuery = "EXEC sp_getCourierAckPending @UserID";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = connection.Query<getCourierAckPendingResponseModel>(sqlQuery,
                    new
                    {
                        UserID = _gluID.GetUserID()

                    }).ToList();
            }
            return result;
        }

        public async Task<string> CourierACk(CourierACKRequestModel request)
        {
            string responseMessage = "";

            try
            {
                var responseMessageParam = new SqlParameter("@Response_Message", SqlDbType.NVarChar, 100);
                responseMessageParam.Direction = ParameterDirection.Output;

                var result = _dbContext.Database.ExecuteSqlRaw("EXEC [dbo].SP_CourierACk @BatchID,@UserId,@Response_Message OUTPUT",
                    new SqlParameter("@BatchID", request.BatchID),                    
                    new SqlParameter("@UserId", _gluID.GetUserID()), responseMessageParam);
                responseMessage = responseMessageParam.Value.ToString();
                return responseMessage;
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("An error occurred: " + ex.Message);
                return "An error occurred while processing your request.";
            }

        }

        public async Task<List<getFileAckPendingResponseModel>> getFileAckPending()
        {
            string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            List<getFileAckPendingResponseModel> result;

            string sqlQuery = "EXEC sp_getFileAckPending @UserID";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = connection.Query<getFileAckPendingResponseModel>(sqlQuery,
                    new
                    {
                        UserID = _gluID.GetUserID()

                    }).ToList();
            }
            return result;
        }

        public async Task<List<getLanDetailsByBatchNoResponseModel>> getLanDetailsByBatchNo(string _BatchNo)
        {
            string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            List<getLanDetailsByBatchNoResponseModel> result;

            string sqlQuery = "EXEC sp_getLanDetailsByBatchNo @UserID ,@BatchNo";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = connection.Query<getLanDetailsByBatchNoResponseModel>(sqlQuery,
                    new
                    {
                        UserID = _gluID.GetUserID(),
                        BatchNo = _BatchNo
,

                    }).ToList();
            }
            return result;
        }

        public async Task<List<getDocumentDetailsByLanNoResponseModel>> getDocumentDetailsByLanNo(string _Ref1)
        {
            string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            List<getDocumentDetailsByLanNoResponseModel> result;

            string sqlQuery = "EXEC sp_getLanDetailsByBatchNo @UserID,@Ref1";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = connection.Query<getDocumentDetailsByLanNoResponseModel>(sqlQuery,
                    new
                    {
                        UserID = _gluID.GetUserID(),
                        Ref1 = _Ref1 

                    }).ToList();
            }
            return result;
        }


        

       public async Task<List<getackDetailsByLanAndBatchNoRM>> getackDetailsByLanAndBatchNo(string _Batchno, string _Ref1)
        {
            string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            List<getackDetailsByLanAndBatchNoRM> result;

            string sqlQuery = "EXEC getackDetailsByLanAndBatchNo @UserID,@Ref1, @Batchno ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = connection.Query<getackDetailsByLanAndBatchNoRM>(sqlQuery,
                    new
                    {
                        UserID = _gluID.GetUserID(),
                        Ref1 = _Ref1,
                        Batchno = _Batchno

                    }).ToList();
            }
            return result;
        }


        public async Task<string> UpdateAckFile(UpdateFileACKRequestModel request)
        {
            string responseMessage = "";

            try
            {
                var responseMessageParam = new SqlParameter("@Response_Message", SqlDbType.NVarChar, 100);
                responseMessageParam.Direction = ParameterDirection.Output;

                var result = _dbContext.Database.ExecuteSqlRaw("EXEC SP_AddFileRecieved @BatchID,@Ref1,@status,@UserId,@Response_Message OUTPUT",
                    new SqlParameter("@BatchID", request.BatchID),
                      new SqlParameter("@Ref1", request.Ref1),
                        new SqlParameter("@status", request.status),                      
                    new SqlParameter("@UserId", _gluID.GetUserID()), responseMessageParam);
                responseMessage = responseMessageParam.Value.ToString();
                return responseMessage;
            }
            catch (Exception ex) 
            {
                // Handle the exception
                Console.WriteLine("An error occurred: " + ex.Message);
                return "An error occurred while processing your request.";
            }

        }

        public async Task<string> UpdateBatchStatusAck(string BatchNo)
        {
            string responseMessage = "";

            try
            {
                var responseMessageParam = new SqlParameter("@Response_Message", SqlDbType.NVarChar, 100);
                responseMessageParam.Direction = ParameterDirection.Output;

                var result = _dbContext.Database.ExecuteSqlRaw("EXEC SP_AddFileRecievedClosed @BatchID,@UserId,@Response_Message OUTPUT",
                    new SqlParameter("@BatchID", BatchNo),
                    new SqlParameter("@UserId", _gluID.GetUserID()), responseMessageParam);
                responseMessage = responseMessageParam.Value.ToString();
                return responseMessage;
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("An error occurred: " + ex.Message);
                return "An error occurred while processing your request.";
            }

        }


        

    }
}
