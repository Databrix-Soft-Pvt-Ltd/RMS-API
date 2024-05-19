namespace TwoWayCommunication.Model.ResponseModel
{
    public   class TokenValidation
    {
        public string ValiedTokenRespose { get; set; }
        public string UserID { get; set; }
        public string RoleID { get; set; }
        public string EmailID { get; set; }
        public bool IsValid { get; set; } = false;
    }
   
}
 