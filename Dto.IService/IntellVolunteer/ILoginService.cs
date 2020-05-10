using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;

namespace Dto.IService.IntellVolunteer
{
    public interface ILoginService
    {

        //记录 用户登录 时间
        BaseViewModel SaveLoginInfo(string uid);
    }
}
