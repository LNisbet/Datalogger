﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger.Models
{
    public class ValueDescription
    {
        public object? Value { get; set; }
        public object? Description { get; set; }

        public ValueDescription()
        {
        }
    }
}
