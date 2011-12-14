using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeoWebSite.Model
{
    public class Odds1x2History
    {
        public Odds1x2History()
		{}
		#region Model
		private int _id;
        private int _scheduleid;
        private int _companyid;
        private float _home;
        private float _draw;
        private float _away;
        private float _homep;
        private float _drawp;
        private float _awayp;
        private float _homek;
        private float _drawk;
        private float _awayk;
        private float _returnrate;
        private DateTime _time;
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
        public int scheduleid
        {
            set { _scheduleid = value; }
            get { return _scheduleid; }
        }
		/// <summary>
		/// 
		/// </summary>
        public int companyid
		{
            set { _companyid = value; }
            get { return _companyid; }
		}
		/// <summary>
		/// 
		/// </summary>
        public float home
		{
            set { _home = value; }
            get { return _home; }
		}
		/// <summary>
		/// 
		/// </summary>
        public float draw
		{
            set { _draw = value; }
            get { return _draw; }
		}
		/// <summary>
		/// 
		/// </summary>
        public float away
		{
            set { _away = value; }
            get { return _away; }
		}
		/// <summary>
		/// 
		/// </summary>
        public float homep
		{
            set { _homep = value; }
            get { return _homep; }
		}
		/// <summary>
		/// 
		/// </summary>
        public float drawp
		{
            set { _drawp = value; }
            get { return _drawp; }
		}
        /// <summary>
        /// 
        /// </summary>
        public float awayp
        {
            set { _awayp = value; }
            get { return _awayp; }
        }
       
        /// <summary>
        /// 
        /// </summary>
        public float homek
        {
            set { _homek = value; }
            get { return _homek; }
        }
        /// <summary>
        /// 
        /// </summary>
        public float drawk
        {
            set { _drawk = value; }
            get { return _drawk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public float awayk
        {
            set { _awayk = value; }
            get { return _awayk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public float returnrate
        {
            set { _returnrate = value; }
            get { return _returnrate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime time
        {
            set { _time = value; }
            get { return _time; }
        }
		#endregion Model

    }
}
