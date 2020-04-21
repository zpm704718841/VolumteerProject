using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;

namespace ViewModel.VolunteerModel.MiddleModel
{
    public class VHA_SignInfoMiddle
    {
        //志愿者ID
        public string VID { get; set; }
        //志愿者姓名
        public string Name { get; set; }
        //志愿者编号
        public string VNO { get; set; }
        //志愿者所在社区
        public string Community { get; set; }
        //志愿者手机号
        public string Mobile { get; set; }
        // 服务领域
        public string Services { get; set; }
        //擅长技能 
        public string Skills { get; set; }
        //志愿者资质证书
        public List<VAttachmentAddViewModel> VAttachmentAddList { get; set; }
        //志愿者资质证书 (含有绑定关系)
        public List<SkillAndFileViewModel> SkillandFilelist { get; set; }
        //参与志愿活动次数
        public string VA_SignTimes { get; set; }
        //参与志愿活动时长
        public string VA_SignHours { get; set; }
        //注册时间
        public string RegisteTime { get; set; }
        //认领时间
        public DateTime Signtime { get; set; }

        public List<VHA_SignMyListMiddle> MyVHASignList { get; set; }
    }
}
