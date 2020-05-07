using AutoMapper;
using Dto.IRepository.IntellVolunteer;
using Dto.IService.IntellVolunteer;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerBackground.MiddleModel;
using ViewModel.VolunteerBackground.ResponseModel;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.RequsetModel.NormalViewModel;
using ViewModel.VolunteerModel.ResponseModel.NormalViewModel;

namespace Dto.Service.IntellVolunteer
{
    public class NormalizationInfoService: INormalizationInfoService
    {
        private readonly INormalizationInfoRepository _INormalizationInfoRepository;
        private readonly IMydutyClaimInfoRepository _IMydutyClaimInfoRepository;
        private readonly IMapper _IMapper;
        private readonly IVolunteerInfoRepository _IVolunteerInfoRepository;

        public NormalizationInfoService(INormalizationInfoRepository iNormalizationInfoRepository,
            IMydutyClaimInfoRepository iMydutyClaimInfoRepository, IMapper iMapper, IVolunteerInfoRepository infoRepository)
        {
            _INormalizationInfoRepository = iNormalizationInfoRepository;
            _IMydutyClaimInfoRepository = iMydutyClaimInfoRepository;
            _IMapper = iMapper;
            _IVolunteerInfoRepository = infoRepository;
        }

        public void AddNormalizationInfoService(NormalAddViewModel normalAddViewModel)
        {
            var xiaocommunityName = normalAddViewModel.XiaoCommunityName.Split(',');
            var xiaocommunitycode = normalAddViewModel.XiaoCommunityNameeCode.Split(',');

            for (int i = 0; i < xiaocommunityName.Length; i++)
            {
                var addmodel = _IMapper.Map<NormalAddViewModel, Normalization_Info>(normalAddViewModel);
                addmodel.XiaoCommunityName = xiaocommunityName[i];
                addmodel.XiaoCommunityNameeCode = xiaocommunitycode[i];
                _INormalizationInfoRepository.Add(addmodel);
            }
            _INormalizationInfoRepository.SaveChanges();
        }

    

        public NornalContainDutyMiddle NormalizationContainDutyService(NormalizationContainSearchViewModel normalizationContainSearchViewModel)
        {
            var searchresult=  _INormalizationInfoRepository.NormalizationContainDuty(normalizationContainSearchViewModel);


            var result = _IMapper.Map<Normalization_Info, NornalContainDutyMiddle>(searchresult);
            if (result == null)
            {
                return result;
            }

            for (int i = 0; i < result.containOnDutyMiddleMiddles.Count; i++)
            {
                  int CountIsDuty;
                if (searchresult.ondutyClaims_Infos[i].mydutyClaim_Infos == null)
                {
                    CountIsDuty = 0;
                    result.containOnDutyMiddleMiddles[i].isClaim = "认领";
                    result.containOnDutyMiddleMiddles[i].mydutyClaim_Infos = null;


                }
                else
                {
                    var lists = searchresult.ondutyClaims_Infos[i].mydutyClaim_Infos.Where(a => a.OndutyClaims_InfoId == searchresult.ondutyClaims_Infos[i].id).ToList();
                    result.containOnDutyMiddleMiddles[i].mydutyClaim_Infos = _IMapper.Map<List<MydutyClaim_Info>,List<MydutyClaimInfoMiddleModel>>(lists);

                    CountIsDuty = searchresult.ondutyClaims_Infos[i].mydutyClaim_Infos.Count();
                    result.containOnDutyMiddleMiddles[i].ReportNum = CountIsDuty;
                    if (CountIsDuty == searchresult.ondutyClaims_Infos[i].TotalReportNum)
                    {
                        result.containOnDutyMiddleMiddles[i].isClaim = "已满";
                    }
                    else
                    {
                        result.containOnDutyMiddleMiddles[i].isClaim = searchresult.ondutyClaims_Infos[i].mydutyClaim_Infos
                                                              .Exists(a => a.Userid == normalizationContainSearchViewModel.userid) == true ? "已认领" : "认领";
                    }

                } 
            }
            return result;

        }

        public List<NornalOndutySearchMiddlecs> NormalizationSeachService(NormalSearchViewModel normalSearchViewModel)
        {     
            var result= _INormalizationInfoRepository.NormalizationSearch(normalSearchViewModel);
            var searchresult = _IMapper.Map<List<Normalization_Info> ,List<NornalOndutySearchMiddlecs> >(result);
            return searchresult;
        }


        //获取 小区信息
        public List<UserDepartSearchMidModel> GetDepartList(CodeViewModel code)
        {
            UserDepartResModel returnDepart = new UserDepartResModel();

            List<User_Depart> departAll = _IVolunteerInfoRepository.GetDepartAll();

            var departList = _IMapper.Map<List<User_Depart>, List<UserDepartSearchMidModel>>(departAll);

            List<UserDepartSearchMidModel> result = new List<UserDepartSearchMidModel>();
            result.AddRange(departList.Where(p => p.ParentId == code.code).ToList());
          
            return result;
        }




        // (小程序端接口)  获取 该值班信息的认领人信息
        public List<MydutyClaimInfoSearchMiddleModel> GetDutyListByID(CodeViewModel code)
        {
            List<MydutyClaimInfoSearchMiddleModel> middleModels = new List<MydutyClaimInfoSearchMiddleModel>();
            var list = _IMydutyClaimInfoRepository.GetByOndutyClaims_InfoID(code.code);

            middleModels = _IMapper.Map<List<MydutyClaim_Info>, List<MydutyClaimInfoSearchMiddleModel>>(list);

            return middleModels;
        }

    }
}
