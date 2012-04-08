using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using iFinance.Models;

namespace iFinance.Controllers
{
    public class authController : Controller
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

        [HttpGet]
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(LoginViewModel m)
        {
            if (m.Password == null || m.UserName == null)
            {
                ViewData["error"] = "没填的input伤不起";
                return View();
            }
            m.Password = Global.md5(m.Password);
            Provider db = new Provider();
            User user = db.getUser(m.UserName);
            if (user == null) {
                ViewData["error"] = "用户名不存在，要先注册有木有！！！";
                return View();
            }
            if (user.Password != m.Password)
            {
                ViewData["error"] = "密码错了，再试试吧";
                return View();
            }
            Session["user"] = user;
            if (user.IsAdmin)
            {
                return RedirectToAction("index", "admin");
            }
            return RedirectToAction("index","home");
        }

        [HttpGet]
        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult register(RegisterViewModel m)
        {
            if (m.UserName==null || m.Password1 == null || m.Password2 == null)
            {
                ViewData["error"] = "没填的input伤不起";
                return View();
            }
            m.Password1 = Global.md5(m.Password1);
            m.Password2 = Global.md5(m.Password2);
            Provider db = new Provider();
            User user = db.getUser(m.UserName);
            if (user != null)
            {
                ViewData["error"] = "哎呀，此用户名已经被注册了";
                return View();
            }
            if (m.Password1 != m.Password2)
            {
                ViewData["error"] = "两次密码不一样";
                return View();
            }
            Regex emailExp = new Regex(@"^\w+@\w+(\.\w+)+(\,\w+@\w+(\.\w+)+)*$");
            if (!string.IsNullOrEmpty(m.Email) && !emailExp.Match(m.Email).Success)
            {
                ViewData["error"] = "邮箱格式不正确";
                return View();
            }

            User u = new User();
            u.UserName = m.UserName;
            u.Password = m.Password1;
            u.RegisterTime = DateTime.Now;
            db.insertUser(u);

            int uid = (int)db.getDataRow("SELECT Id FROM [User] ORDER BY Id DESC")["Id"];
            Global.addDefaultTag(uid);

            UserInfo userinfo = new UserInfo();
            userinfo.Id = uid;
            userinfo.Address = m.Address;
            userinfo.Email = m.Email;
            userinfo.Phone = m.Phone;
            userinfo.Info = "";
            db.insertUserInfo(userinfo);
            return View("msg", new MsgViewModel() { msg = "注册成功", url = "/auth/login" });
        }

        [HttpGet]
        public ActionResult edit()
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
           
            Provider db = new Provider();
            User u=getCurrentUser();
            UserInfo userinfo=db.getUserInfo(u.Id);
            EditPasswordViewModel obj = new EditPasswordViewModel();
            obj.Address = userinfo.Address;
            obj.Phone = userinfo.Phone;
            obj.Email = userinfo.Email;
            obj.Info = userinfo.Info;
            obj.Password = "";
            obj.Password1 = "";
            obj.Password2 = "";
            return View(obj);
        }

        [HttpPost]
        public ActionResult edit(EditPasswordViewModel m)
        {
            m.Password = Global.md5(m.Password);
            m.Password1 = Global.md5(m.Password1);
            m.Password2 = Global.md5(m.Password2);

            
            if (!isLogin()) return RedirectToAction("login", "auth");
            Provider db = new Provider();
            if (m.Password1 != m.Password2)
            {
                ViewData["error"] = "两次密码不一样";
                return View();
            }
            User u = getCurrentUser();
            if (u.Password != m.Password)
            {
                ViewData["error"] = "密码错了，再试试吧";
                return View();
            }
            u.Password = m.Password1;
            db.updateUser(u);
            UserInfo ui = new UserInfo();
            ui.Id = u.Id;
            ui.Address = m.Address;
            ui.Phone = m.Phone;
            ui.Email = m.Email;
            ui.Info = m.Info;
            db.updateUserInfo(ui);
            return View("EditSuccess");
        }
        public ActionResult logout()
        {
            Session["user"] = null;
            return RedirectToAction("login");
        }
    }
}
