using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeoWebSite.Model
{
    /// <summary>
    /// ScheduleAnalysis:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ScheduleAnalysis
    {
        public ScheduleAnalysis()
        { }
        #region Model
        private int _id;
        private int? _scheduleid;
        private decimal? _oddswin;
        private decimal? _oddsdraw;
        private decimal? _oddslost;
        private decimal? _perwin;
        private decimal? _perdraw;
        private decimal? _perlost;
        private int? _totalcount;
        private DateTime? _time;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
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
        public decimal? oddswin
        {
            set { _oddswin = value; }
            get { return _oddswin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? oddsdraw
        {
            set { _oddsdraw = value; }
            get { return _oddsdraw; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? oddslost
        {
            set { _oddslost = value; }
            get { return _oddslost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? perwin
        {
            set { _perwin = value; }
            get { return _perwin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? perdraw
        {
            set { _perdraw = value; }
            get { return _perdraw; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? perlost
        {
            set { _perlost = value; }
            get { return _perlost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? totalCount
        {
            set { _totalcount = value; }
            get { return _totalcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? time
        {
            set { _time = value; }
            get { return _time; }
        }
        #endregion Model

    }
}
