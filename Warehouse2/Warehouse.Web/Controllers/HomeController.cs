using System.Linq;
using System.Web.Mvc;
using e10.Data.Services;
using e10.Shared.Models;
using e10.Shared.Services.Abstraction;
using Warehouse.Web.Models;

namespace Warehouse.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISiteService _siteService;
        private readonly ISystemService _systemService;

        public HomeController(ISiteService siteService, ISystemService systemService)
        {
            _siteService = siteService;
            _systemService = systemService;
        }

        //[OutputCache(Location = OutputCacheLocation.ServerAndClient, Duration = 3600)] //Cache for 1 Hour.
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new FrontEndViewModel();
                if (User.IsInRole(AccountController.Admin)) model.Role = AccountController.Admin;
                return View(model);
            }
            return View("Welcome", new WelcomePageViewModel { Banners = _systemService.Banners().ToList() });
        }


        [Route("welcome")]
        public ActionResult Welcome()
        {
            return View();
        }

        [Route("terms")]
        public ActionResult Terms()
        {
            return View();
        }

        [Route("contact")]
        [HttpPost]
        public ActionResult Contact(FeedbackViewModel model)
        {
            if (ModelState.IsValid) _siteService.AddFeedback(model);
            return RedirectToAction("welcome");
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}