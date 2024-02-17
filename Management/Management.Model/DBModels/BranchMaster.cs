using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TwoWayCommunication.Model.DBModels
{
    [Table("BranchMaster")]
    public partial class BranchMaster
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? BranchName { get; set; }
    }
}
