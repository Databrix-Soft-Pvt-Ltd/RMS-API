using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TwoWayCommunication.Model.DBModels
{
    [Table("T2WC_Users")]
    public partial class T2wcUser
    {
        public T2wcUser()
        {
            T2wcUserRoleMappings = new HashSet<T2wcUserRoleMapping>();
        }

        [Key]
        public long UserId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string UserName { get; set; } = null!;
        [StringLength(255)]
        public string EmailId { get; set; } = null!;
        [StringLength(15)]
        public string MobileNumber { get; set; } = null!;
        [StringLength(50)]
        public string Password { get; set; } = null!;
        [StringLength(50)]
        public string? Address { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public int OrgId { get; set; }
        public int UserType { get; set; }


        [InverseProperty("User")]
        public virtual ICollection<T2wcUserRoleMapping> T2wcUserRoleMappings { get; set; }
    }
}
