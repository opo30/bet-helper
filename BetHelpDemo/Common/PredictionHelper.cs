using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeoWebSite.Common
{
    /// <summary>
    /// 预测法运算
    /// </summary>
    public static class PredictionHelper
    {
        /// <summary>
        /// 埃罗预测法主场积分运算
        /// </summary>
        /// <param name="point">当前积分</param>
        /// <param name="result">1代表胜，2代表平，3代表负</param>
        /// <returns></returns>
        public static float EloHomePointsCalculation(float point,string result)
        {
            float current = 0;
            switch (result)
            {
                case "1":
                    current = point - 7 + 12;
                    break;
                case "0":
                    current = point - 7 + 6;
                    break;
                case "-1":
                    current = point - 7;
                    break;
                default:
                    break;
            }
            return current;
        }

        /// <summary>
        /// 埃罗预测法客场积分运算
        /// </summary>
        /// <param name="point">当前积分</param>
        /// <param name="result">1代表胜，2代表平，3代表负</param>
        /// <returns></returns>
        public static float EloAwayPointsCalculation(float point, string result)
        {
            float current = 0;
            switch (result)
            {
                case "1":
                    current = point - 5 + 12;
                    break;
                case "0":
                    current = point - 5 + 6;
                    break;
                case "-1":
                    current = point - 5;
                    break;
                default:
                    break;
            }
            return current;
        }

        public static float EloHomePrediction(float h_point, float a_point)
        {
            float result = 0.0053f * (h_point - a_point) + 0.448f;
            return result;
        }

        public static float EloAwayPrediction(float h_point, float a_point)
        {
            float result = -0.0039f * (h_point - a_point) + 0.2452f;
            return result;
        }

        public static string GoalRatePrediction(float h_rate, float a_rate)
        {
            float diff = h_rate -a_rate;
            if (diff > 0.3f)
            {
                return "主队";
            }
            else if (diff <= 0.3f && diff > 0.1f && diff > 0)
            {
                return "主队";
            }
            else if (diff <= 0.3f && diff > 0.1f && diff <= 0)
            {
                return "主队或平";
            }
            else
            {
                return "主队或平";
            }
        }

        public static int SixGamePointCalculation(int point ,string result)
        {
            int current = 0;
            switch (result)
            {
                case "1":
                    current = point + 3;
                    break;
                case "0":
                    current = point + 1;
                    break;
                case "-1":
                    current = point + 0;
                    break;
                default:
                    break;
            }
            return current;
        }

        public static string SixGamePrediction(int h_point, int a_point)
        {
            int diff = h_point - a_point;
            if (diff >= 6)
            {
                return "主队";
            }
            else if (diff <= -6)
            {
                return "客队";
            }
            else if (diff == 5)
            {
                return "主队";
            }
            else if (diff == -5)
            {
                return "主队或平";
            }
            else if (diff >=2 && diff <=4)
            {
                return "主队";
            }
            else if (diff <=-2 && diff >=-4)
            {
                return "客队";
            }
            else if (Math.Abs(diff) == 0 || Math.Abs(diff) == 1)
            {
                return "主队或平";
            }
            return "";
        }
    }
}
