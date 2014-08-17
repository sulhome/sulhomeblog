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
    }
}