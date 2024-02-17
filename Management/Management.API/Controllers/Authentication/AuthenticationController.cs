using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TwoWayCommunication.Domain.Authentication;
using TwoWayCommunication.Model.RequestModel;
using TwoWayCommunication.Model.ResponseModel;
//https://documenter.getpostman.com/view/3446823/2s93RTQXw7
namespace TwoWayCommunication.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly IAuthenticationDomain _authenticationDomain;
        public AuthenticationController(IAuthenticationDomain authenticationDomain)
        {
            _authenticationDomain = authenticationDomain;
        } 
        [HttpPost()]
        [ProducesResponseType(typeof(AuthenticationResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [SwaggerOperation("Authentication API", "This API will use for authenticate user base email id")]
        public async Task<IActionResult> Post([FromBody] AuthenticationRequestModel authenticationRequestModel)
        {
            var validationCount = await _authenticationDomain.LoginValidation(authenticationRequestModel);

            if (validationCount.Count() == 0)
            {
                var result = await _authenticationDomain.Login(authenticationRequestModel);
                return Ok(result);
            }
            return BadRequest(validationCount.FirstOrDefault());
        }
    }
}
