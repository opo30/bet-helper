using System;
namespace SeoWebSite.Model
{
	/// <summary>
	/// 实体类betdetail 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class BetDetail
	{
		public BetDetail()
		{}
		#region Model
		private string _id;
		private string _itemid;
		private string _name;
		private string _formulaid;
		private string _description;
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
		public string itemid
		{
			set{ _itemid=value;}
			get{return _itemid;}
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
		public string formulaid
		{
			set{ _formulaid=value;}
			get{return _formulaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string description
		{
			set{ _description=value;}
			get{return _description;}
		}
		#endregion Model

	}
}

