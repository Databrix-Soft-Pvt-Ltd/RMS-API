﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class UpdateRoleRequestModel
    {

        public long Id { get; set; } 
        public string RoleName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool? IsActive { get; set; }
    }
}
