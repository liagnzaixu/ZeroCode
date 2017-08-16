using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
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

            //保存形成保存路径
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string newFileName = Convert.ToInt64(ts.TotalMilliseconds).ToString(CultureInfo.InvariantCulture) + new Random().Next(100, 999);
            string path = Server.MapPath("~/Upload/") + newFileName + ".jpeg";

            //压缩图片、转换格式（jpeg）
            Image ResourceImage = Image.FromStream(file.InputStream);
            if(ResourceImage.Width>300)
            {
                int dWidth = 300;
                int dHeight = 300 * ResourceImage.Height / ResourceImage.Width;
                CompressPicture(file.InputStream, path, dWidth, dHeight,100);
            }
            else
            {
                CompressPicture(file.InputStream, path, ResourceImage.Width, ResourceImage.Height, 100);
            }
            file.InputStream.Close();

            string url = Request.ApplicationPath + "Upload/" + newFileName + ".jpeg";
            return Json(new { Status = "success", Msg = "上传成功",ImgUrl= url });
        }


        #region 压缩图像
        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="inputStream">原图片</param>
        /// <param name="dFile">压缩后保存位置</param>
        /// <param name="dWidth">宽度</param>
        /// <param name="dHeight">高度</param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <returns></returns>
        public bool CompressPicture(Stream inputStream, string dFile,  int dWidth, int dHeight, int flag)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromStream(inputStream);
            ImageFormat tFormat = iSource.RawFormat;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dWidth || tem_size.Height > dHeight)
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

        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片</param>
        /// <param name="dFile">压缩后保存位置</param>
        /// <param name="dWidth">宽度</param>
        /// <param name="dHeight">高度</param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <returns></returns>
        public bool CompressPicture(string sFile, string dFile, int dWidth, int dHeight, int flag)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dWidth || tem_size.Height > dHeight)
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


        /// <summary>
        /// 按指定规格裁剪图片
        /// </summary>
        /// <param name="sFile">源文件路径</param>
        /// <param name="dFile">输出文件路径</param>
        /// <param name="originPoint">裁剪区域的起点</param>
        /// <param name="sSize">裁剪区域的大小</param>
        /// <param name="dSize">生成的规格</param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <returns></returns>
        public bool CropPicture(string sFile, string dFile, Point originPoint , Size sSize, Size dSize, int flag)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            Bitmap ob = new Bitmap(dSize.Width, dSize.Height);
            Graphics g = Graphics.FromImage(ob);
            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(iSource, new Rectangle(0, 0, dSize.Width, dSize.Height), originPoint.X, originPoint.Y, sSize.Width, sSize.Height, GraphicsUnit.Pixel);
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

                for (int i = 0; i < arrayICI.Length; i++)
                {
                    if (arrayICI[i].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[i];
                        break;
                    }
                }
                if(string.IsNullOrEmpty(dFile))
                {
                    string[] temp = sFile.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                    string fileName = temp[temp.Length - 1];
                    fileName = fileName.Insert(fileName.IndexOf('.'), '_'+ dSize.Width.ToString() + '_' + dSize.Height.ToString());
                    temp[temp.Length - 1] = fileName;
                    dFile = string.Empty;
                    foreach(string item in temp)
                    {
                        dFile += item + "\\";
                    }
                    dFile=dFile.TrimEnd('\\');
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);
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

        #region 判断图片格式 
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
        #endregion


        [HttpPost]
        public JsonResult CropPhoto(string url,int x,int y,int w,int h)
        {
            if (string.IsNullOrEmpty(url) || w == 0 || h == 0)
            {
                return Json(new { Status = "error", Msg = "参数错误" });
            }

            Point origin = new Point(x, y);
            Size source = new Size(w, h);
            Size destSmall = new Size(48, 48);
            Size destLarge = new Size(180, 180);

            bool result1 =CropPicture(Server.MapPath(url), null, origin, source, destSmall, 100);
            bool result2 =CropPicture(Server.MapPath(url), null, origin, source, destLarge, 100);


            var jsonResult = result1 && result2
                ? new { Status = "success", Msg = "操作成功" }
                : new { Status = "error", Msg = "裁剪图片出现错误" };

            return Json(jsonResult);
        }
    }
}