using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;

namespace ViewModel.VolunteerModel.RequsetModel
{
    public partial  class VolunteerAddViewModel
    {
        /// <summary>
        /// 志愿者ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 志愿者编号
        /// </summary>
        public string VNO { get; set; }
        /// <summary>
        /// 志愿者姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public string CertificateType { get; set; }
        /// <summary>
        /// 证件编号
        /// </summary>
        public string CertificateID { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string Political { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 所属社区id
        /// </summary>
        public string CommunityID { get; set; }
        /// <summary>
        /// 所属社区
        /// </summary>
        public string Community { get; set; }
        /// <summary>
        /// 所属街道ID
        /// </summary>
        public string SubdistrictID { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public string Subdistrict { get; set; }
        /// <summary>
        /// 所属单位ID
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        /// 所属单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string Degree { get; set; }
        /// <summary>
        /// 婚姻情况
        /// </summary>
        public string Marriage { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        public string Career { get; set; }
        /// <summary>
        /// 设置服务时间
        /// </summary>
        public string ServiceTime { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string PhotoUrl { get; set; }
        /// <summary>
        /// 微信openID
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// 微信UnionID
        /// </summary>
        public string unionid { get; set; }
        /// <summary>
        /// 微信头像
        /// </summary>
        public string Headimgurl { get; set; }

        /// <summary>
        /// 数据来源:直接注册、接口对接 等
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 创建人员ID
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 创建人员时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        public List<Volunteer_Relate_TypeMiddle> RelateUserIDandTypeIDList { get; set; }

        public List<VAttachmentAddViewModel> VAttachmentAddList { get; set; }
    }
}
