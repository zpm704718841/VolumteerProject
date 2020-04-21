using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel.VolunteerBackground.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;
using Serilog;
using Dto.IService.IntellVolunteer;
using ViewModel.VolunteerBackground.ResponseModel;

namespace IntellVolunteerBackground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerInfoController : ControllerBase
    {
        private readonly ILogger _ILogger;
        private readonly IVolunteerService _volunteerService;

        public VolunteerInfoController(IVolunteerService volunteerervice, ILogger log)
        {
            _volunteerService = volunteerervice;
            _ILogger = log;
        }

        /// <summary>
        /// 根据条件获取志愿者信息
        /// </summary>
        /// <param name="volunteer_SearchModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Manage_GetVolunteerInfoList")]
        public ActionResult<VolunteerSearchResModel> Manage_GetVolunteerInfoList(VolunteerInfoSearchViewModel volunteer_SearchModel)
        {
            VolunteerSearchResModel v_ResModle = new VolunteerSearchResModel();
            var searchMidModel = _volunteerService.Volunteer_SearchForBG(volunteer_SearchModel);
            var totalNum = _volunteerService.GetAllCount(volunteer_SearchModel);
            v_ResModle.Volunteer_Infos = searchMidModel;
            v_ResModle.isSuccess = true;
            v_ResModle.baseViewModel.Message = "查询成功";
            v_ResModle.baseViewModel.ResponseCode = 200;
            v_ResModle.TotalNum = totalNum ;
            return Ok(v_ResModle);

        }

        /// <summary>
        /// 志愿者信息审核/软删除  状态位修改
        /// </summary>
        /// <param name="updateViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Manage_VolunteerInfoSetValues")]
        public ActionResult<VolunteerUpdateResModel> Manage_VolunteerInfoSetValues(VolunteerInfoUpdateViewModel updateViewModel)
        {
            VolunteerUpdateResModel returnModel = new VolunteerUpdateResModel();
            int result = _volunteerService.ChangeVolunteer(updateViewModel);
            if (result > 0)
            {
                returnModel.IsSuccess = true;
                returnModel.ResultCount = result;
                returnModel.baseViewModel.Message = "志愿者信息更新完成";
                returnModel.baseViewModel.ResponseCode = 200;
                _ILogger.Information("志愿者信息更新完成");
                return Ok(returnModel);
            }
            else
            {
                returnModel.IsSuccess = false;
                returnModel.ResultCount = 0;
                returnModel.baseViewModel.Message = "志愿者信息更新失败";
                returnModel.baseViewModel.ResponseCode = 400;
                _ILogger.Information("志愿者信息更新失败");
                return BadRequest(returnModel);
            }

        }


    }
}