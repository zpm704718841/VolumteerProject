using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AuthentValitor.AuthentModel;
using AuthentValitor.AuthHelper;
using Dto.IService.IntellUser;
using Dto.IService.IntellVolunteerBackground;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using ViewModel.VolunteerBackground.MiddleModel;
using ViewModel.VolunteerBackground.RequsetModel;
using ViewModel.VolunteerBackground.ResponseModel;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntellVolunteerBackground.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IOptions<VolunteerBackgroundTokenModel> _IOptions;
        private readonly IIntellVolunteerBackgroundHttpClientService _weChatHttpClientService;

        private readonly IIntellVolunteerBackgroundILoginService _loginService;
        //private readonly ILogger _ILogger;

        public LoginController(IIntellVolunteerBackgroundHttpClientService weChatHttpClientService, 
            IOptions<VolunteerBackgroundTokenModel> iOptions, 
            IIntellVolunteerBackgroundILoginService loginService,   IHttpClientFactory httpClientFactory)
        {
            _IOptions = iOptions;
            _loginService = loginService;
            //_ILogger = logger;
            _httpClientFactory = httpClientFactory;
            _weChatHttpClientService = weChatHttpClientService;
        }


        // POST api/<controller>
        [HttpPost]
       public ActionResult<LoginResModel> Manage_BackgroundLogin(LoginViewModel loginModel)
        {
            LoginResModel model = new LoginResModel();
            var UserSearchResult = _loginService.VolunteerBackgroundLogin_User(loginModel);
            if (UserSearchResult == null)
            {
                model.IsSuccess = false;
                model.baseViewModel.Message = "用户名不存在或者密码错误";
                model.baseViewModel.ResponseCode = 400;
                //_ILogger.Information("用户名不存在或者密码错误，进入系统失败");
                return BadRequest(model);
            }
            else
            {
                TokenModelJwt tokenModel = new TokenModelJwt();
                tokenModel.Uid = Convert.ToInt32( UserSearchResult.Id);
                tokenModel.Role = UserSearchResult.UserName;
                string token = JwtHelper.IssueJwt(tokenModel);

                model.user_session = UserSearchResult;
                model.IsSuccess = true;
                model.baseViewModel.Message = "存在该用户，查询成功";
                model.baseViewModel.ResponseCode = 200;
                model.token = token;
                //_ILogger.Information("查询用户信息，存在该用户，权限查询成功");

                return Ok(model);


            }


        }

        [HttpPost]
        public ActionResult<VloginInfoResModel> Manage_LoginSearchInfo(VolunteerBackgroundInfoViewModel viewModel)
        {
            VloginInfoResModel resultModel = new VloginInfoResModel();
            var userInfo = _loginService.IntellVolunteerBackgroundLogin_Search(viewModel);
            if (userInfo.User_Rights.Count < 1)
            {
                resultModel.IsSuccess = false;
                resultModel.baseViewModel.Message = "用户无权限登录";
                resultModel.baseViewModel.ResponseCode = 400;
                //_ILogger.Information("用户无权限，进入系统失败");
                return BadRequest(resultModel);
            }
            else
            {
                resultModel.userInfo = userInfo;
                resultModel.IsSuccess = true;
                resultModel.baseViewModel.Message = "存在该用户，查询成功";
                resultModel.baseViewModel.ResponseCode = 200;
                //_ILogger.Information("查询用户信息，存在该用户，权限查询成功");
                return Ok(resultModel);
            }
        }


    }
}
