using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IVA_HandleRepository : IRepository<VA_Handle>
    {
        VA_Handle GetNewSign(string VID, string ContentID);


        //获取该志愿者 参与志愿活动次数
        string GetMyInTimes(string VID);

        List<VA_Handle> GetMySign(string VID, string ContentID);
    }
}
