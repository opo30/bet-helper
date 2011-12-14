using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeoWebSite.Model
{
    public class OddsLiveHistory
    {
        public OddsLiveHistory()
		{}
		#region Model
		private int _id;
        private string _matchid;
        private float _home;
        private float _draw;
        private float _away;
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
        public string matchid
        {
            set { _matchid = value; }
            get { return _matchid; }
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
        public DateTime time
        {
            set { _time = value; }
            get { return _time; }
        }
    }
        #endregion Model
}