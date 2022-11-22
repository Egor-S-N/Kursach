using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banking_system.Models
{
    public class Register
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int Salary { get; set; }
        public string Phone { get; set; }
        public byte[] Photo { get; set; }
        public Nullable<int> Id_account { get; set; }
    }
}