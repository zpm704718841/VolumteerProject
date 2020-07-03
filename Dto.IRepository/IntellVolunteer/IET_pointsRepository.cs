using Dtol.Easydtol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IET_pointsRepository : IRepository<ET_points>
    {   
        //删除信息
        void RemoveInfo(ET_points user);

        //根据ID 获取 积分记录
        ET_points GetByID(string id);
    }
}
