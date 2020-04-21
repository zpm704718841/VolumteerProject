using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.IService.IntellVolunteer;
using Dtol.dtol;
using Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemFilter.PublicFilter;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;
using System.IO;
using ViewModel.VolunteerBackground.RequsetModel;
using ViewModel.VolunteerBackground.ResponseModel;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace IntellVolunteer.Controllers
{
    [Route("VolunteerManageApi/[controller]/[action]")]
    [ApiController]
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteerService _Volunteerervice;
        private readonly IVolunteer_MessageService _MessageService;
        private readonly ILogger _ILogger;



        public VolunteerController(IVolunteerService volunteerervice, IVolunteer_MessageService messageService)
        {
            _MessageService = messageService;
            _Volunteerervice = volunteerervice;
        }
        /// <summary>
        /// (小程序端接口)  志愿者注册 基本信息(AI 泰达返回信息) 和 完善信息 （擅长技能（资质照片）、服务领域、服务时间等）
        /// </summary>
        /// <param name="VolunteerAddViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public ActionResult<VolunteerAddResModel> Volunteer_add(VolunteerAddViewModel userAddViewModel)
        {

            int User_Add_Count;
            VolunteerAddResModel userAddResModel = new VolunteerAddResModel();
            User_Add_Count = _Volunteerervice.User_Add(userAddViewModel);
            if (User_Add_Count > 0)
            {
                //提示信息：已完善个人信息，等待审核 
                Volunteer_MessageMiddle middle = new Volunteer_MessageMiddle();
                middle.Contents = "您已完善个人信息，等待审核。";
                middle.Name = userAddViewModel.Name;
                middle.VID = userAddViewModel.ID;
                middle.VNO = userAddViewModel.VNO;
                BaseViewModel res = new BaseViewModel();
                res = _MessageService.AddMessages(middle);
 
                userAddResModel.IsSuccess = true;
                userAddResModel.AddCount = User_Add_Count;
                userAddResModel.baseViewModel.Message = "添加成功";
                userAddResModel.baseViewModel.ResponseCode = 200;
                return Ok(userAddResModel);
            }
            else
            {
                userAddResModel.IsSuccess = false;
                userAddResModel.AddCount = 0;
                userAddResModel.baseViewModel.Message = "添加失败";
                userAddResModel.baseViewModel.ResponseCode = 200;
                return Ok(userAddResModel);
            }
        }



        /// <summary>
        /// (小程序端接口)  查询用户信息 （志愿者编号VNO、志愿者姓名Name、证件编号CertificateID、手机号Mobile）
        /// </summary>
        /// <param name="VolunteerSearchViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VolunteerSearchResModel> Manage_User_Search(VolunteerSearchViewModel userSearchViewModel)
        {
            VolunteerSearchResModel userSearchResModel = new VolunteerSearchResModel();
            var UserSearchResult = _Volunteerervice.Volunteer_Search(userSearchViewModel);

            userSearchResModel.Volunteer_Infos = UserSearchResult;
            userSearchResModel.isSuccess = true;
            userSearchResModel.baseViewModel.Message = "查询成功";
            userSearchResModel.baseViewModel.ResponseCode = 200;
            userSearchResModel.TotalNum = 1;
            return Ok(userSearchResModel);

        }


        /// <summary>
        /// (小程序端接口)  获取最新志愿者编号 （无参数）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VolunteerGetNoResModel> GetVNO()
        {
            VolunteerGetNoResModel getNoResModel = new VolunteerGetNoResModel();
            string vno = _Volunteerervice.GetNewVNO();
            getNoResModel.VNO = vno;
            getNoResModel.isSuccess = true;
            getNoResModel.baseViewModel.Message = "查询成功";
            getNoResModel.baseViewModel.ResponseCode = 200;
            getNoResModel.TotalNum = 1;
            return Ok(getNoResModel);
        }



        /// <summary>
        /// (小程序端接口)  获取归属( 社区、小区、单位) 信息   （无参数）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<UserDepartResModel> Manage_SearchDepart()
        {
            UserDepartResModel u_depart = new UserDepartResModel();
            var u_departMidModel = _Volunteerervice.GetDepartList();
            u_depart.IsSuccess = true;
            u_depart.UserDepart = u_departMidModel;
            u_depart.baseViewModel.Message = "查询成功";
            u_depart.baseViewModel.ResponseCode = 200;
            //_ILogger.Information("查询归属信息成功");
            return Ok(u_depart);

        }


        /// <summary>
        /// 验证是否在志愿者系统注册成功  （缓存 AI泰达用户 ID）  弃用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string CheckInfo(SearchByVIDModel vidModel)
        {
            string res = string.Empty;
            res = _Volunteerervice.CheckInfos(vidModel.VID);

            return res;
        }


        /// <summary>
        /// (小程序端接口)  验证是否在志愿者系统注册成功（返回状态：未注册；审核中；审核通过；审核不通过）  （缓存 AI泰达用户 ID）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel CheckInfoNew(SearchByVIDModel vidModel)
        {
            BaseViewModel res = new BaseViewModel();
            res = _Volunteerervice.CheckInfosNew(vidModel.VID);

            return res;
        }



      

        /// <summary>
        /// (小程序端接口)  获取志愿者信息（用户中心数据） （志愿者VID）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public VolunteerAddViewModel GetMyInfo(SearchByVIDModel vidModel)
        {
            VolunteerAddViewModel model = new VolunteerAddViewModel();
            model = _Volunteerervice.GetMyInfos(vidModel);
            return model;
        }


        /// <summary>
        /// (小程序端接口)  提交修改志愿者信息  完善信息 （擅长技能（资质照片）、服务领域、服务时间等
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VolunteerAddResModel> Volunteer_EditSubmit(VolunteerAddViewModel userAddViewModel)
        {

            int User_Edit_Count;
            VolunteerAddResModel userAddResModel = new VolunteerAddResModel();
            User_Edit_Count = _Volunteerervice.User_Edit(userAddViewModel);
            if (User_Edit_Count > 0)
            {
                //提示信息：已修改个人信息，等待审核 

                Volunteer_MessageMiddle middle = new Volunteer_MessageMiddle();
                middle.Contents = "您已修改个人信息，等待审核。";
                middle.Name = userAddViewModel.Name;
                middle.VID = userAddViewModel.ID;
                middle.VNO = userAddViewModel.VNO;
                BaseViewModel res = new BaseViewModel();
                res = _MessageService.AddMessages(middle);


                userAddResModel.IsSuccess = true;
                userAddResModel.AddCount = User_Edit_Count;
                userAddResModel.baseViewModel.Message = "修改成功";
                userAddResModel.baseViewModel.ResponseCode = 200;
                return Ok(userAddResModel);
            }
            else
            {
                userAddResModel.IsSuccess = false;
                userAddResModel.AddCount = 0;
                userAddResModel.baseViewModel.Message = "修改失败";
                userAddResModel.baseViewModel.ResponseCode = 300;
                return Ok(userAddResModel);
            }
        }


        /// <summary>
        /// (小程序端接口)  测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string check()
        {
            string str = "你好，世界";
            return str;
        }

    }
}
