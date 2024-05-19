using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Services.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;

namespace Management.API.Controllers.Transactions
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DumpUploadController : ControllerBase
    {
        // GET: api/<DumpUpladoControl
        private readonly IDumpUploadDomain _uploadDomain;
        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        private IExceptionHandling _exceptionHandling;

        public DumpUploadController(IDumpUploadDomain uploadDomain, IExceptionHandling exceptionHandling, IValidateTokenExtension validateTokenExtension)
        {
            _uploadDomain = uploadDomain;
            _exceptionHandling = exceptionHandling;
        }

        [HttpGet]
        [Route("GetAllDumpUpload")]
        public async Task<IActionResult> Get()
        {
            try
            {
                //GlobalResponse.ReponseData = await _uploadDomain.GetAll();
                var IResult = await _uploadDomain.GetAll();


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
                GlobalError.ErrorCode = 12313;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);


                return Ok(GlobalError);
            }
        }

        [HttpGet]
        [Route("GetAllTemplate")]
        public async Task<IActionResult> GetTemplate()
        {
            try
            {
                //GlobalResponse.ReponseData = await _uploadDomain.GetAll();
                var IResult = await _uploadDomain.GetCloneTemplates();


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
                GlobalError.ErrorCode = 12313;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);


                return Ok(GlobalError);
            }
        }

        [HttpPost("GetDumpUploadById")]
        public async Task<IActionResult> GetDumpUploadById(GETDumpUploadByIdRequest id)
        {

            try
            {
                {
                    GlobalResponse.ReponseData = await _uploadDomain.GetDumpUploadById(id);

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
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 5051231;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        }

        [HttpPost("DumpUpload")]
        public async Task<IActionResult> AddDumpUpload(IFormFile formFile)
        {

            try
            {
               // await _uploadDomain.Add(formFile); 

                var IResult = await _uploadDomain.FileValidation(formFile);

                if (IResult.FirstOrDefault() == "Valid")
                {
                    var GetData = await _uploadDomain.Add(formFile);
                    GlobalResponse.Response_Code = 200;
                    GlobalResponse.Response_Message = "Record Processed Successfully.... : ";
                    GlobalResponse.ReponseData = GetData.Status;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 204;
                    GlobalResponse.Response_Message = IResult.FirstOrDefault();
                    return Ok(GlobalResponse);
                }
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex); 

            } 
            return Ok(GlobalError); 
        } 
        [HttpPut()]
        [Route("UpdateDumpUpload")] 
        public async Task<IActionResult> Put(UpdateDumpUploadRequest request)
        {
            try
            {
                var validations = await _uploadDomain.UpdateValidation(request);
                if (validations.FirstOrDefault() == "0")
                {
                    await _uploadDomain.Update(request);

                    GlobalResponse.Response_Message = "Record Updated successfully";
                    return Ok(GlobalResponse);
                }
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = "Dumpupload does not exists";
                return Ok(GlobalError);
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 505123123;
                GlobalError.Error_Trace_Point = ex.StackTrace;
                GlobalError.Error_Message = ex.Message;
                return Ok(GlobalError);
            }
        }
     
        [HttpPost]
        [Route("DeleteDumpUpload")]
        public async Task<IActionResult> Delete(GETDumpUploadByIdRequest id)
        {
            try
            {
                var validations = await _uploadDomain.DeleteValidation(id);
                if (validations.Count() == 0)
                {
                    await _uploadDomain.Delete(id);

                    GlobalResponse.Response_Message = "Record deleted successfully";

                    return Ok(GlobalResponse);
                }
                GlobalError.ErrorCode = 204;
                GlobalError.Error_Message = "Record not found for deletion";
                return Ok(GlobalError);
            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 505123123121;
                GlobalError.Error_Trace_Point = ex.StackTrace;
                GlobalError.Error_Message = ex.Message;
                return Ok(GlobalError);
            }
        }
    }

}
