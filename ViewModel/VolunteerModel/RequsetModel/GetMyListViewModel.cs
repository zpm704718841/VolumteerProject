using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;


namespace ViewModel.VolunteerModel.RequsetModel
{
    public class GetMyListViewModel
    {

        /// <summary>
        /// 志愿者ID
        /// </summary>
        public string VID { get; set; }


        /// <summary>
        /// 所属组织架构ID
        /// </summary>
        public string CommunityID { get; set; }

        /// <summary>
        /// 活动类型ID
        /// </summary>
        public string TypeIDs { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        public string Type{ get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }
}
