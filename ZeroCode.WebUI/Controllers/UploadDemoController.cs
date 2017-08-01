using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace ZeroCode.WebUI.Controllers
{
    public class UploadDemoController : Controller
    {

        // GET: UploadDemo
        public ActionResult Index()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string newFileName = Convert.ToInt64(ts.TotalMilliseconds).ToString(CultureInfo.InvariantCulture) + new Random().Next(100, 999);
            GetPicThumbnail(Server.MapPath("~/Upload/jk.jpg"), Server.MapPath("~/Upload/") + newFileName,480,320,100);
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile_1()
        {
            string result = "上传成功";
            HttpPostedFileBase file= Request.Files["UserPhoto"];
            file.SaveAs(Server.MapPath("~/Upload/")+ file.FileName);
            return Content(result);
        }

        [HttpPost]
        public ActionResult UploadFile_2()
        {
            HttpPostedFileBase file = Request.Files["UserPhoto"];
            file.SaveAs(Server.MapPath("~/Upload/") + file.FileName);
            return Json("success");
        }

        [HttpPost]
        public ActionResult UploadPhoto()
        {
            HttpPostedFileBase file = Request.Files["UserPhoto"];
            string contentType = file.ContentType;
            string extension = Path.GetExtension(file.FileName);

            //检查图片格式
            if (!IsAllowImg(contentType, extension))
            {
                return Json(new { Status = "error", Msg = "上传文件格式不符合要求。" });
            }

            //保存图片
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string newFileName = Convert.ToInt64(ts.TotalMilliseconds).ToString(CultureInfo.InvariantCulture) + new Random().Next(100, 999);
            string path = Server.MapPath("~/Upload/") + newFileName + extension;
            //file.SaveAs(path);

            //压缩图片
            Image ResourceImage = Image.FromStream(file.InputStream);
            if(ResourceImage.Width>300)
            {

            }

            string url = Request.ApplicationPath + "Upload/" + newFileName + extension;
            return Json(new { Status = "success", Msg = "上传成功",ImgUrl= url });
        }

        //将图片按百分比压缩，flag取值1到100，越小压缩比越大

        public static bool YaSuo(Image iSource, string outPath, int flag)
        {
            ImageFormat tFormat = iSource.RawFormat;
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageDecoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                    iSource.Save(outPath, jpegICIinfo, ep);
                else
                    iSource.Save(outPath, tFormat);
                return true;
            }
            catch
            {
                return false;
            }
            iSource.Dispose();
        }

        #region GetPicThumbnail
        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片</param>
        /// <param name="dFile">压缩后保存位置</param>
        /// <param name="dHeight">高度</param>
        /// <param name="dWidth">宽度</param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <returns></returns>

        public bool GetPicThumbnail(string sFile, string dFile, int dHeight, int dWidth, int flag)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth) //将**改成c#中的或者操作符号
            {
                if ((tem_size.Width * dHeight) > (tem_size.Height * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);
            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();

                ImageCodecInfo jpegICIinfo = null;

                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();

            }
        }
        #endregion

        public bool IsAllowImg(string contentType, string fileExtension)
        {
            contentType = contentType.ToLower();
            if(!contentType.Contains("image"))
            {
                return false;
            }

            fileExtension = fileExtension.ToLower();
            string[] allowExtension = { ".bmp", ".gif", ".jpeg", ".jpg", ".png" };
            foreach (string item in allowExtension)
            {
                if (fileExtension == item)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsAllowedExtension(HttpPostedFileBase file)
        {
            System.IO.Stream stream = file.InputStream;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(stream);
            string fileclass = "";
            //这里的位长要具体判断. 
            byte buffer;
            try
            {
                //buffer = r.ReadByte();
                //fileclass = buffer.ToString();
                //buffer = r.ReadByte();
                //fileclass += buffer.ToString();

                for (int i = 0; i < 2; i++)
                {
                    fileclass += reader.ReadByte().ToString();
                }

            }
            catch
            {

            }
            reader.Close();
            stream.Close();
            if (fileclass == "255216" || fileclass == "7173")//说明255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar 
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 生成缩略图重载方法1，返回缩略图的Image对象
        /// </summary>
        /// <param name="Width">缩略图的宽度</param>
        /// <param name="Height">缩略图的高度</param>
        /// <returns>缩略图的Image对象</returns>
        public Image GetReducedImage(Image resource, int width,int height)
        {
            try
            {
                //用指定的大小和格式初始化Bitmap类的新实例
                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                //从指定的Image对象创建新Graphics对象
                Graphics graphics = Graphics.FromImage(bitmap);
                //清除整个绘图面并以透明背景色填充
                graphics.Clear(Color.Transparent);
                //在指定位置并且按指定大小绘制原图片对象
                graphics.DrawImage(resource, new Rectangle(0, 0, width, height));
                return bitmap;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public JsonResult CropPhoto(string uri,int x,int y,int width,int height)
        {
            HttpPostedFileBase file = Request.Files["UserPhoto"];
            string extension = Path.GetExtension(file.FileName);
            file.SaveAs(Server.MapPath("~/Upload/") + file.FileName);
            return Json("success");
        }
    }
}