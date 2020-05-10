using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Dto.IService.IntellWeChat;
using Dtol.Easydtol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Serilog;
using ViewModel.PublicViewModel;
using ViewModel.WeChatViewModel.MiddleModel;
using ViewModel.WeChatViewModel.RequestViewModel;
using ViewModel.WeChatViewModel.ResponseModel;

namespace IntellWeChat.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IOptions<WeChartTokenMiddles> _IOptions;
        private readonly IWeChatHttpClientService _weChatHttpClientService;

        private readonly ILoginService  _loginService;
        private readonly ILogger _ILogger;

        public LoginController(IWeChatHttpClientService weChatHttpClientService, IOptions<WeChartTokenMiddles> iOptions, ILoginService loginService, ILogger logger, IHttpClientFactory httpClientFactory)
        {
               _IOptions = iOptions;
               _loginService = loginService;
               _ILogger = logger;
               _httpClientFactory = httpClientFactory;
               _weChatHttpClientService = weChatHttpClientService;
        }
        /// <summary>
        /// 根据用户id查询用户信息
        /// </summary>
        /// <param name="weChatInfoViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<WeChatInfoResModel> Manage_WeChatLogin_Search(WeChatInfoViewModel  weChatInfoViewModel)
        {
            WeChatInfoResModel weChatInfoResModel = new WeChatInfoResModel();
            var UserSearchResult = _loginService.WeChatLogin_Search(weChatInfoViewModel);
            if(UserSearchResult.User_Rights.Count<1)
            {
                weChatInfoResModel.IsSuccess = false;
                weChatInfoResModel.baseViewModel.Message = "用户无权限登录";
                weChatInfoResModel.baseViewModel.ResponseCode = 400;
                _ILogger.Information("用户无权限，进入系统失败");
                return BadRequest(weChatInfoResModel);
            }
            else
            {
                weChatInfoResModel.userInfo = UserSearchResult;
                weChatInfoResModel.IsSuccess = true;
                weChatInfoResModel.baseViewModel.Message = "存在该用户，查询成功";
                weChatInfoResModel.baseViewModel.ResponseCode = 200;
                _ILogger.Information("查询用户信息，存在该用户，权限查询成功");
                return Ok(weChatInfoResModel);
            }
        }

        /// <summary>
        /// 根据用户账号和密码查询用户信息
        /// </summary>
        /// <param name="weChatLoginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<WeChatLoginResModel> Manage_WeChatLogin_User(WeChatLoginViewModel weChatLoginViewModel)
        {
            WeChatLoginResModel  weChatLoginResModel = new WeChatLoginResModel();
            var UserSearchResult = _loginService.WeChatLogin_User(weChatLoginViewModel);

            if (UserSearchResult == null)
            {
                weChatLoginResModel.IsSuccess = false;
                weChatLoginResModel.baseViewModel.Message = "用户名不存在或者密码错误";
                weChatLoginResModel.baseViewModel.ResponseCode = 400;
                _ILogger.Information("用户名不存在或者密码错误，进入系统失败");
                return BadRequest(weChatLoginResModel);
            }
            else
            {
                weChatLoginResModel.user_session = UserSearchResult;
                weChatLoginResModel.IsSuccess = true;
                weChatLoginResModel.baseViewModel.Message = "存在该用户，查询成功";
                weChatLoginResModel.baseViewModel.ResponseCode = 200;
                _ILogger.Information("查询用户信息，存在该用户，权限查询成功");
              
             return Ok(weChatLoginResModel);


            }
        }


        /// <summary>
        /// 根据用户账号和密码查询用户信息(旭旭专用)
        /// </summary>
        /// <param name="weChatLoginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<WeChatLoginResModel> Manage_XuXuLogin_User(WeChatLoginViewModel weChatLoginViewModel)
        {
            WeChatLoginResModel weChatLoginResModel = new WeChatLoginResModel();
            var UserSearchResult = _loginService.WeChatLogin_User(weChatLoginViewModel);

            if (UserSearchResult == null)
            {
                weChatLoginResModel.IsSuccess = false;
                weChatLoginResModel.baseViewModel.Message = "用户名不存在或者密码错误";
                weChatLoginResModel.baseViewModel.ResponseCode = 400;
                _ILogger.Information("用户名不存在或者密码错误，进入系统失败");
                return BadRequest(weChatLoginResModel);
            }
            else
            {
                weChatLoginResModel.user_session = UserSearchResult;
                weChatLoginResModel.IsSuccess = true;
                weChatLoginResModel.baseViewModel.Message = "存在该用户，查询成功";
                weChatLoginResModel.baseViewModel.ResponseCode = 200;
                weChatLoginResModel.tokenViewModel.code ="200";
                weChatLoginResModel.tokenViewModel.data  = "2728b712288da12fffd103af3bd616ff" ;

                _ILogger.Information("查询用户信息，存在该用户，权限查询成功");


                return Ok(weChatLoginResModel);

            }
        }
        
        /// <summary>
        /// 获取微信token
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<WeChatTokenResModel>> wechartTokenAsync()
        {
            var weChatTokenResModel =await _weChatHttpClientService.getWeChatTokenAsync();
            return Ok(weChatTokenResModel);
        }


        /// <summary>
        /// 获取公众号Token信息
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<string>> GetWeChartToken()
        {
            string ID = await _weChatHttpClientService.GetWeChartToken();
            return ID;
        }



        /// <summary>
        ///  (小程序端接口)  用户初次进入自愿者小程序验证用户是否是微官网已注册用户，如果是返回微官网用户中心ID，如果不是返回空
        /// </summary>
        [HttpPost]
        public  ActionResult<WeChatUserCheckResModel> CheckWeChartUserInfo(WeChatCodeModel codeModel)
        {
            WeChatUserCheckResModel resModel =  _weChatHttpClientService.GetWeChartInfo(codeModel);
            return resModel;
        }


        /// <summary>
        ///  (小程序端接口)  用户初次进入自愿者小程序验证用户是否是微官网已注册用户，如果是返回微官网用户中心信息，如果不是返回空
        /// </summary>
        [HttpPost]
        public ActionResult<WeChatWGWUserResModel> GetWeChartUserInfo(WeChatCodeModel codeModel)
        {
            WeChatWGWUserResModel resModel = _weChatHttpClientService.GetWeChartUserInfo(codeModel.code);
            return resModel;
        }



        /// <summary>
        ///  (小程序端接口) 20200510 Easy 用户初次进入自愿者小程序验证用户是否是泰便利注册用户，如果是返回泰便利用户中心信息，如果不是返回空   20200510 
        /// </summary>
        [HttpPost]
        public ActionResult<UserInfo> GetWeChartEasyUserInfo(WeChatCodeModel codeModel)
        {
            UserInfo resModel = _weChatHttpClientService.GetEasyUserInfo(codeModel.code);
            return resModel;
        }



        /// <summary>
        ///  (小程序端接口)  向用户推送消息接口
        /// </summary>
        [HttpPost]
        public async Task<BaseViewModel> SendMessage(SendMessageModel model)
        {
            BaseViewModel res = new BaseViewModel();
            res = await _weChatHttpClientService.SendMessageService(model);
            return res;
        }

    }
}