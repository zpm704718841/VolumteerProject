using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IVActivity_PublicShowRepository : IRepository<VActivity_PublicShow>
    {
        //展示公所有益秀信息（志愿活动ID，活动标题，活动地址，参与感受，图片列表 等等）
        List<VActivity_PublicShow> GetPublicShowList();

        //根据id获取 该公益秀信息
        VActivity_PublicShow SearchInfoByID(string id);
    }
}
