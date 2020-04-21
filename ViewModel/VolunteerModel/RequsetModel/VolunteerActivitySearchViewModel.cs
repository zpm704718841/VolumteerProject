using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;

namespace ViewModel.VolunteerModel.RequsetModel
{
    public class VolunteerActivitySearchViewModel
    {
        /// <summary>
        /// 志愿活动ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 志愿活动名称（标题）
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 所属组织架构ID
        /// </summary>
        public string CommunityID { get; set; }

        /// <summary>
        /// 活动地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 活动类型ID
        /// </summary>
        public string TypeIDs { get; set; }

    }
}
