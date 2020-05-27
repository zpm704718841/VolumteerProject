using AutoMapper;
using Dto.IRepository.IntellVolunteer;
using Dto.IService.IntellVolunteer;
using Dtol.dtol;
using Dtol.WGWdtol;
using System;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;
using ViewModel.PublicViewModel;
 
namespace Dto.Service.IntellVolunteer
{
    public class LoginService: ILoginService
    {
        private readonly IMapper _IMapper;
        private readonly ILoginType_LogRepository _loginType_Log;
        private readonly ILoginTypeRepository _loginTypeRepository;
        private readonly ILogger _ILogger;
        private readonly IUserLogin_LogRepository _userLogin_Log;
        private readonly IV_ReadLogRepository _v_ReadLog;
        private readonly ISQLRepository _sQLRepository;
        private readonly IVolunteerInfoRepository _volunteerInfoRepository;


        public LoginService(IMapper mapper, ILoginType_LogRepository loginType_Log, ILoginTypeRepository loginTypeRepository, 
            ILogger logger, IUserLogin_LogRepository userLogin, IV_ReadLogRepository v_Read, ISQLRepository sQLRepository,
            IVolunteerInfoRepository volunteerInfo)
        {
            _IMapper = mapper;
            _loginType_Log = loginType_Log;
            _loginTypeRepository = loginTypeRepository;
            _ILogger = logger;
            _userLogin_Log = userLogin;
            _v_ReadLog = v_Read;
            _sQLRepository = sQLRepository;
            _volunteerInfoRepository = volunteerInfo;
        }




        //记录 用户登录 时间（参数：uid）  
        public BaseViewModel SaveLoginInfo(string uid)
        {
            BaseViewModel baseView = new BaseViewModel();
            if (uid == "")
            {
                baseView.Message = "参数为空";
                baseView.ResponseCode = 2;
            }
            else
            {
                try
                {
                    UserLogin_Log userLogin = new UserLogin_Log();
                    userLogin.ID = Guid.NewGuid().ToString();
                    userLogin.uid = uid;
                    userLogin.Action = "登录系统";
                    userLogin.status = "true";
                    userLogin.CreateDate = DateTime.Now;
                    _userLogin_Log.Add(userLogin);
                    int a = _userLogin_Log.SaveChanges();
                    if (a > 0)
                    {
                        baseView.Message = "保存成功";
                        baseView.ResponseCode = 0;
                    }
                    else
                    {
                        baseView.Message = "保存失败";
                        baseView.ResponseCode = 1;
                    }
                }
                catch (Exception ex)
                {
                    baseView.Message = "出现异常";
                    baseView.ResponseCode = 3;
                    _ILogger.Information("记录用户登录时间出现异常" + ex.Message + ex.StackTrace + ex.Source);
                }
            }
            return baseView;
        }

        //根据uid 获取用户的登录方式（参数：uid）
        public UserLoginResModel GetUserLoginType(SearchByVIDModel uidViewModel)
        {
            UserLoginResModel viewModel = new UserLoginResModel();
            if (uidViewModel.VID == "")
            {
                _ILogger.Information("获取用户的登录方式参数为空");
                viewModel = null;
            }
            else
            {
                try
                {
                    LoginType_Log loginType_Log = _loginType_Log.SearchByUIDModel(uidViewModel);
                    //未选择 登录方式
                    if (loginType_Log == null)
                    {
                        viewModel = null;
                    }
                    else
                    {
                        //获取 该用户设置的登录方式 并计算有效时间
                        LoginType LoginType = _loginTypeRepository.SearchByIDModel(loginType_Log.typeid);

                        viewModel.uid = uidViewModel.VID;
                        viewModel.type = LoginType.type;
                        viewModel.typeid = LoginType.ID;
                        viewModel.days = LoginType.days;
                        viewModel.hours = LoginType.hours;

                    }
                }
                catch (Exception ex)
                {
                    _ILogger.Information("根据uid获取用户的登录方式出现异常" + ex.Message + ex.StackTrace + ex.Source);
                    viewModel = null;
                }
            }

            return viewModel;
        }


        //根据uid判断是否已登录（参数：uid）验证用户是否登录
        public BaseViewModel CheckUserLogin(SearchByVIDModel uidViewModel)
        {
            BaseViewModel viewModel = new BaseViewModel();
            if (uidViewModel.VID == "")
            {
                _ILogger.Information("验证用户是否登录参数为空");
                viewModel.ResponseCode = 2;
                viewModel.Message = "参数为空";
            }
            else
            {
                try
                {
                    LoginType_Log loginType_Log = _loginType_Log.SearchByUIDModel(uidViewModel);
                    //未选择 登录方式
                    if (loginType_Log == null)
                    {
                        viewModel.ResponseCode = 1;
                        viewModel.Message = "未登录";
                    }
                    else
                    {
                        //获取 该用户设置的登录方式 并计算有效时间
                        LoginType LoginType = _loginTypeRepository.SearchByIDModel(loginType_Log.typeid);
                        //获取 用户最新的一次登录记录 20200402
                        var log = _userLogin_Log.GetUserLogin(uidViewModel.VID);
                        if (log == null)
                        {
                            viewModel.ResponseCode = 1;
                            viewModel.Message = "未登录";
                        }
                        else
                        {
                            DateTime lasttime = DateTime.Parse(log.CreateDate.ToString());
                            TimeSpan ts1 = new TimeSpan(lasttime.Ticks);
                            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
                            TimeSpan ts = ts2.Subtract(ts1);

                            //判断 登录时间是否超时
                            //不通登录方式 登录保持时长（单位:小时）
                            int hours = int.Parse(LoginType.hours) * int.Parse(LoginType.days);
                            //上次登录时间 加上 登录保持时长 小于当前时间 说明已经登录超时
                            if (lasttime.AddHours(hours) < DateTime.Now)
                            {
                                viewModel.ResponseCode = 1;
                                viewModel.Message = "未登录";
                                log.status = "false";
                                log.UpdateDate = DateTime.Now;
                                log.bak1 = "LoginTimeout";

                                _userLogin_Log.Update(log);
                                _userLogin_Log.SaveChanges();
                            }
                            else
                            {
                                //获取 用的 审核状态 20200426
                                var userinfo = _volunteerInfoRepository.SearchInfoByID(uidViewModel.VID);
                                if (userinfo != null)
                                {
                     
                                    if (userinfo.Status == "1")
                                    {
                                        viewModel.ResponseCode = 0;
                                        viewModel.Message = "已登录";
                                    }
                                    if (userinfo.Status == "0")
                                    {
                                        viewModel.ResponseCode = 5;
                                        viewModel.Message = "待审核用户已登录";
                                    }
                                    if (userinfo.Status == "3")
                                    {
                                        viewModel.ResponseCode = 6;
                                        viewModel.Message = "审核不通过用户已登录";
                                    }
                                }
                                else
                                {
                                    viewModel.ResponseCode = 1;
                                    viewModel.Message = "未登录";
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    _ILogger.Information("根据uid获取用户的登录方式出现异常" + ex.Message + ex.StackTrace + ex.Source);
                    viewModel.ResponseCode = 3;
                    viewModel.Message = "出现异常";
                }
            }

            return viewModel;
        }

        //记录 用户选择登录方式（参数：uid）(默认人脸识别登录)
        public BaseViewModel SaveLoginTypeInfo(string uid)
        {
            BaseViewModel baseView = new BaseViewModel();
            if (uid == "")
            {
                baseView.Message = "参数为空";
                baseView.ResponseCode = 2;
            }
            else
            {
                try
                {
                    //获取人脸登录方式  默认该用户选用人脸识别登录
                    LoginType LoginType = _loginTypeRepository.SearchFaceModel();

                    LoginType_Log log = new LoginType_Log();
                    log.ID = Guid.NewGuid().ToString();
                    log.uid = uid;
                    log.typeid = LoginType.ID;
                    log.status = "true";
                    log.CreateDate = DateTime.Now;
                    _loginType_Log.Add(log);
                    int a = _loginType_Log.SaveChanges();
                    if (a > 0)
                    {
                        baseView.Message = "保存成功";
                        baseView.ResponseCode = 0;
                    }
                    else
                    {
                        baseView.Message = "保存失败";
                        baseView.ResponseCode = 1;
                    }
                }
                catch (Exception ex)
                {
                    baseView.Message = "出现异常";
                    baseView.ResponseCode = 3;
                    _ILogger.Information("记录用户选择登录方式出现异常" + ex.Message + ex.StackTrace + ex.Source);
                }
            }
            return baseView;
        }


        //记录 用户选择登录方式（参数：uid）(更新操作)
        public BaseViewModel SaveLoginTypeInfo(LoginTypeModel typeModel)
        {
            _ILogger.Information("记录用户选择登录方式参数typeid=" + typeModel.typeid);
            BaseViewModel baseView = new BaseViewModel();
            if (typeModel.uid == "" || typeModel.typeid == "")
            {
                baseView.Message = "参数为空";
                baseView.ResponseCode = 2;
            }
            else
            {
                try
                {
                    SearchByVIDModel uID = new SearchByVIDModel();
                    uID.VID = typeModel.uid;
                    LoginType_Log loginType_Log = _loginType_Log.SearchByUIDModel(uID);
                    //未选择 登录方式
                    if (loginType_Log == null)
                    {
                        //获取人脸登录方式  默认该用户选用人脸识别登录
                        LoginType LoginType = _loginTypeRepository.SearchFaceModel();

                        LoginType_Log log = new LoginType_Log();
                        log.ID = Guid.NewGuid().ToString();
                        log.uid = typeModel.uid;
                        log.typeid = LoginType.ID;
                        log.status = "true";
                        log.CreateDate = DateTime.Now;
                        _loginType_Log.Add(log);
                        int a = _loginType_Log.SaveChanges();
                        if (a > 0)
                        {
                            baseView.Message = "保存成功";
                            baseView.ResponseCode = 0;
                        }
                        else
                        {
                            baseView.Message = "保存失败";
                            baseView.ResponseCode = 1;
                        }
                    }
                    else
                    {
                        //更新 原登录方式为 无效 
                        loginType_Log.status = "false";
                        loginType_Log.UpdateDate = DateTime.Now;
                        _loginType_Log.Update(loginType_Log);
                        int a = _loginType_Log.SaveChanges();
                        if (a > 0)
                        {
                            //保存新的登录方式
                            LoginType_Log log2 = new LoginType_Log();
                            log2.ID = Guid.NewGuid().ToString();
                            log2.uid = typeModel.uid;
                            log2.typeid = typeModel.typeid;
                            log2.status = "true";
                            log2.CreateDate = DateTime.Now;
                            _loginType_Log.Add(log2);
                            _loginType_Log.SaveChanges();
                            baseView.Message = "保存成功";
                            baseView.ResponseCode = 0;
                        }
                        else
                        {
                            baseView.Message = "保存失败";
                            baseView.ResponseCode = 1;
                        }

                    }

                }
                catch (Exception ex)
                {
                    baseView.Message = "出现异常";
                    baseView.ResponseCode = 3;
                    _ILogger.Information("记录用户选择登录方式出现异常" + ex.Message + ex.StackTrace + ex.Source);
                }
            }
            return baseView;
        }

        //获取所有登录方式（无参数）
        public List<LoginType> GetLoginTypes()
        {
            try
            {
                List<LoginType> Infos = _loginTypeRepository.GetList();
                return Infos;
            }
            catch (Exception ex)
            {
                _ILogger.Information("查询登录方式（无参数）出现异常" + ex.Message + ex.StackTrace + ex.Source);
                return null;
            }
        }



        //记录 用户插入阅读隐私政策记录（参数：openid） 
        public BaseViewModel SaveV_ReadLog(string openid)
        {
            BaseViewModel baseView = new BaseViewModel();
            if (openid == "")
            {
                baseView.Message = "参数为空";
                baseView.ResponseCode = 2;
            }
            else
            {
                try
                {
                    V_ReadLog log = new V_ReadLog();
                    //log.id = Guid.NewGuid().ToString();
                    log.openid = openid;
                    log.type = "policy";
                    log.CreateDate = DateTime.Now;
                    _v_ReadLog.Add(log);
                    int a = _v_ReadLog.SaveChanges();
                    if (a > 0)
                    {
                        baseView.Message = "保存成功";
                        baseView.ResponseCode = 0;
                    }
                    else
                    {
                        baseView.Message = "保存失败";
                        baseView.ResponseCode = 1;
                    }
                }
                catch (Exception ex)
                {
                    baseView.Message = "出现异常";
                    baseView.ResponseCode = 3;
                    _ILogger.Information("插入阅读隐私政策记录出现异常" + ex.Message + ex.StackTrace + ex.Source);
                }
            }
            return baseView;
        }


        //验证 用户是否 阅读隐私政策记录（参数：openid） 
        public BaseViewModel CheckV_ReadLog(string openid)
        {
            BaseViewModel baseView = new BaseViewModel();
            if (openid == "")
            {
                baseView.Message = "参数为空";
                baseView.ResponseCode = 2;
            }
            else
            {
                try
                {
                    //验证 用户是否 阅读隐私政策记录
                    V_ReadLog log = _v_ReadLog.GetReadLog(openid, "policy");

                    if (log != null)
                    {
                        baseView.Message = "我已阅读";
                        baseView.ResponseCode = 0;
                    }
                    else
                    {
                        baseView.Message = "我未阅读";
                        baseView.ResponseCode = 1;
                    }
                }
                catch (Exception ex)
                {
                    baseView.Message = "出现异常";
                    baseView.ResponseCode = 3;
                    _ILogger.Information("验证是否阅读隐私政策出现异常" + ex.Message + ex.StackTrace + ex.Source);
                }
            }
            return baseView;
        }


        //记录 用户退出登录操作 时间（参数：uid）
        public BaseViewModel UpdateLoginInfo(string uid)
        {
            BaseViewModel baseView = new BaseViewModel();
            if (uid == "")
            {
                baseView.Message = "参数为空";
                baseView.ResponseCode = 2;
            }
            else
            {
                try
                {
                    //获取 用户最新的一次登录记录 20200402
                    var log = _userLogin_Log.GetUserLogin(uid);
                    if (log != null)
                    {
                        log.status = "false";
                        log.UpdateDate = DateTime.Now;
                        log.bak1 = "Exit";
                        _userLogin_Log.Update(log);
                        int a = _userLogin_Log.SaveChanges();
                        if (a > 0)
                        {
                            baseView.Message = "操作成功";
                            baseView.ResponseCode = 0;
                        }
                        else
                        {
                            baseView.Message = "操作失败";
                            baseView.ResponseCode = 1;
                        }
                    }
                    else
                    {
                        baseView.Message = "您尚未登录";
                        baseView.ResponseCode = 4;
                    }
                }
                catch (Exception ex)
                {
                    baseView.Message = "出现异常";
                    baseView.ResponseCode = 3;
                    _ILogger.Information("记录用户登录时间出现异常" + ex.Message + ex.StackTrace + ex.Source);
                }
            }
            return baseView;
        }

    }
}
