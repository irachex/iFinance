using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iFinance.Models;

namespace iFinance.Controllers
{
    [HandleError]
    public class homeController : Controller
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

        public string getTagString(List<Tag> tags)
        {
            string ans="";
            foreach (Tag t in tags)
            {
                ans += t.Name + " ";
            }
            return ans.Trim();
        }

        public ActionResult index()
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            return View();
        }

        public ActionResult accountList(string tags="", string starttime="", string endtime="")
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            User user=getCurrentUser();
            Provider db = new Provider();
            List<Account> result = new List<Account>();
            tags = tags.Trim();
            if (String.IsNullOrEmpty(tags))
            {
                if (String.IsNullOrEmpty(starttime)) { result = db.getAccountsByUser(user.Id); }
                else { result = db.getAccountsByDate(user.Id, Convert.ToDateTime(starttime), Convert.ToDateTime(endtime)); }
            }
            else
            {
                string[] tagarray = tags.Split(new char[] { ',','|',' ' });
                List<int> tagIds = new List<int>();
                foreach (string s in tagarray)
                {
                    try
                    {
                        tagIds.Add(Convert.ToInt32(s));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }
                List<Account> tmp = db.getAccountsByTags(tagIds);
                if (String.IsNullOrEmpty(starttime))
                {
                    result = tmp;
                }
                else
                {
                    DateTime start=Convert.ToDateTime(starttime);
                    DateTime end=Convert.ToDateTime(endtime);
                    foreach (Account a in tmp)
                    {
                        if (a.Time >= start && a.Time <= end) result.Add(a);
                    }
                }
            }
            AccountListViewModel obj = new AccountListViewModel();
            obj.list=new List<AccountViewModel>();
            foreach (Account r in result) {
                obj.list.Add(new AccountViewModel() {
                     Id=r.Id,
                     Info=r.Info,
                     Money=r.Money,
                     State=r.State,
                     SubmitTime=r.SubmitTime,
                     Time=r.Time,
                     Type=r.Type,
                     UserId=r.UserId,
                     Tag=getTagString(db.getTagsByAccount(r.Id))
                });
            }
            return View(obj);
        }

        public ActionResult detail(int id = 1)
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            User user = getCurrentUser();

            Provider db = new Provider();
            Account account = db.getAccount(id);

            if (account == null) return View("msg", new MsgViewModel() { msg = "出错了肿么办...页面不存在", url = "/" });
            if (account.UserId != user.Id) return View("msg", new MsgViewModel() { msg = "没有权限...看别人隐私的不是好孩纸" });

            return View(account);
        }

        [HttpGet]
        public ActionResult addAccount()
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            User user = getCurrentUser();
            
            Provider db = new Provider();
            ViewData["TagCloud"] = db.getTagsByUser(user.Id);
            
            return View();
        }
        [HttpPost]
        public ActionResult addAccount(AccountViewModel account)
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            User user = getCurrentUser();

            account.SubmitTime = DateTime.Now;
            account.State = false;
            if (account.Info==null) account.Info = "";
            account.UserId = user.Id;

            Provider db=new Provider();
            db.insertAccount(account);

            int aid = (int)db.getDataRow("SELECT Id FROM Account ORDER BY Id DESC")["Id"];
            db.deleteAccountTagByAccount(aid);

            string[] tmp = account.Tag.Split(new char[] { ' ', ',', '，', ' ' });
            foreach (string t in tmp)
            {
                Tag tag = db.getTagsByName(t);
                if (tag == null)
                {
                    db.insertTag(new Tag() {Name=t,Count=0, UserId=user.Id });
                }
                db.insertAccountTag(aid, tag.Id);
            }
            string content = "<tr id='account_item_" + aid + "'><td>" + (account.Type ? "收入" : "支出") + "</td><td>" + account.Money + "</td><td>" + account.Time.ToString("yyyy-MM-dd") + "</td><td>" + account.Info + "</td><td>" + account.Tag + "</td><td style='font-size:20px;'><a href='javascript:void(0)' class='btn_edit' onclick='showEditAccount(" + aid + ")'> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></td><td style='style='font-size:20px;'><a href='javascript:void(0)' class='btn_delete' onclick='showDeleteAccount(" + aid + ")'><span style='font-size:24px'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></td></tr>";
            return Content(content);
        }

        [HttpGet]
        public ActionResult editAccount(int id)
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            User user = getCurrentUser();
           
            Provider db=new Provider();
            Account r=db.getAccount(id);
            if (user.Id != r.UserId) return View("msg", new MsgViewModel() { msg = "没有权限" });
            AccountViewModel obj = new AccountViewModel()
            {
                Id = r.Id,
                Info = r.Info,
                Money = r.Money,
                State = r.State,
                SubmitTime = r.SubmitTime,
                Time = r.Time,
                Type = r.Type,
                UserId = r.UserId,
                Tag = getTagString(db.getTagsByAccount(r.Id))
            };
            ViewData["TagCloud"] = db.getTagsByUser(user.Id);
            
            return View(obj);
        }
        [HttpPost]
        public ActionResult editAccount(AccountViewModel account)
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            User user = getCurrentUser();

            Account a = new Account()
            {
                Id=account.Id,
                Info=account.Info,
                Money=account.Money,
                State=account.State,
                Time=account.Time,
                Type=account.Type,
                UserId=account.UserId
            };
            Provider db = new Provider();
            db.updateAccount(a);

            int aid = account.Id;
            db.deleteAccountTagByAccount(aid);

            string[] tmp = account.Tag.Split(new char[] { ' ', ',', '，', ' ' });
            foreach (string t in tmp)
            {
                Tag tag = db.getTagsByName(t);
                if (tag == null)
                {
                    db.insertTag(new Tag() { Name = t, Count = 0, UserId = user.Id });
                    tag = new Tag();
                    tag.Id = (int)db.getDataRow("SELECT Id FROM Tag ORDER BY Id DESC")["Id"];
                }
                db.insertAccountTag(aid, tag.Id);
            }

            string content = "<td>" + (account.Type ? "收入" : "支出") + "</td><td>" + account.Money + "</td><td>" + account.Time.ToString("yyyy-MM-dd") + "</td><td>" + account.Info + "</td><td>" + account.Tag + "</td><td style='font-size:20px;'><a href='javascript:void(0)' class='btn_edit' onclick='showEditAccount(" + aid + ")'> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></td><td style='font-size:20px;'><a href='javascript:void(0)' class='btn_delete' onclick='showDeleteAccount(" + aid + ")'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></td>";

            return Content(content);
        }

        [HttpPost]
        public ActionResult deleteAccount(int id)
        {
            if (!isLogin()) return RedirectToAction("login", "auth");

            Provider db = new Provider();
            try
            {
                db.deleteAccountTagByAccount(id);
                db.deleteAccount(id);
            }
            catch (Exception e)
            {
                return Content("fail"+e.Message);
            }
            return Content("ok");
        }

        [HttpGet]
        public ActionResult addTag()
        {

            return View();
        }
        [HttpPost]
        public ActionResult addTag(Tag tag)
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            User user = getCurrentUser();

            tag.UserId = user.Id;
            tag.Count = 0;
            Provider db = new Provider();
            try
            {
                db.insertTag(tag);
            }
            catch (Exception e)
            {
                return Content("fail" + e.Message);
            }
            tag.Id=(int)db.getDataRow("SELECT Id FROM Tag ORDER BY Id DESC")["Id"];

            db.mergeTag(tag.Id, tag.Name);
            string content = "<li id='tag_item_+" + tag.Id + "> <span class='tag_name'>" + tag.Name + " </span><a href='javascript:void(0)' onclick='showEditTag(" + tag.Id + ")'>编辑</a> | <a href='javascript:void(0)' onclick='showDeleteTag(" + tag.Id + ")'>删除</a></li>";
            return Content(content);
        }

        [HttpPost]
        public ActionResult editTag(Tag tag)
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            User user = getCurrentUser();

            tag.UserId = user.Id;
            Provider db = new Provider();
            try
            {
                db.updateTag(tag);
                db.mergeTag(tag.Id, tag.Name);
            }
            catch (Exception e)
            {
                return Content("fail" + e.Message);
            }
            return Content("ok");
        }


        [HttpPost]
        public ActionResult deleteTag(int id)
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            User user = getCurrentUser();

            Provider db = new Provider();
            try
            {
                db.deleteAccountTagByTag(id);
                db.deleteTag(id);
            }
            catch (Exception e)
            {
                return Content("fail" + e.Message);
            }
            return Content("ok");
        }

        [HttpGet]
        public ActionResult editTagList()
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            User user = getCurrentUser();

            Provider db = new Provider();
            List<Tag> tags = db.getTagsByUser(user.Id);
            TagListViewModel obj = new TagListViewModel()
            {
                list = tags
            };

            return View(obj);
        }
       
        public ActionResult taglist()
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            int uid = getCurrentUser().Id;
            Provider db = new Provider();
            TagListViewModel obj = new TagListViewModel()
            {
                list=db.getTagsByUser(uid)
            };
            return View(obj);
        }

        public ActionResult statics(string tags="", string starttime="", string endtime="")
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            User user=getCurrentUser();
            Provider db = new Provider();
            List<Account> result = new List<Account>();
            tags = tags.Trim();
            if (String.IsNullOrEmpty(tags))
            {
                if (String.IsNullOrEmpty(starttime)) { result = db.getAccountsByUser(user.Id); }
                else { result = db.getAccountsByDate(user.Id, Convert.ToDateTime(starttime), Convert.ToDateTime(endtime)); }
            }
            else
            {
                string[] tagarray = tags.Split(new char[] { ',','|',' ' });
                List<int> tagIds = new List<int>();
                foreach (string s in tagarray)
                {
                    try
                    {
                        tagIds.Add(Convert.ToInt32(s));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }
                List<Account> tmp = db.getAccountsByTags(tagIds);
                if (String.IsNullOrEmpty(starttime))
                {
                    result = tmp;
                }
                else
                {
                    DateTime start=Convert.ToDateTime(starttime);
                    DateTime end=Convert.ToDateTime(endtime);
                    foreach (Account a in tmp)
                    {
                        if (a.Time >= start && a.Time <= end) result.Add(a);
                    }
                }
            }

            double tincome = 0;
            double toutcome = 0;
            Dictionary<string, double> ttagout = new Dictionary<string, double>();
            Dictionary<string, double> ttagin = new Dictionary<string, double>();
            
            Dictionary<long, double> inchart = new Dictionary<long, double>();
            Dictionary<long, double> outchart = new Dictionary<long, double>();
            Dictionary<long, double> chart = new Dictionary<long, double>();

            foreach (Account a in result)
            {
                if (a.Type == false) toutcome += a.Money;
                else tincome += a.Money;
                List<Tag> tlist = db.getTagsByAccount(a.Id);
                foreach (Tag tt in tlist)
                {
                    if (!ttagin.ContainsKey(tt.Name) || !ttagout.ContainsKey(tt.Name))
                    {
                        ttagin[tt.Name] = 0;
                        ttagout[tt.Name] = 0;
                    }
                    if (a.Type == false) ttagout[tt.Name] += a.Money;
                    else ttagin[tt.Name] += a.Money;
                }
               
                long ts = Global.getTimestamp(a.Time);
                if (!chart.ContainsKey(ts)) chart[ts] = 0;
                if (!inchart.ContainsKey(ts)) inchart[ts] = 0;
                if (!outchart.ContainsKey(ts)) outchart[ts] = 0;

                if (a.Type == false)
                {
                
                   chart[ts] -= a.Money;
                   inchart[ts] += a.Money;
                }
                else
                {
                  
                    chart[ts] += a.Money;
                    outchart[ts] += a.Money;
                }
            }
            StaticsViewModel obj = new StaticsViewModel()
            {
                income = tincome,
                outcome = toutcome,
                tagin = ttagin,
                tagout = ttagout,
                schart = chart,
                ichart= inchart,
                ochart = outchart
            };
            return View(obj);
        }
        
        public ActionResult about()
        {
            return View();
        }
    }
}
