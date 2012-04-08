using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iFinance.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime RegisterTime { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Info { get; set; }
    }

    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Money { get; set; }
        public DateTime Time { get; set; }
        public string Info { get; set; }
        public bool State { get; set; }
        public DateTime SubmitTime { get; set; }
        public bool Type { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class Account_Tag
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int TagId { get; set; }
    }

    public class Note
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime SubmitTime { get; set; }
    }

}