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
     public interface ISQLRepository
    {
         Task<string> CheckSign(string VID, string ContentID);

        // 验证 是否 报名 同时段活动
        string CheckSignNew(string VID, string ContentID);

        //首页 签到 定位当前时段活动 返回活动ID
        string GetNowContent(string VID);

        //获取积分排名情况
        List<VScoreRankMiddle> GetScoreRank();

        //获取该志愿者 参与志愿活动时长
        string GetVA_SignHours(string VID);


        //获取该志愿者 参与的互助信息
        List<VHA_SignMyListMiddle> GetVHA_Signs(string VID);

        //获取 志愿者服务领域
        string GetVServices(string VID);


        //获取 志愿者擅长技能
        string GetVSkills(string VID);

        // 通过 社区名称 获取社区ID
        string GetIDByName(string name);


        //获取 技能对应的资质证明文件
        List<SkillAndFileViewModel> GetSkillandFiles(string VID);

    }
}
