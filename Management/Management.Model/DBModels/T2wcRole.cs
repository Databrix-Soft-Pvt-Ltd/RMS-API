using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TwoWayCommunication.Model.DBModels
{
    [Table("T2WC_Roles")]
    public partial class T2wcRole
    {
        public T2wcRole()
        {
            T2wcUserRoleMappings = new HashSet<T2wcUserRoleMapping>();
        }

        [Key]
        public short RoleId { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        public bool? IsActive { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<T2wcUserRoleMapping> T2wcUserRoleMappings { get; set; }
    }
}
