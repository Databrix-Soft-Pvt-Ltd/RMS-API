using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Services.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers.Transactions
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsDomain _iReportsDomain;
        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        private IExceptionHandling _exceptionHandling;
        public ReportsController(IReportsDomain _iReportsDomain, IExceptionHandling exceptionHandling)
        {
            this._iReportsDomain = _iReportsDomain;
            _exceptionHandling = exceptionHandling;
        }
        [HttpPost("GetDumpReport")]
        public async Task<IActionResult> Get_Dump_Reports(Report_Dump_Model report_DumP_Model)
        {
            try
            {
                var RsultData = await _iReportsDomain.GetReports_DumpUpload(report_DumP_Model);

                if (RsultData.Count > 0)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record fetched Successfully";
                    GlobalResponse.ReponseData = RsultData;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 400;
                    GlobalResponse.Response_Message = "Record not found";
                    GlobalResponse.ReponseData = "";
                    return Ok(GlobalResponse);
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 500;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }

            return Ok(GlobalResponse);


        }

        [HttpPost("GetReteivalReport")]
        public async Task<IActionResult> Get_Retrival_Reports(Report_Retrivel_Model report_Retrival_Model)
        {
            try
            {
                var RsultData = await _iReportsDomain.GetReports_RetrivelUpload(report_Retrival_Model);

                if (RsultData.Count > 0)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record fetched Successfully";
                    GlobalResponse.ReponseData = RsultData;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 400;
                    GlobalResponse.Response_Message = "Record not found";
                    GlobalResponse.ReponseData = "";
                    return Ok(GlobalResponse);
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 500;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }

            return Ok(GlobalResponse);

        }


        [HttpPost("GetRefillingReport")]
        public async Task<IActionResult> Get_Refilling_Report(Report_Refilling_Model report_Refilling_Model)
        {
            try
            {
                var RsultData = await _iReportsDomain.GetReports_RefillingUpload(report_Refilling_Model);

                if (RsultData.Count > 0)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record fetched Successfully";
                    GlobalResponse.ReponseData = RsultData;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 400;
                    GlobalResponse.Response_Message = "Record not found";
                    GlobalResponse.ReponseData = "";
                    return Ok(GlobalResponse);
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 500;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }

            return Ok(GlobalResponse);

        }

        [HttpPost("GetQuick_Search")]
        public async Task<IActionResult> GetQuick_Search(string Search_By,string Search_Values)
        {
            try
            {
                var RsultData = await _iReportsDomain.GetReports_GetQuick_Search(Search_By, Search_Values);

                if (RsultData.Count > 0)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record fetched Successfully";
                    GlobalResponse.ReponseData = RsultData;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 400;
                    GlobalResponse.Response_Message = "Record not found";
                    GlobalResponse.ReponseData = "";
                    return Ok(GlobalResponse);
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 500;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }

            return Ok(GlobalResponse);

        }
    }

}