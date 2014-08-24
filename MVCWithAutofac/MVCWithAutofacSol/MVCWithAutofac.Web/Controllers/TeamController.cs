using MVCWithAutofac.Core.Interfaces;
using MVCWithAutofac.Core.Model;
using MVCWithAutofac.Web.Filters;
using System.Linq;
using System.Web.Mvc;

namespace MVCWithAutofac.Web.Controllers
{
    public class TeamController : Controller
    {
        private IRepository repo;
        public TeamController(IRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var teams = repo.GetAll<Team>().ToList();
            return View(teams);
        }

        [HttpGet]
        public ActionResult InsertTeam()
        {
            return View();
        }

        public ActionResult TeamDetails(int id)
        {
            ViewBag.TeamId = id;
            return View();
        }
        
        [HttpPost]
        [LogActionFilter]
        public ActionResult InsertTeam(Team team)
        {
            if (ModelState.IsValid)
            {
                repo.Insert<Team>(team);
                repo.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View();
        }
    }
}