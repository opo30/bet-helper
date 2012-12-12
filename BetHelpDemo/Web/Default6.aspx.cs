using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;
using System.Data;
using SeoWebSite.DBUtility;
using System.Text;

public partial class Default6 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds0 = DbHelperSQL.Query("select id from schedule where updated=1 and id > (select MAX(scheduleid) from history) order by id");
            foreach (DataRow item in ds0.Tables[0].Rows)
            {
                DataSet ds = DbHelperSQL.Query("select * from odds where e_win is not null and e_draw is not null and e_lost is not null and e_winper is not null and e_drawper is not null and e_lostper is not null and scheduleid=" + item["id"]);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select count(*) c,");
                    strSql.Append("w=100.0*sum(case when a.home>a.away then 1 else 0 end)/count(a.id)-" + dr["e_winper"] + ",");
                    strSql.Append("d=100.0*sum(case when a.home=a.away then 1 else 0 end)/count(a.id)-" + dr["e_drawper"] + ",");
                    strSql.Append("l=100.0*sum(case when a.home<a.away then 1 else 0 end)/count(a.id)-" + dr["e_lostper"] + " ");
                    strSql.Append("from Odds b join schedule a on b.scheduleid=a.id join scheduleclass c on a.sclassid=c.id");
                    strSql.Append(" where a.updated=1 and b.companyid=" + dr["companyid"] + " and b.e_win=" + dr["e_win"] + " and b.e_draw=" + dr["e_draw"] + " and b.e_lost=" + dr["e_lost"]);
                    DataSet ds1 = DbHelperSQL.Query(strSql.ToString(), 999);
                    if (ds1 != null && Convert.ToInt32(ds1.Tables[0].Rows[0]["c"]) > 100)
                    {
                        DataRow dr1 = ds1.Tables[0].Rows[0];
                        DbHelperSQL.ExecuteSql("insert into history values (" + dr["scheduleid"] + "," + dr["companyid"] + "," + dr1["w"] + "," + dr1["d"] + "," + dr1["l"] + "," + dr1["c"] + ")");
                    }
                }
            }
        }
    }
}