﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class AddPickListRequestModel
    {
        public string? LogoPath { get; set; }
        public long? TemplateId { get; set; }
        public string? Address { get; set; }
    }
}
