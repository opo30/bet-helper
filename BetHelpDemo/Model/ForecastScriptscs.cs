using System;
namespace SeoWebSite.Model
{
    /// <summary>
    /// 实体类forecastscripts 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ForecastScripts
    {
        public ForecastScripts()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _content;
        private string _remark;
        private int _win;
        private int _lost;
        private int _resultwin;
        private int _resultlost;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int win
        {
            set { _win = value; }
            get { return _win; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int lost
        {
            set { _lost = value; }
            get { return _lost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int resultwin
        {
            set { _win = value; }
            get { return _win; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int resultlost
        {
            set { _lost = value; }
            get { return _lost; }
        }
        #endregion Model

    }
}

