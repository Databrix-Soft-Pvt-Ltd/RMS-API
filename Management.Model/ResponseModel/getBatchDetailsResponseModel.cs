using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.ResponseModel
{
    public class getBatchDetailsResponseModel
    {
            public int id { get; set; }
            public int documentID { get; set; }
            public  string batch_id { get; set; }
            public string Ref1 { get; set; }
            public string file_no { get; set; }
            public string carton_no { get; set; }
            public string document_type { get; set; }
            public string file_status { get; set;}
            public string CheckListName1 { get; set; }
            public string CheckListName2 { get; set; }
            public string CheckListName3 { get; set; }
    



    }

    public class getBranchInwardDetailsResponsemodel
    {
        public int id { get; set; }
        public string batch_id { get; set; }
        public string Ref1 { get; set; }
        public string file_no { get; set; }
        public string carton_no { get; set; }
        public string document_type { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string Ref4 { get; set; }
        public string Ref5 { get; set; }

        public string Ref6 { get; set; }
        public string Ref7 { get; set; }
        public string Ref8 { get; set; }
        public string Ref9 { get; set; }
        public string Ref10 { get; set; }
        public string Ref11 { get; set; }
        public string Ref12 { get; set; }
        public string Ref13 { get; set; }
        public string Ref14 { get; set; }
        public string Ref15 { get; set; }

    }

    public class getBIPendingDetailsResponseModel
    {
        public string batch_id { get; set; }
        public string status { get; set; }
        public int file_count { get; set; }
        public string consignment_number { get; set; }
        public string CourierName { get; set; }
        public string UserName { get; set; }

    }
    public class getCourierAckPendingResponseModel
    {
        public string batch_id { get; set; }
        public string status { get; set; }
        public int file_count { get; set; }
        public string consignment_number { get; set; }
        public string CourierName { get; set; }
        public string batch_close_date { get; set; }

        public string userName { get; set; }

    }

    public class getFileAckPendingResponseModel
    {
        public string batch_id { get; set; }
        public string status { get; set; }
        public int file_count { get; set; }
        public string consignment_number { get; set; }
        public string CourierName { get; set; }
        public string batch_close_date { get; set; }

        public string userName { get; set; }

        public string courier_ack_by { get; set; }
        public string courier_ack_date { get; set; }
    }
    public class getLanDetailsByBatchNoResponseModel
    {
        public string batch_id { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string Ref4 { get; set; }
        public string Ref5 { get; set; }
        public string file_status { get; set; }
        public string status { get; set; }

    }
    public class getDocumentDetailsByLanNoResponseModel
    {
        public string batch_id { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string Ref4 { get; set; }
        public string Ref5 { get; set; }
        public string file_status { get; set; }
        public string status { get; set; }
        public string CheckListName1 { get; set; }
        public string CheckListName2 { get; set; }
        public string CheckListName3 { get; set; }
        public string document_status { get; set; }
        public int DocumentID { get; set; }
    }

    

      public class getackDetailsByLanAndBatchNoRM
    {
        public string batch_id { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string Ref4 { get; set; }
        public string Ref5 { get; set; }

        public string Ref6 { get; set; }
        public string Ref7 { get; set; }
        public string Ref8 { get; set; }
        public string Ref9 { get; set; }
        public string Ref10 { get; set; }
        public string Ref11 { get; set; }
        public string Ref12 { get; set; }
        public string Ref13 { get; set; }
        public string Ref14 { get; set; }
        public string Ref15 { get; set; }
        public string file_status { get; set; }
        public string status { get; set; }
        public string CheckListName1 { get; set; }
        public string CheckListName2 { get; set; }
        public string CheckListName3 { get; set; }
        public string document_status { get; set; }
        public int DocumentID { get; set; }
    }

}
