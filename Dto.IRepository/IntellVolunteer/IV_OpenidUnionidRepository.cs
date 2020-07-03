using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IV_OpenidUnionidRepository : IRepository<V_OpenidUnionid>
    {
        V_OpenidUnionid GetByParasOne(string openid);
    }
}
