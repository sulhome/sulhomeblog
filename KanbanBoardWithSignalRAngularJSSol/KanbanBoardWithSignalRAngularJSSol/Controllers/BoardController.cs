using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using KanbanBoardWithSignalRAngularJSSol.Models;

namespace KanbanBoardWithSignalRAngularJSSol.Controllers
{
    public class BoardController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetColumns()
        {
            var repo = new BoardRepository();
            return Json(repo.GetColumns(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CanMove(int sourceColId, int targetColId)
        {
            if (sourceColId == (targetColId - 1))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void MoveTask(int taskId, int targetColId)
        {
            var repo = new BoardRepository();
            repo.MoveTask(taskId, targetColId);
        }
    }
}