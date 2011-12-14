using System;
namespace SeoWebSite.Model
{
	/// <summary>
	/// odds_dx:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class odds_dx
	{
		public odds_dx()
		{}
		#region Model
		private int _id;
		private int? _companyid;
		private int? _scheduleid;
		private decimal? _pankou;
		private decimal? _big;
		private decimal? _small;
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
		public decimal? pankou
		{
			set{ _pankou=value;}
			get{return _pankou;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? big
		{
			set{ _big=value;}
			get{return _big;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? small
		{
			set{ _small=value;}
			get{return _small;}
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

