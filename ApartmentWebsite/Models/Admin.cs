﻿using System;
using System.Collections.Generic;

namespace ApartmentWebsite.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string? AdminName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}
