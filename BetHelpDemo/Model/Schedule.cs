using System;
namespace SeoWebSite.Model
{
	/// <summary>
	/// Schedule:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Schedule
	{
		public Schedule()
		{}
		#region Model
		private int _id;
		private string _data;
		private bool _updated;
		private DateTime? _date;
		private int? _home;
		private int? _away;
        private int? _halfhome;
        private int? _halfaway;
        private int? _h_teamid;
        private int? _g_teamid;
        private int? _sclassid;
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
		public bool updated
		{
			set{ _updated=value;}
			get{return _updated;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? date
		{
			set{ _date=value;}
			get{return _date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? home
		{
			set{ _home=value;}
			get{return _home;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? away
		{
			set{ _away=value;}
			get{return _away;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int? halfhome
        {
            set { _halfhome = value; }
            get { return _home; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? halfaway
        {
            set { _halfaway = value; }
            get { return _halfaway; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? h_teamid
        {
            set { _h_teamid = value; }
            get { return _h_teamid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? g_teamid
        {
            set { _g_teamid = value; }
            get { return _g_teamid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? sclassid
        {
            set { _sclassid = value; }
            get { return _sclassid; }
        }
		#endregion Model

	}
}

