using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iFinance.Models;

namespace iFinance.Controllers
{
    public class adminController : Controller
    {
        public User getCurrentUser()
        {
            if (Session["user"] == null) return null;
            return (User)Session["user"];
        }

        public bool isLogin()
        {
            if (Session["user"] == null) return false;
            return true;
        }

        public ActionResult index()
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            return View();
        }
        public ActionResult list()
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            User user = getCurrentUser();
            Provider db = new Provider();
            List<User> mlist = db.getAllUser();
            List<UserViewModel> obj = new List<UserViewModel>();
            foreach (User u in mlist)
            {
                UserInfo ui=db.getUserInfo(u.Id);
                UserViewModel uv = new UserViewModel();
                uv.Id = u.Id;
                uv.IsAdmin = u.IsAdmin;
                uv.RegisterTime = u.RegisterTime;
                uv.UserName = u.UserName;
                uv.Address = ui.Address;
                uv.Email = ui.Email;
                uv.Info = ui.Info;
                uv.Phone = ui.Phone;
                obj.Add(uv);
            }
            UserListViewModel viewmodel = new UserListViewModel()
            {
                list=obj
            };
            return View(viewmodel);
        }

       
    }
}
