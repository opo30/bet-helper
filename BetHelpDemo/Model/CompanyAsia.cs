using System;
namespace SeoWebSite.Model
{
	/// <summary>
	/// Company:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class CompanyAsia
	{
		public CompanyAsia()
		{}
		#region Model
		private int _id;
		private string _name;
		private bool _isasia;
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
		public bool isasia
		{
			set{ _isasia=value;}
			get{return _isasia;}
		}
		#endregion Model

	}
}

