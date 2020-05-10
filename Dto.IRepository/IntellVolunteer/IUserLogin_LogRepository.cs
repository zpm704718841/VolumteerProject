using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IUserLogin_LogRepository : IRepository<UserLogin_Log>
    {
        //获取用户最新的一次登录记录 20200402
        UserLogin_Log GetUserLogin(string uid);
    }
}
