using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using SeoWebSite.BLL;

namespace SeoWebSite.Web.Data.NowGoal
{
    public partial class GetChartsXmlData : System.Web.UI.Page
    {
        HistoryOddsBLL bll = new HistoryOddsBLL();
        protected string StringJSON = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.Form["action"];
            switch (action)
            {
                case "odds":
                    this.CreatOddsChart();
                    break;
                case "kelly":
                    this.CreatKellyChart();
                    break;
                case "avekelly":
                    this.CreatAverageKellyChart();
                    break;
                case "kellycolumn":
                    this.CreateKellyColumnChart();
                    break;
                case "company":
                    this.CreateCompanyChart();
                    break;
                case "point":
                    this.CreatePointChart();
                    break;
                default:
                    break;
            }
        }

        private void CreatePointChart()
        {
            string scheduleID = Request.Form["scheduleid"];
            string[] companyids = Request.Form["companyids"].Split(',');
            StringJSON = bll.GetCompanysSupportChartsData(scheduleID,companyids);
        }

        private void CreateCompanyChart()
        {
            string scheduleID = Request.Form["scheduleid"];
            string[] companyids = Request.Form["companyids"].Split(',');
            double time = double.Parse(Request.Form["time"]);

            StringJSON = bll.GetOddsCompanyChartsData(scheduleID, companyids, time * 3600);
        }

        private void CreateKellyColumnChart()
        {
            string scheduleID = Request.Form["scheduleid"];
            string[] companyids = Request.Form["companyids"].Split(',');

            StringJSON = bll.GetAveKellyChartsData(scheduleID, companyids);
        }

        private void CreatOddsChart()
        {
            string scheduleID = Request.Form["scheduleid"];
            string[] companyids = Request.Form["companyids"].Split(',');
            double time = double.Parse(Request.Form["time"]);

            StringJSON = bll.GetOdds1x2ChartsData(scheduleID, companyids, time * 3600);
        }

        /// <summary>
        /// 凯利
        /// </summary>
        private void CreatKellyChart()
        {
            string scheduleID = Request.Form["scheduleid"];
            string[] companyids = Request.Form["companyids"].Split(',');

            StringJSON = bll.GetOddsKellyChartsData(scheduleID, companyids);
        }

        /// <summary>
        /// 平均凯利
        /// </summary>
        private void CreatAverageKellyChart()
        {
            string scheduleID = Request.Form["scheduleid"];
            double time = double.Parse(Request.Form["time"]);
            bool isRefresh = (Request.Form["isrefresh"] == "1");
            StringJSON = bll.GetKellyAverageLineChart(scheduleID, null, time, isRefresh);
        }
    }
}
