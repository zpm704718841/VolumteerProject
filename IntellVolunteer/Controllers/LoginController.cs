using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Serilog;
using ViewModel.PublicViewModel;
using Dto.IService.IntellVolunteer;
using SystemFilter.PublicFilter;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;
using Dtol.Easydtol;
using ViewModel.WeChatViewModel.RequestViewModel;
using ViewModel.WeChatViewModel.MiddleModel;
using System.Net.Http;

namespace IntellVolunteer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        //private readonly ILogger _ILogger;
        private IOptions<WeChartTokenMiddles> _IOptions;
        private readonly IWeChatHttpClientService _weChatHttpClientService;
        //20200510

        public LoginController(ILoginService  loginService,  IOptions<WeChartTokenMiddles> iOptions,
            IWeChatHttpClientService weChatHttpClientService, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _IOptions = iOptions;
            _loginService = loginService;
            //_ILogger = logger;
            _weChatHttpClientService = weChatHttpClientService;
        }




        /// <summary>
        ///  (小程序端接口) 20200514 根据code 获取openid
        /// </summary>
        [HttpPost]
        public ActionResult<OpenidViewModel> GetWeChartOpenid(WeChatCodeModel codeModel)
        {
            OpenidViewModel resModel = _weChatHttpClientService.GetWeChartOpenid(codeModel.code);
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
        ///  (小程序端接口) 20200629 用户授权 通过解密获取unionid  再查询是否是泰便利注册用户
        /// </summary>
        [HttpPost]
        public ActionResult<UserInfoResModel> GetWeChartUserInfoByDE(WeChatCodeDEModel codeModel)
        {
            UserInfoResModel resModel = _weChatHttpClientService.GetWeChartUserInfoByDE(codeModel);
            return resModel;
        }




        /// <summary>
        ///  (小程序端接口) 20200629 用户初次进入自愿者小程序  判断是否能获取unionid，如果有unionid验证用户是否是泰便利注册用户，如果是返回泰便利用户中心信息，如果不是返回空 
        /// </summary>
        [HttpPost]
        public ActionResult<UserInfoResModel> GetWeChartEasyUserInfoNew(WeChatCodeModel codeModel)
        {
            UserInfoResModel resModel = _weChatHttpClientService.GetEasyUserInfoByCode(codeModel.code);
            return resModel;
        }



        /// <summary>
        /// (小程序端接口)  获取该用户登录方式（参数：uid）
        /// </summary>
        /// <param name="uidViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public ActionResult<UserLoginResModel> GetUserLoginType(SearchByVIDModel uidViewModel)
        {
            UserLoginResModel loginResModel = new UserLoginResModel();
            loginResModel = _loginService.GetUserLoginType(uidViewModel);
            return loginResModel;
        }



        /// <summary>
        /// (小程序端接口)  验证用户是否登录（参数：uid）
        /// </summary>
        /// <param name="uidViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public ActionResult<BaseViewModel> CheckUserLogin(SearchByVIDModel uidViewModel)
        {
            BaseViewModel loginResModel = new BaseViewModel();
            loginResModel = _loginService.CheckUserLogin(uidViewModel);
            return loginResModel;
        }


        /// <summary>
        /// (小程序端接口)  获取所有登录方式（无参数）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public ActionResult<LoginTypesResModel> GetLoginTypes()
        {
            LoginTypesResModel loginResModel = new LoginTypesResModel();
            loginResModel.List = _loginService.GetLoginTypes();
            loginResModel.isSuccess = true;
            loginResModel.TotalNum = loginResModel.List.Count;

            return loginResModel;
        }


        /// <summary>
        /// (小程序端接口)  记录 用户登录方式 (参数 uid)
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public ActionResult<BaseViewModel> SaveLoginTypeInfo(LoginTypeModel userInfo)
        {
            BaseViewModel baseView = new BaseViewModel();
            baseView = _loginService.SaveLoginTypeInfo(userInfo);
            return baseView;
        }


        /// <summary>
        /// (小程序端接口)  用户退出登录(参数 uid)
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public ActionResult<BaseViewModel> ExitInfo(SearchByVIDModel userInfo)
        {
            BaseViewModel baseView = new BaseViewModel();
            baseView = _loginService.UpdateLoginInfo(userInfo.VID);
            return baseView;
        }



        /// <summary>
        /// (小程序端接口)  记录 用户登录 时间(参数 uid)
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public ActionResult<BaseViewModel> SaveLoginInfo(SearchByVIDModel userInfo)
        {
            BaseViewModel baseView = new BaseViewModel();
            baseView = _loginService.SaveLoginInfo(userInfo.VID);
            return baseView;
        }


        /// <summary>
        /// 验证 用户是否 阅读隐私政策记录（参数：openid） 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public ActionResult<BaseViewModel> CheckV_ReadLog(OpenidViewModel userInfo)
        {
            BaseViewModel baseView = new BaseViewModel();
            baseView = _loginService.CheckV_ReadLog(userInfo.openid);
            return baseView;
        }


        /// <summary>
        ///记录 用户插入阅读隐私政策记录（参数：openid）
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public ActionResult<BaseViewModel> SaveV_ReadLog(OpenidViewModel userInfo)
        {
            BaseViewModel baseView = new BaseViewModel();
            baseView = _loginService.SaveV_ReadLog(userInfo.openid);
            return baseView;
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
