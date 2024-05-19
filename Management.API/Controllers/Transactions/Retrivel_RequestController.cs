using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Services.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;

namespace Management.API.Controllers.Transactions
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class Retrivel_RequestController : ControllerBase
    {

        private readonly IRetrivel_RequestDomain _RequestDomain;
        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        private IExceptionHandling _exceptionHandling;

        public Retrivel_RequestController(IRetrivel_RequestDomain requestDomain, IExceptionHandling exceptionHandling, IValidateTokenExtension validateTokenExtension)
        {
            _RequestDomain = requestDomain;
            _exceptionHandling = exceptionHandling;
        }

        [HttpPost("Gennerate_Request")]
        public async Task<IActionResult> Gennerate_Request()
        {
            try
            {
                {
                    var ReqGenerationKey = await _RequestDomain.AddValidation_GenRR();


                    if (ReqGenerationKey != null)
                    {
                        string GetNewReqNumber = ReqGenerationKey.FirstOrDefault();
                        GlobalResponse.ReponseData = await _RequestDomain.AddRetrivel(GetNewReqNumber);
                        GlobalResponse.ReponseData = GlobalResponse.ReponseData;
                        GlobalResponse.Response_Code = 200;

                        GlobalResponse.Response_Message = "Record fetched Successfully";
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 404;
                        GlobalResponse.Response_Message = "Record not found";
                        return Ok(GlobalResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 5051231;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        }
        [HttpGet("GetRetrivelBy_REQNumber")] 
        public async Task<IActionResult> GetRetrivelBy_REQNumber(string GetReqNumber)
        {
            try
            {

                var ReqGenerationKey = await _RequestDomain.GetRetrivelDetailsByReqNumber(GetReqNumber);


                if (ReqGenerationKey != null)
                {
                    GlobalResponse.ReponseData = ReqGenerationKey;
                    GlobalResponse.Response_Code = 200;

                    GlobalResponse.Response_Message = "Record fetched Successfully";
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 404;
                    GlobalResponse.Response_Message = "Record not found";
                    return Ok(GlobalResponse);
                }

            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 5051231;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        }
        [HttpGet("GetRetrive_RefNumber")]
        public async Task<IActionResult> GetRetrive_RefNumber(string Str_Ref1)
        {
            try
            {
                {
                    var ReqGenerationKey = await _RequestDomain.GetRetrivelDetailsByLenNumber(Str_Ref1);


                    if (ReqGenerationKey != null)
                    {
                        GlobalResponse.ReponseData = ReqGenerationKey;
                        GlobalResponse.Response_Code = 200;

                        GlobalResponse.Response_Message = "Record fetched Successfully";
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 404;
                        GlobalResponse.Response_Message = "Record not found";
                        return Ok(GlobalResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 5051231;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        }
        [HttpPost("Add_Retrivel_Transaction")]
        public async Task<IActionResult> AddRetrivel_Transaction(RequestRetrivelTransaction requestRetrivelTransaction)
        {
            try
            {
                {
                    var ReqGenerationKey = await _RequestDomain.CheckRetrievalValidationAndInsert(requestRetrivelTransaction);

                    if (ReqGenerationKey != null)
                    {
                        // GlobalResponse.ReponseData = ReqGenerationKey;
                        GlobalResponse.Response_Code = 200;
                        GlobalResponse.Response_Message = ReqGenerationKey.FirstOrDefault();
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 404;
                        GlobalResponse.Response_Message = "Record not found";
                        return Ok(GlobalResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 5051231;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        }
        [HttpGet("GetAllRetrivel_Closed")]
        public async Task<IActionResult> GetAllRetrivel_Closed()
        {
            try
            {
                {
                    var ReqGenerationKey = await _RequestDomain.GetAllRetrivel_Closed();

                    if (ReqGenerationKey != null)
                    {
                       
                        GlobalResponse.Response_Code = 200;
                        GlobalResponse.Response_Message = "Record fatch Succefullay" ;
                         GlobalResponse.ReponseData = ReqGenerationKey;
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 404;
                        GlobalResponse.Response_Message = "Record not found";
                        return Ok(GlobalResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 5051231;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        }
        [HttpGet("GetAllRetrivel_Approval")]
        public async Task<IActionResult> GetAllRetrivel_Approval()
        {
            try
            {
                {
                    var ReqGenerationKey = await _RequestDomain.GetAllRetrive1_Approval();

                    if (ReqGenerationKey != null)
                    {

                        GlobalResponse.Response_Code = 200;
                        GlobalResponse.Response_Message = "Record fatch Succefullay";
                        GlobalResponse.ReponseData = ReqGenerationKey;
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 404;
                        GlobalResponse.Response_Message = "Record not found";
                        return Ok(GlobalResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 5051231;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        }

        [HttpGet("GetAllRetrivel_Dispatch")]
        public async Task<IActionResult> GetAllParent_Retrive1_Dispatch()
        {
            try
            {
                {
                    var ReqGenerationKey = await _RequestDomain.GetAllParent_Retrive1_Dispatch();

                    if (ReqGenerationKey != null)
                    {

                        GlobalResponse.Response_Code = 200;
                        GlobalResponse.Response_Message = "Record fatch Succefullay";
                        GlobalResponse.ReponseData = ReqGenerationKey;
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 404;
                        GlobalResponse.Response_Message = "Record not found";
                        return Ok(GlobalResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 5051231;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        }
        [HttpGet("GetRetrievalForDispatchByRequestNumber")]
        public async Task<IActionResult> GetAllRetrivel_ByRef(string ReqNumber)
        {
            try
            {
                {
                    var ReqGenerationKey = await _RequestDomain.GetAllRetrive1_ByRef(ReqNumber);

                    if (ReqGenerationKey != null)
                    {

                        GlobalResponse.Response_Code = 200;
                        GlobalResponse.Response_Message = "Record fatch Succefullay";
                        GlobalResponse.ReponseData = ReqGenerationKey;
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 404;
                        GlobalResponse.Response_Message = "Record not found";
                        return Ok(GlobalResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 5051231;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        }


        [HttpPost("Retrivel_Process")]
        public async Task<IActionResult> Retrivel_Process(RetrivelProcess_Model retrivelProcess_Model)
        {
            var GetResult = await _RequestDomain.Retrivel_Process(retrivelProcess_Model);

            if (GetResult != null)
            {
                GlobalResponse.ReponseData = GetResult.FirstOrDefault();
                GlobalResponse.Response_Code = 200;
                return Ok(GlobalResponse);
            }
            else
            {
                GlobalResponse.Response_Code = 404;
                GlobalResponse.Response_Message = "Record not found for Process";
                return Ok(GlobalResponse);
            }

        }
    }

}
