using Dtol.dtol;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using System.Threading.Tasks;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.ResponseModel;

namespace Dto.IService.IntellVolunteer
{
    public interface IVolunteerActivityService
    {

        /// <summary>
        /// 查询活动信息
        /// </summary>
        /// <param name="VolunteerActivitySearchViewModel"></param>
        List<VolunteerActivitySearchMiddle> VolunteerActivity_Search(AllSearchViewModel SearchViewModel);


        //根据ID 获取 活动信息
        List<VolunteerActivitySearchMiddle> GetVolunteerActivity(SearchByContentIDModel model);

        /// <summary>
        /// 查询所有活动信息
        /// </summary>
        List<VolunteerActivitySearchMiddle> VolunteerActivity_All();


        /// <summary>
        ///   获取不同类型的活动数
        /// </summary>
        int GetTypeCounts(VolunteerActivityTypeModel typeModel);

        //志愿者活动信息报名提交
        int SignAdd(VA_SignAddViewModel AddViewModel);

        //获取 活动类型top4 
        List<VActivityTypeCountMiddle> GetTypeCounts();


        //志愿者活动  签到、签退接口
        int HandleAdd(VA_HandleAddViewModel AddViewModel);

        // 志愿者活动 活动中 上传现场服务照片
        int SubmitImg(VA_HandleImgAddModel AddViewModel);

        //获取我的活动信息
        List<VolunteerActivitySearchMiddle> GetMyAllInfo(string VID);


        //获取我的活动信息(志愿者ID、所属社区ID、活动类型ID、状态 等)
        List<VolunteerActivitySearchMiddle> GetMyAllInfoByWhere(GetMyListViewModel vidModel);


        //验证 是否 报名 同时段活动
        Task<string> CheckSigns(string VID, string ContentID);

        //验证 是否 报名 同时段活动
        string CheckSignsNew(string VID, string ContentID);

        //验证是否 报名该活动
        string CheckSignAdd(string VID, string ContentID);

        ////首页 签到 定位当前时段活动 返回活动ID
        string HomeGetContentID(string VID);

        /// 获取该志愿者针对该活动 签到签退信息 ( 活动ID,志愿者ID)
        VA_HandleGetMyResModel GetMyAHandleInfo(ContentIDandVIDModel model);


        BaseViewModel GetVAStatus(string VID, string ContentID);

        //首页 志愿活动推荐( 志愿者VID )
        List<VolunteerActivitySearchMiddle> GetMyRecommendList(string VID);


        //判断是否是注册用户
        bool CheckInfos(string ID);

        //志愿者取消 志愿活动报名( 志愿者VID ,活动ID)
        BaseViewModel CancelVASign(ContentIDandVIDModel AddViewModel);


        //志愿者成功参与的 志愿活动数（只有签到过才算成功）
        BaseViewModel GetMyActivityNums(SearchByVIDModel vidModel);

    }
}
