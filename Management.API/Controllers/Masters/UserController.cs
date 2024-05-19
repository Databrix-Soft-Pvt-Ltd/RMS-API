using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Model.RMSEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;  
using static Management.Services.Masters.UserDomain;

namespace Management.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {

        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        readonly IUserDomain _userDomain;
        private IExceptionHandling _exceptionHandling;

        public UserController(IUserDomain userDomain, IExceptionHandling exceptionHandling)
        {
            _userDomain = userDomain;
            _exceptionHandling = exceptionHandling;
        } 
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> Get()
        {
            try
            {
                GlobalResponse.ReponseData = await _userDomain.GetAll();

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
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 123505;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);


                return Ok(GlobalError);
            }
        }

        [HttpGet]
        [Route("GetAllUsersIsActive")]
        public async Task<IActionResult> GetAllUsersIsActive()
        {
            try
            {
                GlobalResponse.ReponseData = await _userDomain.GetAllIsActiveUser();

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
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 123505;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);


                return Ok(GlobalError);
            }
        }

        [HttpPost("GetUserById")]
        public async Task<IActionResult> GetUserById(GetUserByIdRequest userById)
        {
            try
            {
                GlobalResponse.ReponseData = await _userDomain.GetUserById(userById);

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
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 531205;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        } 
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> Post([FromBody] AddUserRequestModel request)
        {
            try
            {
                //string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                //var validations = await _userDomain.AddValidation(request, "User");
                //if (validations.FirstOrDefault() == "0")
                //{
                //    var validationsRoles = await _userDomain.AddValidation(request, "Role");
                //    if (validationsRoles.Skip(0).FirstOrDefault() == "0")
                //    {
                        var response = await _userDomain.Add(request);
                        GlobalResponse.Response_Message = response;
                        return Ok(GlobalResponse);
                //    }
                //    else
                //    {
                //        GlobalResponse.Response_Code = 204;
                //        GlobalResponse.Response_Message = "Role not found";
                //        return Ok(GlobalResponse);
                //    }
                //}
                //else
                //{
                //    GlobalResponse.Response_Code = 204;
                //    GlobalResponse.Response_Message = " Failed to add Record ";
                //    return Ok(GlobalResponse);
                //}
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 512305;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        }
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> Put(UpdateUserRequestModel request)
        {
            try
            {
                //var validations = await _userDomain.UpdateValidation(request, "User");

                //if (validations.FirstOrDefault() == "0")
                //{
                //    var validationsEmail = await _userDomain.UpdateValidation(request, "email");

                //    if (validationsEmail.FirstOrDefault() == "0")
                //    {
                //        //Check Role Name...
                //        var validationsRoles = await _userDomain.UpdateValidation(request, "Role");
                //        if (validationsRoles.Skip(0).FirstOrDefault() == "0")
                //        {
                            var response = await _userDomain.Update(request);
                            GlobalResponse.Response_Code = 200;
                            GlobalResponse.Response_Message = response;
                            return Ok(GlobalResponse);
                //        }
                //        else
                //        {
                //            GlobalResponse.Response_Code = 204;
                //            GlobalResponse.Response_Message = "Role not found";
                //            return Ok(GlobalResponse);
                //        }
                //    }
                //    else
                //    {
                //        GlobalResponse.Response_Code = 204;
                //        GlobalResponse.Response_Message = "Record already exists...";
                //        return Ok(GlobalResponse);
                //    }
                //}
                //else
                //{
                //    GlobalResponse.Response_Code = 204;
                //    GlobalResponse.Response_Message = "Record not found...";
                //    return Ok(GlobalResponse);
                //}
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 50512341;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }

        }

        [HttpPost]
        [Route("DeleteUser")]
        public async Task<IActionResult> Delete(GetUserByIdRequest request)
        {
            try
            {

                //var validations = await _userDomain.DeleteValidation(request);
                //if (validations.FirstOrDefault() == "1")
                //{
                var message = await _userDomain.Delete(request);
                GlobalResponse.Response_Message = message;

                if (message == "User deleted successfully.")
                {
                    return Ok(GlobalResponse);
                }
                else
                {
                    return StatusCode(204, GlobalResponse); // Internal Server Error
                }
                //}
                //else
                //{
                //    GlobalResponse.Response_Code = 204;
                //    GlobalResponse.Response_Message = "User does not  Exists...";
                //    return Ok(GlobalResponse);
                //}
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
        [Route("UserIsActive")]
        public async Task<IActionResult> BranchMapIsActive(User_IsAction branchMapList)
        {
            try
            {
                await _userDomain.UpdateUserIsActive(branchMapList);
                GlobalResponse.Response_Message = "Record IsActivated successfully";
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

    }
}
