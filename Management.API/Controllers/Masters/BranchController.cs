using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Model.RMSEntity;
using Management.Services.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TwoWayCommunication.Model.Enums;

namespace Management.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BranchController : ControllerBase
    {
        
        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        readonly IBranchMasterDomain _branchMasterDomain;
        private IExceptionHandling _exceptionHandling;
        public BranchController(IBranchMasterDomain branchMasterDomain, IExceptionHandling exceptionHandling)
        {
            _branchMasterDomain = branchMasterDomain;
            _exceptionHandling = exceptionHandling;
           
        }
        [HttpGet]
        [Route("GetAllBranch")]
        public async Task<IActionResult> Get()
        {
            try
            { 
                var IResult = await _branchMasterDomain.GetAll();
                    //GlobalResponse.ReponseData = await _branchMasterDomain.GetAll();


                    if (IResult.Count != 0)
                    {
                        GlobalResponse.Response_Code = 200;
                        GlobalResponse.Response_Message = "Record fetched Successfully";
                        GlobalResponse.ReponseData = IResult;
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 204;
                        GlobalResponse.Response_Message = "Record not found";
                        return Ok(GlobalResponse);
                    } 
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 12313;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);


                return Ok(GlobalError);
            }
        }

        [HttpGet]
        [Route("GetAllIsActiveBranch")]
        public async Task<IActionResult> GetAllIsActiveBranch()
        {
            try
            {
                var IResult = await _branchMasterDomain.GetAllIsActiveBranch();
                //GlobalResponse.ReponseData = await _branchMasterDomain.GetAll();


                if (IResult.Count != 0)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record fetched Successfully";
                    GlobalResponse.ReponseData = IResult;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 204;
                    GlobalResponse.Response_Message = "Record not found";
                    GlobalResponse.ReponseData = IResult;
                    return Ok(GlobalResponse);
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 12313;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);


                return Ok(GlobalError);
            }
        }

        [HttpPost("GetBranchById")]
        public async Task<IActionResult> GetBranchById(GetBranchByIdRequestModel id)
        {

            try
            {
                {
                    GlobalResponse.ReponseData = await _branchMasterDomain.GetBranchById(id);

                    if (GlobalResponse.ReponseData != null)
                    {
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
        [HttpPost]
        [Route("AddBranch")]
        public async Task<IActionResult> Post(AddBranchRequestModel request)
        {
            //string GetToken = "";
            //var GetResponse = _validateTokenExtension.ValidateToken(GetToken);

            try
            {
                //if (GetResponse.ValiedTokenRespose.Trim() == "Valid token")
                //{
                    var validations = await _branchMasterDomain.AddValidation(request);

                    if (validations.Count == 0)
                    {
                        await _branchMasterDomain.Add(request);
                        GlobalResponse.Response_Message = "Branch Added successfully";
                        GlobalResponse.Response_Code = 200;
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalError.ErrorCode = 505;
                        GlobalError.Error_Message = "Branch already exists";
                        return Ok(GlobalError);
                    }
                //}
                //else
                //{
                //    GlobalResponse.Response_Code = 204;
                //    GlobalResponse.Response_Message = "Token is not Valid....";
                //    return Ok(GlobalResponse);
                //}
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 501232135;
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
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = "Branch does not exists";
                return Ok(GlobalError);
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 505123123;
                GlobalError.Error_Trace_Point = ex.StackTrace;
                GlobalError.Error_Message = ex.Message;
                return Ok(GlobalError);
            }
        }
        [HttpPost]
        [Route("DeleteBranch")]
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
                GlobalError.ErrorCode = 204;
                GlobalError.Error_Message = "Record not found for deletion";
                return Ok(GlobalError);
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 505123123121;
                GlobalError.Error_Trace_Point = ex.StackTrace;
                GlobalError.Error_Message = ex.Message;
                return Ok(GlobalError);
            }
        }
        
    }
    
}