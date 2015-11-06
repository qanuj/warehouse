using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using e10.Data.Services;
using e10.Shared.Data.Abstraction;
using e10.Shared.Security;
using e10.Shared.Services.Abstraction;
using e10.Shared.Util;
using Warehouse.Web.Models;
using Microsoft.AspNet.Identity;

namespace Warehouse.Web.Controllers
{
    public class SetupController : Controller
    {
        private readonly ISystemService _service;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;
        private readonly INotificationService _notificationService;

        private readonly string[] _systemRoles = { AccountController.Admin };

        private const string SuperAdminEmail = "a@e10.in";
        private const string SuperAdminFullName = "Anuj Pandey";
        public SetupController(ISystemService service,
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager, INotificationService notificationService)
        {
            _service = service;
            _userManager = userManager;
            _roleManager = roleManager;
            _notificationService = notificationService;
        }

        public ActionResult Upgrade()
        {
            return Json(_service.Upgrade(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Master()
        {
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Verify()
        {
            if (!_roleManager.RoleExists(AccountController.Admin)) { await _roleManager.CreateRolesAsync(new[] { AccountController.Admin }); }

            var adminUsers = _userManager.FindByRole(AccountController.Admin);
            if (adminUsers.Any()) return Json("Success", JsonRequestBehavior.AllowGet);

            var adminUser = await _userManager.FindByEmailAsync(SuperAdminEmail);
            if (adminUser == null)
            {
                adminUser = new User { FullName = SuperAdminFullName, UserName = SuperAdminEmail, Email = SuperAdminEmail };
                var pwd = Randomizer.Generate(10);
                var result = await _userManager.CreateAsync(adminUser, pwd);
                if (!result.Succeeded) { return Json(result, JsonRequestBehavior.AllowGet); }
            }

            if (!await _userManager.IsInRoleAsync(adminUser.Id, AccountController.Admin))
            {
                await _userManager.AddToRoleAsync(adminUser.Id, AccountController.Admin);
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(adminUser.Id);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = adminUser.Id, code = code }, protocol: Request.Url.Scheme);
            _notificationService.PasswordRecovery(adminUser.Email, callbackUrl);

            return Json("Success", JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> Roles()
        {
            await _roleManager.CreateRolesAsync(new[]
            {
                AccountController.Admin,
                AccountController.Member
            });

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Index()
        {
            if (await _userManager.Users.AnyAsync())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(SetupViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateRolesAsync(_systemRoles);
                if (!User.Identity.IsAuthenticated)
                {
                    await _userManager.CreateGodAsync(model.Email, model.Password);
                }
                else
                {
                    model.Email = User.Identity.Name;
                }
            }
            return View(model);
        }

        public ActionResult Version()
        {
            return Json("v6.12.13.1", JsonRequestBehavior.AllowGet);
        }
    }
}
