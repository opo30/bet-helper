using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace SeoWebSite.Common.JSON
{
    /// <summary>
    /// LiveDataJSONHelper ��ʱ����JSON
    /// </summary>
    public class LiveDataJSONHelper
    {
        //��ӦJSON��singleInfo��Ա
        public string singleInfo = string.Empty;
        protected string _error = string.Empty;
        protected bool _success = true;
        protected long _mtotalCount = 0;
        protected long _ctotalCount = 0;
        protected System.Collections.ArrayList arrMatch = new ArrayList();
        protected System.Collections.ArrayList arrMatchItem = new ArrayList();
        protected System.Collections.ArrayList arrClass = new ArrayList();
        protected System.Collections.ArrayList arrClassItem = new ArrayList();

        public LiveDataJSONHelper()
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

        public long mtotlalCount
        {
            get { return _mtotalCount; }
            set { _mtotalCount = value; }
        }

        public long ctotlalCount
        {
            get { return _ctotalCount; }
            set { _ctotalCount = value; }
        }

        //���ã�ÿ��������һ��json����ʱ����ִ�и÷���
        public void Reset()
        {
            _success = true;
            _error = string.Empty;
            singleInfo = string.Empty;
            arrMatch.Clear();
            arrMatchItem.Clear();
            arrClass.Clear();
            arrClassItem.Clear();
        }



        public void AddMatch(string name, string value)
        {
            arrMatch.Add("\"" + name + "\":" + "\"" + value + "\"");
        }
        public void AddClass(string name, string value)
        {
            arrClass.Add("\"" + name + "\":" + "\"" + value + "\"");
        }


        public void MatchOk()
        {
            arrMatch.Add("<BR>");
            mtotlalCount++;
        }

        public void ClassOk()
        {
            arrClass.Add("<BR>");
            ctotlalCount++;
        }

        //���л�JSON���󣬵õ����ص�JSON����
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("mtotalCount:" + mtotlalCount.ToString() + ",");
            sb.Append("ctotalCount:" + ctotlalCount.ToString() + ",");
            sb.Append("success:" + _success.ToString().ToLower() + ",");
            sb.Append("error:\"" + _error.Replace("\"", "\\\"") + "\",");
            sb.Append("singleInfo:\"" + singleInfo.Replace("\"", "\\\"") + "\",");
            sb.Append("mdata:[");

            int mindex = 0;
            int cindex = 0;
            
            if (arrMatch.Count <= 0)
            {
                sb.Append("]");
            }
            else
            {
                sb.Append("{");
                foreach (string val in arrMatch)
                {
                    mindex++;

                    if (val != "<BR>")
                    {
                        sb.Append(val + ",");
                    }
                    else
                    {
                        sb = sb.Replace(",", "", sb.Length - 1, 1);
                        sb.Append("},");
                        if (mindex < arrMatch.Count)
                        {
                            sb.Append("{");
                        }
                    }

                }
                sb = sb.Replace(",", "", sb.Length - 1, 1);
                sb.Append("],");
            }

            sb.Append("cdata:[");
            if (arrClass.Count <= 0)
            {
                sb.Append("]");
            }
            else
            {
                sb.Append("{");
                foreach (string val in arrClass)
                {
                    cindex++;

                    if (val != "<BR>")
                    {
                        sb.Append(val + ",");
                    }
                    else
                    {
                        sb = sb.Replace(",", "", sb.Length - 1, 1);
                        sb.Append("},");
                        if (cindex < arrClass.Count)
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