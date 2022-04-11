using System;
using System.Collections.Generic;
using System.Text;

namespace AllStore.Domain.Models
{
    public class SignInResult
    {
        public AppUser AppUser { get; set; }
        public bool Success { get; set; }
    }
}
