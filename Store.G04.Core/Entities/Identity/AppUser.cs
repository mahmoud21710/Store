﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Entities.Identity
{
    public class AppUser  :IdentityUser
    {
        public string DisplayName { get; set; } //after login
        public Address  Address { get; set; }
    }
}
