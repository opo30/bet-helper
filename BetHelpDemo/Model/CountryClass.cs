using System;
namespace SeoWebSite.Model
{
    /// <summary>
    /// CountryClass:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CountryClass
    {
        public CountryClass()
        { }
        #region Model
        private int _id;
        private string _data;
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
        public string data
        {
            set { _data = value; }
            get { return _data; }
        }
        #endregion Model

    }
}

