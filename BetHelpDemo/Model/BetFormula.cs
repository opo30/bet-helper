using System;
namespace SeoWebSite.Model
{
	/// <summary>
	/// 实体类betformula 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class BetFormula
	{
		public BetFormula()
		{}
		#region Model
		private string _id;
		private string _name;
		private string _content;
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
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string content
		{
			set{ _content=value;}
			get{return _content;}
		}
		#endregion Model

	}
}

