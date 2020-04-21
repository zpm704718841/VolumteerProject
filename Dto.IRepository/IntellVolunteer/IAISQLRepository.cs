using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ViewModel.VolunteerModel.MiddleModel;
namespace Dto.IRepository.IntellVolunteer
{
    public interface IAISQLRepository
    {
        //积分信息插入到微官网积分表
        int InsertPoints(AIpointMiddle middle);
    }
}
