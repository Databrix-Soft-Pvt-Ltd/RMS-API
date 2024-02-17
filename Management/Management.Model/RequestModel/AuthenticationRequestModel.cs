using System;
using System.Collections.Generic;
using System.Text;

namespace TwoWayCommunication.Model.RequestModel
{
    public class AuthenticationRequestModel
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
