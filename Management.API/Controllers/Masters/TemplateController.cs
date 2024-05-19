using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Services.Masters;
using Management.Services.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TemplateController : ControllerBase
    {

        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        readonly ITemplateMasterDomain _templateMasterDomain;
        private IExceptionHandling _exceptionHandling;

        public TemplateController(ITemplateMasterDomain templateMasterDomain, IExceptionHandling exceptionHandling)
        {
            _templateMasterDomain = templateMasterDomain;
            _exceptionHandling = exceptionHandling;
        }

        [HttpGet]
        [Route("GetAllTemplate")]
        public async Task<IActionResult> Get()
        {
            try
            {
                //GlobalResponse.ReponseData = await _templateMasterDomain.GetAll();

                var IResult = await _templateMasterDomain.GetAll();

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


        [HttpPost("GetTemplateById")]
        public async Task<IActionResult> GetTemplateById(GEtTemplateByIdRequest id)
        {
            try
            {
                GlobalResponse.ReponseData = await _templateMasterDomain.GetTemplateById(id);

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
        [Route("AddTemplate")]
        public async Task<IActionResult> Post(GetTemplateName request)
        {
            try
            {
                var validations = await _templateMasterDomain.AddValidation(request);

                if (validations.Count == 0)
                {
                    await _templateMasterDomain.Add(request);
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Template Added successfully"; 
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 409;
                    GlobalResponse.Response_Message = "Template Already Exists";
                    return Conflict(GlobalError);
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
        [Route("UpdateTemplate")]

        public async Task<IActionResult> Put(UpdateTemplateRequest request)
        {
            try
            {
                var validations = await _templateMasterDomain.UpdateValidation(request);
                if (validations.FirstOrDefault() == "0")
                {
                    await _templateMasterDomain.Update(request);
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record Updated Successfully";
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 204;
                    GlobalResponse.Response_Message = "Template doesnot exits Exists";
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
        }


        [HttpPost]
        [Route("DeleteTemplate")]
        public async Task<IActionResult> Delete(GEtTemplateByIdRequest id)
        {
            try
            {

                //var validations = await _templateMasterDomain.DeleteValidation(id);
                if (true)
                {
                   var GetResult =  await _templateMasterDomain.Delete(id);

                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = GetResult.FirstOrDefault().ToString();
                    return Ok(GlobalResponse); 
                }
                else
                {
                    GlobalResponse.Response_Code = 204;
                    GlobalResponse.Response_Message = "Record not found for deletetion";
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
        }




    }
}
