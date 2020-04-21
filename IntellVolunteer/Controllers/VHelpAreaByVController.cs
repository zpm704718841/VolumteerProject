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
    public class VHelpAreaByVController: ControllerBase
    {
        private readonly IVHelpAreaByVService _VHelpAreaByVService;

        public VHelpAreaByVController(IVHelpAreaByVService vaservice)
        {
            _VHelpAreaByVService = vaservice;

        }

        /// <summary>
        /// (小程序端接口) 社区居民上传互助信息（名称、内容、所需擅长技能、姓名、联系方式、详细地址、可得积分）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BaseViewModel> AddHelpArea(VHelpAreaSearchMiddle middle)
        {
            BaseViewModel result = new BaseViewModel();
            result = _VHelpAreaByVService.AddHelpArea(middle);
            return result;
        }


        /// <summary>
        /// (小程序端接口) 首页 发布互助入口（验证是否是区内用户，区外用户不能发布）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BaseViewModel> CheckRights(SearchByVIDModel middle)
        {
            BaseViewModel result = new BaseViewModel();
            result = _VHelpAreaByVService.CheckRights(middle);
            return result;
        }

        /// <summary>
        /// (小程序端接口) 获取我发布的互助信息列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VHelpAreaSearchResModel> GetMyPublishInfoList(VHAVIDandStatusModel model)
        {
            VHelpAreaSearchResModel SearchResModel = new VHelpAreaSearchResModel();
            var SearchResult = _VHelpAreaByVService.VHelpArea_MyAll(model.VID, model.status);

            SearchResModel.VHelpArea = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);
        }



        /// <summary>
        /// (小程序端接口)  查看该互助信息对应的认领人列表信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<PublishHelpAreaResModel> GetMyPublishInfo(SearchByContentIDModel idModel)
        {
            PublishHelpAreaResModel model = new PublishHelpAreaResModel();
            model = _VHelpAreaByVService.GetVList(idModel.ContentID);
            return model;
        }


        /// <summary>
        /// (小程序端接口) 获取 查看该互助信息对应的认领人 的具体信息 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VHA_SignInfoMiddle> GetVDetail(ContentIDandVIDModel vidModel)
        {
            VHA_SignInfoMiddle model = new VHA_SignInfoMiddle();

            model = _VHelpAreaByVService.GetVDetail(vidModel.VID);

            //查找到 最新的一条 结果记录
            VHA_Sign sign = _VHelpAreaByVService.GetVHandleList(vidModel.ContentID,"1");
            if (sign != null && sign.ID != null)
            {
                model.Signtime = DateTime.Parse(sign.CreateDate.ToString());
            }
            else
            {
                model.Signtime = DateTime.Now;
            }
           

            return model;
        }




        /// <summary>
        /// (小程序端接口) 确认该志愿者为 认领人（其他人 状态为审核不通过）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel SetVHAStatus(ContentIDandVIDModel model)
        {
            BaseViewModel result = new BaseViewModel();
            result = _VHelpAreaByVService.SetVHAStatus(model.VID, model.ContentID);
            return result;
        }



        /// <summary>
        /// (小程序端接口) 发布者查看 认领人上传的 处理结果信息 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VHA_HandleGetListResModel> GetHandleInfo(SearchByContentIDModel idModel)
        {
            //查找到 最新的一条 结果记录
            VHA_Sign sign = _VHelpAreaByVService.GetVHandleList(idModel.ContentID,"2");
         
            VHA_HandleGetListResModel MyResModel = new VHA_HandleGetListResModel();
            MyResModel.isSuccess = true;
           
            if (sign != null && sign.ID != null)
            {
                MyResModel.Signtime = DateTime.Parse(sign.CreateDate.ToString());
                //获取 查看该互助信息对应的认领人 的具体信息 
                VHA_SignInfoMiddle infoMiddle = _VHelpAreaByVService.GetVDetail(sign.VID);
                MyResModel.middle = infoMiddle;
                MyResModel.List = _VHelpAreaByVService.GetMyHandleInfo(sign.VID, idModel.ContentID);
                MyResModel.TotalNum = MyResModel.List.Count();
            }
            else
            {
                MyResModel.Signtime = DateTime.Now;
            }
            
            MyResModel.baseViewModel.Message = "请求正常";
            MyResModel.baseViewModel.ResponseCode = 200;
            return MyResModel;
        }



        /// <summary>
        /// (小程序端接口) 发布者审核 认领人上传的 处理结果信息    审核通过任务完结
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel SetSignPass(ContentIDandVIDModel model)
        {
            BaseViewModel result = new BaseViewModel();
            result = _VHelpAreaByVService.SetSignPass(model);
            return result;
        }



        /// <summary>
        /// (小程序端接口) 发布者审核 认领人上传的 处理结果信息   审核不通过，志愿者重新上传处理结果
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel SetSignReturn(ContentIDandVIDModel model)
        {
            BaseViewModel result = new BaseViewModel();
            result = _VHelpAreaByVService.SetSignReturn(model);
            return result;
        }


        /// <summary>
        /// (小程序端接口) 发布者审核 认领人上传的 处理结果信息   退回至互助信息，志愿者可重新认领
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel SetSignBack(ContentIDandVIDModel model)
        {
            BaseViewModel result = new BaseViewModel();
            result = _VHelpAreaByVService.SetSignBack(model);
            return result;
        }




        /// <summary>
        /// (小程序端接口) 认领人针对本次认领任务的所有状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel GetVHAByVStatus(ContentIDandVIDModel model)
        {
            BaseViewModel result = new BaseViewModel();
            result = _VHelpAreaByVService.GetVHAByVStatus(model);
            return result;
        }


        /// <summary>
        /// (小程序端接口) 获取该互助信息的所有状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel GetVHAStatusAll(SearchByContentIDModel model)
        {
            BaseViewModel result = new BaseViewModel();
            result = _VHelpAreaByVService.GetVHAStatusAll(model);
            return result;
        }


    }
}
