using Management.Model.RequestModel;
using Management.Model.RMSEntity;
using Management.Services.CourierMasterDomain;
using Management.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwoWayCommunication.Domain.Authentication;
using static Management.Services.CourierMasterDomain.CourierMasterDomain;

namespace Management.API.Controllers.Courier
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class CourierMasterController : ControllerBase
    {

        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        readonly ICourierMasterDomain _courierMasterDomain;
        private IExceptionHandling _exceptionHandling;

        public CourierMasterController(ICourierMasterDomain courierMasterDomain, IExceptionHandling exceptionHandling)
        {
            _courierMasterDomain = courierMasterDomain;
            _exceptionHandling = exceptionHandling;
        }


        [HttpGet]
        [Route("GetAllCourier")]
        public async Task<IActionResult> Get()
        {
            try
            {
                GlobalResponse.ReponseData = await _courierMasterDomain.GetAll();

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



        [HttpPost("GetCourierById")]
        public async Task<IActionResult> GetUserById(GetCourierByIdRequestModel userById)
        {
            try
            {
                GlobalResponse.ReponseData = await _courierMasterDomain.GetUserById(userById);

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
        [Route("AddCourier")]
        public async Task<IActionResult> Post(AddCourierRequestModel request)
        {
            try
            {
                var validations = await _courierMasterDomain.AddValidation(request);

                if (validations.Count == 0)
                {
                    await _courierMasterDomain.Add(request);

                    GlobalResponse.Response_Message = "Record Added successfully";
                    GlobalResponse.Response_Code = 200;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalError.ErrorCode = 505;
                    GlobalError.Error_Message = "Failed to add record";
                    return BadRequest(validations.FirstOrDefault());
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
        [Route("UpdateCourier")]

        public async Task<IActionResult> Put(UpdateCourierRequestModel request)
        {
            try
            {
                var validations = await _courierMasterDomain.UpdateValidation(request);
                if (validations.Count() == 0)
                {
                    await _courierMasterDomain.Update(request);

                    GlobalResponse.Response_Message = "Record Updated successfully";
                    return Ok(GlobalResponse);
                }
                else
                {

                    GlobalResponse.Response_Message = "Courier does not exists";
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


        [HttpDelete]
        [Route("DeleteClient")]
        public async Task<IActionResult> Delete(GetCourierByIdRequestModel id)
        {
            try
            {

                var validations = await _courierMasterDomain.DeleteValidation(id);
                if (validations.Count() == 0)
                {
                    await _courierMasterDomain.Delete(id);
                    GlobalResponse.Response_Message = "Record deleted successfully";

                    return Ok(GlobalResponse);
                }
                GlobalResponse.Response_Code = 404;
               GlobalResponse.Response_Message = "Record does not found";
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
