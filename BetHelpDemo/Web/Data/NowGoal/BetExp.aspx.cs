using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeoWebSite.BLL;
using Newtonsoft.Json.Linq;
using System.Data;
using SeoWebSite.Model;
using System.Text.RegularExpressions;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Tags;

public partial class Data_NowGoal_BetExperience : System.Web.UI.Page
{
    BetExpBLL bll = new BetExpBLL();
    protected string JsonStr = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            switch (Request.QueryString["a"])
            {
                case "save":
                    this.ProcessSaveBetExp();
                    break;
                case "saveexp":
                    this.ProcessSaveBetExpText();
                    break;
                case "saveanalysis":
                    this.ProcessSaveAnalysis();
                    break;
                case "similarlist"://近似的列表
                    this.ProcessSimilarList();
                    break;
                case "delete":
                    this.ProcessDeleteBetExp();
                    break;
                case "list":
                    this.ProcessGetBetExpList();
                    break;
                case "exist":
                    this.ProcessCheckExist();
                    break;
                case "similartrends":
                    this.ProcessSimilarTrends();
                    break;
                default:
                    break;
            }
        }
    }

    private void ProcessSimilarTrends()
    {
        JObject result = new JObject();
        JArray arr = JArray.Parse(Request.Form["trends"]);
        string match = Request.Form["match"];
        string[] values = new string[arr.Count];
        for (int i = 0; i < arr.Count; i++)
        {
            values[i] = bll.GetTrendsValue(JObject.Parse(arr[i].ToString()), match);
        }
        result.Add("success", true);
        string changes = bll.GetChangesValue(arr, match);
        DataSet ds = bll.GetListByTrends(string.Join(",", values), changes);
        JArray data = new JArray();
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                JObject item = new JObject();
                string[] A = HttpUtility.HtmlDecode(dr["Data"].ToString()).Split(',');
                item.Add("scheduleid", A[0]);
                item.Add("hometeam",A[4]);
                item.Add("fullscore", A[13] + "-" + A[14]);
                item.Add("awayteam", A[7]);
                item.Add("halfscore",A[15] + "-" + A[16]);
                item.Add("victory", float.Parse(A[13]) - float.Parse(A[14]));
                item.Add("asia", A[25]);
                if (string.IsNullOrEmpty(A[25]))
                {
                    item.Add("asiawin", "");
                }
                else
                {
                    item.Add("asiawin", float.Parse(A[13]) - float.Parse(A[14]) - float.Parse(A[25]));
                }
                item.Add("chartdata", dr["chartdata"].ToString());
                data.Add(item);
            }
        }
        result.Add("data", data);
        JsonStr = result.ToString();
    }

    private void ProcessSimilarList()
    {
        JObject result = new JObject();
        try
        {
            string data = Request.Form["data"];
            if (string.IsNullOrEmpty(Request.Form["spot"]))//如果临场为空，那么完全匹配
            {
                result.Add("success", true);
                result.Add("data", JArray.FromObject(bll.GetListByAnalysis(data).Tables[0]));
            }
            else
            {
                string spot = Request.Form["spot"];
                result.Add("success", true);
                result.Add("data", JArray.FromObject(bll.GetSimilarListByAnalysis(data, spot == "true")));
            }
        }
        catch (Exception e)
        {
            result.Add("success", false);
            result.Add("error", e.ToString());
        }
        JsonStr = result.ToString();
    }

    protected List<float> bubbleUp(List<float> Array)
    {
        for (int i = 0; i < Array.Count; i++)
        {
            for (int j = i + 1; j < Array.Count; j++)
            {
                if (Array[i] > Array[j])
                {
                    float temp = Array[i];
                    Array[i] = Array[j];
                    Array[j] = temp;
                }
            }
        }
        return Array;
    }


    private void ProcessSaveAnalysis()
    {
        JObject result = new JObject();
        try
        {
            int matchid = int.Parse(Request.Form["matchid"]);
            string data = Request.Form["data"];
            bll.UpdateAnalysis(matchid, data);
            result.Add("success", true);
            result.Add("data", JArray.FromObject(bll.GetListByAnalysis(data).Tables[0]));
            result.Add("result", this.getMatchResult(matchid.ToString()));
        }
        catch (Exception e)
        {
            result.Add("success", false);
            result.Add("error", e.ToString());
        }
        JsonStr = result.ToString();
    }

    private string getMatchResult(string matchid)
    {
        WebClientBLL bll = new WebClientBLL();
        string html = bll.GetRemoteHtml("/detail/{0}.html", new string[] { matchid });
        Lexer lexer = new Lexer(html);
        Parser parser = new Parser(lexer);
        INode tableNode = parser.Parse(new TagNameFilter("HTML"))[0].Children.ExtractAllNodesThatMatch(new TagNameFilter("BODY"))[0].Children[0];
        TableTag tt = tableNode as TableTag;
        string home = tt.GetRow(0).Columns[0].Children[0].Children[0].ToPlainTextString();
        string away = tt.GetRow(0).Columns[2].Children[0].Children[0].ToPlainTextString();
        string hscore = tt.GetRow(0).Columns[1].Children[0].Children[1].Children[0].ToPlainTextString();
        string ascore = tt.GetRow(0).Columns[1].Children[0].Children[1].Children[2].ToPlainTextString();
        string asia = tt.GetRow(0).Columns[1].Children[0].Children[0].Children[0].ToPlainTextString();
        return home + " " + hscore + "-" + ascore + " " + away + " " + asia;
    }

    private void ProcessCheckExist()
    {
        JObject result = new JObject();
        try
        {
            List<string> data = new List<string>();
            string[] matchids = Request.Form["matchids"].Split(',');
            foreach (string id in matchids)
            {
                string s = bll.GetIsExpByID(id);
                data.Add(s);
            }
            result.Add("success", true);
            result.Add("data", string.Join(",",data.ToArray()));
        }
        catch (Exception e)
        {
            result.Add("success", false);
            result.Add("error", e.ToString());
        }
        JsonStr = result.ToString();
    }

    private void ProcessSaveBetExpText()
    {
        JObject result = new JObject();
        try
        {
            int matchid = int.Parse(Request.Form["matchid"]);
            string content = Request.Form["content"];
            bll.Update(matchid, content);
            result.Add("success", true);
        }
        catch (Exception e)
        {
            result.Add("success", false);
            result.Add("error", e.ToString());
        }
        JsonStr = result.ToString();
    }

    private void ProcessDeleteBetExp()
    {
        JObject result = new JObject();
        try
        {
            bll.Delete(int.Parse(Request.Form["matchid"]));
            result.Add("success", true);
        }
        catch (Exception e)
        {
            result.Add("success", false);
            result.Add("error", e.ToString());
        }
    }

    private void ProcessGetBetExpList()
    {
        JObject result = new JObject();
        int start = int.Parse(Request.Form["start"]);
        int limit = int.Parse(Request.Form["limit"]);
        string where = Request.Form["where"];


        DataSet ds = bll.GetList(start + 1, start + limit, where);

        result.Add("success", true);
        result.Add("totalCount", bll.GetListCount(where));
        result.Add("data", JArray.FromObject(ds.Tables[0]));

        JsonStr = result.ToString();
    }

    private void ProcessSaveBetExp()
    {
        JObject result = new JObject();
        JArray rowData = JArray.Parse(Request.Form["row"]);

        try
        {
            JArray errorResults = new JArray();
            foreach (JObject row in rowData)
            {
                int matchid = int.Parse(row["match_0"].ToString());
                if (bll.Exists(matchid))
                {
                    BetExp model = new BetExp();
                    model.id = matchid;
                    model.isexp = true;
                    model.exp = Request.Form["content"];
                    model.hometeam = (((Newtonsoft.Json.Linq.JValue)(row["match_4"]))).Value.ToString();
                    model.awayteam = (((Newtonsoft.Json.Linq.JValue)(row["match_7"]))).Value.ToString();
                    model.homescore = (int)row["match_13"];
                    model.awayscore = (int)row["match_14"];
                    int score = model.homescore - model.awayscore;
                    model.asia = float.Parse((((Newtonsoft.Json.Linq.JValue)(row["match_25"]))).Value.ToString());
                    if (score > 0)
                    {
                        model.victory = 3;
                    }
                    else if (score == 0)
                    {
                        model.victory = 1;
                    }
                    else
                    {
                        model.victory = 0;
                    }
                    if (score > model.asia)
                    {
                        model.win = 3;
                    }
                    else if (score == model.asia)
                    {
                        model.win = 1;
                    }
                    else
                    {
                        model.win = 0;
                    }
                    bll.Update(model);
                }
                else
                {
                    JObject o = new JObject();
                    o.Add("error" ,row["match_4"].ToString() + "-" + row["match_7"].ToString());
                    errorResults.Add(o);
                }
            }
            result.Add("success", true);
            result.Add("errorData", errorResults);
            
        }
        catch (Exception e)
        {
            result.Add("success", false);
            result.Add("error", e.ToString());
        }
        JsonStr = result.ToString();
    }

}