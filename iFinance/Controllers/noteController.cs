using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iFinance.Models;

namespace iFinance.Controllers
{
    public class noteController : Controller
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
            List<Note> mlist = db.getNotesByUser(user.Id);
            NoteListViewModel obj = new NoteListViewModel()
            {
                list = mlist
            };
            return View(obj);
        }

        [HttpGet]
        public ActionResult add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult add(Note m)
        {
            if (!isLogin()) return RedirectToAction("login", "auth");
            User user = getCurrentUser();
            m.UserId = user.Id;
            m.SubmitTime = DateTime.Now;
            Provider db = new Provider();
            try
            {
                db.insertNote(m);
                int nid = (int)db.getDataRow("SELECT Id FROM Note ORDER BY Id DESC")["Id"];
            
                string content = "<tr id='note_item_" + nid + "'><td>" + m.Name+ "</td><td>" +m.Content + "</td><td>" + m.SubmitTime.ToString("yyyy-MM-dd") + "</td><td style='font-size:20px;'><a href='javascript:void(0)' class='btn_edit' onclick='showEditNote(" + nid + ")'> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></td><td style='style='font-size:20px;'><a href='javascript:void(0)' class='btn_delete' onclick='showDeleteNote(" + nid + ")'><span style='font-size:24px'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></td></tr>";
                return Content(content);
            }
            catch (Exception e)
            {
                return Content("fail" + e.Message);
            }
        }

        [HttpGet]
        public ActionResult edit(int id)
        {
            Provider db = new Provider();
            Note m = db.getNoteById(id);
            return View(m);
        }
        [HttpPost]
        public ActionResult edit(Note m)
        {
            try
            {
                Provider db = new Provider();
                db.updateNote(m);
                string content = "<td>" + m.Name + "</td><td>" + m.Content + "</td><td>" + m.SubmitTime.ToString("yyyy-MM-dd") + "</td><td style='font-size:20px;'><a href='javascript:void(0)' class='btn_edit' onclick='showEditNote(" + m.Id + ")'> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></td><td style='font-size:20px;'><a href='javascript:void(0)' class='btn_delete' onclick='showDeleteNote(" + m.Id + ")'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></td>";
                return Content(content);
            }
            catch (Exception e)
            {
                return Content("fail" + e.Message);
            }
        }

        public ActionResult delete(int id)
        {
            if (!isLogin()) return RedirectToAction("login", "auth");

            Provider db = new Provider();
            try
            {
                db.deleteNote(id);
            }
            catch (Exception e)
            {
                return Content("fail" + e.Message);
            }
            return Content("ok");
        }

    }
}
