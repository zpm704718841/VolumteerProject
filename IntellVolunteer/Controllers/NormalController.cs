﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Dto.IService.IntellVolunteer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel.VolunteerModel.RequsetModel.NormalViewModel;
using ViewModel.VolunteerModel.ResponseModel.NormalViewModel;

namespace IntellVolunteer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NormalController : ControllerBase
    {

        private readonly INormalizationInfoService normalizationInfoService;
        private readonly IMydutyClaimInfoService  mydutyClaimInfoService;

        public NormalController(INormalizationInfoService normalizationInfoService, IMydutyClaimInfoService mydutyClaimInfoService)
        {
            this.normalizationInfoService = normalizationInfoService;
            this.mydutyClaimInfoService = mydutyClaimInfoService;
        }

        [HttpPost]


        public ActionResult NormalLizationAdd(NormalAddViewModel normalAddViewModel)
        {
            normalizationInfoService.AddNormalizationInfoService(normalAddViewModel);
            return null;
        }
        [HttpPost]
        /// <summary>
        ///  查询常态化信息
        /// </summary>
        /// <param name="normalSearchViewModel"></param>
        /// <returns></returns>
        public ActionResult<NormalClaimsSearhResViewModel> NormalIzationSearch(NormalSearchViewModel normalSearchViewModel)
        {
            NormalClaimsSearhResViewModel normalClaimsSearhViewModel = new NormalClaimsSearhResViewModel();
            normalClaimsSearhViewModel.nornalOndutySearchMiddlecs = normalizationInfoService.NormalizationSeachService(normalSearchViewModel);
            normalClaimsSearhViewModel.isSuccess = true;
            normalClaimsSearhViewModel.baseViewModel.Message = "查询成功";
            normalClaimsSearhViewModel.baseViewModel.ResponseCode = 200;
            normalClaimsSearhViewModel.TotalNum = normalizationInfoService.NormalizationSeachService(normalSearchViewModel).Count();
            return Ok(normalClaimsSearhViewModel);

        }

        /// <summary>
        /// 通过id获取主要信息以及其相关时段的值班列表
        /// </summary>
        [HttpPost]
        public ActionResult<NormalClaimsSearhResContainDutyInfoViewModel> NormalIzationContainDutyByIdSearch(NormalizationContainSearchViewModel normalizationContainSearchViewModel)
        {
            NormalClaimsSearhResContainDutyInfoViewModel normalClaimsSearhResContainDutyInfoViewModel = new NormalClaimsSearhResContainDutyInfoViewModel();
            normalClaimsSearhResContainDutyInfoViewModel.nornalContainDutyMiddle = normalizationInfoService.NormalizationContainDutyService(normalizationContainSearchViewModel);
            normalClaimsSearhResContainDutyInfoViewModel.isSuccess = true;
            normalClaimsSearhResContainDutyInfoViewModel.baseViewModel.Message = "查询成功";
            normalClaimsSearhResContainDutyInfoViewModel.baseViewModel.ResponseCode = 200;
            normalClaimsSearhResContainDutyInfoViewModel.TotalNum = 1;
            return Ok(normalClaimsSearhResContainDutyInfoViewModel);
        }

        /// <summary>
        /// 获取个人认领信息
        /// </summary>
        /// <param name="mydutyClaimInfoSearchViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMyDutyInfo(MydutyClaimInfoSearchViewModel mydutyClaimInfoSearchViewModel)
        {
            MydutyClaimInfoSearchResModel mydutyClaimInfoSearchResModel = new MydutyClaimInfoSearchResModel();
            mydutyClaimInfoSearchResModel.mydutyClaimInfoSearchMiddleModel = mydutyClaimInfoService.getMydutyInfoService(mydutyClaimInfoSearchViewModel);
            mydutyClaimInfoSearchResModel.isSuccess = true;
            mydutyClaimInfoSearchResModel.baseViewModel.Message = "查询成功";
            mydutyClaimInfoSearchResModel.baseViewModel.ResponseCode = 200;
            mydutyClaimInfoSearchResModel.TotalNum = 1;
            return Ok(mydutyClaimInfoSearchResModel);
        }




        /// <summary>
        /// 更新个人信息
        /// </summary>
        /// <param name="mydutyClaimInfoUpdateViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUpdateMyDutyInfo(MydutyClaimInfoUpdateViewModel mydutyClaimInfoUpdateViewModel)
        {

            mydutyClaimInfoService.getMydutyInfoUpdateService(mydutyClaimInfoUpdateViewModel);

            return Ok("更新成功");
        }

        /// <summary>
        /// 添加个人认领
        /// </summary>
        /// <param name="mydutyClaimInfoAddViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAddMyDutyInfo(MydutyClaimInfoAddViewModel  mydutyClaimInfoAddViewModel)
        {

            mydutyClaimInfoService.getMydutyInfoAddService(mydutyClaimInfoAddViewModel);

            return Ok("添加成功");
        }

    }
} 