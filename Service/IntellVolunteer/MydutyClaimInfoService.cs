using AutoMapper;
using Dto.IRepository.IntellVolunteer;
using Dto.IService.IntellVolunteer;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;
using ViewModel.VolunteerModel.RequsetModel.NormalViewModel;

namespace Dto.Service.IntellVolunteer
{
    public class MydutyClaimInfoService: IMydutyClaimInfoService
    {

        private readonly IMydutyClaimInfoRepository _IMydutyClaimInfoRepository;
        private readonly IMapper _IMapper;

        public MydutyClaimInfoService(IMydutyClaimInfoRepository iMydutyClaimInfoRepository, IMapper iMapper)
        {
            _IMydutyClaimInfoRepository = iMydutyClaimInfoRepository;
            _IMapper = iMapper;
        }

        public void getMydutyInfoAddService(MydutyClaimInfoAddViewModel mydutyClaimInfoAddViewModel)
        {
            
               var result = _IMapper.Map<MydutyClaimInfoAddViewModel, MydutyClaim_Info > (mydutyClaimInfoAddViewModel);
            _IMydutyClaimInfoRepository.Add(result);
            _IMydutyClaimInfoRepository.SaveChanges();

        }

        public List<MydutyClaimInfoSearchMiddleModel> getMydutyInfoService(MydutyClaimInfoSearchViewModel  mydutyClaimInfoSearchViewModel)
        {
            var searchresult = _IMydutyClaimInfoRepository.getMydutyInfo(mydutyClaimInfoSearchViewModel);
            List<MydutyClaimInfoSearchMiddleModel> lists = new List<MydutyClaimInfoSearchMiddleModel>();
           
            foreach (var item in searchresult)
            {
                var res = new MydutyClaimInfoSearchMiddleModel();
                res.id = item.id;
                res.StartDutyTime = item.StartDutyTime;
                res.EndDutyTime = item.EndDutyTime;
                res.CreateDate = item.CreateDate;
                res.status = item.status;
                res.title = item.OndutyClaims_Info.Normalization_Info.title;
                res.Userid = item.Userid;
                res.UserName = item.UserName;
                res.XiaoCommunityName = item.OndutyClaims_Info.Subdistrict;
                lists.Add(res);
            }
            return lists;

        }

        public void getMydutyInfoUpdateService(MydutyClaimInfoUpdateViewModel mydutyClaimInfoUpdateViewModel)
        {
            var searchresult= _IMydutyClaimInfoRepository.GetInfoById(mydutyClaimInfoUpdateViewModel.id);
            var updatemodel= _IMapper.Map<MydutyClaimInfoUpdateViewModel, MydutyClaim_Info>(mydutyClaimInfoUpdateViewModel, searchresult);//mapper没配置
            updatemodel.UpdateDate = DateTime.Now;
            updatemodel.UpdateUser = updatemodel.Userid;

            _IMydutyClaimInfoRepository.Update(updatemodel);
            _IMydutyClaimInfoRepository.SaveChanges();
        }
    }
}
