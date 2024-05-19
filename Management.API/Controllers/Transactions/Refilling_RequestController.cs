using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Services.Masters;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;

namespace Management.API.Controllers.Transactions
{
    [Route("api/[controller]")]
    [ApiController]
    public class Refilling_RequestController : ControllerBase
    {

        private readonly IRefilling_RequestDomain _RequestDomain;
        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        private IExceptionHandling _exceptionHandling;

        public Refilling_RequestController(IRefilling_RequestDomain requestDomain, IExceptionHandling exceptionHandling, IValidateTokenExtension validateTokenExtension)
        {
            _RequestDomain = requestDomain;
            _exceptionHandling = exceptionHandling;
        }

        [HttpPost("Gennerate_Request")]
        public async Task<IActionResult> Gennerate_Request(Refilling_ReqCheck ReqNumber)
        {
            try
            {
                {
                    var ReqGenerationKey = await _RequestDomain.AddValidation_GenRR(ReqNumber);


                    if (ReqGenerationKey != null)
                    {
                        ReqNumber.REF_Number = ReqGenerationKey.FirstOrDefault();

                        GlobalResponse.ReponseData = await _RequestDomain.AddRefilling(ReqNumber);
                        GlobalResponse.ReponseData = GlobalResponse.ReponseData;
                        GlobalResponse.Response_Code = 200;

                        GlobalResponse.Response_Message = "Record fetched Successfully";
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 204;
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
        [HttpGet("GetRefillingBy_REF_Number")]
        public async Task<IActionResult> GetRetrivelBy_REQNumber(string Get_REF_Number)
        {
            try
            {
                {
                    var ReqGenerationKey = await _RequestDomain.GetRefillingDetailsByReqNumber(Get_REF_Number);


                    if (ReqGenerationKey != null)
                    {
                        GlobalResponse.ReponseData = ReqGenerationKey;
                        GlobalResponse.Response_Code = 200;

                        GlobalResponse.Response_Message = "Record fetched Successfully";
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 204;
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
        [HttpGet("GeRefilling_Ref1")]
        public async Task<IActionResult> GetRetrive_RefNumber(string Str_Ref1)
        {
            try
            {
                {
                    var ReqGenerationKey = await _RequestDomain.GetRefillingDetailsByLenNumber(Str_Ref1);


                    if (ReqGenerationKey != null)
                    {
                        GlobalResponse.ReponseData = ReqGenerationKey;
                        GlobalResponse.Response_Code = 200;

                        GlobalResponse.Response_Message = "Record fetched Successfully";
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 204;
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

        [HttpGet("GeRefillingAll_RefNumber")]
        public async Task<IActionResult> GeRefillingAll_RefNumber(string RefNumber)
        {
            try
            {
                {
                    var ReqGenerationKey = await _RequestDomain.GetRefillingAllByRefNumber(RefNumber);


                    if (ReqGenerationKey.Count > 0)
                    {
                        GlobalResponse.ReponseData = ReqGenerationKey;
                        GlobalResponse.Response_Code = 200;

                        GlobalResponse.Response_Message = "Record fetched Successfully";
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 204;
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

        [HttpPost("Add_Refilling_Transaction")]
        public async Task<IActionResult> AddRetrivel_Transaction(RequestRefillingTransaction requestRetrivelTransaction)
        {
            try
            {
                {
                    var ReqGenerationKey = await _RequestDomain.CheckRefillingValidationAndInsert(requestRetrivelTransaction);

                    if (ReqGenerationKey != null)
                    {
                        // GlobalResponse.ReponseData = ReqGenerationKey;
                        GlobalResponse.Response_Code = 200;
                        GlobalResponse.Response_Message = ReqGenerationKey.FirstOrDefault();
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 204;
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

        [HttpPost("Refilling_Process")]
        public async Task<IActionResult> Retrivel_Process(RefillingProcess_Model rfillingProcess_Model)
        {
            var GetResult = await _RequestDomain.Refilling_Process(rfillingProcess_Model);

            if (GetResult != null)
            {
                GlobalResponse.ReponseData = GetResult.FirstOrDefault();
                GlobalResponse.Response_Code = 200; 
                return Ok(GlobalResponse);
            }
            else
            {
                GlobalResponse.Response_Code = 204;
                GlobalResponse.Response_Message = "Record not found for Process";
                return Ok(GlobalResponse);
            }

        }

        [HttpGet("GetAllRefilling_Closed")]
        public async Task<IActionResult> GetAllRefilling_Closed()
        {
            try
            {
                {
                    var ReqGenerationKey = await _RequestDomain.GetAllRefilling_Closed();

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

        [HttpPost("GetALLReffilBefore_ACK")]
        public async Task<IActionResult> GetALLReffilBefore_ACK(string UserID)
        {
            var GetResult = await _RequestDomain.GetALLReffilBefore_ACK(UserID);

            if (GetResult != null)
            {
                GlobalResponse.ReponseData = GetResult;
                GlobalResponse.Response_Code = 200;
                return Ok(GlobalResponse);
            }
            else
            {
                GlobalResponse.Response_Code = 204;
                GlobalResponse.Response_Message = "Record not found for Process";
                return Ok(GlobalResponse);
            }

        }
        [HttpPost("GetALLReffilParents_ACK")]
        public async Task<IActionResult> GetALLReffilParents_ACK()
        {
            var GetResult = await _RequestDomain.GetAllParent_Retrive1_Ack();

            if (GetResult != null)
            {
                GlobalResponse.ReponseData = GetResult;
                GlobalResponse.Response_Code = 200;
                return Ok(GlobalResponse);
            }
            else
            {
                GlobalResponse.Response_Code = 204;
                GlobalResponse.Response_Message = "Record not found for Process";
                return Ok(GlobalResponse);
            }

        }
    }

}
