using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Services.Masters;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Management.API.Controllers.Transactions
{
    [Route("api/[controller]")]
    [ApiController]
    public class DumpUpladoController : ControllerBase
    {
        // GET: api/<DumpUpladoControl
        private readonly IDumpUploadDomain _uploadDomain;
        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        private IExceptionHandling _exceptionHandling;

        public DumpUpladoController(IDumpUploadDomain uploadDomain, IExceptionHandling exceptionHandling, IValidateTokenExtension validateTokenExtension)
        {
            _uploadDomain = uploadDomain;
            _exceptionHandling = exceptionHandling;
        }

        [HttpGet]
        public async Task<IActionResult> GetListDump()
        {
            return Ok();
            ;
        }

        // GET api/<DumpUpladoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        // POST api/<DumpUpladoController>
        [HttpPost]
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
                    GlobalResponse.Response_Message = "Record Processed Successfully.... : " + GetData.Status;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalResponse.Response_Code = 404;
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
        // PUT api/<DumpUpladoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DumpUpladoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
