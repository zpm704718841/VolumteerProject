﻿using Dtol.dtol;
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
        UserInfo WGWDecrypt(string code, string appId, string appSecret);

    }



}
