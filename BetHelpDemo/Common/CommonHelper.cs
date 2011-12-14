using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

namespace SeoWebSite.Common
{
    public class CommonHelper
    {
        /// <summary>
        /// 去除数据库中的\r\n引起JSON报错
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string CleanString(string s)
        {
            return s.Replace("\r\n", "");
        }

        //================================================================ 
        /**/
        // 
        // 返回 GUID 用于数据库操作，特定的时间代码可以提高检索效率 
        // 
        // COMB (GUID 与时间混合型) 类型 GUID 数据 
        public static Guid NewComb()
        {
            byte[] guidArray = System.Guid.NewGuid().ToByteArray();
            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now; // Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks));
            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));
            // Reverse the bytes to match SQL Servers ordering Array.Reverse(daysArray); 
            Array.Reverse(msecsArray);
            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new System.Guid(guidArray);
        }
        //================================================================ 
        /**/
        // 从 SQL SERVER 返回的 GUID 中生成时间信息
        // 包含时间信息的 COMB 
        // 时间 
        public static DateTime GetDateFromComb(System.Guid guid)
        {
            DateTime baseDate = new DateTime(1900, 1, 1);
            byte[] daysArray = new byte[4];
            byte[] msecsArray = new byte[4];
            byte[] guidArray = guid.ToByteArray();
            // Copy the date parts of the guid to the respective byte arrays. 
            Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2); Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4);
            // Reverse the arrays to put them into the appropriate order 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);
            // Convert the bytes to ints 
            int days = BitConverter.ToInt32(daysArray, 0); int msecs = BitConverter.ToInt32(msecsArray, 0);
            DateTime date = baseDate.AddDays(days); date = date.AddMilliseconds(msecs * 3.333333);
            return date;
        }

        //读filename到byte[]

        public static byte[] ReadFile(string fileName)
        {

            FileStream pFileStream = null;

            byte[] pReadByte = new byte[0];

            try
            {

                pFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                BinaryReader r = new BinaryReader(pFileStream);

                r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开

                pReadByte = r.ReadBytes((int)r.BaseStream.Length);

                return pReadByte;

            }

            catch
            {

                return pReadByte;

            }

            finally
            {

                if (pFileStream != null)

                    pFileStream.Close();

            }

        }

        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="Htmlstring">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = Htmlstring.Replace("</font></b></td><td><script>showtime(", "|");
            Htmlstring = Htmlstring.Replace(")</script></td></tr><tr align=center bgcolor=#FFFFFF><td height=22><b><font color=red>", ";");
            Htmlstring = Htmlstring.Replace(")</script></td></tr><tr align=center bgcolor=#FFFFFF><td height=22><b><font color=green>", ";");
            Htmlstring = Htmlstring.Replace(")</script></td></tr><tr align=center bgcolor=#FFFFFF><td height=22><b><font color=>", ";");
            Htmlstring = Htmlstring.Replace(")", "");
            Htmlstring = Htmlstring.Replace("</font></b></td><td><b><font color=red>", "|");
            Htmlstring = Htmlstring.Replace("</font></b></td><td><b><font color=green>", "|");
            Htmlstring = Htmlstring.Replace("</font></b></td><td><b><font color=>", "|");
            Htmlstring = Regex.Replace(Htmlstring, @"<script.*?</script>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<style.*?</style>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<.*?>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);


            Htmlstring = Htmlstring.Replace("<", "");
            Htmlstring = Htmlstring.Replace(">", "");
            Htmlstring = Htmlstring.Replace("\r\n", "");
            return Htmlstring;
        }

        public static List<float> bubbleUp(List<float> Array)
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

        /// <summary>
        /// 截取字符串函数
        /// </summary>
        /// <param name="Str">所要截取的字符串</param>
        /// <param name="Num">截取字符串的长度</param>
        /// <returns></returns>
        public static string GetSubString(string Str, int Num)
        {
            if (Str == null || Str == "")
                return "";
            string outstr = "";
            int n = 0;
            foreach (char ch in Str)
            {
                n += System.Text.Encoding.Default.GetByteCount(ch.ToString());
                if (n > Num)
                    break;
                else
                    outstr += ch;
            }
            return outstr;
        }

        public static string GoalCnToGoal(string goal) { //数字盘口转汉汉字	
            string[] GoalCn1 = { "0", "0.25", "0.5", "0.75", "1", "1.25", "1.5", "1.75", "2", "2.25", "2.5", "2.75", "3", "3.25", "3.5", "3.75", "4", "4.25", "4.5", "4.75", "5", "5.25", "5.5", "5.75", "6", "6.25", "6.5", "6.75", "7", "7.25", "7.5", "7.75", "8", "8/8.5", "8.5", "8.75", "9", "9.25", "9.5", "9.75", "10", "10.25", "10.5", "10.75", "11", "11.25", "11.5", "11.75", "12", "12.25", "12.5", "12.75", "13", "13.25", "13.5", "13.75", "14" };
            string[] GoalCn2 = { "0", "-0.25", "-0.5", "-0.75", "-1", "-1.25", "-1.5", "-1.75", "-2", "-2.25", "-2.5", "-2.75", "-3", "-3.25", "-3.5", "-3.75", "-4", "-4.25", "-4.5", "-4.75", "-5", "-5.25", "-5.5", "-5.75", "-6", "-6.25", "-6.5", "-6.75", "-7", "-7.25", "-7.5", "-7.75", "-8", "-8.25", "-8.5", "-8.75", "-9", "-9.25", "-9.5", "-9.75", "-10" };
            string[] GoalCn = "平手,平/半,半球,半/一,一球,一/球半,球半,球半/两,两球,两/两球半,两球半,两球半/三,三球,三/三球半,三球半,三球半/四,四球,四/四球半,四球半,四球半/五球,五球,五/五球半,五球半,五球半/六,六球,六球/六球半,六球半,六球半/七球,七球,七球/七球半,七球半,七球半/八球,八球,八球/八球半,八球半,八球半/九球,九球,九球/九球半,九球半,九球半/十球,十球".Split(',');
            if (goal.IndexOf("受") == -1)
            {
                for (int i = 0; i < GoalCn.Length; i++)
                {
                    if (goal == GoalCn[i])
                    {
                        return GoalCn1[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < GoalCn.Length; i++)
                {
                    if (goal == "受" + GoalCn[i])
                    {
                        return GoalCn2[i];
                    }
                }
            }
            return null;
        }

        public static string BallSizeToBall(string size) { 
            string[] GoalCn1 = {"0", "0/0.5", "0.5", "0.5/1", "1", "1/1.5", "1.5", "1.5/2", "2", "2/2.5", "2.5", "2.5/3", "3", "3/3.5", "3.5", "3.5/4", "4", "4/4.5", "4.5", "4.5/5", "5", "5/5.5", "5.5", "5.5/6", "6", "6/6.5", "6.5", "6.5/7", "7", "7/7.5", "7.5", "7.5/8", "8", "8/8.5", "8.5", "8.5/9", "9", "9/9.5", "9.5", "9.5/10", "10", "10/10.5", "10.5", "10.5/11", "11", "11/11.5", "11.5", "11.5/12", "12", "12/12.5", "12.5", "12.5/13", "13", "13/13.5", "13.5", "13.5/14", "14"};
            string[] GoalCn2 = { "0", "0/-0.5", "-0.5", "-0.5/-1", "-1", "-1/-1.5", "-1.5", "-1.5/-2", "-2", "-2/-2.5", "-2.5", "-2.5/-3", "-3", "-3/-3.5", "-3.5", "-3.5/-4", "-4", "-4/-4.5", "-4.5", "-4.5/-5", "-5", "-5/-5.5", "-5.5", "-5.5/-6", "-6", "-6/-6.5", "-6.5", "-6.5/-7", "-7", "-7/-7.5", "-7.5", "-7.5/-8", "-8", "-8/-8.5", "-8.5", "-8.5/-9", "-9", "-9/-9.5", "-9.5", "-9.5/-10", "-10" };
            string[] Goal1 = { "0", "0.25", "0.5", "0.75", "1", "1.25", "1.5", "1.75", "2", "2.25", "2.5", "2.75", "3", "3.25", "3.5", "3.75", "4", "4.25", "4.5", "4.75", "5", "5.25", "5.5", "5.75", "6", "6.25", "6.5", "6.75", "7", "7.25", "7.5", "7.75", "8", "8.25", "8.5", "8.75", "9", "9.25", "9.5", "9.75", "10", "10.25", "10.5", "10.75", "11", "11.25", "11.5", "11.75", "12", "12.25", "12.5", "12.75", "13", "13.25", "13.5", "13.75", "14" };
            string[] Goal2 = { "0", "-0.25", "-0.5", "-0.75", "-1", "-1.25", "-1.5", "-1.75", "-2", "-2.25", "-2.5", "-2.75", "-3", "-3.25", "-3.5", "-3.75", "-4", "-4.25", "-4.5", "-4.75", "-5", "-5.25", "-5.5", "-5.75", "-6", "-6.25", "-6.5", "-6.75", "-7", "-7.25", "-7.5", "-7.75", "-8", "-8.25", "-8.5", "-8.75", "-9", "-9.25", "-9.5", "-9.75", "-10" };
            for (int i = 0; i < GoalCn1.Length; i++)
            {
                if (GoalCn1[i] == size)
                {
                    return Goal1[i];
                }
            }
            for (int i = 0; i < GoalCn2.Length; i++)
            {
                if (GoalCn2[i] == size)
                {
                    return Goal2[i];
                }
            }
            return null;
        }
    }
}
