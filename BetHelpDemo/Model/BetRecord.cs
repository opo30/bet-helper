using System;
namespace SeoWebSite.Model
{
	/// <summary>
	/// 实体类betrecord 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class BetRecord
	{
		public BetRecord()
		{}
		#region Model
		private string _id;
		private string _lineid;
		private string _teamname;
		private string _traditional;
		private DateTime _starttime;
		private DateTime _endtime;
		private DateTime _bettime;
		private string _itemid;
		private string _detailid;
		private decimal _betmoney;
		private decimal _returnmoney;
		private string _result;
        private string _sourceid;
        private decimal _odds;
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
		public string lineid
		{
			set{ _lineid=value;}
			get{return _lineid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string teamname
		{
			set{ _teamname=value;}
			get{return _teamname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string traditional
		{
			set{ _traditional=value;}
			get{return _traditional;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime starttime
		{
			set{ _starttime=value;}
			get{return _starttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime endtime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime bettime
		{
			set{ _bettime=value;}
			get{return _bettime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string itemid
		{
			set{ _itemid=value;}
			get{return _itemid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string detailid
		{
			set{ _detailid=value;}
			get{return _detailid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal betmoney
		{
			set{ _betmoney=value;}
			get{return _betmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal returnmoney
		{
			set{ _returnmoney=value;}
			get{return _returnmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string result
		{
			set{ _result=value;}
			get{return _result;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string sourceid
        {
            set { _sourceid = value; }
            get { return _sourceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal odds
        {
            set { _odds = value; }
            get { return _odds; }
        }
		#endregion Model

	}
}

