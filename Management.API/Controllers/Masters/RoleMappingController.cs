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
        readonly IRoleMapDomain _roleMapDomain;
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
                //GlobalResponse.ReponseData = await _roleMapDomain.GetAllRoleMap();
                var IResult = await _roleMapDomain.GetAllRoleMap();
                if (IResult != null)
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
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);


                return Ok(GlobalError);
            }
        }
        [HttpGet]
        [Route("GetAllMenuForAll_RoleMapping")]
        public async Task<IActionResult> GetAllMenuForAll_RoleMapping()
        {
            try
            {
                //GlobalResponse.ReponseData = await _roleMapDomain.GetAllRoleMap();
                var IResult = await _roleMapDomain.GetAllMenuForAll_RoleMapping();
                if (IResult != null)
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
                var GetDATA = await _roleMapDomain.GetRoleMapById(roleId);

                //if (GlobalResponse.ReponseData == null)
                //{
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record fetched Successfully....";
                    GlobalResponse.ReponseData = GetDATA;
                    return Ok(GlobalResponse);
                //}
                //else
                //{
                //    GlobalResponse.Response_Code = 204;
                //    GlobalResponse.Response_Message = "Record not found";
                //    GlobalResponse.ReponseData = [];
                //    return Ok(GlobalResponse);
                //}
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
                var GetResponse = await _roleMapDomain.Add(request);

                GlobalResponse.Response_Message = GetResponse.FirstOrDefault();
                GlobalResponse.Response_Code = 200;
                return Ok(GlobalResponse); 
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
        public async Task<IActionResult> Put(AddRoleMapRequestModel request)
        {
            try
            {
                var GetResponse  =  await _roleMapDomain.Update(request);
                GlobalResponse.Response_Message = GetResponse.FirstOrDefault();
                return Ok(GlobalResponse);
                 
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

        [HttpPost()]
        [Route("Update_RightsByRole_id")]
        public async Task<IActionResult> Update_RightsBy_Role_id(UpdateRoleRight_by_Role_ID request)
        {
            try
            {
                await _roleMapDomain.Update_Rights(request);
                GlobalResponse.Response_Message = "Record Updated successfully";
                return Ok(GlobalResponse);

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


        [HttpPost]
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