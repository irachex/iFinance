using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iFinance.Models
{
    public class MsgViewModel
    {
        public string msg { get; set; }
        public string url { get; set; }
    }
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Info { get; set; }
    }

    public class EditPasswordViewModel
    {
        public string Password { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Info { get; set; }
    }

    public class AccountListViewModel
    {
        public List<AccountViewModel> list { get; set; }
    }
    public class AccountViewModel : Account
    {
        public string Tag { get; set; }
    }

    public class ChartDataModel
    {
        public double money { get; set; }
        public long time { get; set; }
    }
    public class StaticsViewModel
    {
        public double income { get; set; }
        public double outcome { get; set; }
        public Dictionary<string, double> tagin { get; set; }
        public Dictionary<string, double> tagout { get; set; }
        public Dictionary<long,double> ichart { get; set; }
        public Dictionary<long, double> ochart { get; set; }
        public Dictionary<long, double> schart { get; set; }
    }
    public class TagListViewModel
    {
        public List<Tag> list { get; set; }
    }

    public class NoteListViewModel
    {
        public List<Note> list { get; set; }
    }
    public class UserViewModel : User
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Info { get; set; }
    }
    public class UserListViewModel
    {
        public List<UserViewModel> list { get; set; }
    }
}