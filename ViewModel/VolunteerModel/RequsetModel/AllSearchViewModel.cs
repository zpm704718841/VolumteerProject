using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerModel.RequsetModel
{
    public class AllSearchViewModel
    {
        /// <summary>
        /// 志愿者ID
        /// </summary>
        public string VID { get; set; }

        /// <summary>
        /// 志愿活动名称（标题）
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 所属组织架构
        /// </summary>
        public string Community { get; set; }
        /// <summary>
        /// 所属组织架构
        /// </summary>
        public string CommunityID { get; set; }

        /// <summary>
        /// 活动地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 擅长技能
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 擅长技能ID
        /// </summary>
        public string TypeID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }
}
