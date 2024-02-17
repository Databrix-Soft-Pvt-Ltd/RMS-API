﻿using Management.Model.RequestModel;
using Management.Model.RMSEntity;
using Management.Services.ClientMasterDomain;
using Management.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwoWayCommunication.Domain.Authentication;
using static Management.Services.User.UserDomain;

namespace Management.API.Controllers.Client
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ClientMasterController : ControllerBase
    {

        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        readonly IClientMasterDomain _clientMasterDomain;
        private IExceptionHandling _exceptionHandling;

        public ClientMasterController(IClientMasterDomain clientMasterDomain, IExceptionHandling exceptionHandling)
        {
            _clientMasterDomain = clientMasterDomain;
            _exceptionHandling = exceptionHandling;
        }

        [HttpGet]
        [Route("GetAllClient")]
        public async Task<IActionResult> Get()
        {
            try
            {
                GlobalResponse.ReponseData = await _clientMasterDomain.GetAll();

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



        [HttpPost("GetClientById")]
        public async Task<IActionResult> GetUserById(GetClientByIdRequestModel clientId)
        {
            try
            {
                GlobalResponse.ReponseData = await _clientMasterDomain.GetClientById(clientId);

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
        [Route("AddClient")]
        public async Task<IActionResult> Post( AddClientRequest request)
        {
            try
            {
                var validations = await _clientMasterDomain.AddValidation(request);

                if (validations.Count == 0)
                {
                    await _clientMasterDomain.Add(request);

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
        [Route("UpdateClient")]

        public async Task<IActionResult> Put( UpdateClientRequestModel request)
        {
            try
            {
                var validations = await _clientMasterDomain.UpdateValidation(request);
                if (validations.Count() == 0)
                {
                    await _clientMasterDomain.Update(request);

                    GlobalResponse.Response_Message = "Record Updated successfully";
                    return Ok(GlobalResponse);
                }
                return BadRequest(validations.FirstOrDefault());

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
        public async Task<IActionResult> Delete(GetClientByIdRequestModel id)
        {
            try
            {

                var validations = await _clientMasterDomain.DeleteValidation(id);
                if (validations.Count() == 0)
                {
                    await _clientMasterDomain.Delete(id);
                    return Ok(true);
                }
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = "Failed to delete the record";
                return BadRequest(validations);
            } 
            catch(Exception ex)
            {
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Trace_Point = ex.StackTrace;
                GlobalError.Error_Message = ex.Message;
                return Ok(GlobalError);
            }
        }   
    }
}