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
    [Authorize]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        readonly IRoleDomain _roleDomain;
        private IExceptionHandling _exceptionHandling;
        public RoleController(IRoleDomain roleDomain, IExceptionHandling exceptionHandling)
        {
            _roleDomain = roleDomain;
            _exceptionHandling = exceptionHandling;
        }
        [HttpPost]
        [Route("GetPageAccess")]
        public async Task<IActionResult> GetRoleAccess(int RoleID,int MenuId)
        {
            var GetAccess = await _roleDomain.GetCheckPageAccess(RoleID, MenuId);
             
            if (GetAccess.FirstOrDefault() == "0")
            { 
                GlobalResponse.Response_Message = "You have Authorizie this Page"; 
                return Ok(GlobalResponse);
            }
            else
            {
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = "You have not Authorizie this Page.";
                return Ok(GlobalError);
            } 
            return Ok(GlobalError);
        } 
        [HttpGet]
        [Route("GetAllRole")]
        public async Task<IActionResult> Get()
        {
            try
            {
                //GlobalResponse.ReponseData = await _roleDomain.GetAll();

                var IResult = await _roleDomain.GetAll();

                if (IResult.Count != 0)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record fetched Successfully....";
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
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);


                return Ok(GlobalError);
            }
        }
        [HttpPost("GetRoleById")]
        public async Task<IActionResult> GetRoleById(GetRoleByIdRequestModel roleId)
        {
            try
            {
                GlobalResponse.ReponseData = await _roleDomain.GetRoleById(roleId);

                if (GlobalResponse.ReponseData != null)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record fetched Successfully....";
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
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        }
        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> Post(AddRoleRequestModel request)
        {
            try
            {
                var validations = await _roleDomain.AddValidation(request);

                if (validations.Count == 0)
                {
                    await _roleDomain.Add(request);

                    GlobalResponse.Response_Message = "Record Added successfully";
                    GlobalResponse.Response_Code = 200;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 204;
                    GlobalResponse.Response_Message = "Role Already Exists";
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
        [Route("UpdateRole")]

        public async Task<IActionResult> Put(UpdateRoleRequestModel request)
        {
            try
            {
                var validations = await _roleDomain.UpdateValidation(request);
                if (validations.FirstOrDefault() == "0")
                {

                    await _roleDomain.Update(request);
                    GlobalResponse.Response_Message = "Record Updated successfully"; 

                }
                else if (validations.FirstOrDefault() == "1")
                {
                    GlobalResponse.Response_Code = 204;
                    GlobalResponse.Response_Message = "Role already exists..";
               
                }
                else if (validations.FirstOrDefault() == "2")
                {
                    GlobalResponse.Response_Code = 204;
                    GlobalResponse.Response_Message = "Role not found for update."; 


                }
                return Ok(GlobalResponse);
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Trace_Point = ex.StackTrace;
                GlobalError.Error_Message = ex.Message;
                return Ok(GlobalError);
            }
        }
        [HttpPost]
        [Route("DeleteRole")]
        public async Task<IActionResult> Delete(GetRoleByIdRequestModel id)
        {
            try
            {

                var validations = await _roleDomain.DeleteValidation(id);
                if (validations.FirstOrDefault() == "1")
                {
                    await _roleDomain.Delete(id);
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