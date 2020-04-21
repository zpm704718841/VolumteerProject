using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerBackground.MiddleModel;
using ViewModel.VolunteerBackground.RequsetModel;
using ViewModel.VolunteerBackground.ResponseModel;

namespace Dto.IService.IntellVolunteerBackground
{
    public interface IIntellVolunteerBackgroundILoginService
    {
        LoginMidModel VolunteerBackgroundLogin_User(LoginViewModel weChatLoginViewModel);
        VolunteerBackgroundInfoModel IntellVolunteerBackgroundLogin_Search(VolunteerBackgroundInfoViewModel weChatInfoViewModel);
    }
}
