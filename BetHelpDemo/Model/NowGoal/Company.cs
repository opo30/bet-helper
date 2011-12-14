using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Model.NowGoal
{
    public class Company
    {
        //公司名称
        public string CompanyName { get; set; }
        //初始主胜凯利
        public float kellywin { get; set; }
        //初始平局凯利
        public float kellyequal { get; set; }
        //初始客胜凯利
        public float kellyfail { get; set; }
        //更新时间
        public DateTime updatatime { get; set; }
        //比赛初始赔率
        public float swin { get; set; }
        public float sequal { get; set; }
        public float sfail { get; set; }
        //比赛初始概率
        public float swin1 { get; set; }
        public float sequal1 { get; set; }
        public float sfail1 { get; set; }

        //初始返还率
        public float srestore { get; set; }
        //比赛初始赔付包容率
        public float spaidinclusion { get; set; }

        //比赛即时赔率
        public float ewin { get; set; }
        public float eequal { get; set; }
        public float efail { get; set; }
        //比赛即时概率
        public float ewin1 { get; set; }
        public float eequal1 { get; set; }
        public float efail1 { get; set; }

        //即时返还率
        public float erestore { get; set; }
        //比赛即时赔付包容率
        public float epaidinclusion { get; set; }

        //基础数据来源
        public string source { get; set; }
        


        public void Calculate()
        {
            string temp = source.Substring(1, source.Length - 2);
            string[] data = temp.Split(new char[] { '|' });
            //公司名称
            CompanyName = data[2];
            //主胜凯利
            kellywin = Convert.ToSingle(data[17]);
            //平局凯利
            kellyequal = Convert.ToSingle(data[18]);
            //客胜凯利
            kellyfail = Convert.ToSingle(data[19]);
            //更新时间
            updatatime = Convert.ToDateTime(data[20]);
            //初始赔率
            swin = Convert.ToSingle(data[3]);
            sequal = Convert.ToSingle(data[4]);
            sfail = Convert.ToSingle(data[5]);
            //初始胜率
            swin1 = Convert.ToSingle(data[6]);
            sequal1 = Convert.ToSingle(data[7]);
            sfail1 = Convert.ToSingle(data[8]);
            //初始返还率
            srestore = Convert.ToSingle(data[9]);
            spaidinclusion = 1 / (1 / swin + 1 / sequal + 1 / sfail);

            //即时赔率
            ewin = Convert.ToSingle(data[10]);
            eequal = Convert.ToSingle(data[11]);
            efail = Convert.ToSingle(data[12]);
            //即时胜率
            ewin1 = Convert.ToSingle(data[13]);
            eequal1 = Convert.ToSingle(data[14]);
            efail1 = Convert.ToSingle(data[15]);
            //即时返还率
            erestore = Convert.ToSingle(data[16]);
            epaidinclusion = 1 / (1 / ewin + 1 / eequal + 1 / efail);
        }
    }
}
