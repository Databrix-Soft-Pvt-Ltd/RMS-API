using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Services.Masters;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        readonly IMenuMasterDomain _menuMasterDomain;
        private IExceptionHandling _exceptionHandling;
        private readonly IValidateTokenExtension _validateTokenExtension;
        public MenuController(IMenuMasterDomain menuMasterDomain, IExceptionHandling exceptionHandling, IValidateTokenExtension validateTokenExtension)
        {
            _menuMasterDomain = menuMasterDomain;
            _exceptionHandling = exceptionHandling;
            _validateTokenExtension = validateTokenExtension;
        }
        [HttpGet]
        [Route("GetAllMenu")]
        public async Task<IActionResult> Get()
        {
            try
            {

                GlobalResponse.ReponseData = await _menuMasterDomain.GetAll();

                if (GlobalResponse.ReponseData != null)
                {
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record fetched Successfully";
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



        [HttpPost("GetMenuById")]
        public async Task<IActionResult> GetMenuById(GetMenuByIdRequestModel id)
        {
           // var GetResponse = _validateTokenExtension.ValidateToken(token);

            try
            {
                //if (GetResponse.ValiedTokenRespose.Trim() == "Valid token")
                //{
                    {
                        GlobalResponse.ReponseData = await _menuMasterDomain.GetMenuById(id);

                        if (GlobalResponse.ReponseData != null)
                        {
                            GlobalResponse.Response_Code = 200;
                            GlobalResponse.Response_Message = "Record fetched Successfully";
                            return Ok(GlobalResponse);
                        }
                        else
                        {
                            GlobalResponse.Response_Code = 404;
                            GlobalResponse.Response_Message = "Record not found";
                            return Ok(GlobalResponse);
                        }
                    }
                //}
                //else
                //{
                //    GlobalResponse.Response_Code = 404;
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
        [Route("AddMenu")]
        public async Task<IActionResult> Post(AddMenuRequestModel request)
        {
            //string GetToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJTaHdldGEiLCJlbWFpbCI6InNod2V0YUAxMjMiLCJVc2VySWQiOiI0IiwiZXhwIjoxNzA2NjAzMDc2LCJpc3MiOiJUZXN0LmNvbSIsImF1ZCI6IlRlc3QuY29tIn0.dPxCvkNPWvZeeLGGpiCITNgnBS8PTFMh10lwsNAbS2";
            //var GetResponse = _validateTokenExtension.ValidateToken(GetToken);

            try
            {
                //  if (GetResponse.ValiedTokenRespose.Trim() == "Valid token")
                //{
                var validations = await _menuMasterDomain.AddValidation(request);

                if (validations.Count == 0)
                {
                    await _menuMasterDomain.Add(request);
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
                //}
                //else
                //{
                //    GlobalResponse.Response_Code = 404;
                //    GlobalResponse.Response_Message = "Token is not Valid....";
                //    return Ok(GlobalResponse);
                //}
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
        [Route("UpdateMenu")]

        public async Task<IActionResult> Put(UpdateMenuRequestBody request)
        {
            try
            {
                var validations = await _menuMasterDomain.UpdateValidation(request);
                if (validations.FirstOrDefault() == "0")
                {
                    await _menuMasterDomain.Update(request);

                    GlobalResponse.Response_Message = "Record Updated successfully";
                    return Ok(GlobalResponse);
                }
                GlobalResponse.Response_Code = 404;
                GlobalResponse.Response_Message = "Record Already Exists";
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


        [HttpDelete]
        [Route("DeleteMenu")]
        public async Task<IActionResult> Delete(GetMenuByIdRequestModel id)
        {
            try
            {
                var validations = await _menuMasterDomain.DeleteValidation(id);
                if (validations.Count() == 0)
                {
                    await _menuMasterDomain.Delete(id);

                    GlobalResponse.Response_Message = "Record deleted successfully";

                    return Ok(GlobalResponse);
                }
                GlobalResponse.Response_Code = 404;
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
