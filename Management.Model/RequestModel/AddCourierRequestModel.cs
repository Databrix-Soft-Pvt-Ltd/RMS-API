﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.RequestModel
{
    public class AddCourierRequestModel
    {
        public string? CourierName { get; set; }

        public bool? IsActive { get; set; }
    }
}
