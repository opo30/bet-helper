using System;
namespace SeoWebSite.Model
{
	/// <summary>
	/// ScheduleType:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class ScheduleClass
	{
		public ScheduleClass()
		{}
		#region Model
		private int _id;
		private string _name;
		private string _data;
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
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string data
		{
			set{ _data=value;}
			get{return _data;}
		}
		#endregion Model

	}
}

