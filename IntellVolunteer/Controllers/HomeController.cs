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
using ViewModel.VolunteerModel.ResponseModel.NormalViewModel;

namespace IntellVolunteer.Controllers
{
    [Route("HomeManageApi/[controller]/[action]")]
    [ApiController]
    public class HomeController: ControllerBase
    {
        private readonly IVolunteerActivityService _VolunteerActivityService;
        private readonly IVolunteer_MessageService _MessageService;
        private readonly IHomeService _homeService;

        public HomeController(IVolunteerActivityService vaservice, IVolunteer_MessageService messageService, IHomeService homeService)
        {
            _VolunteerActivityService = vaservice;
            _MessageService = messageService;
            _homeService = homeService;
        }

        /// <summary>
        /// (小程序端接口) 获取当前用户 今天以后的报名活动情况 日期列表 20200527
        /// </summary>
        /// <param name="vidModel"></param>
        /// <returns></returns>
        [HttpPost]
        public List<string> GetMyAllDate(SearchByVIDModel vidModel)
        {
            var SearchResult = _homeService.GetMyAllDate(vidModel);
            return  SearchResult ;
        }


        /// <summary>
        /// (小程序端接口) 获取当前用户 根据活动时间显示具体 活动情况列表 20200527
        /// </summary>
        /// <param name="vidModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VolunteerActivitySearchResModel> GetMyAllByDate(VolunteerActivitySearchByDateModel vidModel)
        {
            VolunteerActivitySearchResModel SearchResModel = new VolunteerActivitySearchResModel();
             var SearchResult = _homeService.GetMyAllByDate(vidModel);

            SearchResModel.Volunteer_Activity = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);
    
        }



        /// <summary>
        /// (小程序端接口) 获取当前用户 根据时间显示具体常态化管控认领 列表 
        /// </summary>
        /// <param name="vidModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<MyDutyListResModel> GetMyDutyAllByDate(VolunteerActivitySearchByDateModel vidModel)
        {
            MyDutyListResModel SearchResModel = new MyDutyListResModel();
            var SearchResult = _homeService.GetMyDutyAllByDate(vidModel);

            SearchResModel.mydutyClaims = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);

        }

    }
}
