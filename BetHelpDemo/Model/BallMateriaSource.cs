using System;
namespace SeoWebSite.Model
{
	/// <summary>
    /// ʵ����BallMateriaSource ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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

