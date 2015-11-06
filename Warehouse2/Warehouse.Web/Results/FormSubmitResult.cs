using System.Collections.Specialized;
using System.Web.Mvc;

namespace Warehouse.Web.Results
{
    public class FormSubmitResult : ActionResult
    {
        private readonly NameValueCollection _inputs;
        private readonly string _url;
        public FormSubmitResult(NameValueCollection input, string url)
        {
            _inputs = input;
            _url = url;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            const string formName = "form1";
            const string method = "post";

            context.HttpContext.Response.Clear();
            context.HttpContext.Response.Write("<html><head>");

            context.HttpContext.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", formName));
            context.HttpContext.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", formName, method, _url));
            for (int i = 0; i < _inputs.Keys.Count; i++)
            {
                context.HttpContext.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", _inputs.Keys[i], _inputs[_inputs.Keys[i]]));
            }
            context.HttpContext.Response.Write("</form>");
            context.HttpContext.Response.Write("</body></html>");

            context.HttpContext.Response.End();
        }
    }
}