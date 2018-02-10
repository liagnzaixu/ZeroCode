using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ZeroCode.Web.MVC.UI
{
    //excel 2007及以上
    public class FileExcelResult: ActionResult
    {
        private string _fileDownloadName;
        private MemoryStream _fileContents_stream;
        private string _contentType;

        public string FileDownloadName
        {
            get { return _fileDownloadName ?? string.Empty; }

            set { _fileDownloadName = value; }
        }

        public string ContentType
        {
            get { return _contentType ?? "application/vnd.ms-excel"; }

            set { _contentType = value; }
        }

        public FileExcelResult(byte[] fileContents, string fileDownloadName)
        {
            _fileContents_stream = new MemoryStream();
            _fileContents_stream.Write(fileContents, 0, fileContents.Length);
            this._fileDownloadName = fileDownloadName;
        }

        public FileExcelResult(MemoryStream stream, string fileDownloadName)
        {
            this._fileContents_stream = stream;
            this._fileDownloadName = fileDownloadName;
        }

        public FileExcelResult(byte[] fileContents, string fileDownloadName,string contentType):this(fileContents, fileDownloadName)
        {
            this._fileDownloadName = contentType;

        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string userAgent = string.Empty;
            if (context.HttpContext.Request.UserAgent != null)
            {
                userAgent = context.HttpContext.Request.UserAgent.ToLower();
            }

            HttpResponseBase response = context.HttpContext.Response;
            
            WriteFile(context.HttpContext.Response, userAgent);
        }

        protected  void WriteFile(HttpResponseBase response,string userAgent)
        {
            response.Clear();
            response.ContentType = ContentType;
            response.ContentEncoding = Encoding.UTF8;
            if (string.IsNullOrWhiteSpace(_fileDownloadName))
            {
                _fileDownloadName = Convert.ToString(Guid.NewGuid());
            }
            else
            {
                _fileDownloadName = userAgent.Contains("ie") || userAgent.Contains("trident/7.0; rv:11.0") ? HttpUtility.UrlEncode(_fileDownloadName, Encoding.UTF8) : _fileDownloadName;
            }
            response.AddHeader("Content-Disposition", $"attachment;filename={_fileDownloadName}");
            _fileContents_stream.WriteTo(response.OutputStream);
        }
    }
}
