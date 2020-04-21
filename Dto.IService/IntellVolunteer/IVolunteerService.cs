using Dtol.dtol;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerBackground.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerBackground.MiddleModel;
using ViewModel.PublicViewModel;

namespace Dto.IService.IntellVolunteer
{
    public interface IVolunteerService
    {
        /// <summary>
        /// 注册志愿者
        /// </summary>
        /// <param name="VolunteerAddViewModel"></param>
        /// <returns></returns>
        int User_Add(VolunteerAddViewModel userAddViewModel);

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="VolunteerSearchViewModel"></param>
        List<VolunteerSearchMiddle> Volunteer_Search(VolunteerSearchViewModel userSearchViewModel);


        string GetNewVNO();

        //根据志愿者ID 获取志愿者信息
        VolunteerSearchMiddle SearchMiddle(string id);

        List<VolunteerSearchMiddle> Volunteer_SearchForBG(VolunteerInfoSearchViewModel VSearchViewModel);

        int GetAllCount(VolunteerInfoSearchViewModel searchModel);
        int ChangeVolunteer(VolunteerInfoUpdateViewModel updateViewModel);
        List<UserDepartSearchMidModel> GetDepartList();


        string CheckInfos(string ID);

        BaseViewModel CheckInfosNew(string ID);


        //获取已注册志愿者详细用户信息
        VolunteerAddViewModel GetMyInfos(SearchByVIDModel vidModel);

        /// 修改志愿者信息
        int User_Edit(VolunteerAddViewModel VuserAddViewModel);
    }
}
