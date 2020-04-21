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
    public interface IVHelpAreaService
    {
        /// <summary>
        /// 根据条件查询互助信息 （互助标题Title、所属组织架构Community,互助地址Address,擅长技能Type）
        /// </summary>
        /// <param name="VHelpAreaSearchViewModel"></param>
        List<VHelpAreaSearchMiddle> VHelpArea_Search(AllSearchViewModel SearchViewModel);

        //互助ID标识ContentID、
        List<VHelpAreaSearchMiddle> GetVHelpAreaSearch(SearchByContentIDModel model);

        // 获取所有互助信息  （无参数）
        List<VHelpAreaSearchMiddle> VHelpArea_All();


        //验证是否 认领该互助信息
        string CheckSignAdd(string VID, string ContentID);


        //志愿者互助信息认领提交
        int SignAdd(VHA_SignAddViewModel AddViewModel);


        //志愿者上传互助信息处理结果
        int HandleAdd(VHA_HandleAddModel AddViewModel);


        //获取我的互助信息
        List<VHelpAreaSearchMiddle> GetMyAllInfo(string VID);


        //获取我的互助信息(志愿者ID、所属社区ID、活动类型ID、状态 等)
        List<VHelpAreaSearchMiddle> GetMyAllInfoByWhere(GetMyListViewModel vidModel);


        //获取本志愿者针对该互助信息 所有状态 （互助ID标识ContentID、志愿者唯一VID、擅长技能TypeID为空 ）
        BaseViewModel GetVHAStatus(string VID, string ContentID);


        //志愿者互助信息退出功能 （互助ID标识ContentID、志愿者唯一VID、擅长技能TypeID为空不传参数）
        string SetVHAStatusBack(string VID, string ContentID);


        //获取本志愿者针对该互助信息 上传结果信息 （互助ID标识ContentID、志愿者唯一VID、擅长技能TypeID为空不传参数 ）
        List<VHA_HandleGetMyResModel> GetMyHandleInfo(string VID, string ContentID);

        //验证是否是注册用户
        bool CheckInfos(string ID);

        //首页 志愿活动推荐( 志愿者VID )
        List<VHelpAreaSearchMiddle> GetMyRecommendList(string VID);

        //志愿者成功参与的 互助次数（只有最终审核处理结果通过才算成功）
        BaseViewModel GetMyHelpAreaNums(SearchByVIDModel vidModel);
    }
}
