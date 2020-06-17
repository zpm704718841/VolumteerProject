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
using ViewModel.VolunteerModel.MiddleModel;
using System.IO;
using ViewModel.PublicViewModel;

namespace IntellVolunteer.Controllers
{
    [Route("VolunteerManageApi/[controller]/[action]")]
    [ApiController]
    public class VActivity_PublicShowController : ControllerBase
    {
        private readonly IVActivity_PublicShowService _IVActivity_PublicShowService;

        public VActivity_PublicShowController(IVActivity_PublicShowService showService)
        {
            _IVActivity_PublicShowService = showService;
        }



        /// <summary>
        /// (小程序端接口) 志愿者参与的志愿活动（已结束和进行中）(必须有签到记录) 参数 志愿者VID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VolunteerActivitySearchResModel> GetMyAllActivity(SearchByVIDModel vidModel)
        {
            VolunteerActivitySearchResModel SearchResModel = new VolunteerActivitySearchResModel();
            var SearchResult = _IVActivity_PublicShowService.GetMyAllActivity(vidModel.VID);

            SearchResModel.Volunteer_Activity = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);

        }


        /// <summary>
        /// (小程序端接口) 志愿者上传 公益秀（志愿活动ID，活动标题，活动地址，参与感受，图片列表 等等）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VActivity_PublicShowAddResModel> AddPublicShow(VActivity_PublicShowAddModel middle)
        {
            VActivity_PublicShowAddResModel result = new VActivity_PublicShowAddResModel();
            result = _IVActivity_PublicShowService.AddPublicShow(middle);
            return result;
        }


        /// <summary>
        /// (小程序端接口) 展示公所有益秀信息（志愿活动ID，活动标题，活动地址，参与感受，图片列表 等等）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VActivity_PublicShowListResModel> GetPublicShowList()
        {
            VActivity_PublicShowListResModel result = new VActivity_PublicShowListResModel();
            var SearchResult = _IVActivity_PublicShowService.GetPublicShowList();

            result.List = SearchResult;
            result.isSuccess = true;
            result.TotalNum = SearchResult.Count;
            result.baseViewModel.Message = "查询成功";
            result.baseViewModel.ResponseCode = 200;
            return result;
        }



        /// <summary>
        /// (小程序端接口) 获取我的益秀信息（志愿活动ID，活动标题，活动地址，参与感受，图片列表 等等） 参数志愿者VID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VActivity_PublicShowListResModel> GetMyPublicShowList(SearchByVIDModel middle)
        {
            VActivity_PublicShowListResModel result = new VActivity_PublicShowListResModel();
            var SearchResult = _IVActivity_PublicShowService.GetMyPublicShowList(middle.VID);

            result.List = SearchResult;
            result.isSuccess = true;
            result.TotalNum = SearchResult.Count;
            result.baseViewModel.Message = "查询成功";
            result.baseViewModel.ResponseCode = 200;
            return result;
        }


        /// <summary>
        /// (小程序端接口) 志愿者针对一条公益秀点赞   参数志愿者VID，公益秀ID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BaseViewModel> PublicShow_GiveLike(PublicShowIDandVID showDandVid)
        {
            BaseViewModel result = new BaseViewModel();
            result = _IVActivity_PublicShowService.PublicShow_GiveLike(showDandVid);
            return result;
        }


        /// <summary>
        /// (小程序端接口)志愿者针对一条公益秀 取消点赞   参数志愿者VID，公益秀ID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BaseViewModel> PublicShow_CancelLike(PublicShowIDandVID showDandVid)
        {
            BaseViewModel result = new BaseViewModel();
            result = _IVActivity_PublicShowService.PublicShow_CancelLike(showDandVid);
            return result;
        }


        /// <summary>
        /// (小程序端接口)志愿者删除该公益秀（自己发布的）   参数志愿者VID，公益秀ID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BaseViewModel> PublicShow_Delete(PublicShowIDandVID showDandVid)
        {
            BaseViewModel result = new BaseViewModel();
            result = _IVActivity_PublicShowService.PublicShow_Delete(showDandVid);
            return result;
        }



        /// <summary>
        /// (小程序端接口) 验证该志愿者是否 已经点赞该公益秀 20200608
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BaseViewModel> CheckIsGiveLike(PublicShowIDandVID showIDandVID)
        {
            BaseViewModel result = new BaseViewModel();
            result = _IVActivity_PublicShowService.CheckIsGiveLike(showIDandVID);
            return result;

        }

    }
}
