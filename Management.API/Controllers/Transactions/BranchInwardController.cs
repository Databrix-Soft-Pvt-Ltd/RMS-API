using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Services.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TwoWayCommunication.Model.Enums;

namespace Management.API.Controllers.Transactions
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BranchInwardController : ControllerBase
    {
        private readonly IBranchInwardDomain _iBranchInwardDomain;
        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        private IExceptionHandling _exceptionHandling;
        private readonly IValidateTokenExtension _validateTokenExtension;
        public BranchInwardController(IBranchInwardDomain _iBIDomain, IExceptionHandling exceptionHandling, IValidateTokenExtension validateTokenExtension)
        {
            this._iBranchInwardDomain = _iBIDomain;
            _exceptionHandling = exceptionHandling;
            _validateTokenExtension = validateTokenExtension;
        }

        [HttpGet("getBatchID")]
        public async Task<IActionResult> getBatchID()
        {
            try
            {
                var RsultData = await _iBranchInwardDomain.getBatchID();

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

        [HttpGet("getBatchDetails")]
        public async Task<IActionResult> getBatchDetails(string BatchID)
        {
            try
            {
                var RsultData = await _iBranchInwardDomain.getBatchDetails(BatchID);
                if (RsultData.Count > 0)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record found Successfully";
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

        [HttpGet("getRef1DetailsBranchInward")]
        public async Task<IActionResult> getRef1DetailsBranchInward(string Ref1)
        {
            try
            {
                var RsultData = await _iBranchInwardDomain.getRef1DetailsBranchInward(Ref1);
                if (RsultData.Count > 0)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record found Successfully";
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

        [HttpPost("AddBranchInward")]
        public async Task<IActionResult> AddBranchInward(AddBIDetailsRequestModel request)
        {
            try
            {
                var RsultData = await _iBranchInwardDomain.AddBranchInward(request);
                if (RsultData != "")
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record saved Successfully";
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

        [HttpPost("CloseBatch")]
        public async Task<IActionResult> UpdatePODNumber(UpdatePODNumberRequestModel request)
        {
            try
            {
                var RsultData = await _iBranchInwardDomain.UpdatePODNumber(request);
                if (RsultData != "")
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record saved Successfully";
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

        [HttpPost("UpdateFileCk")]
        public async Task<IActionResult> UpdateFileCk(UpdateFileACKRequestModel request)
        {
            try
            {
                var RsultData = await _iBranchInwardDomain.UpdateAckFile(request);
                if (RsultData != "")
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record saved Successfully";
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

        [HttpGet("getBatchInwardPendingDetails")]
        public async Task<IActionResult> getBatchInwardPendingDetails()
        {
            try
            {
                var RsultData = await _iBranchInwardDomain.getBatchInwardPendingDetails();
                if (RsultData.Count > 0)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record found Successfully";
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

        [HttpGet("getCourierAckPending")]
        public async Task<IActionResult> getCourierAckPending()
        {
            try
            {
                var RsultData = await _iBranchInwardDomain.getCourierAckPending();
                if (RsultData.Count > 0)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record found Successfully";
                    GlobalResponse.ReponseData = RsultData;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 400;
                    GlobalResponse.Response_Message = "Record not found";
                    GlobalResponse.ReponseData = RsultData;
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

        [HttpPost("CourierACk")]
        public async Task<IActionResult> CourierACk(CourierACKRequestModel requestd)
        {
            try
            {
                var RsultData = await _iBranchInwardDomain.CourierACk(requestd);
                if (RsultData != "")
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record saved Successfully";
                    GlobalResponse.ReponseData = RsultData;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 400;
                    GlobalResponse.Response_Message = "Record not found";
                    GlobalResponse.ReponseData = RsultData;
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

        [HttpGet("getFileAckPending")]
        public async Task<IActionResult> getFileAckPending()
        {
            try
            {
                var RsultData = await _iBranchInwardDomain.getFileAckPending();
                if (RsultData.Count > 0)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record found Successfully";
                    GlobalResponse.ReponseData = RsultData;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 204;
                    GlobalResponse.Response_Message = "Record not found";
                    GlobalResponse.ReponseData = RsultData;
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

        [HttpGet("getLanDetailsByBatchNo")]
        public async Task<IActionResult> getLanDetailsByBatchNo(string BatchNo)
        {
            try
            {
                var RsultData = await _iBranchInwardDomain.getLanDetailsByBatchNo(BatchNo);
                if (RsultData.Count > 0)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record found Successfully";
                    GlobalResponse.ReponseData = RsultData;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 204;
                    GlobalResponse.Response_Message = "Record not found";
                    GlobalResponse.ReponseData = RsultData;
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

        [HttpGet("getackDetailsByLanAndBatchNo")]
        public async Task<IActionResult> getackDetailsByLanAndBatchNo(string BatchNo, string Ref1)
        {
            try
            {
                var RsultData = await _iBranchInwardDomain.getackDetailsByLanAndBatchNo(BatchNo, Ref1);
                if (RsultData.Count > 0)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record found Successfully";
                    GlobalResponse.ReponseData = RsultData;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 204;
                    GlobalResponse.Response_Message = "Record not found";
                    GlobalResponse.ReponseData = RsultData;
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

        [HttpGet("getDocumentDetailsByLanNo")]
        public async Task<IActionResult> getDocumentDetailsByLanNo(string Ref1)
        {
            try
            {
                var RsultData = await _iBranchInwardDomain.getDocumentDetailsByLanNo(Ref1);
                if (RsultData.Count > 0)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record found Successfully";
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

        [HttpGet("UpdateBatchStatusAck")]
        public async Task<IActionResult> UpdateBatchStatusAck(string BatchNo)
        {
            try
            {
                var RsultData = await _iBranchInwardDomain.UpdateBatchStatusAck(BatchNo);
                if (RsultData != "")
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record saved Successfully";
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