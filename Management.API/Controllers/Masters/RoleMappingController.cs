using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Model.ResponseModel;
using Management.Model.RMSEntity;
using Management.Services.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace Management.API.Controllers.Masters
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class RoleMappingController : ControllerBase
    {

        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        readonly IRoleMapDomain  _roleMapDomain;
        private IExceptionHandling _exceptionHandling;
        public RoleMappingController(IRoleMapDomain roleMapDomain, IExceptionHandling exceptionHandling)
        {
            _roleMapDomain = roleMapDomain;
            _exceptionHandling = exceptionHandling;
        } 
        [HttpGet]
        [Route("GetAllRoleMap")]
        public async Task<IActionResult> Get()
        {
            try
            {
                GlobalResponse.ReponseData = await _roleMapDomain.GetAllRoleMap();

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
        [HttpPost("GetRoleMapById")]
        public async Task<IActionResult> GetRoleById(GetRoleMapByIdRequestModel roleId)
        {
            try
            {
                GlobalResponse.ReponseData = await _roleMapDomain.GetRoleMapById(roleId);

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
        [Route("AddRoleMap")]
        public async Task<IActionResult> Post(AddRoleMapRequestModel request)
        {
            try
            {
                var validations1 = await _roleMapDomain.AddRoleMapValidation(request,"RoleMap");

                if (validations1.FirstOrDefault() == "0")
                { 
                    var validations2 = await _roleMapDomain.AddRoleMapValidation(request, "Role");

                    if (validations2.FirstOrDefault() == "0")
                    { 
                        var validations3 = await _roleMapDomain.AddRoleMapValidation(request, "Menu");

                        if (validations3.FirstOrDefault() == "0")
                        { 
                            await _roleMapDomain.Add(request);
                            GlobalResponse.Response_Message = "Record Added successfully";
                            GlobalResponse.Response_Code = 200;
                            return Ok(GlobalResponse);
                        }
                        else 
                        {
                            GlobalResponse.Response_Code = 404;
                            GlobalResponse.Response_Message = "Menu not found";
                            return Ok(GlobalResponse);
                        }
                    }
                    else 
                    {
                        GlobalResponse.Response_Code = 404;
                        GlobalResponse.Response_Message = "Role not found";
                        return Ok(GlobalResponse);
                    }
                } 
                else 
                {
                    GlobalResponse.Response_Code = 404;
                    GlobalResponse.Response_Message = "RoleMap alredy found";
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
        [Route("UpdateRoleMap")]

        public async Task<IActionResult> Put(UpdateRoleMappingResponse request)
        {
            try
            {
                var validations1 = await _roleMapDomain.UpdateRoleMapValidation(request,"RoleMap");

                if (validations1.FirstOrDefault() == "0")
                {
                    var validations2 = await _roleMapDomain.UpdateRoleMapValidation(request, "Role");

                    if (validations2.FirstOrDefault() == "0")
                    {
                        var validations3 = await _roleMapDomain.UpdateRoleMapValidation(request, "Menu");

                        if (validations3.FirstOrDefault() == "0")
                        { 
                            await _roleMapDomain.Update(request);
                            GlobalResponse.Response_Message = "Record Updated successfully";
                            return Ok(GlobalResponse); 
                        } 
                        else
                        {
                            GlobalResponse.Response_Code = 404;
                            GlobalResponse.Response_Message = "Menu not found";
                            return Ok(GlobalResponse);
                        }
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 404;
                        GlobalResponse.Response_Message = "Role not found";
                        return Ok(GlobalResponse);
                    }
                }
                else if (validations1.Count == 1)
                {
                    GlobalResponse.Response_Code = 404;
                    GlobalResponse.Response_Message = "RoleMap Already Exists";
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
        [Route("DeleteRoleMap")]
        public async Task<IActionResult> Delete(GetRoleMapByIdRequestModel id)
        {
            try
            { 
                var validations = await _roleMapDomain.DeleteValidation(id);
                if (validations.FirstOrDefault() == "1")
                {
                    await _roleMapDomain.Delete(id);
                    GlobalResponse.Response_Message = "Record deleted successfully"; 
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalError.ErrorCode = 505;
                    GlobalError.Error_Message = "Role doesnot  Exists";
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