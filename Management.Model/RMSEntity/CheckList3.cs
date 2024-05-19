using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class CheckList3
    {
        public int Id { get; set; }
        public int? CheckListId2 { get; set; }
        public string? CheckListName3 { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
