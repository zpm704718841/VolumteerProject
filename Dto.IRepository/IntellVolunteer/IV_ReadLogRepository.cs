using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IV_ReadLogRepository : IRepository<V_ReadLog>
    {
        //获取用户阅读政策记录
        V_ReadLog GetReadLog(string uid, string type);
    }
}
