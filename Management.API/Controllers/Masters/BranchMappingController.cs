using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Model.ResponseModel;
using Management.Services.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Management.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchMappingController : ControllerBase
    {
        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        readonly IBranchMappingDomain _branchMappingDomain;
        private IExceptionHandling _exceptionHandling;
        public BranchMappingController(IBranchMappingDomain branchMappingDomain, IExceptionHandling exceptionHandling)
        {
            _branchMappingDomain = branchMappingDomain;
            _exceptionHandling = exceptionHandling;
        }
        [HttpGet]
        [Route("GetAllBranchMap")]
        public async Task<IActionResult> Get()
        {
            try
            {
                GlobalResponse.ReponseData = await _branchMappingDomain.GetAllBranchMap();

                if (GlobalResponse.ReponseData != "0")
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
        [HttpPost("GetBranchMapById")]
        public async Task<IActionResult> GetRoleById(GetBranchMapByIdRequestModel roleId)
        {
            try
            {
                GlobalResponse.ReponseData = await _branchMappingDomain.GetBranchMapById(roleId);

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
        [HttpPost]
        [Route("AddBranchMap")]
        public async Task<IActionResult> Post(AddBranchMapRequestModel request)
        {
            try
            {
                var validations1 = await _branchMappingDomain.AddBranchMapValidation(request, "BranchMap");

                if (validations1.FirstOrDefault() == "0")
                {
                    var validations2 = await _branchMappingDomain.AddBranchMapValidation(request, "Branch");

                    if (validations2.FirstOrDefault() == "0")
                    {
                        var validations3 = await _branchMappingDomain.AddBranchMapValidation(request, "User");

                        if (validations3.FirstOrDefault() == "0")
                        {
                            await _branchMappingDomain.Add(request);
                            GlobalResponse.Response_Message = "Record Added successfully";
                            GlobalResponse.Response_Code = 200;
                            return Ok(GlobalResponse);
                        }
                        else
                        {
                            GlobalResponse.Response_Code = 404;
                            GlobalResponse.Response_Message = "User not found";
                            return Ok(GlobalResponse);
                        }
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 404;
                        GlobalResponse.Response_Message = "Branch not found";
                        return Ok(GlobalResponse);
                    }
                }
                else
                {
                    GlobalResponse.Response_Code = 404;
                    GlobalResponse.Response_Message = "BranchMap alredy found";
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
            return Ok(GlobalResponse);
        }
        [HttpPut()]
        [Route("UpdateBranchMap")]
        public async Task<IActionResult> Put(UpdateBranchMappingResponse request)
        {
            try
            {
                var validations1 = await _branchMappingDomain.UpdateBranchMapValidation(request, "BranchMap");

                if (validations1.FirstOrDefault() == "0")
                {
                    var validations2 = await _branchMappingDomain.UpdateBranchMapValidation(request, "Branch");

                    if (validations2.FirstOrDefault() == "0")
                    {
                        var validations3 = await _branchMappingDomain.UpdateBranchMapValidation(request, "User");

                        if (validations3.FirstOrDefault() == "0")
                        {
                            await _branchMappingDomain.Update(request);
                            GlobalResponse.Response_Message = "Record Updated successfully";
                            return Ok(GlobalResponse);
                        }
                        else
                        {
                            GlobalResponse.Response_Code = 404;
                            GlobalResponse.Response_Message = "User not found";
                            return Ok(GlobalResponse);
                        }
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 404;
                        GlobalResponse.Response_Message = "Branch not found";
                        return Ok(GlobalResponse);
                    }
                }
                else if (validations1.Count == 1)
                {
                    GlobalResponse.Response_Code = 404;
                    GlobalResponse.Response_Message = "BranchMap Already Exists";
                    return Ok(GlobalResponse);
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Trace_Point = ex.StackTrace;
                GlobalError.Error_Message = ex.Message;
                return Ok(GlobalError);
            }
            return Ok(GlobalResponse);
        }
        [HttpDelete]
        [Route("DeleteBranchMap")]
        public async Task<IActionResult> Delete(GetBranchMapByIdRequestModel id)
        {
            try
            {
                


                var validations = await _branchMappingDomain.DeleteValidation(id);
                if (validations.FirstOrDefault() == "1")
                {
                    await _branchMappingDomain.Delete(id);
                    GlobalResponse.Response_Message = "Record deleted successfully";
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalError.ErrorCode = 505;
                    GlobalError.Error_Message = "Record doesnot  Exists";
                    return Ok(GlobalError);
                }
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
