using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace SeoWebSite.Common.JSON
{
    /// <summary>
    /// LiveDataJSONHelper 即时数据JSON
    /// </summary>
    public class LiveDataJSONHelper
    {
        //对应JSON的singleInfo成员
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

        //对应于JSON的success成员
        public bool success
        {
            get
            {
                return _success;
            }
            set
            {
                //如设置为true则清空error
                if (success) _error = string.Empty;
                _success = value;
            }
        }

        //对应于JSON的error成员
        public string error
        {
            get
            {
                return _error;
            }
            set
            {
                //如设置error，则自动设置success为false
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

        //重置，每次新生成一个json对象时必须执行该方法
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

        //序列化JSON对象，得到返回的JSON代码
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