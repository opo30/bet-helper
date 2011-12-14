using System;
namespace SeoWebSite.Model
{
	/// <summary>
	/// 实体类bettingline 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class BettingLine
	{
		public BettingLine()
		{}
		#region Model
		private string _id;
        private string _name;
		private decimal? _betmoney;
		private decimal? _returnmoney;
		private decimal? _profit;
		private string _state;
		private string _formulaid;
		private string _userid;
        private string _iscomplete;
        private string _isbetting;
		/// <summary>
		/// 
		/// </summary>
		public string id
		{
			set{ _id=value;}
			get{return _id;}
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
		public decimal? betmoney
		{
			set{ _betmoney=value;}
			get{return _betmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? returnmoney
		{
			set{ _returnmoney=value;}
			get{return _returnmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? profit
		{
			set{ _profit=value;}
			get{return _profit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string state
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string formulaid
		{
			set{ _formulaid=value;}
			get{return _formulaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string iscomplete
        {
            set { _iscomplete = value; }
            get { return _iscomplete; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string isbetting
        {
            set { _isbetting = value; }
            get { return _isbetting; }
        }
		#endregion Model

	}
}

