﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsuranceWebAPI.Entity
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}