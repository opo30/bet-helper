using System;
namespace SeoWebSite.Model
{
	/// <summary>
	/// company:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Company
	{
		public Company()
		{}
		#region Model
		private int _id;
		private string _fullname;
		private string _name;
		private bool? _isprimary;
		private bool? _isexchange;
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
		public string fullname
		{
			set{ _fullname=value;}
			get{return _fullname;}
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
		public bool? isprimary
		{
			set{ _isprimary=value;}
			get{return _isprimary;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool? isexchange
		{
			set{ _isexchange=value;}
			get{return _isexchange;}
		}
		#endregion Model

	}
}

