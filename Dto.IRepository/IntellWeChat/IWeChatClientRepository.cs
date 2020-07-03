using Dtol.dtol;
using Dtol.WGWdtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using ViewModel.WeChatViewModel.RequestViewModel;
using ViewModel.WeChatViewModel.MiddleModel;
using ViewModel.WeChatViewModel.ResponseModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;

namespace Dto.IRepository.IntellWeChat
{
    public interface IWeChatClientRepository : IRepository<V_GetToken>
    {
        string getToken();

        string GetOpenIdAndSessionKeyString(string code, string appId, string appSecret);

        WeChatUserCheckResModel Decrypt(WeChatCodeModel codeModel, string appId, string appSecret);

        /// <summary>
        /// 用户初次进入自愿者小程序验证用户是否是微官网已注册用户，如果是返回微官网用户中心信息，如果不是返回空
        /// </summary>
        Dtol.WGWdtol.UserInfo WGWDecrypt(string code, string appId, string appSecret);

        /// <summary>
        /// 用户初次进入自愿者小程序验证用户是否是泰便利已注册用户，如果是返回泰便利用户中心信息，如果不是返回空  20200510
        /// </summary>
        Dtol.Easydtol.UserInfo EasyDecrypt(string code, string appId, string appSecret);


        /// <summary>  
        /// 20200629 用户授权 通过解密获取unionid  再查询是否是泰便利注册用户 
        /// </summary>  

        UserInfoResModel EasyDecryptByDE(WeChatCodeDEModel codeModel, string appId, string appSecret);


        /// <summary>
        /// 20200629  用户初次进入自愿者小程序验证用户是否是泰便利已注册用户，如果是返回泰便利用户中心信息，如果不是返回空  20200510
        /// </summary>
        UserInfoResModel EasyDecryptByCode(string code, string appId, string appSecret);
    }



}
