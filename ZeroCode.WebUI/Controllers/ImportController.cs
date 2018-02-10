using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using ClosedXML;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using ZeroCode.Web.MVC;

namespace ZeroCode.WebUI.Controllers
{
    public class ImportController : Controller
    {
        // GET: Import
        public ActionResult Index()
        {
            SchoolClass model = new SchoolClass();
            model.Id = "CLS0001";
            model.Name = "三年二班";
            model.Manager = "黄SIR";
            model.PhoneNumber = "13800138000";
            model.Manager2 = "李SIR";
            model.PhoneNumber2 = "13888138666";
            model.Remark = "这是一段有很多个字的班级说明，只有足够长的字，才能证明这段文字很长，如果100个字还不够长，那么就再来100个字！";
            model.stuList = new List<Student>();
            model.stuList.Add(new Student() { Id = "STU0001", Name = "牛掰掰", Sex = "男", Age = "23", Point = "80", PhoneNumber = "13545678547" });
            model.stuList.Add(new Student() { Id = "STU0002", Name = "张三", Sex = "女", Age = "23", Point = "70", PhoneNumber = "13545654874" });
            model.stuList.Add(new Student() { Id = "STU0003", Name = "李四", Sex = "女", Age = "25", Point = "50", PhoneNumber = "13545633552" });
            model.stuList.Add(new Student() { Id = "STU0004", Name = "王五", Sex = "男", Age = "22", Point = "66", PhoneNumber = "13566885541" });
            model.stuList.Add(new Student() { Id = "STU0005", Name = "林蛋大", Sex = "男", Age = "26", Point = "95", PhoneNumber = "13821298458" });
            model.stuList.Add(new Student() { Id = "STU0006", Name = "刘丽丽", Sex = "女", Age = "19", Point = "95", PhoneNumber = "13821298458" });

            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("班级");
            ws.Cell("A1").Value = model.Name + "班级信息表";
            //标题
            ws.Cell("A2").Value = "班级代号";
            ws.Cell("B2").Value = "班级名称";
            ws.Cell("C2").Value = "班主任";
            ws.Cell("D2").Value = "联系电话";
            ws.Cell("E2").Value = "副班主任";
            ws.Cell("F2").Value = "联系电话";
            //主表内容
            ws.Cell("A3").Value = model.Id;
            ws.Cell("B3").Value = model.Name;
            ws.Cell("C3").Value = model.Manager;
            ws.Cell("D3").Value = model.PhoneNumber;
            ws.Cell("E3").Value = model.Manager2;
            ws.Cell("F3").Value = model.PhoneNumber2;
            ws.Cell("A4").Value = model.Remark;//说明

            //明细表标题
            ws.Cell("A5").Value = "学号";
            ws.Cell("B5").Value = "姓名";
            ws.Cell("C5").Value = "性别";
            ws.Cell("D5").Value = "年龄";
            ws.Cell("E5").Value = "得分";
            ws.Cell("F5").Value = "电话号码";

            for (int i = 0; i < model.stuList.Count(); i++)
            {
                ws.Cell(i + 6, 1).Value = model.stuList[i].Id;
                ws.Cell(i + 6, 2).Value = model.stuList[i].Name;
                ws.Cell(i + 6, 3).Value = model.stuList[i].Sex;
                ws.Cell(i + 6, 4).Value = model.stuList[i].Age;
                ws.Cell(i + 6, 5).Value = model.stuList[i].PhoneNumber;
                ws.Cell(i + 6, 6).Value = model.stuList[i].PhoneNumber;
            }

            IXLRange rngTable = ws.Range("A1:F" + (model.stuList.Count() + 5));

            IXLRange header = rngTable.Range("A1:F1");
            ws.Row(1).Height = 20;
            rngTable.FirstCell().Style.Font.SetBold()
                .Fill.SetBackgroundColor(XLColor.Buff)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            header.FirstRow().Merge();

            header = rngTable.Range("A2:F2");
            header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            header.Style.Font.Bold = true;
            header.Style.Fill.BackgroundColor = XLColor.Aqua;

            var rngRemark = rngTable.Range("A4:F4");
            ws.Row(4).Height = 30;
            rngRemark.Style.Alignment.WrapText = true;
            rngRemark.FirstCell().Comment.Style.Size.SetAutomaticSize();
            rngRemark.FirstRow().Merge();


            rngTable = ws.Range("A5:F" + (model.stuList.Count() + 5));
            var excelTable = rngTable.CreateTable();
            ws.Columns().AdjustToContents();

            string fileName = Guid.NewGuid().ToString()+".xlsx";

            using (FileStream fsWrite = new FileStream(Server.MapPath(string.Format("~/Upload/{0}.xlsx", fileName)), FileMode.Create, FileAccess.ReadWrite))
            {
                wb.SaveAs(fsWrite);

            }
            byte[] fileContents;
            using (FileStream fsRead = new FileStream(Server.MapPath(string.Format("~/Upload/{0}.xlsx", fileName)), FileMode.Open))
            {
                int length = (int)fsRead.Length;
                 fileContents = new byte[length];
                fsRead.Read(fileContents, 0, length);
            }

            //MemoryStream memoryStream = new MemoryStream();
            //wb.SaveAs(memoryStream);

            return new FileExcelResult(fileContents, "哇哈哈.xlsx");

        }
    }

    public class SchoolClass
    {

        public string Id { get; set; }//班级标示
        public string Name { get; set; }//班级名称
        public string Manager { get; set; }//班主任姓名
        public string Manager2 { get; set; }//副班主任姓名
        public string PhoneNumber { get; set; }//班主任联系电话
        public string PhoneNumber2 { get; set; }//副主任联系电话
        public string Remark { get; set; } //班级说明
        public List<Student> stuList { get; set; }//一个班级对应多个学生
    }

    public class Student
    {
        public string Id { get; set; }//学号
        public string Name { get; set; }//姓名
        public string Sex { get; set; }//性别
        public string Age { get; set; }//年龄
        public string Point { get; set; }//年度得分
        public string PhoneNumber { get; set; }//电话
    }

}