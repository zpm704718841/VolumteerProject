using Dtol.dtol;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;
using ViewModel.PublicViewModel;

namespace Dto.IService.IntellVolunteer
{
    public interface IVActivity_PublicShowService
    {
        //志愿者参与的志愿活动（已结束和进行中）(必须有签到记录) 参数 志愿者VID
        List<VolunteerActivitySearchMiddle> GetMyAllActivity(string VID);

        //(小程序端接口) 志愿者上传 公益秀（志愿活动ID，活动标题，活动地址，参与感受，图片列表 等等）
        VActivity_PublicShowAddResModel AddPublicShow(VActivity_PublicShowAddModel  showAddModel);

        //展示公所有益秀信息（志愿活动ID，活动标题，活动地址，参与感受，图片列表 等等）
        List<VActivity_PublicShowMiddle> GetPublicShowList();

        //获取我的益秀信息（志愿活动ID，活动标题，活动地址，参与感受，图片列表 等等）
        List<VActivity_PublicShowMiddle> GetMyPublicShowList(string VID);


        //志愿者针对一条公益秀点赞   参数志愿者VID，公益秀ID
        BaseViewModel PublicShow_GiveLike(PublicShowIDandVID showDandVid);

        //志愿者针对一条公益秀 取消点赞   参数志愿者VID，公益秀ID
        BaseViewModel PublicShow_CancelLike(PublicShowIDandVID showDandVid);

        //志愿者删除该公益秀（自己发布的）   参数志愿者VID，公益秀ID
        BaseViewModel PublicShow_Delete(PublicShowIDandVID showDandVid);

        //验证该志愿者是否 已经点赞该公益秀 20200608
        BaseViewModel CheckIsGiveLike(PublicShowIDandVID showIDandVID);
    }
}
