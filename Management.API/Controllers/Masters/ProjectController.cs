using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Services.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        readonly IProjectMasterDomain _projectMasterDomain;
        private IExceptionHandling _exceptionHandling;
        private readonly IValidateTokenExtension _validateTokenExtension;
        public ProjectController(IProjectMasterDomain projectMasterDomain, IExceptionHandling exceptionHandling, IValidateTokenExtension validateTokenExtension)
        {
            _projectMasterDomain = projectMasterDomain;
            _exceptionHandling = exceptionHandling;
            _validateTokenExtension = validateTokenExtension;
        }
        [HttpGet]
        [Route("GetAllProject")]
        public async Task<IActionResult> Get()
        {
            try
            {

                //GlobalResponse.ReponseData = await _projectMasterDomain.GetAll();
                var IResult = await _projectMasterDomain.GetAll();


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
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);


                return Ok(GlobalError);
            }
        }


        [HttpPost("GetProjectById")]
        public async Task<IActionResult> GetUserById(GetProjectByIdRequestModel id)
        {
            // var GetResponse = _validateTokenExtension.ValidateToken(token);

            try
            {
                // if (GetResponse.ValiedTokenRespose.Trim() == "Valid token")
                //{
                {
                    GlobalResponse.ReponseData = await _projectMasterDomain.GetProjectById(id);

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
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        }


        [HttpPost]
        [Route("AddProject")]
        public async Task<IActionResult> Post(AddProjectReuestModel request)
        {
            // string GetToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJTaHdldGEiLCJlbWFpbCI6InNod2V0YUAxMjMiLCJVc2VySWQiOiI0IiwiZXhwIjoxNzA2NTI0ODU2LCJpc3MiOiJUZXN0LmNvbSIsImF1ZCI6IlRlc3QuY29tIn0.rg3kXT9QT-hTQqGsX6OrLzir_317blODSOREuOzJyAs";
            //var GetResponse = _validateTokenExtension.ValidateToken(GetToken);

            try
            {
                //  if (GetResponse.ValiedTokenRespose.Trim() == "Valid token")
                // {
                var validations = await _projectMasterDomain.AddValidation(request);

                if (validations.Count == 0)
                {
                    await _projectMasterDomain.Add(request);
                    GlobalResponse.Response_Message = "Record Added successfully";
                    GlobalResponse.Response_Code = 200;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 409;
                    GlobalResponse.Response_Message = "Record already exists";
                    return Ok(GlobalResponse);
                }
            }
            //    }
            //    else
            //    {
            //        GlobalResponse.Response_Code = 204;
            //        GlobalResponse.Response_Message = "Token is not Valid....";
            //        return Ok(GlobalResponse);
            //    }
            //}
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Trace_Point = ex.StackTrace;
                GlobalError.Error_Message = ex.Message;
                return StatusCode(500, GlobalError);
            }
        }

        [HttpPut()]
        [Route("UpdateProject")]

        public async Task<IActionResult> Put(UpdateProjectRequestModel request)
        {
            try
            {
                var validations = await _projectMasterDomain.UpdateValidation(request);
                if (validations.Count() == 0)
                {
                    await _projectMasterDomain.Update(request);

                    GlobalResponse.Response_Message = "Record Updated successfully";
                    return Ok(GlobalResponse);
                }
                GlobalResponse.Response_Code = 204;
                GlobalResponse.Response_Message = "Record not found";
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
        [Route("DeleteProject")]
        public async Task<IActionResult> Delete(GetProjectByIdRequestModel id)
        {
            try
            {
                var validations = await _projectMasterDomain.DeleteValidation(id);
                if (validations.Count() == 0)
                {
                    await _projectMasterDomain.Delete(id);

                    GlobalResponse.Response_Message = "Record deleted successfully";

                    return Ok(GlobalResponse);
                }
                GlobalError.ErrorCode = 204;
                GlobalError.Error_Message = "Record not found";
                return Ok(GlobalError);
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
