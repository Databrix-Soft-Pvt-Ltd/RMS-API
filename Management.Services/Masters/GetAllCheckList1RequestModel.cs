using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class GetAllCheckList1RequestModel
    {
        public long id { get; set; }
        public string? CheckListName1 { get; set; }

    }
    public class AddCheckList1
    {

        public string? CheckListName1 { get; set; }

    }

    public class AUpdateCheckList1
    {
        public long id { get; set; }
        public string? CheckListName1 { get; set; }

    }

    public class GetChkListByIdRequestModel
    {
        public long id { get; set; }
    }

    ////Check List 2
    ///
    public class GetAllCheckList2RequestModel
    {
        public long id { get; set; }
        public string? CheckListName2 { get; set; }
        public long? CheckListID1 { get; set; }
        //public string? CreatedBy { get; set; }
        public string? CheckListName1 { get; set; }
    }
    public class AddCheckList2
    {

        public string? CheckListName2 { get; set; }
        public long? CheckListID1 { get; set; }

    }

    public class UpdateCheckList2
    {
        public long id { get; set; }
        public string? CheckListName2 { get; set; }
        public long? CheckListID1 { get; set; }

    }
    public class AddCheckList3
    {
        public long id { get; set; }
        public string? CheckListName3 { get; set; }
        public long? CheckListID2 { get; set; }
    }

    public class GetAllCheckList3RequestModel
    {
        public long id { get; set; }
        public string? CheckListName1 { get; set; }
        public string? CheckListName2 { get; set; }
        public string? CheckListName3 { get; set; }
        public long? CheckListID1 { get; set; }
        public long? CheckListID2 { get; set; }

    }

    public class GetAllCheckList3ByChkList2RequestModel
    {
        public long id { get; set; }
        public string? CheckListName1 { get; set; }
        public string? CheckListName2 { get; set; }
        public string? CheckListName3 { get; set; }
        public long? CheckListID1 { get; set; }
        public long? CheckListID2 { get; set; }

    }

    public class GetAllChkList3ByChkList2ID
    {
        public long id { get; set; }
        public string? CheckListName3 { get; set; }
    }


}
