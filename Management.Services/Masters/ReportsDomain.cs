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
namespace Management.Services.Masters
{
    public interface IReportsDomain
    {
        Task<List<DumpUpload>> GetReports_DumpUpload(Report_Dump_Model objReportDumpModel);
        Task<List<RetrievalTransactionModel>> GetReports_RetrivelUpload(Report_Retrivel_Model objRetrivalModel);
        Task<List<RefillingTransactionModel>> GetReports_RefillingUpload(Report_Refilling_Model objReplorRefillingModel);
        Task<List<QuickSearch>> GetReports_GetQuick_Search(string Search_By, string Search_Values);

    }
    public class ReportsDomain : IReportsDomain
    {
        readonly RMS_2024Context _dbContext;
        private HashSet<string> validationMessage { get; set; }
        public ReportsDomain(RMS_2024Context _dbContext)
        {
            this._dbContext = _dbContext;
            validationMessage = new HashSet<string>();
        }
        public async Task<List<DumpUpload>> GetReports_DumpUpload(Report_Dump_Model objReportDumpModel)
        {

            var ref1Param = objReportDumpModel.Ref1 != null ?
              new SqlParameter("@Ref1", objReportDumpModel.Ref1) :
              new SqlParameter("@Ref1", DBNull.Value);
            var fromDateParam = objReportDumpModel.FromDate != null ?
                new SqlParameter("@FromDate", objReportDumpModel.FromDate) :
                new SqlParameter("@FromDate", DBNull.Value);
            var toDateParam = objReportDumpModel.ToDate != null ?
                new SqlParameter("@ToDate", objReportDumpModel.ToDate) :
                new SqlParameter("@ToDate", DBNull.Value);
            var outputMessage = new StringBuilder();

            return await _dbContext.Set<DumpUpload>().FromSqlRaw("EXEC SP_Report_Dump_Details @Ref1, @FromDate, @ToDate", ref1Param, fromDateParam, toDateParam).ToListAsync();



        }

        public async Task<List<RetrievalTransactionModel>> GetReports_RetrivelUpload(Report_Retrivel_Model objRetrivalModel)
        {
            string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            List<RetrievalTransactionModel> result;

            string sqlQuery = "EXEC SP_Report_Retrivel_Details @REQ_Number, @Ref1, @FromDate, @ToDate";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = connection.Query<RetrievalTransactionModel>(sqlQuery,
                    new
                    {
                        REQ_Number = objRetrivalModel.Reqnumber ?? (object)DBNull.Value,
                        Ref1 = objRetrivalModel.Ref1 ?? (object)DBNull.Value,
                        FromDate = objRetrivalModel.FromDate.HasValue ? (object)objRetrivalModel.FromDate : DBNull.Value,
                        ToDate = objRetrivalModel.ToDate.HasValue ? (object)objRetrivalModel.ToDate : DBNull.Value

                    }).ToList();
            }
            return result; 
        }
        public async Task<List<RefillingTransactionModel>> GetReports_RefillingUpload(Report_Refilling_Model objReplorRefillingModel)
        {
            string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            List<RefillingTransactionModel> result;

            string sqlQuery = "EXEC SP_Report_Refilling_Details @REQ_Number, @Ref1, @FromDate, @ToDate";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = connection.Query<RefillingTransactionModel>(sqlQuery,
                    new
                    {
                        REQ_Number = objReplorRefillingModel.Refnumber ?? (object)DBNull.Value,
                        Ref1 = objReplorRefillingModel.Ref1 ?? (object)DBNull.Value,
                        FromDate = objReplorRefillingModel.FromDate.HasValue ? (object)objReplorRefillingModel.FromDate : DBNull.Value,
                        ToDate = objReplorRefillingModel.ToDate.HasValue ? (object)objReplorRefillingModel.ToDate : DBNull.Value

                    }).ToList();
            } 
            return result;
        } 
        public async Task<List<QuickSearch>> GetReports_GetQuick_Search(string Search_By, string Search_Values)
        {
            string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            List<QuickSearch> result;

            string sqlQuery = "EXEC SP_Quick_Report_Details @Search_by, @SearchValue";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = connection.Query<QuickSearch>(sqlQuery,
                    new
                    {
                        Search_By = Search_By ?? (object)DBNull.Value,
                        @SearchValue = Search_Values ?? (object)DBNull.Value, 

                    }).ToList();
            }
            return result;
        }

    }
}
