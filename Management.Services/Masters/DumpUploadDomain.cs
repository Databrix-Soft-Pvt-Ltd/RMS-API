using ExcelDataReader;
using Management.Model.RequestModel;
using Management.Model.ResponseModel;
using Management.Model.RMSEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TwoWayCommunication.Core.UnitOfWork;
using TwoWayCommunication.Model.Enums;

namespace Management.Services.Masters
{
    public class DumpUploadDomain : IDumpUploadDomain
    {


        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        private readonly GlobalUserID _gluID;
        public List<AddDumpUploadRequestModel> DumpLIstData { get; set; }

        private List<TemplateDetail> ListTemp { get; set; }

        public DumpUploadDomain(IUnitOfWork unitOfWork, GlobalUserID globalUserID)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();
            _gluID = globalUserID;
        }
        public async Task<List<GetAllDumpUploadResponse>> GetAll()
        {
            var query = _unitOfWork.DumpUploadMasterRepository.AsQueryable().
                Select(c => new GetAllDumpUploadResponse
                {

                }).ToList();
            return query;
        }
        public async Task<List<GetTemplatesClone>> GetCloneTemplates()
        {
            var query = _unitOfWork.TemplateDetailRepository.AsQueryable().
                Select(c => new GetTemplatesClone
                {
                    DisplayName = c.DisplayName.Replace("\r\n", "").Trim(),
                    DatabaseName = c.DatabaseName.Replace("\r\n", "").Trim()

                }).ToList();
            return query;
        }
        public async Task<GetAllDumpUploadResponse> GetDumpUploadById(GETDumpUploadByIdRequest request)
        {
            var getDuploadList = _unitOfWork.DumpUploadMasterRepository.AsQueryable()
                .Where(r => r.Id == request.DumpId)
                .Select(c => new GetAllDumpUploadResponse
                {
                    Id = c.Id,
                    Ref1 = c.Ref1,
                    Ref2 = c.Ref2,
                    Ref3 = c.Ref3,
                    Ref4 = c.Ref4,
                    Ref5 = c.Ref5,
                    Ref6 = c.Ref6,
                    Ref7 = c.Ref7,
                    Ref8 = c.Ref8,
                    Ref9 = c.Ref9,
                    Ref10 = c.Ref10,
                    Ref11 = c.Ref11,
                    Ref12 = c.Ref12,
                    Ref13 = c.Ref13,
                    Ref14 = c.Ref14,
                    Ref15 = c.Ref15,
                    Ref16 = c.Ref16,
                    Ref17 = c.Ref17,
                    Ref18 = c.Ref18,
                    Ref19 = c.Ref19,
                    Ref20 = c.Ref20,
                    Ref21 = c.Ref21,
                    Ref22 = c.Ref22,
                    Ref23 = c.Ref23,
                    Ref24 = c.Ref24,
                    Ref25 = c.Ref25,
                    Ref26 = c.Ref26,
                    Ref27 = c.Ref27,
                    Ref28 = c.Ref28,
                    Ref29 = c.Ref29,
                    Ref30 = c.Ref30,
                    Status = c.Status
                })
                .FirstOrDefault();

            return getDuploadList;
        }

        public async Task<HashSet<string>> AddValidation(AddDumpUploadRequestModel request)
        {
            bool isDumpUpload = await _unitOfWork.DumpUploadMasterRepository.Any(x => x.Id == request.Id);
            if (isDumpUpload)
                validationMessage.Add("0");
            else validationMessage.Add("1");

            return validationMessage;
        }
        public async Task<HashSet<string>> FileValidation(IFormFile CSVData)
        {
            ListTemp = _unitOfWork.TemplateDetailRepository.AsQueryable().ToList();

            var validationMessage = new HashSet<string>();

            DataTable GetCsVData = convertcsvtodatatable(CSVData);

            if (GetCsVData != null && GetCsVData.Rows.Count > 0)
            {
                string[] GetColumnName = GetCsVData.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

                if (ListTemp.Count > 0)
                {
                    foreach (var template in ListTemp)
                    {
                        bool? isMandatory = template.IsMandatory;

                        var isValid = template.Validation?.ToUpper() == "YES";  

                        if (isValid && isMandatory.GetValueOrDefault())
                        {
                            var templateColumnName = template.DisplayName;
 
                            if (!GetColumnName.Contains(templateColumnName))
                            {
                                validationMessage.Add($"{templateColumnName} Not-Valid"); 
                                break;
                            }
                        }
                    } 
                    validationMessage.Add("Valid");
                }
            }
            else
            {
                validationMessage.Add("Invalid CSV Data");
            }

            return validationMessage;
        }

        public async Task<DumpUpload> Add(IFormFile formFile)
        {
            DumpUpload response = null;

            DataTable DT = convertcsvtodatatable(formFile);
            if (DT != null && DT.Rows.Count > 0)
            {
                var jsonResult = new Dictionary<string, object>
                {
                    { "AddedRef1Values", new List<string>() },
                    { "UpdatedRef1Values", new List<string>() },
                    { "TotalUpload", new Dictionary<string, int>
                        {
                            { "TotalCount", 0 },
                            { "AddedCount", 0 },
                            { "UpdatedCount", 0 }
                        }
                    }
                };

                List<object> rowColls = new List<object>();

                foreach (DataRow dr in DT.Rows)
                {
                    Dictionary<string, string> rowColl = new Dictionary<string, string>();
                    int i = 1;

                    foreach (DataColumn dc in DT.Columns)
                    {
                        var GetRowValue = dr[dc].ToString();

                        rowColl.Add("Ref" + i, dr[dc].ToString());
                        i++;
                    }
                    rowColls.Add(rowColl);
                }
                int addedCount = 0;
                int updatedCount = 0;

                foreach (var item in rowColls)
                {
                    string isValid = "Add";
                    if (item is Dictionary<string, string> dictionary)
                    {
                        DumpUpload newDumpUpload = null;

                        foreach (var kvp in dictionary)
                        {
                            string key = kvp.Key;
                            string value = kvp.Value;

                            if (key == "Ref1")
                            {
                                var isRef1Exist = await _unitOfWork.DumpUploadMasterRepository.FirstOrDefault(x => x.Ref1 == value);

                                if (isRef1Exist != null)
                                { 
                                    isValid = "Update";
                                    newDumpUpload = isRef1Exist;
                                }
                                else
                                {
                                    isValid = "Add";
                                    newDumpUpload = new DumpUpload(); 
                                }

                                if (isValid == "Add")
                                {
                                    ((List<string>)jsonResult["AddedRef1Values"]).Add(value);
                                }
                                else if (isValid == "Update")
                                {
                                    ((List<string>)jsonResult["UpdatedRef1Values"]).Add(value);
                                }
                            }

                            typeof(DumpUpload).GetProperty(key)?.SetValue(newDumpUpload, value);
                        }

                        newDumpUpload.UploadBy = _gluID.GetUserID(); 
                        newDumpUpload.UploadDate = DateTime.Now;
                        newDumpUpload.ItemStatus = "IN";

                        if (isValid == "Add")
                        {
                            response = await _unitOfWork.DumpUploadMasterRepository.Add(newDumpUpload);
                            addedCount++;
                        }
                        else if (isValid == "Update")
                        {
                            response = await _unitOfWork.DumpUploadMasterRepository.Update(newDumpUpload);
                            updatedCount++;
                        }
                        if(addedCount == 178)
                        {
                            string KK = "aDD";
                        }
                        await _unitOfWork.Commit();
                    }
                } 
                ((Dictionary<string, int>)jsonResult["TotalUpload"])["TotalCount"] = DT.Rows.Count;
                ((Dictionary<string, int>)jsonResult["TotalUpload"])["AddedCount"] = addedCount;
                ((Dictionary<string, int>)jsonResult["TotalUpload"])["UpdatedCount"] = updatedCount;

                // Convert the JSON result to a string
                var jsonResponse = JsonConvert.SerializeObject(jsonResult);

                response.Status = jsonResponse;
            }
            else
            {
                // Handle the case when DT is null or has no rows
                response.Status = "No data to process.";
            }
            return response;
        }
        public async Task<HashSet<string>> UpdateValidation(UpdateDumpUploadRequest request)
        {
            bool isDumpUpload = await _unitOfWork.DumpUploadMasterRepository.Any(x => x.Id != request.Id);
            if (isDumpUpload)
            {
                validationMessage.Add("DumpUpload doesnot exits Exists");
            }
            return validationMessage;
        }

        public async Task<DumpUpload> Update(UpdateDumpUploadRequest request)
        {
            var existingDumpUpload = await _unitOfWork.DumpUploadMasterRepository.GetById(request.Id);

            existingDumpUpload.Ref1 = request.Ref1;
            existingDumpUpload.Ref2 = request.Ref2;
            existingDumpUpload.Ref3 = request.Ref3;
            existingDumpUpload.Ref4 = request.Ref4;
            existingDumpUpload.Ref5 = request.Ref5;
            existingDumpUpload.Ref6 = request.Ref6;
            existingDumpUpload.Ref7 = request.Ref7;
            existingDumpUpload.Ref8 = request.Ref8;
            existingDumpUpload.Ref9 = request.Ref9;
            existingDumpUpload.Ref10 = request.Ref10;
            existingDumpUpload.Ref11 = request.Ref11;
            existingDumpUpload.Ref12 = request.Ref12;
            existingDumpUpload.Ref13 = request.Ref13;
            existingDumpUpload.Ref14 = request.Ref14;
            existingDumpUpload.Ref15 = request.Ref15;
            existingDumpUpload.Ref16 = request.Ref16;
            existingDumpUpload.Ref17 = request.Ref17;
            existingDumpUpload.Ref18 = request.Ref18;
            existingDumpUpload.Ref19 = request.Ref19;
            existingDumpUpload.Ref20 = request.Ref20;
            existingDumpUpload.Ref21 = request.Ref21;
            existingDumpUpload.Ref22 = request.Ref22;
            existingDumpUpload.Ref23 = request.Ref23;
            existingDumpUpload.Ref24 = request.Ref24;
            existingDumpUpload.Ref25 = request.Ref25;
            existingDumpUpload.Ref26 = request.Ref26;
            existingDumpUpload.Ref27 = request.Ref27;
            existingDumpUpload.Ref28 = request.Ref28;
            existingDumpUpload.Ref29 = request.Ref29;
            existingDumpUpload.Ref30 = request.Ref30;
            existingDumpUpload.Status = request.Status;
            var response = await _unitOfWork.DumpUploadMasterRepository.Update(existingDumpUpload);
            await _unitOfWork.Commit();

            return response;
        }

        public async Task<HashSet<string>> DeleteValidation(GETDumpUploadByIdRequest request)
        {
            bool IsRecord = await _unitOfWork.DumpUploadMasterRepository.Any(x => x.Id == request.DumpId);
            if (!IsRecord)
                validationMessage.Add("Record not found for deletetion");
            return validationMessage;
        }

        public async Task Delete(GETDumpUploadByIdRequest id)
        {
            var DumpUploadToDelete = await _unitOfWork.DumpUploadMasterRepository.GetById(id.DumpId);

            if (DumpUploadToDelete != null)
            {
                _unitOfWork.DumpUploadMasterRepository.Delete(DumpUploadToDelete);
                await _unitOfWork.Commit();
            }
        }
        public static DataTable convertcsvtodatatable(IFormFile file)
        {
            DataTable dt = new DataTable();
            using (var stream = file.OpenReadStream())
            using (StreamReader sr = new StreamReader(stream))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
    public interface IDumpUploadDomain
    {
        Task<List<GetAllDumpUploadResponse>> GetAll();
        Task<List<GetTemplatesClone>> GetCloneTemplates();
        Task<GetAllDumpUploadResponse> GetDumpUploadById(GETDumpUploadByIdRequest request);
        Task<HashSet<string>> AddValidation(AddDumpUploadRequestModel request);
        Task<DumpUpload> Add(IFormFile CSVData);
        Task<HashSet<string>> UpdateValidation(UpdateDumpUploadRequest request);
        Task<DumpUpload> Update(UpdateDumpUploadRequest request);
        Task<HashSet<string>> DeleteValidation(GETDumpUploadByIdRequest request);
        Task<HashSet<string>> FileValidation(IFormFile CSVData);
        Task Delete(GETDumpUploadByIdRequest id);


    }

}
