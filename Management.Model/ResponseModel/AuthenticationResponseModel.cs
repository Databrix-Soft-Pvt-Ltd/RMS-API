using System;
using System.Collections.Generic;
using System.Text;

namespace TwoWayCommunication.Model.ResponseModel
{
    public class AuthenticationResponseModel
    {
        public long UserId { get; set; }      
        public int RoleID { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public int UserType { get; set; }
        public string Token { get; set; } 
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
