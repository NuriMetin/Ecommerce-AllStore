using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class SignInResult
    {
        public AppUser AppUser { get; set; }
        public bool Success { get; set; }
    }
}
