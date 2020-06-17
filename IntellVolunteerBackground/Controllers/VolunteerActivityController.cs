using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dto.IService.IntellVolunteerBackground;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ViewModel.VolunteerBackground.RequsetModel;
using ViewModel.VolunteerBackground.ResponseModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntellVolunteerBackground.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VolunteerActivityController : ControllerBase
    {
        //private readonly ILogger _ILogger;
        private readonly IVolunteerActivityService _volunteerActivityService;
        
        public VolunteerActivityController(  IVolunteerActivityService volunteerActivityService)
        {
           // _ILogger = log;
            _volunteerActivityService = volunteerActivityService;
        }

        /// <summary>
        /// 活动信息添加
        /// </summary>
        /// <param name="volunteerActivityViewModel"></param>
        /// <returns></returns>
        
        [HttpPost]
        public ActionResult<VolunteerActivityAddResModel> Manage_VolunteerActivity_Add(VolunteerActivityViewModel model)
        {
            VolunteerActivityAddResModel returnModel = new VolunteerActivityAddResModel();

            if (_volunteerActivityService.ActivityDistinct(model))
            {
                int result;
                result = _volunteerActivityService.Add(model);
                if (result > 0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.ResultCount = result;
                    returnModel.baseViewModel.Message = "添加成功";
                    returnModel.baseViewModel.ResponseCode = 200;
                    //_ILogger.Information("活动信息添加成功");
                    return Ok(returnModel);
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.ResultCount = 0;
                    returnModel.baseViewModel.Message = "添加失败";
                    returnModel.baseViewModel.ResponseCode = 400;
                    //_ILogger.Information("活动信息添加失败");
                    return BadRequest(returnModel);
                }
            }
            else
            {
                returnModel.IsSuccess = false;
                returnModel.ResultCount = 0;
                returnModel.baseViewModel.Message = "添加失败,该活动已存在";
                returnModel.baseViewModel.ResponseCode = 400;
                //_ILogger.Information("活动信息添加失败");
                return BadRequest(returnModel);
            }
        }
        /// <summary>
        /// 活动信息修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VolunteerActivityUpdatedResModel> Manage_VolunteerActivity_Update(VolunteerActivityViewModel model)
        {
            VolunteerActivityUpdatedResModel returnModel = new VolunteerActivityUpdatedResModel();
            if (_volunteerActivityService.ActivityDistinctForUpdate (model))
            {
                int result = _volunteerActivityService.Update(model);
                if(result>0)
                {
                    returnModel.IsSuccess = true;
                    returnModel.ResultCount = result;
                    returnModel.baseViewModel.Message = "更新成功";
                    returnModel.baseViewModel.ResponseCode = 200;
                    //_ILogger.Information("活动信息更新成功");
                    return Ok(returnModel);
                }
                else
                {
                    returnModel.IsSuccess = false;
                    returnModel.ResultCount = 0;
                    returnModel.baseViewModel.Message = "活动信息更新失败";
                    returnModel.baseViewModel.ResponseCode = 400;
                    //_ILogger.Information("活动信息更新失败");
                    return BadRequest(returnModel);

                }
            }
            else
            {
                returnModel.IsSuccess = false;
                returnModel.ResultCount = 0;
                returnModel.baseViewModel.Message = "活动信息修改失败,该活动已存在";
                returnModel.baseViewModel.ResponseCode = 400;
                //_ILogger.Information("活动信息修改失败,该活动已存在");
                return BadRequest(returnModel);

            }
        }

        /// <summary>
        /// 活动信息修改 软删除 status、其它状态待定
        /// </summary>
        /// <param name="delModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VolunteerActivityUpdatedResModel> Manage_VolunteerActivity_Delete(VolunteerActivityDeleteViewModel delModel)
        {
            VolunteerActivityUpdatedResModel returnModel = new VolunteerActivityUpdatedResModel();
            int result = _volunteerActivityService.DeleteByID(delModel);
            if (result > 0)
            {
                returnModel.IsSuccess = true;
                returnModel.ResultCount = result;
                returnModel.baseViewModel.Message = "信息已删除";
                returnModel.baseViewModel.ResponseCode = 200;
                //_ILogger.Information("活动信息已删除");
                return Ok(returnModel);
            }
            else
            {
                returnModel.IsSuccess = false;
                returnModel.ResultCount = 0;
                returnModel.baseViewModel.Message = "活动信息删除失败";
                returnModel.baseViewModel.ResponseCode = 400;
                //_ILogger.Information("活动信息删除失败");
                return BadRequest(returnModel);
            }

        }
        /// <summary>
        /// 活动信息查询
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<VolunteerActivitySearchResModel> Manage_VolunteerActivity_Search(VolunteerActivitySearchViewModel searchModel)
        {
            VolunteerActivitySearchResModel returnModel = new VolunteerActivitySearchResModel();
            var midModel = _volunteerActivityService.SearchList(searchModel);
            var totalNum = _volunteerActivityService.GetAllCount(searchModel);
            returnModel.IsSuccess= true;
            returnModel.TotalNum = totalNum;
            returnModel.Activity = midModel;
            returnModel.baseViewModel.Message = "查询成功";
            returnModel.baseViewModel.ResponseCode = 200;
            //_ILogger.Information("查询活动信息成功");
            return Ok(returnModel);

        }
        [HttpPost]
        public ActionResult<UserDepartResModel> Manage_SearchDepart(string values)
        {
            UserDepartResModel u_depart = new UserDepartResModel();
            var u_departMidModel = _volunteerActivityService.GetDepartList();
            u_depart.IsSuccess = true;
            u_depart.UserDepart = u_departMidModel;
            u_depart.baseViewModel.Message = "查询成功";
            u_depart.baseViewModel.ResponseCode = 200;
            //_ILogger.Information("查询单位信息成功");
            return Ok(u_depart);

        }
    }
}
