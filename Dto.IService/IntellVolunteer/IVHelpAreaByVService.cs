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
    public interface IVHelpAreaByVService
    {
        //社区居民上传互助信息（名称、内容、所需擅长技能、姓名、联系方式、详细地址、可得积分）
        BaseViewModel AddHelpArea(VHelpAreaSearchMiddle area);

        //首页 发布互助入口（验证是否是区内用户，区外用户不能发布）
        BaseViewModel CheckRights(SearchByVIDModel model);

        //获取我发布的 互助信息
        List<VHelpAreaSearchMiddle> VHelpArea_MyAll(string VID, string status);

        // 获取 查看该互助信息对应的认领人列表  
        PublishHelpAreaResModel GetVList(string ContentID);

        // 获取 查看该互助信息对应的认领人 的具体信息 
        VHA_SignInfoMiddle GetVDetail(string VID);


        // 发布者 确认该志愿者为 认领人（其他人 状态为审核不通过）
        BaseViewModel SetVHAStatus(string VID, string ContentID);


        //查看 认领人上传的 处理结果信息 
        List<VHA_HandleGetMyResModel>  GetMyHandleInfo(string VID, string ContentID);

        //发布者审核 认领人上传的 处理结果信息    审核通过任务完结
        BaseViewModel SetSignPass(ContentIDandVIDModel model);



        //发布者审核 认领人上传的 处理结果信息   审核不通过，志愿者重新上传处理结果
        BaseViewModel SetSignReturn(ContentIDandVIDModel model);


        //发布者审核 认领人上传的 处理结果信息   退回至互助信息，志愿者可重新认领
        BaseViewModel SetSignBack(ContentIDandVIDModel model);


        // (小程序端接口) 认领人针对本次认领任务的所有状态
        BaseViewModel GetVHAByVStatus(ContentIDandVIDModel model);


        // 获取 查看该互助信息对应的认领人 上传结果列表 
        VHA_Sign GetVHandleList(string ContentID, string status);

        // (小程序端接口) 获取该互助信息的所有状态
        BaseViewModel GetVHAStatusAll(SearchByContentIDModel model);

    }
}
