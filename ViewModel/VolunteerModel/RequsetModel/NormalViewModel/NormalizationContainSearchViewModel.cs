using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerModel.RequsetModel.NormalViewModel
{
    public   class NormalizationContainSearchViewModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string userid;
        /// <summary>
        /// 主表id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string clamtime { get; set; }

        public string status { get; set; }
        public string SubdistrictID { get; set; }//小区code
    }
}
