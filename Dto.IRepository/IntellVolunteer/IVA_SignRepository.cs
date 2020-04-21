using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerBackground.RequsetModel;


namespace Dto.IRepository.IntellVolunteer
{
    public interface IVA_SignRepository : IRepository<VA_Sign>
    {
        int GetCount(string ContentID, string typeID);

        List<String> GetMyList(string VID);

        VA_Sign GetNewSign(string VID, string ContentID);

        //获取该活动的报名人数
        int GetSingNum(string ContentID);

        void RemoveNew(VA_Sign obj);

    }
}
