﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.ResponseModel
{
    public class UpdateClientMappingResponse
    {
        public long Id { get; set; }
        public long? ClientId { get; set; }
        public long? UserId { get; set; }
     
    }
}
