using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;

namespace ViewModel.VolunteerModel.RequsetModel.NormalViewModel
{
    public class NormalSearchViewModel
    {

        public string id { get; set; } 
        public string title { get; set; }
        public string CommunityName { get; set; }
        public string CommunityNameCode { get; set; }
        public string XiaoCommunityName { get; set; }
        public string XiaoCommunityNameeCode { get; set; }
        public string PointsEarned { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        //值班认领的时间范围
        public string DutyStartTime { get; set; }
        public string DutyEndTime { get; set; }
        public string ServiceContent { get; set; }
        public string status { get; set; }

        /// <summary>
        /// 分页
        /// </summary>
        public PageViewModel pageViewModel { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        NormalSearchViewModel()
        {
            pageViewModel = new PageViewModel();
        }

    }
}
