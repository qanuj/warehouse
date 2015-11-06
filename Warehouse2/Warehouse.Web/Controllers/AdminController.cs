using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using e10.Data.Core;
using e10.Data.Services;
using e10.Shared.Security;
using e10.Shared.Models;

namespace Warehouse.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/v1/admin")]
    public class AdminController : BasicApiController
    {
        private readonly ISystemService _service;
        private readonly ApplicationUserManager _userManager;

        public AdminController(ISystemService service, ApplicationUserManager userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("transaction")]
        public PageResult<TransactionViewModel> GetTransactions(ODataQueryOptions<TransactionViewModel> options)
        {
            return Page(_service.AllTransactions(), options);
        }

        [HttpGet]
        [Route("feedback")]
        public PageResult<FeedbackViewModel> GetFeedbacks(ODataQueryOptions<FeedbackViewModel> options)
        {
            return Page(_service.Feedbacks(), options);
        }

        [HttpPut]
        [Route("feedback/{id}/{read}")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage ReadFeedback([FromUri] int id, [FromUri] bool read)
        {
            return !ModelState.IsValid ? Bad(ModelState) : Ok(_service.ReadFeedback(id, read));
        }

        [HttpDelete]
        [Route("feedback/{id}")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage DeleteFeedback([FromUri] int id)
        {
            return !ModelState.IsValid ? Bad(ModelState) : Ok(_service.DeleteFeedback(id));
        }

        [HttpGet]
        [Route("transaction/{id}")]
        public InvoiceViewModel GetTransactionById([FromUri]int id)
        {
            return _service.TransactionById(id);
        }


        [HttpPost]
        [Route("gift")]
        [ResponseType(typeof(bool))]
        public HttpResponseMessage GiftCredits(GiftViewModel model)
        {
            return !ModelState.IsValid ? Bad(ModelState) : Ok(_service.SendGift(model));
        }

        [HttpPut]
        [Route("config")]
        [ResponseType(typeof(AppSiteConfig))]
        public HttpResponseMessage UpdateConfig(AppSiteConfig model)
        {
            return !ModelState.IsValid ? Bad(ModelState) : Ok(_service.UpdateConfig(model));
        }

        [HttpGet]
        [Route("config")]
        public AppSiteConfig GetConfig()
        {
            return _service.GetOrCreateConfig();
        }

        [HttpGet]
        [Route("profile")]
        public ProfileViewModel GetProfile()
        {
            var user = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (user == null) return null;
            return new ProfileViewModel
            {
                Email = user.Email,
                FullName = user.FullName,
                Hash = _service.Hash(user.Email)
            };
        }
    }
}