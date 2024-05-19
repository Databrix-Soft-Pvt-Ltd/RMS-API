using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class CheckList2
    {
        public int Id { get; set; }
        public int? CheckListId1 { get; set; }
        public string? CheckListName2 { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
