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
    public interface IVHA_SignRepository : IRepository<VHA_Sign>
    {
        List<String> GetMyList(string VID);

        //根据 contentID，VID 获取 该志愿者针对该互助信息 最新状态
        VHA_Sign GetNewSign(string VID, string ContentID);

        //志愿者互助信息退出功能 （互助ID标识ContentID、志愿者唯一VID、擅长技能TypeID为空不传参数）
        string SetStatusBack(string VID, string ContentID);

        //获取该 互助信息对应的 认领信息
        List<VHA_Sign> GetByContentID(string ContentID, string flag);



        //志愿者成功参与的 互助次数（只有最终审核处理结果通过才算成功）
        int GetMyHelpAreaNums(string VID);

        //通过 handle 查找 sign记录表里的 opinion 审核意见
        VHA_Sign GetMySign(string handleID);


    }
}
