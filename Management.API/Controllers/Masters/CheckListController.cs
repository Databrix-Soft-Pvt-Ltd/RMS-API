using Management.API.Miscellaneous;
using Management.Model.RequestModel;
using Management.Model.RMSEntity;
using Management.Services.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TwoWayCommunication.Model.Enums;

namespace Management.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CheckListController : ControllerBase
    {
        private GlobalResponse GlobalResponse = new GlobalResponse();
        private GlobalError GlobalError = new GlobalError();
        readonly ICheckListMasterDomain _CheckListMasterDomain;
        private IExceptionHandling _exceptionHandling;
        public CheckListController(ICheckListMasterDomain checkListMasterDomain, IExceptionHandling exceptionHandling)
        {
            _CheckListMasterDomain = checkListMasterDomain;
            _exceptionHandling = exceptionHandling;

        }

        [HttpGet]
        [Route("GetAllCheckList1")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var IResult = await _CheckListMasterDomain.GetAll();
                //GlobalResponse.ReponseData = await _branchMasterDomain.GetAll();


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

        [HttpPost]
        [Route("AddCheckList1")]
        public async Task<IActionResult> Post(AddCheckList1 request)
        {

            try
            {

                var validations = await _CheckListMasterDomain.AddValidation(request);

                if (validations.Count == 0)
                {
                    await _CheckListMasterDomain.Add(request);
                    GlobalResponse.Response_Message = "CheckList Added successfully";
                    GlobalResponse.Response_Code = 200;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalError.ErrorCode = 505;
                    GlobalError.Error_Message = "CheckList already exists";
                    return Ok(GlobalError);
                }

            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 501232135;
                GlobalError.Error_Trace_Point = ex.StackTrace;
                GlobalError.Error_Message = ex.Message;
                return StatusCode(500, GlobalError);
            }
        }

        [HttpPut()]
        [Route("UpdateCheckList1")]
        public async Task<IActionResult> Put(AUpdateCheckList1 request)
        {
            try
            {
                var validations = await _CheckListMasterDomain.UpdateValidation(request);
                if (validations.Count() == 0)
                {
                    await _CheckListMasterDomain.Update(request);

                    GlobalResponse.Response_Message = "Record Updated successfully";
                    return Ok(GlobalResponse);
                }
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = "Check List does not exists";
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
        [Route("DeleteCheckList")]
        public async Task<IActionResult> Delete(GetChkListByIdRequestModel id)
        {
            try
            {
                var validations = await _CheckListMasterDomain.DeleteValidation(id);
                if (validations.Count() == 0)
                {
                    await _CheckListMasterDomain.Delete(id);

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

        /// <summary>
        /// Check List 2
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet]
        [Route("GetAllCheckList2")]
        public async Task<IActionResult> GetAllCheckList2()
        {
            try
            {
                var IResult = await _CheckListMasterDomain.GetAllCheckList2();

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

        [HttpPost]
        [Route("GetAllCheckList2ByID")]
        public async Task<IActionResult> GetAllCheckList2ByID(GetChkListByIdRequestModel id)
        {
            try
            {
                var IResult = await _CheckListMasterDomain.GetAllCheckList2ByID(id.id);

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

        [HttpPost]
        [Route("AddCheckList2")]
        public async Task<IActionResult> AddCheckList2(AddCheckList2 request)
        {
            try
            {
                var validations = await _CheckListMasterDomain.AddValidationChkList2(request);

                if (validations.Count == 0)
                {
                    await _CheckListMasterDomain.AddChkList2(request);
                    GlobalResponse.Response_Message = "CheckList 2 Added successfully";
                    GlobalResponse.Response_Code = 200;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalError.ErrorCode = 505;
                    GlobalError.Error_Message = "CheckList 2 already exists";
                    return Ok(GlobalError);
                }

            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 501232135;
                GlobalError.Error_Trace_Point = ex.StackTrace;
                GlobalError.Error_Message = ex.Message;
                return StatusCode(500, GlobalError);
            }
        }

        [HttpPut()]
        [Route("UpdateCheckList2")]
        public async Task<IActionResult> UpdateCheckList2(UpdateCheckList2 request)
        {
            try
            {
                var validations = await _CheckListMasterDomain.UpdateChkList2Validation(request);
                if (validations.Count() == 0)
                {
                    await _CheckListMasterDomain.UpdateChkList2(request);

                    GlobalResponse.Response_Message = "Record Updated successfully";
                    return Ok(GlobalResponse);
                }
                GlobalError.ErrorCode = 505;
                GlobalError.Error_Message = "Check List does not exists";
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
        [Route("DeleteCheckList2")]
        public async Task<IActionResult> DeleteCheckList2(GetChkListByIdRequestModel id)
        {
            try
            {
                var validations = await _CheckListMasterDomain.DeleteValidationChkList2(id);
                if (validations.Count() == 0)
                {
                    await _CheckListMasterDomain.DeleteChkList2(id);

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


        /// <summary>
        /// Check List 3
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet]
        [Route("GetAllCheckList3")]
        public async Task<IActionResult> GetAllCheckList3()
        {
            try
            {
                var IResult = await _CheckListMasterDomain.GetAllCheckList3();

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

        [HttpPost]
        [Route("GetAllCheckList3ByID")]
        public async Task<IActionResult> GetAllCheckList3ByID(GetChkListByIdRequestModel id)
        {
            try
            {
                var IResult = await _CheckListMasterDomain.GetAllCheckList3ByID(id.id);

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

        [HttpPost]
        [Route("AddCheckList3")]
        public async Task<IActionResult> AddCheckList3(AddCheckList3 request)
        {
            try
            {
                var validations = await _CheckListMasterDomain.AddValidationChkList3(request);

                if (validations.Count == 0)
                {
                    await _CheckListMasterDomain.AddChkList3(request);
                    GlobalResponse.Response_Message = "CheckList 3 Added successfully";
                    GlobalResponse.Response_Code = 200;
                    return Ok(GlobalResponse);
                }
                else
                {
                    GlobalError.ErrorCode = 505;
                    GlobalError.Error_Message = "CheckList 2 already exists";
                    return Ok(GlobalError);
                }

            }
            catch (Exception ex)
            {
                GlobalError.ErrorCode = 501232135;
                GlobalError.Error_Trace_Point = ex.StackTrace;
                GlobalError.Error_Message = ex.Message;
                return StatusCode(500, GlobalError);
            }
        }

        [HttpPost]
        [Route("DeleteCheckList3")]
        public async Task<IActionResult> DeleteCheckList3(GetChkListByIdRequestModel id)
        {
            try
            {
                var validations = await _CheckListMasterDomain.DeleteValidationChkList3(id);
                if (validations.Count() == 0)
                {
                    await _CheckListMasterDomain.DeleteChkList3(id);

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
