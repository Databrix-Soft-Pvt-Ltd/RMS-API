using Management.API.Miscellaneous;
using Management.Model.ResponseModel;
using Management.Services.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientMappingController : ControllerBase
    {
        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        readonly IClientMappingDomain _clientMappingDomain;
        private IExceptionHandling _exceptionHandling;
        public ClientMappingController(IClientMappingDomain clientMappingDomain, IExceptionHandling exceptionHandling)
        {
            _clientMappingDomain = clientMappingDomain;
            _exceptionHandling = exceptionHandling;
        }
        [HttpGet]
        [Route("GetAllClientMap")]
        public async Task<IActionResult> Get()
        {
            try
            {
                GlobalResponse.ReponseData = await _clientMappingDomain.GetAllClientMap();

                if (GlobalResponse.ReponseData != "0")
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
        [HttpPost("GetClientMapById")]
        public async Task<IActionResult> GetClientById(GetClientMapByIdRequestModel roleId)
        {
            try
            {
                GlobalResponse.ReponseData = await _clientMappingDomain.GetClientMapById(roleId);

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
        [Route("AddClientMap")]
        public async Task<IActionResult> Post(AddClientMapRequestModel request)
        {
            try
            {
                var validations1 = await _clientMappingDomain.AddClientMapValidation(request, "ClientMap");

                if (validations1.FirstOrDefault() == "0")
                {
                    var validations2 = await _clientMappingDomain.AddClientMapValidation(request, "Client");

                    if (validations2.FirstOrDefault() == "0")
                    {
                        var validations3 = await _clientMappingDomain.AddClientMapValidation(request, "User");

                        if (validations3.FirstOrDefault() == "0")
                        {
                            await _clientMappingDomain.Add(request);
                            GlobalResponse.Response_Message = "Record Added successfully";
                            GlobalResponse.Response_Code = 200;
                            return Ok(GlobalResponse);
                        }
                        else
                        {
                            GlobalResponse.Response_Code = 404;
                            GlobalResponse.Response_Message = "User not found";
                            return Ok(GlobalResponse);
                        }
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 404;
                        GlobalResponse.Response_Message = "Client not found";
                        return Ok(GlobalResponse);
                    }
                }
                else
                {
                    GlobalResponse.Response_Code = 404;
                    GlobalResponse.Response_Message = "ClientMap alredy found";
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
            return Ok(GlobalResponse);
        }
        [HttpPut()]
        [Route("UpdateClientMap")]

        public async Task<IActionResult> Put(UpdateClientMappingResponse request)
        {
            try
            {
                var validations1 = await _clientMappingDomain.UpdatClientMapValidation(request, "ClientMap");

                if (validations1.FirstOrDefault() == "0")
                {
                    var validations2 = await _clientMappingDomain.UpdatClientMapValidation(request, "Client");

                    if (validations2.FirstOrDefault() == "0")
                    {
                        var validations3 = await _clientMappingDomain.UpdatClientMapValidation(request, "User");

                        if (validations3.FirstOrDefault() == "0")
                        {
                            await _clientMappingDomain.Update(request);
                            GlobalResponse.Response_Message = "Record Updated successfully";
                            return Ok(GlobalResponse);
                        }
                        else
                        {
                            GlobalResponse.Response_Code = 404;
                            GlobalResponse.Response_Message = "User not found";
                            return Ok(GlobalResponse);
                        }
                    }
                    else
                    {
                        GlobalResponse.Response_Code = 404;
                        GlobalResponse.Response_Message = "Client not found";
                        return Ok(GlobalResponse);
                    }
                }
                else if (validations1.Count == 1)
                {
                    GlobalResponse.Response_Code = 404;
                    GlobalResponse.Response_Message = "ClientMap Already Exists";
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
            return Ok(GlobalResponse);
        }
        [HttpDelete]
        [Route("DeleteClientMap")]
        public async Task<IActionResult> Delete(GetClientMapByIdRequestModel id)
        {
            try
            {
                var validations = await _clientMappingDomain.DeleteValidation(id);
                if (validations.FirstOrDefault() == "1")
                {
                    await _clientMappingDomain.Delete(id);
                    GlobalResponse.Response_Message = "Record deleted successfully";
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalError.ErrorCode = 505;
                    GlobalError.Error_Message = "Record doesnot  Exists";
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
