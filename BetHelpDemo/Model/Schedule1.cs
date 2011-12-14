using System;
namespace SeoWebSite.Model
{
	/// <summary>
	/// 实体类Schedule 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Schedule1
	{
		public Schedule1()
		{}
		#region Model
		private int _id;
		private int _scheduleid;
		private string _data;
		private DateTime? _date;
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
		public int ScheduleID
		{
			set{ _scheduleid=value;}
			get{return _scheduleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Data
		{
			set{ _data=value;}
			get{return _data;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Date
		{
			set{ _date=value;}
			get{return _date;}
		}
		#endregion Model

	}
}

