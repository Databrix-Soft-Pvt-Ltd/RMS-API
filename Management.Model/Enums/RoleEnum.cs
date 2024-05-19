using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwoWayCommunication.Model.Enums
{
    public enum RoleEnum
    {
        Admin = 1,
        SuperAdmin = 2,
        Agent = 3,
        EndUser = 4
    }
    public class GlobalUserID
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GlobalUserID(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long GetUserID()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (userIdString != null && long.TryParse(userIdString, out long userId))
            {
                return userId;
            } 
            return -1;
        }
    }
}
