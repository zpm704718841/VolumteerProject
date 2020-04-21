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
    public class VHelpAreaController : ControllerBase
    {
        private readonly IVHelpAreaService _VHelpAreaService;




        public VHelpAreaController(IVHelpAreaService vaservice)
        {
            _VHelpAreaService = vaservice;

        }

        /// <summary>
        /// (小程序端接口)  根据条件查询互助信息 （互助标题Title、所属组织架构Community,互助地址Address,擅长技能Type）
        /// </summary>
        /// <param name="VHelpAreaSearchViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VHelpAreaSearchResModel> Manage_Search(AllSearchViewModel SearchViewModel)
        {
            VHelpAreaSearchResModel SearchResModel = new VHelpAreaSearchResModel();
            var SearchResult = _VHelpAreaService.VHelpArea_Search(SearchViewModel);


            SearchResModel.VHelpArea = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);

        }


        /// <summary>
        /// (小程序端接口) 获取该互助详细信息 ( 互助ID)
        /// </summary>
        /// <param name="SearchByContentIDModel "></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VHelpAreaSearchResModel> GetContentDetail(SearchByContentIDModel viewModel)
        {
            VHelpAreaSearchResModel SearchResModel = new VHelpAreaSearchResModel();
            var SearchResult = _VHelpAreaService.GetVHelpAreaSearch(viewModel);

            SearchResModel.VHelpArea = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);

        }


        /// <summary>
        /// (小程序端接口) 获取所有互助信息  （无参数）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VHelpAreaSearchResModel> GetAll()
        {
            VHelpAreaSearchResModel SearchResModel = new VHelpAreaSearchResModel();
            var SearchResult = _VHelpAreaService.VHelpArea_All();


            SearchResModel.VHelpArea = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);

        }


        /// <summary>
        /// (小程序端接口) 获取我的互助信息(志愿者ID、所属社区ID、活动类型ID、状态 等)
        /// </summary>
        /// <returns></returns>
        /// [HttpPost]
        [HttpPost]
        public ActionResult<VHelpAreaSearchResModel> GetMyAllByWhere(GetMyListViewModel vidModel)
        {
            VHelpAreaSearchResModel SearchResModel = new VHelpAreaSearchResModel();
            var SearchResult = _VHelpAreaService.GetMyAllInfoByWhere(vidModel);

            SearchResModel.VHelpArea = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);
        }


        /// <summary>
        ///  验证是否已经报名（互助ID标识ContentID、志愿者唯一VID ）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string CheckVHA_Sign(ContentIDandVIDModel AddViewModel)
        {
            string result = String.Empty;
            result = _VHelpAreaService.CheckSignAdd(AddViewModel.VID, AddViewModel.ContentID);
            return result;
        }



        /// <summary>
        /// (小程序端接口)  志愿者互助信息认领提交 （互助ID标识ContentID、志愿者唯一VID、擅长技能TypeID）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VHA_SignAddResModel> VHA_SignAdd(VHA_SignAddViewModel AddViewModel)
        {
            int Add_Count;
            VHA_SignAddResModel AddResModel = new VHA_SignAddResModel();
            Add_Count = _VHelpAreaService.SignAdd(AddViewModel);
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
                AddResModel.baseViewModel.Message = "您不能认领自己发布的互助任务";
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
                AddResModel.baseViewModel.Message = "参数不能为空认领失败";
                AddResModel.baseViewModel.ResponseCode = 600;
                return Ok(AddResModel);
            }
            else if (Add_Count == 8)
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "您没有权限认领，仅限泰达街社区居民或机关单位工作人员认领";
                AddResModel.baseViewModel.ResponseCode = 500;
                return Ok(AddResModel);
            }
            else if (Add_Count == 9 )
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "您没有资质认领";
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
        /// (小程序端接口)  志愿者上传互助信息处理结果 （互助ID标识ContentID、志愿者唯一VID、处理信息Results、处理结果图片信息列表）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VHA_HandleAddResModel> VHA_HandleAdd(VHA_HandleAddModel AddViewModel)
        {
            int Add_Count = 0;
            VHA_HandleAddResModel AddResModel = new VHA_HandleAddResModel();
            Add_Count = _VHelpAreaService.HandleAdd(AddViewModel);
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
        /// (小程序端接口) 获取我的互助信息  （志愿者唯一VID）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VHelpAreaSearchResModel> GetMyAll(SearchByVIDModel vidModel)
        {
            VHelpAreaSearchResModel SearchResModel = new VHelpAreaSearchResModel();
            var SearchResult = _VHelpAreaService.GetMyAllInfo(vidModel.VID);

            SearchResModel.VHelpArea = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);

        }


        /// <summary>
        ///  (小程序端接口) 获取本志愿者针对该互助信息 所有状态 （互助ID标识ContentID、志愿者唯一VID  ）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel GetVHAStatus(ContentIDandVIDModel AddViewModel)
        {
            BaseViewModel result = new BaseViewModel();
            result = _VHelpAreaService.GetVHAStatus(AddViewModel.VID, AddViewModel.ContentID);
            return result;
        }


        /// <summary>
        ///  (小程序端接口) 志愿者互助信息退出功能 （互助ID标识ContentID、志愿者唯一VID ）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string SetVHAStatusBack(ContentIDandVIDModel AddViewModel)
        {
            string  result = String.Empty;
            result = _VHelpAreaService.SetVHAStatusBack(AddViewModel.VID, AddViewModel.ContentID);
            return result;
        }



        /// <summary>
        ///  (小程序端接口) 获取本志愿者针对该互助信息 上传结果信息 （互助ID标识ContentID、志愿者唯一VID  ）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VHA_HandleGetListResModel> GetHandleInfo(ContentIDandVIDModel AddViewModel)
        {
            VHA_HandleGetListResModel MyResModel = new VHA_HandleGetListResModel();
            MyResModel.List = _VHelpAreaService.GetMyHandleInfo(AddViewModel.VID, AddViewModel.ContentID);
            MyResModel.isSuccess = true;
            MyResModel.TotalNum = MyResModel.List.Count();
            MyResModel.baseViewModel.Message = "请求正常";
            MyResModel.baseViewModel.ResponseCode = 200;
            return MyResModel;
        }


        /// <summary>
        /// (小程序端接口) 首页 互助信息推荐( 志愿者VID )
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VHelpAreaSearchResModel> GetHARecommendList(SearchByVIDModel vidModel)
        {
            VHelpAreaSearchResModel SearchResModel = new VHelpAreaSearchResModel();
            var SearchResult = _VHelpAreaService.GetMyRecommendList(vidModel.VID);

            SearchResModel.VHelpArea = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);
        }


        /// <summary>
        /// (小程序端接口)  志愿者成功参与的 互助次数（只有最终审核处理结果通过才算成功）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel GetMyHelpAreaNum(SearchByVIDModel vidModel)
        {
            BaseViewModel result = new BaseViewModel();
            result = _VHelpAreaService.GetMyHelpAreaNums(vidModel);
            return result;
        }

    }
}
