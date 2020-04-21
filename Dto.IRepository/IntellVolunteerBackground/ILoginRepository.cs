using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerBackground.RequsetModel;

namespace Dto.IRepository.IntellVolunteerBackground
{
    public interface ILoginRepository:IRepository<User_Info>
    {
        List<User_Relate_Info_Role> SearchInfoByWhere(int id);
        User_Info ValideUserInfo(LoginViewModel weChatLoginViewModel);
    }
}
