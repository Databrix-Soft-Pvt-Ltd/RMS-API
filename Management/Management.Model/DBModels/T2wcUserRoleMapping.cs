using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TwoWayCommunication.Model.DBModels
{
    [Table("T2WC_UserRoleMappings")]
    public partial class T2wcUserRoleMapping
    {
        [Key]
        public long UserRoleMappingId { get; set; }
        public long UserId { get; set; }
        public short RoleId { get; set; }
        [Required]
        public bool? IsActive { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("T2wcUserRoleMappings")]
        public virtual T2wcRole Role { get; set; } = null!;
        [ForeignKey("UserId")]
        [InverseProperty("T2wcUserRoleMappings")]
        public virtual T2wcUser User { get; set; } = null!;
    }
}
