using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Services.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Management.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PickListController : ControllerBase
    {


        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        readonly IPickListDomain _pickListDomain;
        private IExceptionHandling _exceptionHandling;
        private readonly IValidateTokenExtension _validateTokenExtension;
        public PickListController(IPickListDomain pickListDomain, IExceptionHandling exceptionHandling, IValidateTokenExtension validateTokenExtension)
        {
            _pickListDomain = pickListDomain;
            _exceptionHandling = exceptionHandling;
            _validateTokenExtension = validateTokenExtension;
        }
        [HttpGet]
        [Route("GetAllPickList")]
        public async Task<IActionResult> Get()
        {
            try
            {

                //GlobalResponse.ReponseData = await _pickListDomain.GetAll();
                var IResult = await _pickListDomain.GetAll();

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

        [HttpPost("GetPickListById")]
        public async Task<IActionResult> GetPickListById(GetPickListByIdRequestModel id)
        {
            //  var GetResponse = _validateTokenExtension.ValidateToken(token);

            try
            {
                // if (GetResponse.ValiedTokenRespose.Trim() == "Valid token")
                //{
                {
                    GlobalResponse.ReponseData = await _pickListDomain.GetPickListById(id);

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
        [Route("AddPickList")]
        public async Task<IActionResult> Post([FromBody] AddPickListRequestModel request)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var validations = await _pickListDomain.AddValidation(request, "PickList");
                if (validations.FirstOrDefault() == "0")
                {
                    //Check Role Name...
                    var validationsRoles = await _pickListDomain.AddValidation(request, "Template");
                    if (validationsRoles.Skip(0).FirstOrDefault() == "0")
                    {
                        var response = await _pickListDomain.Add(request);
                        GlobalResponse.Response_Message = "PickList Record Save Successfully";
                        return Ok(GlobalResponse);
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 204;
                        GlobalResponse.Response_Message = "TemplateId not found";
                        return Ok(GlobalResponse);
                    }
                }
                else
                {
                    GlobalResponse.Response_Code = 204;
                    GlobalResponse.Response_Message = " Record already exists ";
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

        [HttpPut]
        [Route("UpdatePickList")]
        public async Task<IActionResult> Put(UpdatePickListRequest request)
        {
            try
            {
                var validations = await _pickListDomain.UpdateValidation(request, "PickList");

                if (validations.FirstOrDefault() == "0")
                {
                    var validationsLogPath = await _pickListDomain.UpdateValidation(request, "LogoPath");

                    if (validationsLogPath.FirstOrDefault() == "0")
                    {
                        //Check Templete Name...
                        var validationsTemplete = await _pickListDomain.UpdateValidation(request, "Template");

                        if (validationsTemplete.Skip(0).FirstOrDefault() == "0")
                        {
                            var response = await _pickListDomain.Update(request);
                            GlobalResponse.Response_Code = 200;
                            GlobalResponse.Response_Message = "Pickup Record Updated Successfully";
                            return Ok(GlobalResponse);
                        }
                        else
                        {
                            GlobalResponse.Response_Code = 204;
                            GlobalResponse.Response_Message = "Templete not found";
                            return Ok(GlobalResponse);
                        }
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 204;
                        GlobalResponse.Response_Message = "Record already exists...";
                        return Ok(GlobalResponse);
                    }
                }
                else
                {
                    GlobalResponse.Response_Code = 204;
                    GlobalResponse.Response_Message = "Pickup Record not found...";
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
        [Route("DeletePickList")]
        public async Task<IActionResult> Delete(GetPickListByIdRequestModel id)
        {
            try
            {
                var validations = await _pickListDomain.DeleteValidation(id);
                if (validations.Count() == 0)
                {
                    await _pickListDomain.Delete(id);

                    GlobalResponse.Response_Message = "Record deleted successfully";
                    return Ok(GlobalResponse);
                }
                GlobalResponse.Response_Code = 204;
                GlobalResponse.Response_Message = "Record not found for deletion";
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
