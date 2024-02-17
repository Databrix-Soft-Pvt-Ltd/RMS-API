using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Services.Masters;
using Management.Services.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers.Masters
{
    [Route("api/[controller]")]
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
                GlobalResponse.ReponseData = await _templateMasterDomain.GetAll();

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
        [Route("AddTemplate")]
        public async Task<IActionResult> Post(AddTemplateRequestModel request)
        {
            try
            {
                var validations = await _templateMasterDomain.AddValidation(request);

                if (validations.Count == 0)
                {
                    await _templateMasterDomain.Add(request);

                    GlobalResponse.Response_Message = "Template Added successfully";
                    GlobalResponse.Response_Code = 200;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalError.ErrorCode = 409;
                    GlobalError.Error_Message = "Template Already Exists";
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
                if (validations.Count() == 0)
                {
                    await _templateMasterDomain.Update(request);

                    GlobalResponse.Response_Message = "Template Updated successfully";
                    return Ok(GlobalResponse);
                }
                GlobalError.ErrorCode = 404;
                GlobalError.Error_Message = "Template Not Found";
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


        [HttpDelete]
        [Route("DeleteTemplate")]
        public async Task<IActionResult> Delete(GEtTemplateByIdRequest id)
        {
            try
            {

                var validations = await _templateMasterDomain.DeleteValidation(id);
                if (validations.Count() == 0)
                {
                    await _templateMasterDomain.Delete(id);
                    return Ok(true);
                }
                GlobalError.ErrorCode = 404;
                GlobalError.Error_Message = "Template not found for deletion";
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
