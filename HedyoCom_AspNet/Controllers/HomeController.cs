using HedyoCom_AspNet.DAL;
using HedyoCom_AspNet.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HedyoCom_AspNet.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            return View();// RedirectToAction("Index", "Wedding");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Search(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var SearchReq = this.unitOfWork.EventRepository.GetByID(id);

                ManageEventViewModel Transferrer = new ManageEventViewModel();

                if (SearchReq != null)
                {
                    Transferrer.EventId = SearchReq.Id;
                    Transferrer.EventType = SearchReq.EventType.Name;
                    Transferrer.Date = SearchReq.Date;
                }





                return View(Transferrer);
            }
        }
    }
}