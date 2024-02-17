using System;
using System.Collections.Generic;

namespace Management.Model.RMSEntity
{
    public partial class UserMaster
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }
        public int? UserType { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? PasswordExpiryDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? Ipaddress { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        // Navigation property to access the related Role entity
        public virtual Role Role { get; set; }
    }
}
