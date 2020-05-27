using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;

namespace Dto.IService.IntellVolunteer
{
    public interface ILoginService
    {
        //根据uid 获取用户的登录方式（参数：uid）
        UserLoginResModel GetUserLoginType(SearchByVIDModel uidViewModel);


        //获取所有登录方式（无参数）
        List<LoginType> GetLoginTypes();

        //根据uid判断是否已登录（参数：uid）验证用户是否登录
        BaseViewModel CheckUserLogin(SearchByVIDModel uidViewModel);

        //记录 用户选择登录方式（参数：uid）(更新操作)
        BaseViewModel SaveLoginTypeInfo(LoginTypeModel typeModel);

        //记录 用户选择登录方式（参数：uid）(默认人脸识别登录)
        BaseViewModel SaveLoginTypeInfo(string uid);

        //记录 用户登录 时间
        BaseViewModel SaveLoginInfo(string uid);

        //记录 用户插入阅读隐私政策记录（参数：openid） 
        BaseViewModel SaveV_ReadLog(string openid);

        //验证 用户是否 阅读隐私政策记录（参数：openid） 
        BaseViewModel CheckV_ReadLog(string openid);

        //记录 用户退出登录操作 时间（参数：uid）
        BaseViewModel UpdateLoginInfo(string uid);


 
    }
}
