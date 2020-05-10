﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Dto.IService.IntellVolunteer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerBackground.ResponseModel;
using ViewModel.VolunteerModel.RequsetModel;
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

        /// <summary>
        /// (小程序端接口)  获取 小区  信息   （社区id）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<UserDepartResModel> Manage_SearchDepart(CodeViewModel code)
        {
            UserDepartResModel u_depart = new UserDepartResModel();
            var u_departMidModel = normalizationInfoService.GetDepartList(code);
            u_depart.IsSuccess = true;
            u_depart.UserDepart = u_departMidModel;
            u_depart.baseViewModel.Message = "查询成功";
            u_depart.baseViewModel.ResponseCode = 200;
 
            return Ok(u_depart);

        }


        /// <summary>
        /// (小程序端接口)  获取 该值班信息的认领人信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<MydutyClaimInfoSearchResModel> GetDutyListByID(CodeViewModel code)
        {
            MydutyClaimInfoSearchResModel searchResModel = new MydutyClaimInfoSearchResModel();
            var lists = normalizationInfoService.GetDutyListByID(code);
            searchResModel.isSuccess = true;
            searchResModel.mydutyClaimInfoSearchMiddleModel = lists;
            searchResModel.baseViewModel.Message = "查询成功";
            searchResModel.baseViewModel.ResponseCode = 200;

            return Ok(searchResModel);

        }


        /// <summary>
        /// (小程序端接口)  值班认领 上传现场服务照片  （认领值班ID、uid、 现场图片信息列表）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BaseViewModel> MyDutySign_SubmitImg(MyDutySignImgAddModel AddViewModel)
        {

            BaseViewModel AddResModel = new BaseViewModel();

            if (!String.IsNullOrEmpty(AddViewModel.uid) || !String.IsNullOrEmpty(AddViewModel.MydutyClaim_InfoID))
            {
                int Add_Count = mydutyClaimInfoService.SubmitImg(AddViewModel);
                if (Add_Count >= 1)
                {
                    AddResModel.Message = "上传成功";
                    AddResModel.ResponseCode = 200;
                }
                else
                {
                    AddResModel.Message = "上传失败";
                    AddResModel.ResponseCode = 400;
                }

            }
            else
            {
                AddResModel.Message = "参数为空，上传失败";
                AddResModel.ResponseCode = 500;
            }
            
            return Ok(AddResModel);
        }




        /// <summary>
        /// (小程序端接口) 获取该认领信息具体情况 包括签到、签退 现场图片等
        /// </summary>
        /// <param name="SearchByIDAnduidModel "></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<MydutyClaim_InfoResModel> GetMydutyDetail(SearchByIDAnduidModel viewModel)
        {
            MydutyClaim_InfoResModel SearchResModel = new MydutyClaim_InfoResModel();
            if (!string.IsNullOrEmpty(viewModel.uid) && !string.IsNullOrEmpty(viewModel.MydutyClaim_InfoID))
            {
                SearchResModel = mydutyClaimInfoService.GetMydutyDetail(viewModel);
            }
            else
            {
                SearchResModel.claim_SignInfo = null;
                SearchResModel.MiddleModel = null;
                SearchResModel.isSuccess = false;
                SearchResModel.baseViewModel.Message = "参数为空";
                SearchResModel.baseViewModel.ResponseCode = 500;
                SearchResModel.TotalNum = 0;
            }
            return Ok(SearchResModel);

        }


    }
} 