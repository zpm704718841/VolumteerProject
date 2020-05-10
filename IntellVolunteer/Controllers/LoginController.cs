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
using Dto.IService.IntellWeChat;
using SystemFilter.PublicFilter;
using ViewModel.VolunteerModel.RequsetModel;

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
        /// (小程序端接口)  记录 用户登录 时间(参数 uid)
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public ActionResult<BaseViewModel> SaveLoginInfo(SearchByVIDModel userInfo)
        {
            BaseViewModel baseView = new BaseViewModel();
            //baseView = _userInfoService.SaveLoginInfo(userInfo.uid);
            return baseView;
        }

    }
}
