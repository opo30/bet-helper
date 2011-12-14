using System;
namespace SeoWebSite.Model
{
	/// <summary>
	/// odds:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Odds
	{
		public Odds()
		{}
		#region Model
		private int _id;
		private int? _companyid;
        private int? _scheduleid;
		private decimal? _s_win;
		private decimal? _s_draw;
		private decimal? _s_lost;
		private decimal? _s_winper;
		private decimal? _s_drawper;
		private decimal? _s_lostper;
		private decimal? _s_return;
		private decimal? _e_win;
		private decimal? _e_draw;
		private decimal? _e_lost;
		private decimal? _e_winper;
		private decimal? _e_drawper;
		private decimal? _e_lostper;
		private decimal? _e_return;
		private decimal? _winkelly;
		private decimal? _drawkelly;
		private decimal? _lostkelly;
		private DateTime? _lastupdatetime;
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
		public int? companyid
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int? scheduleid
        {
            set { _scheduleid = value; }
            get { return _scheduleid; }
        }
		/// <summary>
		/// 
		/// </summary>
		public decimal? s_win
		{
			set{ _s_win=value;}
			get{return _s_win;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? s_draw
		{
			set{ _s_draw=value;}
			get{return _s_draw;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? s_lost
		{
			set{ _s_lost=value;}
			get{return _s_lost;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? s_winper
		{
			set{ _s_winper=value;}
			get{return _s_winper;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? s_drawper
		{
			set{ _s_drawper=value;}
			get{return _s_drawper;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? s_lostper
		{
			set{ _s_lostper=value;}
			get{return _s_lostper;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? s_return
		{
			set{ _s_return=value;}
			get{return _s_return;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? e_win
		{
			set{ _e_win=value;}
			get{return _e_win;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? e_draw
		{
			set{ _e_draw=value;}
			get{return _e_draw;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? e_lost
		{
			set{ _e_lost=value;}
			get{return _e_lost;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? e_winper
		{
			set{ _e_winper=value;}
			get{return _e_winper;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? e_drawper
		{
			set{ _e_drawper=value;}
			get{return _e_drawper;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? e_lostper
		{
			set{ _e_lostper=value;}
			get{return _e_lostper;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? e_return
		{
			set{ _e_return=value;}
			get{return _e_return;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? winkelly
		{
			set{ _winkelly=value;}
			get{return _winkelly;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? drawkelly
		{
			set{ _drawkelly=value;}
			get{return _drawkelly;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? lostkelly
		{
			set{ _lostkelly=value;}
			get{return _lostkelly;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? lastupdatetime
		{
			set{ _lastupdatetime=value;}
			get{return _lastupdatetime;}
		}
		#endregion Model

	}
}

