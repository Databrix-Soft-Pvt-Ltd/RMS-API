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
using System.Threading.Tasks;
using TwoWayCommunication.Core.UnitOfWork;

namespace Management.Services.Masters
{
    public class DumpUploadDomain : IDumpUploadDomain
    {


        readonly IUnitOfWork _unitOfWork;
        private HashSet<string> validationMessage { get; set; }
        public List<AddDumpUploadRequestModel> DumpLIstData { get; set; }



        public DumpUploadDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            validationMessage = new HashSet<string>();

        }
        public async Task<IEnumerable<GetAllDumpUploadResponse>> GetAll()
        {
            var query = _unitOfWork.DumpUploadMasterRepository.AsQueryable().
                Select(c => new GetAllDumpUploadResponse
                {
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
            bool isDumpUploadExists = await _unitOfWork.DumpUploadMasterRepository.Any(x => x.Ref1.ToLower().Trim() == request.Ref1.ToLower().Trim());
            if (isDumpUploadExists)
            {
                validationMessage.Add("DumpUpload Already Exists");
            }
            return validationMessage;
        }
        public async Task<HashSet<string>> FileValidation(IFormFile CSVData)
        {
            var TemplateGetList = _unitOfWork.TemplateMasterRepository.AsQueryable().ToArray();
            var validationMessage = new HashSet<string>();

            DataTable GetLength = convertcsvtodatatable(CSVData);
            string[] GetColumnName = GetLength.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();


            if (TemplateGetList.Length > 0)
            {
                foreach (var template in TemplateGetList)
                {
                    bool isMandatory = (bool)template.IsMandatory;
                    var isValid = template.Validation.ToUpper() == "YES";

                    if (isValid && isMandatory)
                    {
                        var templateColumnName = template.DisplayName;

                        // Check if any column matches the template
                        if (!GetColumnName.Contains(templateColumnName))
                        {
                            validationMessage.Add($"{templateColumnName} Not-Valid");
                            // If a mismatch is found, you might want to break out of the loop here
                            break;
                        }
                    }
                }
                // If you need to check the validity for each template regardless of previous mismatches,
                // move this line outside the loop.
                validationMessage.Add("Valid");
            }
            return validationMessage;
        }
        public async Task<DumpUpload> Add(IFormFile formFile)
        {
            DumpUpload response = null; // Declare response outside the loop

            DataTable DT = convertcsvtodatatable(formFile);

            #region Check Duplication.. 

            //DataTable Datatablenonduplicate = table.Clone();

            DataTable Datatableduplicate = DT.Clone();

            var dupsFromCol = from dr in DT.AsEnumerable().Distinct()
                              group dr by dr["Branch Code"] into groups
                              where groups.Count() > 1
                              select groups;
            foreach (var duplicate in dupsFromCol)
            {
                for (int i = 0; i < duplicate.Count(); i++)
                {
                    DataRow dr = Datatableduplicate.NewRow();
                    dr = duplicate.ElementAt(i);
                    Datatableduplicate.ImportRow(dr);
                    DT.Rows.Remove(dr);
                }
            }

            var TotalDuplication = Datatableduplicate.AsEnumerable().Select(r => r.Field<string>("Branch Code")).Distinct().Count();
                                          
            var NonDuplication = DT.Rows.Count;
            #endregion

            var GetJso = Encryption.GlobalCommonResponse.DataTableToJsonObj(Datatableduplicate);


            // Check if the DataTable is not null and has rows
            if (DT != null && DT.Rows.Count > 0)
            {
                // Use LINQ to group by the Branch_Code column and count the groups
                var duplicateGroups = DT.AsEnumerable().GroupBy(row => row.Field<string>("Branch Code")).Where(group => group.Count() > 1).Select(group => group.Key).ToList();

                List<Object> rowColls = new List<Object>();


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
                foreach (var item in rowColls)
                {
                    if (item is Dictionary<string, string> dictionary)
                    {
                        var newDumpUpload = new DumpUpload();

                        foreach (var kvp in dictionary)
                        {
                            string key = kvp.Key;
                            string value = kvp.Value;




                            switch (key)
                            {
                                case "Ref1":
                                    newDumpUpload.Ref1 = value;
                                    break;
                                case "Ref2":
                                    newDumpUpload.Ref2 = value;
                                    break;
                                case "Ref3":
                                    newDumpUpload.Ref3 = value;
                                    break;
                                case "Ref4":
                                    newDumpUpload.Ref4 = value;
                                    break;
                                case "Ref5":
                                    newDumpUpload.Ref5 = value;
                                    break;
                                case "Ref6":
                                    newDumpUpload.Ref6 = value;
                                    break;
                                case "Ref7":
                                    newDumpUpload.Ref7 = value;
                                    break;
                                case "Ref8":
                                    newDumpUpload.Ref8 = value;
                                    break;
                                case "Ref9":
                                    newDumpUpload.Ref9 = value;
                                    break;
                                case "Ref10":
                                    newDumpUpload.Ref10 = value;
                                    break;
                                case "Ref11":
                                    newDumpUpload.Ref11 = value;
                                    break;
                                case "Ref12":
                                    newDumpUpload.Ref12 = value;
                                    break;
                                case "Ref13":
                                    newDumpUpload.Ref13 = value;
                                    break;
                                case "Ref14":
                                    newDumpUpload.Ref14 = value;
                                    break;
                                case "Ref15":
                                    newDumpUpload.Ref15 = value;
                                    break;
                                case "Ref16":
                                    newDumpUpload.Ref16 = value;
                                    break;
                                case "Ref17":
                                    newDumpUpload.Ref17 = value;
                                    break;
                                case "Ref18":
                                    newDumpUpload.Ref18 = value;
                                    break;
                                case "Ref19":
                                    newDumpUpload.Ref19 = value;
                                    break;
                                case "Ref20":
                                    newDumpUpload.Ref20 = value;
                                    break;
                                case "Ref21":
                                    newDumpUpload.Ref21 = value;
                                    break;
                                case "Ref22":
                                    newDumpUpload.Ref22 = value;
                                    break;
                                case "Ref23":
                                    newDumpUpload.Ref23 = value;
                                    break;
                                case "Ref24":
                                    newDumpUpload.Ref24 = value;
                                    break;
                                case "Ref25":
                                    newDumpUpload.Ref25 = value;
                                    break;
                                case "Ref26":
                                    newDumpUpload.Ref26 = value;
                                    break;
                                case "Ref27":
                                    newDumpUpload.Ref27 = value;
                                    break;
                                case "Ref28":
                                    newDumpUpload.Ref28 = value;
                                    break;
                                case "Ref29":
                                    newDumpUpload.Ref29 = value;
                                    break;
                                case "Ref30":
                                    newDumpUpload.Ref30 = value;
                                    break;
                            }
                        }


                        newDumpUpload.UploadBy = 100;
                        newDumpUpload.UploadDate = DateTime.Now;
                        response = await _unitOfWork.DumpUploadMasterRepository.Add(newDumpUpload);
                        await _unitOfWork.Commit();
                    }
                }
                response.Status = "Total Upload : " + DT.Rows.Count + "  Total-Impoert-Duplication :  " + Datatableduplicate.Rows.Count + "jsonDuplication" + GetJso;
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
        Task<IEnumerable<GetAllDumpUploadResponse>> GetAll();
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
