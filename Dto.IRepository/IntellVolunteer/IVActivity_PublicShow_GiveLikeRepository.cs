using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IVActivity_PublicShow_GiveLikeRepository : IRepository<VActivity_PublicShow_GiveLike>
    {
        //取消点赞
        void RemoveNew(VActivity_PublicShow_GiveLike giveLike);
        //获取当前志愿者针对该公益秀的点赞信息
        VActivity_PublicShow_GiveLike GetLike(string VID, string PublicShowID);

        //获取该公益秀的 点赞信息
        List<VActivity_PublicShow_GiveLike> GetLikeList(string PublicShowID);
    }
}
