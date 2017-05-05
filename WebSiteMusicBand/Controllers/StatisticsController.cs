using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebSiteMusicBand.Model;

namespace WebSiteMusicBand.Controllers
{
    public class StatisticsController : Controller
    {

        private MusicBandSiteDB db = new MusicBandSiteDB();

        // GET: Statistics
        public ActionResult Index()
        {
            var users = db.CustomUsers.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.ForumName });
            var model = new StatViewM
            {
                Users = new SelectList(users, "Value", "Text")
            };
            return View(model);
        }

        // GET: Statistics
        public ActionResult StatForAll(DateTime? from, DateTime? to)
        {
            DrawChart(from,to);

            return View();
        }



        // GET: Statistics
        public ActionResult StatForSelectUser(int SelectedUserId)
        {
            DrawChart(null, null, SelectedUserId);

            return View();
        }


        public ActionResult StatTable()
        {
            List<TableViewM> model = new List<TableViewM>();
            foreach (CustomUsers item in db.CustomUsers.OrderBy(x=>x.News.Count))
            {
                model.Add(new TableViewM { Name = item.ForumName, NumOfComments = item.News.Count, NumOfPosts = item.NewsComments.Count });
            }
            return View(model);
        }



        [HttpPost]
        public ActionResult ExportData()
        {
            List<TableViewM> model = new List<TableViewM>();
            foreach (CustomUsers item in db.CustomUsers.OrderBy(x => x.News.Count))
            {
                model.Add(new TableViewM { Name = item.ForumName, NumOfComments = item.News.Count, NumOfPosts = item.NewsComments.Count });
            }
            DataTable dt = ConvertToDataTable<TableViewM>(model);
            using (XLWorkbook wb = new XLWorkbook())
            {

                wb.Worksheets.Add(dt, "Stat Sheet");
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= EmployeeReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("StatTable");
        }


        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        private void DrawChart(DateTime? from , DateTime? to,int? userId=null)
        {
            IQueryable<News> newsPosts ;
            IQueryable<NewsComments> newsComments;
            if (from == null )
            {
                from = db.News.FirstOrDefault().CreateDate;
            }
            if(to == null)
            {
                to = DateTime.Now;
            }

            newsPosts = db.News.Where(x => x.CreateDate > from && x.CreateDate < to);
            newsComments = db.NewsComments.Where(x => x.CreateDate > from && x.CreateDate < to);

            if(userId != null)
            {
                newsPosts = newsPosts.Where(x => x.UserId == userId);
                newsComments = newsComments.Where(x => x.UserId == userId);
            }
           
            Dictionary<DateTime, int> newsDic = new Dictionary<DateTime, int>();
            foreach (News item in newsPosts)
            {
                if (newsDic.ContainsKey(item.CreateDate.Date))
                {
                    newsDic[item.CreateDate.Date] += 1;
                }
                else
                {
                    newsDic.Add(item.CreateDate.Date, 1);
                }
            }
           
            Dictionary<DateTime, int> commDic = new Dictionary<DateTime, int>();
            foreach (NewsComments item in newsComments)
            {
                if (commDic.ContainsKey(item.CreateDate.Date))
                {
                    commDic[item.CreateDate.Date] += 1;
                }
                else
                {
                    commDic.Add(item.CreateDate.Date, 1);
                }
            }

            if (commDic.Count == 0)
            {
                commDic.Add(DateTime.Today, 0);
            }
            else if (newsDic.Count == 0)
            {
                newsDic.Add(DateTime.Today, 0);
            }
            else
            {
                if (commDic.Keys.Max() > newsDic.Keys.Max())
                {
                    newsDic.Add(commDic.Keys.Max(), 0);
                }
                else
                {
                    commDic.Add(newsDic.Keys.Max(), 0);
                }
                if (commDic.Keys.Min() < newsDic.Keys.Min())
                {
                    newsDic.Add(commDic.Keys.Min(), 0);
                }
                else
                {
                    commDic.Add(newsDic.Keys.Min(), 0);
                }
            }




            commDic = commDic.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            newsDic = newsDic.OrderBy(x => x.Key).ToDictionary(x=>x.Key,x=>x.Value);
            var myChart = new Chart(width: 1280, height: 720)
                .AddTitle("Chart Title")
                .AddSeries(chartType: "line",
                    name: "Posts",
                    yValues: newsDic.Values,
                    xValue: newsDic.Keys,
                    xField: "Date",
                    yFields: "Num")
            .AddSeries(chartType: "line",
                    name: "Comments",
                    yValues: commDic.Values,
                    xValue: commDic.Keys,
                    xField: "Date",
                    yFields: "Num")
            .AddLegend("Posts", "Posts")
            .AddLegend("Comments", "Comments")
            .Write();
            //myChart.Save("~/Content/chart", "jpeg");
            //// Return the contents of the Stream to the client
            //return base.File("~/Content/chart", "jpeg");
        }
    }
}