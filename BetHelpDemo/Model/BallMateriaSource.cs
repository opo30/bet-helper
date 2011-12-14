using System;
namespace SeoWebSite.Model
{
	/// <summary>
    /// 实体类BallMateriaSource 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public class BallMateriaSource
	{
		public BallMateriaSource()
		{}
		#region Model
		private string _id;
		private string _name;
		private string _url;
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
		public string url
		{
            set { _url = value; }
            get { return _url; }
		}
		#endregion Model

	}
}

