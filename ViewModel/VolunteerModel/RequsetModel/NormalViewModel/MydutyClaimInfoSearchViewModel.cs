using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;

namespace ViewModel.VolunteerModel.RequsetModel.NormalViewModel
{
    public class MydutyClaimInfoSearchViewModel
    {

        public String Userid { get; set; }
        public String UserName { get; set; }
        public String StartDutyTime { get; set; }
        public String EndDutyTime { get; set; }
        public string status { get; set; }


        public String CreateDate { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public PageViewModel pageViewModel { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        MydutyClaimInfoSearchViewModel()
        {
            pageViewModel = new PageViewModel();
        }

    }
}
