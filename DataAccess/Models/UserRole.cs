﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class UserRole
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
