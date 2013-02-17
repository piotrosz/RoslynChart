﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartScript.Models
{
    [Serializable]
    public class CreateChartResult
    {
        public string Guid { get; set; }
        public string Message { get; set; }
    }
}