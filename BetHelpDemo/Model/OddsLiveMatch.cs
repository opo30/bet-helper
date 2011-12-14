using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeoWebSite.Model
{
    public class OddsLiveMatch
    {
        public OddsLiveMatch()
		{}
		#region Model
		private string _id;
        private string _name;
        private string _urlparams;
        private DateTime _time;
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
        public string urlparams
        {
            set { _urlparams = value; }
            get { return _urlparams; }
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