using System;
using System.Collections.Generic;
using System.Text;

namespace AllStore.Domain.Models
{
    public class AppUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int WrongPasswordCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LockedDate { get; set; }
        public bool Active { get; set; }
    }
}
