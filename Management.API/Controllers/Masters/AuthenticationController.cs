using Management.API.Miscellaneous;
using Management.Services.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TwoWayCommunication.Model.RequestModel;
using TwoWayCommunication.Model.ResponseModel;
namespace Management.API.Controllers.Masters
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly IAuthenticationDomain _authenticationDomain;
        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        private IExceptionHandling _exceptionHandling;
        public AuthenticationController(IAuthenticationDomain authenticationDomain, IExceptionHandling exceptionHandling)
        {
            _authenticationDomain = authenticationDomain;
            _exceptionHandling = exceptionHandling;

        }
        [HttpPost()]
        [ProducesResponseType(typeof(AuthenticationResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("Authentication API", "This API will use for authenticate user base email id")]

        [HttpGet]
        [Route("Auth")]
        public async Task<IActionResult> Post([FromBody] AuthenticationRequestModel authenticationRequestModel)
        {
            try
            { 
                var validationCount = await _authenticationDomain.LoginValidation(authenticationRequestModel);

                if (validationCount.Count() == 0)
                {
                    var result = await _authenticationDomain.Login(authenticationRequestModel);
                    return Ok(result);
                }
                GlobalResponse.Response_Code = 404;
                GlobalResponse.Response_Message = validationCount.FirstOrDefault();
                return Ok(GlobalResponse);
            }

            catch (Exception ex)
            {
                GlobalError.ErrorCode = 50511;
                GlobalError.Error_Message = ex.Message;
                _exceptionHandling.LogError(ex);
                return Ok(GlobalError);
            }
        }
    }
}
