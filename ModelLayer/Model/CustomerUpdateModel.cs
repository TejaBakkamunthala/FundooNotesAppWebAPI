﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Model
{
    public class CustomerUpdateModel
    {
        public string CustomerName {  get; set; }

        public string CustomerAddress {  get; set; }

        public string CustomerCity { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPassword {  get; set; }

        public string CustomerPhoneNumber { get; set; }
    }
}
