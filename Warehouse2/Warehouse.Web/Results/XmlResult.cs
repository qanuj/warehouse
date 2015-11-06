using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace Warehouse.Web.Results
{
    public class XmlResult : ActionResult
    {
        public static string Serialize<T>(T value)
        {
            if (value == null) { return null; }
            MemoryStream ms = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(ms, new UTF8Encoding());

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            writer.Formatting = Formatting.Indented;
            writer.IndentChar = ' ';
            writer.Indentation = 3;
            serializer.Serialize(writer, value);
            byte[] Result = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(Result, 0, (int)ms.Length);
            return Encoding.UTF8.GetString(Result, 0, (int)ms.Length);
        }
        public static T Deserialize<T>(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return default(T);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            XmlReaderSettings settings = new XmlReaderSettings();
            // No settings need modifying here

            using (StringReader textReader = new StringReader(xml))
            {
                using (XmlReader xmlReader = XmlReader.Create(textReader, settings))
                {
                    return (T)serializer.Deserialize(xmlReader);
                }
            }
        }
        public XmlResult()
        {
            ContentEncoding = Encoding.UTF8;
        }
        public object Content { get; set; }
        public Encoding ContentEncoding { get; set; }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null) { throw new ArgumentNullException("context"); }
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "text/xml";
            if (ContentEncoding != null) { response.ContentEncoding = ContentEncoding; }
            if (Content != null)
            {
                response.Write(Serialize(Content));
            }
        }
    }
}