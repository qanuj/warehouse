using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Warehouse.Web.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Warehouse.Web.Controllers
{
    public class CoreController : Controller
    {
        protected ActionResult Json2(object obj)
        {
            return Content(JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }), "application/json");
        }

        protected string WebBaseUrl => Request?.Url != null ? Request.Url.Scheme + "://" + Request.Url.Authority + "/" : string.Empty;

        protected ActionResult Xml(object value, Encoding encoding = null)
        {
            return new XmlResult { Content = value, ContentEncoding = encoding ?? Encoding.UTF8 };
        }

        protected CsvResult Csv(IEnumerable<string> values, string name = "file.csv", Encoding encoding = null)
        {
            return new CsvResult { Content = values, Name = name, ContentEncoding = encoding ?? Encoding.UTF8 };
        }
    }
}