using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.IService.IntellVolunteer;
using Dtol.dtol;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemFilter.PublicFilter;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;
using System.IO;
using ViewModel.PublicViewModel;



namespace IntellVolunteer.Controllers
{
    [Route("VolunteerManageApi/[controller]/[action]")]
    [ApiController]
    public class VolunteerActivityController: ControllerBase
    {
        private readonly IVolunteerActivityService _VolunteerActivityService;
        private readonly IVolunteer_MessageService _MessageService;

        public VolunteerActivityController(IVolunteerActivityService vaservice, IVolunteer_MessageService messageService)
        {
            _VolunteerActivityService = vaservice;
            _MessageService = messageService;
        }


        /// <summary>
        /// (小程序端接口) 根据条件查询活动信息 ( 活动名称Title、所属社区Community、活动地址Address、活动类型Type等)
        /// </summary>
        /// <param name="VolunteerActivitySearchViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VolunteerActivitySearchResModel> Manage_Search(AllSearchViewModel viewModel)
        {
            VolunteerActivitySearchResModel SearchResModel = new VolunteerActivitySearchResModel();
            var SearchResult = _VolunteerActivityService.VolunteerActivity_Search(viewModel);
 
            SearchResModel.Volunteer_Activity = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);

        }



        /// <summary>
        /// (小程序端接口) 获取该活动详细信息 ( 活动ID)
        /// </summary>
        /// <param name="SearchByContentIDModel "></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VolunteerActivitySearchResModel> GetContentDetail(SearchByContentIDModel viewModel)
        {
            VolunteerActivitySearchResModel SearchResModel = new VolunteerActivitySearchResModel();
            var SearchResult = _VolunteerActivityService.GetVolunteerActivity(viewModel);

            SearchResModel.Volunteer_Activity = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);

        }


        /// <summary>
        /// (小程序端接口) 获取该志愿者针对该活动 签到签退信息 ( 活动ID,志愿者ID)
        /// </summary>
        /// <param name="SearchByContentIDModel "></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VA_HandleGetMyResModel> GetMyAHandleInfo(ContentIDandVIDModel model)
        {
            VA_HandleGetMyResModel MyResModel = new VA_HandleGetMyResModel();
            MyResModel = _VolunteerActivityService.GetMyAHandleInfo(model);
            return MyResModel;
        }


        /// <summary>
        /// (小程序端接口)  获取所有活动信息  （无参数）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult <VolunteerActivitySearchResModel> GetAll()
        {
            VolunteerActivitySearchResModel SearchResModel = new VolunteerActivitySearchResModel();
            var SearchResult = _VolunteerActivityService.VolunteerActivity_All();


            SearchResModel.Volunteer_Activity = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);

        }


        /// <summary>
        /// (小程序端接口)  获取不同类型的活动数   （活动类型TypeIDs）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int GetTypeCount(VolunteerActivityTypeModel typeModel)
        {
            var SearchResult = _VolunteerActivityService.GetTypeCounts(typeModel);
            return SearchResult;

        }


        /// <summary>
        /// (小程序端接口)  验证是否已经报名（活动ID标识ContentID、志愿者唯一VID ）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string  CheckVA_Sign (ContentIDandVIDModel AddViewModel)
        {
            string result = String.Empty;
            result = _VolunteerActivityService.CheckSignAdd(AddViewModel.VID, AddViewModel.ContentID);
            return result;
        }



        /// <summary>
        /// (小程序端接口)  志愿者活动报名提交 （活动ID标识ContentID、志愿者唯一VID、擅长技能TypeID）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VA_SignAddResModel> VA_SignAdd(VA_SignAddViewModel AddViewModel)
        {
            int Add_Count;
            VA_SignAddResModel AddResModel = new VA_SignAddResModel();
            Add_Count = _VolunteerActivityService.SignAdd(AddViewModel);
            if (Add_Count == 1 )
            {
                AddResModel.IsSuccess = true;
                AddResModel.AddCount = Add_Count;
                AddResModel.baseViewModel.Message = "添加成功";
                AddResModel.baseViewModel.ResponseCode = 200;
                return Ok(AddResModel);
            }
            else if (Add_Count == 5)
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "您不能报名同时段活动";
                AddResModel.baseViewModel.ResponseCode = 800;
                return Ok(AddResModel);
            }
            else if (Add_Count == 6)
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "您还未进行注册请先注册";
                AddResModel.baseViewModel.ResponseCode = 700;
                return Ok(AddResModel);
            }
            else if (Add_Count == 7)
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "参数不能为空无法报名";
                AddResModel.baseViewModel.ResponseCode = 600;
                return Ok(AddResModel);
            }
            else if (Add_Count == 8)
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "名额已满无法报名";
                AddResModel.baseViewModel.ResponseCode = 500;
                return Ok(AddResModel);
            }
            else if (Add_Count == 9)
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "您没有资质报名该类型";
                AddResModel.baseViewModel.ResponseCode = 400;
                return Ok(AddResModel);
            }
            else
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "添加失败";
                AddResModel.baseViewModel.ResponseCode = 300;
                return Ok(AddResModel);
            }
        }


        /// <summary>
        /// (小程序端接口)  最新获取不同类型的活动数 top4  修改 活动数是 0 不显示 （无参数）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VActivityTypeCountResModel> GetTypeCounts()
        {
            VActivityTypeCountResModel SearchResModel = new VActivityTypeCountResModel();
            var SearchResult = _VolunteerActivityService.GetTypeCounts();


            SearchResModel.VActivityTypeCount = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);

        }


        /// <summary>
        /// (小程序端接口)  志愿者活动  签到、签退接口 （活动ID标识ContentID、志愿者唯一VID、签到地址CheckAddress、经度Checklongitude、纬度Checklatitude、类型type（签到：type=in,签退：type=out;上传现场图片:type=img））
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VA_HandleAddResModel> VA_HandleAdd(VA_HandleAddViewModel AddViewModel)
        {
            int Add_Count = 0;
            VA_HandleAddResModel AddResModel = new VA_HandleAddResModel();
            Add_Count = _VolunteerActivityService.HandleAdd(AddViewModel);
            if (Add_Count == 1)
            {
                AddResModel.IsSuccess = true;
                AddResModel.AddCount = Add_Count;
                AddResModel.baseViewModel.Message = "操作成功";
                AddResModel.baseViewModel.ResponseCode = 200;
            }
            else if (Add_Count == 4)
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "已签退，无法多次签退。";
                AddResModel.baseViewModel.ResponseCode = 900;
            }
            else if (Add_Count == 5)
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "必填参数为空。";
                AddResModel.baseViewModel.ResponseCode = 800;
            }
            else if (Add_Count == 6)
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "您还未进行注册请先注册。";
                AddResModel.baseViewModel.ResponseCode = 700;
            }
            else if (Add_Count == 7)
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "签到地址有误签到失败。";
                AddResModel.baseViewModel.ResponseCode = 600;
            }
            else if (Add_Count == 8)
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "活动地址有误签到失败。";
                AddResModel.baseViewModel.ResponseCode = 500;
            }
            else if (Add_Count == 9)
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "签到地址不在活动范围500米内。";
                AddResModel.baseViewModel.ResponseCode = 400;
            }
            else
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "操作失败。";
                AddResModel.baseViewModel.ResponseCode = 300;
            }
            return Ok(AddResModel);
        }


        /// <summary>
        /// (小程序端接口)  志愿者活动 活动中 上传现场服务照片  （活动ID标识ContentID、志愿者唯一VID、 现场图片信息列表）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VAttachmentAddResModel> VA_SubmitImg(VA_HandleImgAddModel AddViewModel)
        {
            int Add_Count = 0;
            VAttachmentAddResModel AddResModel = new VAttachmentAddResModel();
            Add_Count = _VolunteerActivityService.SubmitImg(AddViewModel);
            if (Add_Count == 8)
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "请上传现场图片";
                AddResModel.baseViewModel.ResponseCode = 800;
            }
            if (Add_Count == 9)
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "您还未进行注册请先注册";
                AddResModel.baseViewModel.ResponseCode = 700;
            }
            else
            {
                AddResModel.IsSuccess = true;
                AddResModel.AddCount = Add_Count;
                AddResModel.baseViewModel.Message = "上传成功";
                AddResModel.baseViewModel.ResponseCode = 200;
            }

            return Ok(AddResModel);
        }



        /// <summary>
        /// (小程序端接口) 获取我的活动信息  （志愿者唯一VID）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VolunteerActivitySearchResModel> GetMyAll(SearchByVIDModel vidModel)
        {
            VolunteerActivitySearchResModel SearchResModel = new VolunteerActivitySearchResModel();
            var SearchResult = _VolunteerActivityService.GetMyAllInfo(vidModel.VID);

            SearchResModel.Volunteer_Activity = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);

        }



        /// <summary>
        /// (小程序端接口) 获取我的活动信息(志愿者ID、所属社区ID、活动类型ID、状态 等)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VolunteerActivitySearchResModel> GetMyAllByWhere(GetMyListViewModel vidModel)
        {
            VolunteerActivitySearchResModel SearchResModel = new VolunteerActivitySearchResModel();
            var SearchResult = _VolunteerActivityService.GetMyAllInfoByWhere(vidModel);

            SearchResModel.Volunteer_Activity = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);
        }


        /// <summary>
        ///  测试  验证 是否 报名 同时段活动  (活动ID标识ContentID、志愿者VID )
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> checkTime(ContentIDandVIDModel AddViewModel)
        {
            var SearchResult = await _VolunteerActivityService.CheckSigns(AddViewModel.VID, AddViewModel.ContentID);
            return SearchResult;
        }


        /// <summary>
        ///  测试  验证 是否 报名 同时段活动 new (活动ID标识ContentID、志愿者VID )
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string checkTimeNew(ContentIDandVIDModel AddViewModel)
        {
            var SearchResult =  _VolunteerActivityService.CheckSignsNew(AddViewModel.VID, AddViewModel.ContentID);
            return SearchResult;
        }



        /// <summary>
        /// (小程序端接口) 首页 签到 定位当前时段活动 返回活动ID ( 志愿者VID )
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetContentID(SearchByVIDModel vidModel)
        {
            var result = string.Empty;
            result = _VolunteerActivityService.HomeGetContentID(vidModel.VID);
            return result;
        }


        /// <summary>
        ///  (小程序端接口) 获取本志愿者针对该活动信息 所有状态 （活动ID标识ContentID、志愿者唯一VID ）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel GetVAStatus(ContentIDandVIDModel AddViewModel)
        {
            BaseViewModel result = new BaseViewModel();
            result = _VolunteerActivityService.GetVAStatus(AddViewModel.VID, AddViewModel.ContentID);
            return result;
        }


        /// <summary>
        /// (小程序端接口) 首页 志愿活动推荐( 志愿者VID )
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VolunteerActivitySearchResModel> GetRecommendList(SearchByVIDModel vidModel)
        {
            VolunteerActivitySearchResModel SearchResModel = new VolunteerActivitySearchResModel();
            var SearchResult = _VolunteerActivityService.GetMyRecommendList(vidModel.VID);

            SearchResModel.Volunteer_Activity = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);
        }

        /// <summary>
        /// (小程序端接口)  志愿者取消 志愿活动报名( 志愿者VID ,活动ID)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel CancelSign(ContentIDandVIDModel AddViewModel)
        {
            BaseViewModel result = new BaseViewModel();
            result = _VolunteerActivityService.CancelVASign(AddViewModel);
            return result;
        }


        /// <summary>
        /// (小程序端接口)  志愿者成功参与的 志愿活动数（只有签到过才算成功）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel GetMyActivityNum(SearchByVIDModel vidModel)
        {
            BaseViewModel result = new BaseViewModel();
            result = _VolunteerActivityService.GetMyActivityNums(vidModel);
            return result;
        }
    }
}
