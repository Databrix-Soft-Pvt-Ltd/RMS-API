using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class AddBIDetailsRequestModel
    {
        public string BatchID { get; set; }
        public string Ref1 { get; set; }
        public string carton_number { get; set; }
        public string file_number { get; set; }
        public int [] CheckListID1 { get; set; }
        public int[] CheckListID2 { get; set; }     
        public int[] CheckListID3 { get; set; } 

    }


    public class UpdatePODNumberRequestModel
    {
        public string BatchID { get; set; }
        public int CourierId { get; set; }
        public string ConsignmentNumber { get; set; }
        public string DispatchAddress { get; set; } 

    }
    public class CourierACKRequestModel
    {
        public string BatchID { get; set; }

    }


    public class UpdateFileACKRequestModel
    {
        public string BatchID { get; set; }
        public string Ref1 { get; set; }
        public string status { get; set; }

    }

}
