using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace SeoWebSite.Common.JSON
{
    /// <summary>
    /// JSONHelper ��ժҪ˵��
    /// </summary>
    public class JSONHelper
    {
        //��ӦJSON��singleInfo��Ա
        public string singleInfo = string.Empty;
        protected string _error = string.Empty;
        protected bool _success = true;
        protected long _totalCount = 0;
        protected System.Collections.ArrayList arrData = new ArrayList();
        protected System.Collections.ArrayList arrDataItem = new ArrayList();


        public JSONHelper()
        {

        }

        //public static string ToJSON(object obj)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    return serializer.Serialize(obj);
        //}

        //public static string ToJSON(object obj, int recursionDepth)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    serializer.RecursionLimit = recursionDepth;
        //    return serializer.Serialize(obj);
        //}

        //��Ӧ��JSON��success��Ա
        public bool success
        {
            get
            {
                return _success;
            }
            set
            {
                //������Ϊtrue�����error
                if (success) _error = string.Empty;
                _success = value;
            }
        }

        //��Ӧ��JSON��error��Ա
        public string error
        {
            get
            {
                return _error;
            }
            set
            {
                //������error�����Զ�����successΪfalse
                if (value != "") _success = false;
                _error = value;
            }
        }

        public long totlalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }


        //���ã�ÿ��������һ��json����ʱ����ִ�и÷���
        public void Reset()
        {
            _success = true;
            _error = string.Empty;
            singleInfo = string.Empty;
            arrData.Clear();
            arrDataItem.Clear();
        }



        public void AddItem(string name, string value)
        {
            arrData.Add("\"" + name + "\":" + "\"" + value + "\"");
        }



        public void ItemOk()
        {
            arrData.Add("<BR>");
            totlalCount++;
        }

        //���л�JSON���󣬵õ����ص�JSON����
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:" + totlalCount.ToString() + ",");
            sb.Append("success:" + _success.ToString().ToLower() + ",");
            sb.Append("error:\"" + _error.Replace("\"", "\\\"") + "\",");
            sb.Append("singleInfo:\"" + singleInfo.Replace("\"", "\\\"") + "\",");
            sb.Append("data:[");

            int index = 0;
            
            if (arrData.Count <= 0)
            {
                sb.Append("]");
            }
            else
            {
                sb.Append("{");
                foreach (string val in arrData)
                {
                    index++;

                    if (val != "<BR>")
                    {
                        sb.Append(val + ",");
                    }
                    else
                    {
                        sb = sb.Replace(",", "", sb.Length - 1, 1);
                        sb.Append("},");
                        if (index < arrData.Count)
                        {
                            sb.Append("{");
                        }
                    }

                }
                sb = sb.Replace(",", "", sb.Length - 1, 1);
                sb.Append("]");
            }

            sb.Append("}");
            return sb.ToString();
        }
    }
}