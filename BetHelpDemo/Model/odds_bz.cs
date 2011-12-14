using System;
namespace SeoWebSite.Model
{
	/// <summary>
	/// odds_bz:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class odds_bz
	{
		public odds_bz()
		{}
		#region Model
		private int _id;
		private int? _companyid;
		private int? _scheduleid;
		private decimal? _home;
		private decimal? _draw;
		private decimal? _away;
		private DateTime? _time;
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
		public int? companyID
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? scheduleID
		{
			set{ _scheduleid=value;}
			get{return _scheduleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? home
		{
			set{ _home=value;}
			get{return _home;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? draw
		{
			set{ _draw=value;}
			get{return _draw;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? away
		{
			set{ _away=value;}
			get{return _away;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? time
		{
			set{ _time=value;}
			get{return _time;}
		}
		#endregion Model

	}
}

