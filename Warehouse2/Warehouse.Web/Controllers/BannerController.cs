using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using e10.Data.Services;
using e10.Shared.Models;

namespace Warehouse.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/v1/banner")]
    public class BannerController : BasicApiController
    {
        private readonly ISystemService _service;

        public BannerController(ISystemService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("paged")]
        public PageResult<BannerEditViewModel> GetPaged(ODataQueryOptions<BannerEditViewModel> options)
        {
            return Page(_service.BannerList, options);
        }

        [HttpGet]
        [Route("all")]
        [EnableQuery]
        [AllowAnonymous]
        public IQueryable<BannerEditViewModel> GetAll()
        {
            return _service.BannerList;
        }

        [HttpPost]
        [Route]
        public HttpResponseMessage Create(BannerCreateViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route]
        public HttpResponseMessage Edit(BannerEditViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            return ModelState.IsValid ? Ok(_service.Delete(new BannerDeleteViewModel { Id = id })) : Bad(ModelState);
        }
    }
}