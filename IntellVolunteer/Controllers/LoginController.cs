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

namespace IntellVolunteer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        private readonly ILoginService  _loginService;
        private readonly ILogger _ILogger;
        //20200510

        public LoginController(ILoginService  loginService, ILogger logger)
        {
            _loginService = loginService;
            _ILogger = logger;
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


    }
}
