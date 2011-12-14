using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeoWebSite.DAL;
using SeoWebSite.Common.JSON;
using System.Data;

namespace SeoWebSite.BLL
{
    public class TreeBLL
    {
        private DataSet ds;
        private readonly BetRecordDAO recordDal = new BetRecordDAO();
        private readonly BettingLineDAO lineDal = new BettingLineDAO();

        /// <summary>
        /// 查询追杀投注树
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public string GetBetRecordTree(string parentID)
        {
            string DTreeJSON = "";
            TreeJSONHelper json = new TreeJSONHelper();
            try
            {
                if (parentID == "0")//1是公共通讯录的根
                {
                    ds = lineDal.GetList("1=1");
                    json.success = true;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        json.AddItem("id", dr["id"].ToString());
                        json.AddItem("parentid", "1");
                        json.AddItem("text", dr["name"].ToString());
                        json.AddItem("leaf", "0");
                        json.AddItem("iconCls", "");
                        json.AddItem("qtip", "1111111111");
                        json.AddItem("number", "1");
                        json.AddItem("url", "");
                        //json.AddItem("type", "deptclass");
                        //json.AddItem("classid", dr["code"].ToString());
                        //json.AddItem("classname", dr["name"].ToString());
                        json.ItemOk();
                    }
                }
                else
                {
                    ds = recordDal.GetList(parentID);
                    json.success = true;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        json.AddItem("id", dr["code"].ToString());
                        json.AddItem("parentid", dr["pcode"].ToString());
                        json.AddItem("text", dr["name"].ToString());
                        json.AddItem("leaf", "1");
                        json.AddItem("iconCls", "guesticon");
                        json.AddItem("number", dr["seq"].ToString());
                        json.AddItem("url", "");
                        json.AddItem("type", "dept");
                        json.AddItem("classid", dr["gcode"].ToString());
                        json.AddItem("classname", dr["deptclassname"].ToString());
                        json.ItemOk();
                    }
                }
                DTreeJSON = json.ToString();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                throw;
            }
            return DTreeJSON;
        }
    }
}
