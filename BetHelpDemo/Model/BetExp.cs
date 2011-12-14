using System;
namespace SeoWebSite.Model
{
	/// <summary>
	/// 实体类betexp 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public class BetExp
	{
        public BetExp()
		{}
        public BetExp(int _id, string _data,string _trends,string _changes, bool _isexp)
        {
            this._id = _id;
            this._data = _data;
            this._isexp = _isexp;
            this._trends = _trends;
            this._changes = _changes;
        }
		#region Model
		private int _id;
		private string _data;
        private string _trends;
        private string _changes;
		private bool _isexp;
        private bool _hasstatistics;
		private string _exp;
        private string _hometeam;
        private string _awayteam;
        private int _homescore;
        private int _awayscore;
        private int _victory;
        private int _win;
        private float _asia;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string data
		{
			set{ _data=value;}
			get{return _data;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string trends
        {
            set { _trends = value; }
            get { return _trends; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string changes
        {
            set { _changes = value; }
            get { return _changes; }
        }
		/// <summary>
		/// 
		/// </summary>
		public bool isexp
		{
			set{ _isexp=value;}
			get{return _isexp;}
		}
        /// <summary>
		/// 
		/// </summary>
        public bool hasstatistics
		{
            set { _hasstatistics = value; }
            get { return _hasstatistics; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string exp
		{
			set{ _exp=value;}
			get{return _exp;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string hometeam
        {
            set { _hometeam = value; }
            get { return _hometeam; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string awayteam
        {
            set { _awayteam = value; }
            get { return _awayteam; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int homescore
        {
            set { _homescore = value; }
            get { return _homescore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int awayscore
        {
            set { _awayscore = value; }
            get { return _awayscore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int victory
        {
            set { _victory = value; }
            get { return _victory; }
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
        public float asia
        {
            set { _asia = value; }
            get { return _asia; }
        }
		#endregion Model

	}
}

