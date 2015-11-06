using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web.Hosting;
using System.Web.Mvc;
using e10.Shared.Data.Abstraction;
using e10.Shared.Models;
using Microsoft.AspNet.Identity;

namespace Warehouse.Web.Controllers
{
    [Authorize]
    public class UploadController : CoreController
    {
        private readonly IFileAccessProvider _fileAccessProvider;
        private readonly string _storageRoot;

        public UploadController(IFileAccessProvider fileAccessProvider)
        {
            _fileAccessProvider = fileAccessProvider;
            var folder = "~/App_Data/Uploads";
            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == "StorageRoot"))
            {
                var tmp = ConfigurationManager.AppSettings["StorageRoot"];
                if (!string.IsNullOrWhiteSpace(tmp)) folder = tmp;
            }
            _storageRoot = HostingEnvironment.MapPath(folder);
            if (!string.IsNullOrWhiteSpace(_storageRoot) && !Directory.Exists(_storageRoot))
                Directory.CreateDirectory(_storageRoot);
        }

        [HttpPost]
        [Route("~/upload")]
        public ActionResult UploadFile()
        {
            var xFileName = Request.Headers["X-File-Name"];
            return Json2(string.IsNullOrEmpty(xFileName) ? UploadWholeFile() : UploadPartialFile(xFileName));
        }

        [HttpPost]
        [Route("~/upload/picture")]
        public ActionResult UploadPicture()
        {
            var xFileName = Request.Headers["X-File-Name"];
            return Json2(string.IsNullOrEmpty(xFileName) ? UploadWholeFile(true) : UploadPartialFile(xFileName, true));
        }

        [Route("~/files/{filename}")]
        public ActionResult Download(string filename)
        {
            var applicant = _fileAccessProvider.ByUrl(User.Identity.GetUserId(), "/files/" + filename);
            if (applicant == null) return HttpNotFound();
            var f = new FileInfo(GetFullFileName(filename));
            return File(f.FullName, MediaTypeNames.Application.Octet, string.Format("{0}{1}", applicant.Name, f.Extension));
        }

        [Route("~/picture/{filename}")]
        [AllowAnonymous]
        public ActionResult ViewPicture(string filename)
        {
            var f = new FileInfo(GetFullFileName(filename));
            return File(f.FullName, MediaTypeNames.Image.Jpeg);
        }

        private static bool IsAllowed(string ext, bool picture = false)
        {
            if (picture) return new[] { ".jpg", ".jpeg", ".png", ".gif" }.Any(x => x == ext.ToLower());
            return new[] { ".doc", ".docx", ".pdf", ".txt", ".rtf" }.Any(x => x == ext.ToLower());
        }
        private static ICollection<FilesStatus> MakeFileStatusError(string name, bool isPicture, string error)
        {
            return new List<FilesStatus> { new FilesStatus(name, "", isPicture) { Error = error } };
        }
        private ICollection<FilesStatus> UploadPartialFile(string fileName, bool isPicture = false)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return MakeFileStatusError(fileName, isPicture, "No File Name Received");
            if (Request.Files == null || Request.Files.Count != 1)
                return MakeFileStatusError(fileName, isPicture,
                    "Attempt to upload chunked file containing more than one fragment per request");
            if (Request.Files[0] == null) return MakeFileStatusError(fileName, isPicture, "No Files Received");

            var inputStream = Request.Files[0].InputStream;
            var status = new FilesStatus(new FileInfo(fileName));
            var ext = Path.GetExtension(fileName);
            if (!IsAllowed(ext, isPicture))
                return MakeFileStatusError(fileName, isPicture, string.Format("{0} not allowed", ext));
            var fullName = GetFullFileName(status.Name);
            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
            {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0)
                {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }
                fs.Flush();
                fs.Close();
            }
            status.Size = (int)new FileInfo(fullName).Length;
            return new List<FilesStatus> { status };
        }
        private string GetFullFileName(string fileName)
        {
            return Path.Combine(_storageRoot, Path.GetFileName(fileName));
        }
        private ICollection<FilesStatus> UploadWholeFile(bool isPicture = false)
        {
            var statuses = new List<FilesStatus>();
            for (var i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file == null) continue;
                if (file.FileName == null) continue;
                var status = new FilesStatus(new FileInfo(file.FileName), isPicture);
                var ext = Path.GetExtension(file.FileName);
                if (!IsAllowed(ext, isPicture))
                    return MakeFileStatusError(file.FileName, isPicture, string.Format("{0} not allowed", ext));
                file.SaveAs(GetFullFileName(status.Name));
                statuses.Add(status);
            }
            return statuses;
        }
    }
}