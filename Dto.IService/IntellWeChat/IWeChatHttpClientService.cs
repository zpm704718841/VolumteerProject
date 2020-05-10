using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.WeChatViewModel.MiddleModel;
using ViewModel.WeChatViewModel.ResponseModel;
using ViewModel.WeChatViewModel.RequestViewModel;
using ViewModel.PublicViewModel;
using Dtol.Easydtol;

namespace Dto.IService.IntellWeChat
{
    public interface IWeChatHttpClientService
    {
        /// <summary>
        /// 获取token
        /// </summary>
        Task<WeChatTokenResModel> getWeChatTokenAsync();

        /// <summary>
        /// 
        /// </summary>
        Task<string> GetWeChartToken();

        /// <summary>
        /// 用户初次进入自愿者小程序验证用户是否是微官网已注册用户，如果是返回微官网用户中心ID，如果不是返回空
        /// </summary>
        WeChatUserCheckResModel GetWeChartInfo(WeChatCodeModel codeModel);

        /// <summary>
        /// 用户初次进入自愿者小程序验证用户是否是微官网已注册用户，如果是返回微官网用户中心信息，如果不是返回空
        /// </summary>
        WeChatWGWUserResModel GetWeChartUserInfo(string code);


        /// <summary>
        /// 20200510 Easy 用户初次进入自愿者小程序验证用户是否是泰便利注册用户，如果是返回泰便利用户中心信息，如果不是返回空  
        /// </summary>
        UserInfo GetEasyUserInfo(string code);

        int AddToken(V_GetTokenAddModel addModel);


        Task<BaseViewModel> SendMessageService(SendMessageModel model);

        string GetPage(string posturl, string postData);
    }
}
