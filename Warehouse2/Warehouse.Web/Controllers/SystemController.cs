using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using e10.Data.Services;
using e10.Shared.Models;

namespace Warehouse.Web.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/v1/system")]
    public class SystemController : BasicApiController
    {
        private readonly ISystemService _service;

        public SystemController(ISystemService service)
        {
            _service = service;
        }

        [HttpGet, Route("enums")]
        public EnumList GetAllEnums()
        {
            return _service.Enums("e10.Data.Core", "e10.Data");
        }

        [HttpGet]
        [Route("country/paged")]
        public PageResult<CountryDictionaryViewModel> Viewcountrys(ODataQueryOptions<CountryDictionaryViewModel> options)
        {
            return Page(_service.Countries, options);
        }

        [HttpGet]
        [Route("country/all")]
        [EnableQuery]
        public IQueryable<CountryDictionaryViewModel> ViewcountrysQuery()
        {
            return _service.Countries;
        }

        [HttpPost]
        [Route("country/create")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage Addcountry(CountryDictionaryCreateViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Create(model)) : Bad(ModelState);
        }

        [HttpPut]
        [Route("country/update")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage Editcountry(CountryDictionaryEditViewModel model)
        {
            return ModelState.IsValid ? Ok(_service.Update(model)) : Bad(ModelState);
        }

        [HttpDelete]
        [Route("country/{id}")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage Deletecountry([FromUri] int id)
        {
            return ModelState.IsValid ? Ok(_service.Delete(new CountryDeleteViewModel { Id = id })) : Bad(ModelState);
        }
    }
}



