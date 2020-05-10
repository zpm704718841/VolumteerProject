using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;
using ViewModel.VolunteerModel.RequsetModel.NormalViewModel;
using ViewModel.VolunteerModel.ResponseModel.NormalViewModel;

namespace Dto.IService.IntellVolunteer
{
    public  interface IMydutyClaimInfoService
    {
        List<MydutyClaimInfoSearchMiddleModel> getMydutyInfoService(MydutyClaimInfoSearchViewModel  mydutyClaimInfoSearchViewModel);
        void getMydutyInfoUpdateService(MydutyClaimInfoUpdateViewModel mydutyClaimInfoUpdateViewModel);
        void getMydutyInfoAddService(MydutyClaimInfoAddViewModel mydutyClaimInfoAddViewModel);


        // 值班认领 上传现场服务照片
        int SubmitImg(MyDutySignImgAddModel AddViewModel);


        //获取该认领信息具体情况  包括签到、签退 现场图片等
        MydutyClaim_InfoResModel GetMydutyDetail(SearchByIDAnduidModel viewModel);
    }
}
