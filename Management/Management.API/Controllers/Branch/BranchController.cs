using Management.Model.RequestModel;
using Management.Model.RMSEntity;
using Management.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwoWayCommunication.Domain.Authentication;

namespace Management.API.Controllers.Branch
{
    [Route("api/[controller]")]
   // [Authorize]
    [ApiController]
    public class BranchController : ControllerBase
    {

        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        readonly IBranchMasterDomain _branchMasterDomain;
        private IExceptionHandling _exceptionHandling; 
        private  readonly IValidateTokenExtension _validateTokenExtension; 
        public BranchController(IBranchMasterDomain branchMasterDomain, IExceptionHandling exceptionHandling,IValidateTokenExtension validateTokenExtension)
        {
            _branchMasterDomain = branchMasterDomain;
            _exceptionHandling = exceptionHandling;
            _validateTokenExtension = validateTokenExtension;
        } 
        [HttpGet]
        [Route("GetAllBranch")]
        public async Task<IActionResult> Get()
        {
            try
            {

                GlobalResponse.ReponseData = await _branchMasterDomain.GetAll();

                if (GlobalResponse.ReponseData != null)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record fetched Successfully....";
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
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);


                return Ok(GlobalError);
            }
        }



        [HttpPost("GetBranchById")]
        public async Task<IActionResult> GetUserById(GetBranchByIdRequestModel id)
        {
            string GetToken = "";  
            var GetResponse = _validateTokenExtension.ValidateToken(GetToken);

            try
            {
                if (GetResponse.ValiedTokenRespose.Trim() == "Valid token")
                {
                    {
                        GlobalResponse.ReponseData = await _branchMasterDomain.GetBranchById(id);

                        if (GlobalResponse.ReponseData != null)
                        {
                            GlobalResponse.Response_Code = 200;
                            GlobalResponse.Response_Message = "Record fetched Successfully....";
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
                else
                {
                    GlobalResponse.Response_Code = 404;
                    GlobalResponse.Response_Message = "Token is not Valid....";
                    return Ok(GlobalResponse);
                }
            } 
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        }
        [HttpPost]
        [Route("AddBranch")]
        public async Task<IActionResult> Post(AddBranchRequestModel request)
        {
            string GetToken = ""; 
            var GetResponse = _validateTokenExtension.ValidateToken(GetToken);

            try
            {
                if (GetResponse.ValiedTokenRespose.Trim() == "Valid token")
                { 
                    var validations = await _branchMasterDomain.AddValidation(request);

                    if (validations.Count == 0)
                    {
                        await _branchMasterDomain.Add(request);
                        GlobalResponse.Response_Message = "Record Added successfully";
                        GlobalResponse.Response_Code = 200;
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalError.ErrorCode = 505;
                        GlobalError.Error_Message = "Failed to add record";
                        return BadRequest(validations.FirstOrDefault());
                    }
                }
                else
                {
                    GlobalResponse.Response_Code = 404;
                    GlobalResponse.Response_Message = "Token is not Valid....";
                    return Ok(GlobalResponse);
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Trace_Point = ex.StackTrace;
                GlobalError.Error_Message = ex.Message;
                return StatusCode(500, GlobalError);
            }
        }
        [HttpPut()]
        [Route("UpdateBranch")]

        public async Task<IActionResult> Put(UpdateBranchRequestModel request)
        {
            try
            {
                var validations = await _branchMasterDomain.UpdateValidation(request);
                if (validations.Count() == 0)
                {
                    await _branchMasterDomain.Update(request);

                    GlobalResponse.Response_Message = "Record Updated successfully";
                    return Ok(GlobalResponse);
                }
                return BadRequest(validations.FirstOrDefault());

            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Trace_Point = ex.StackTrace;
                GlobalError.Error_Message = ex.Message;
                return Ok(GlobalError);
            }
        }


        [HttpDelete]
        [Route("DeleteClient")]
        public async Task<IActionResult> Delete(GetBranchByIdRequestModel id)
        {
            try
            {
                var validations = await _branchMasterDomain.DeleteValidation(id);
                if (validations.Count() == 0)
                {
                    await _branchMasterDomain.Delete(id);

                    GlobalResponse.Response_Message = "Record deleted successfully";

                    return Ok(GlobalResponse);
                }
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = "Failed to delete the record";
                return BadRequest(validations);
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Trace_Point = ex.StackTrace;
                GlobalError.Error_Message = ex.Message;
                return Ok(GlobalError);
            }
        }
    }
}