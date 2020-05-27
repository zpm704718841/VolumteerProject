using Dtol;
using Dtol.dtol;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Dapper;
using ViewModel.VolunteerModel.RequsetModel;
using System.Linq.Expressions;
using Dtol.EfCoreExtion;
using ViewModel.VolunteerModel.MiddleModel;
using Dto.IRepository.IntellVolunteer;
using System.Data.SqlClient;
using System.Threading.Tasks;



namespace Dto.Repository.IntellVolunteer
{
    public class SQLRepository : ISQLRepository
    {
        protected string _connectionString = string.Empty;



        public SQLRepository(string  con)
        {
            _connectionString = con;
        }

        public async Task<string>   CheckSign(string VID , string ContentID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = string.Empty;
                connection.Open();

                string sql = "  select * FROM  VA_Sign " +
                  " where vid = '" + VID + "' and ContentID in (select id from VolunteerActivity " +
                  " where ((Stime >= ( select Stime FROM VolunteerActivity where id = '" + ContentID + "') " +
                  " and Stime <= (select Etime FROM VolunteerActivity where id ='" + ContentID + "')) " +
                  " or(Etime >= (select Stime FROM VolunteerActivity where id = '" + ContentID + "') " +
                  " and Etime <=(select Etime FROM VolunteerActivity where id = '" + ContentID + "'))) and id<>'" + ContentID + "') ";
                //var res = await connection.QueryAsync<dynamic>(sql);
                //var res =   connection.Query<dynamic>(sql);
                //if (res.ToList().Count == 0)
                //{
                //    result = "true";
                //}
                //else
                //{
                //    result = "false";
                //}
                return result;
                connection.Close();
            }
 
        }


        // 验证 是否 报名 同时段活动
        public string CheckSignNew(string VID, string ContentID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = string.Empty;
                connection.Open();

                string sql = "  select * FROM  VA_Sign " +
                  " where vid = '" + VID + "' and ContentID in (select id from VolunteerActivity " +
                  " where ((Stime >= ( select Stime FROM VolunteerActivity where id = '" + ContentID + "') " +
                  " and Stime <= (select Etime FROM VolunteerActivity where id ='" + ContentID + "')) " +
                  " or(Etime >= (select Stime FROM VolunteerActivity where id = '" + ContentID + "') " +
                  " and Etime <=(select Etime FROM VolunteerActivity where id = '" + ContentID + "'))) and id<>'" + ContentID + "') ";
                var res = connection.Query<dynamic>(sql);
                if (res.ToList().Count == 0)
                {
                    result = "true";
                }
                else
                {
                    result = "false";
                }

                connection.Close();
                return result;

            }

        }


        //首页 签到 定位当前时段活动 返回活动ID
        public string GetNowContent(string VID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var contentID = string.Empty;
                connection.Open();

                string sql = " select id from VolunteerActivity where id in (select ContentID FROM VA_Sign where vid = '" + VID + "') and Stime<= " + DateTime.Now + " and Etime>=" + DateTime.Now + " ";
                var res = connection.Query<dynamic>(sql);
                if (res.ToList().Count != 0)
                {
                    var model = new SearchByVIDModel();

                    foreach (dynamic item in res)
                    {
                        model.VID = item.id;
                    }
                    contentID = model.VID;
                }
                connection.Close();
                return contentID;

            }
            
        }


        //积分排名
        public List<VScoreRankMiddle> GetScoreRank()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
 
                connection.Open();

                string sql = " select bb.*,cc.Name,cc.Nickname,cc.VNO,cc.Community,cc.Headimgurl from (select aa.VID,aa.scores,rank () over( order by aa.scores desc) rankNo from ( " +
                            " select vid, sum(score) as scores  from Volunteer_Score  group by vid )as aa ) as bb left join Volunteer_Info cc on bb.VID = cc.ID ";
                var res = connection.Query<VScoreRankMiddle>(sql).ToList();
                connection.Close();
                return res;

            }
        }



        //获取该志愿者 参与志愿活动时长
        public string GetVA_SignHours(string VID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var hours = string.Empty;
                connection.Open();

                string sql = " select sum(datediff(HOUR,Stime,Etime))  as times FROM VolunteerActivity where id in (select ContentID FROM VA_Handle where VID = '" +VID + "' and type = 'in')  ";
                var res = connection.Query<BySQLMiddle>(sql);
                if (res.ToList().Count > 0)
                {
                    foreach (BySQLMiddle item in res)
                    {
                        hours = item.column;
                    }
                    if (hours == null)
                    {
                        hours = "0";
                    }
                }
                else
                {
                    hours = "0";
                }
                connection.Close();
                return hours;

            }
        }


        //获取该志愿者 参与的互助信息
        public List<VHA_SignMyListMiddle> GetVHA_Signs(string VID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = " select  *   from VHelpArea where  id in (select ContentID FROM  VHA_Sign where VID = '" + VID + "' and flag = '2' and Status = '1')  ";
                var res = connection.Query<VHA_SignMyListMiddle>(sql).ToList();

                connection.Close();
                return res;

            }
        }


        //获取 志愿者服务领域
        public string GetVServices(string VID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var Services = string.Empty;
                connection.Open();

                string sql = " select * from  VBaseType where id in (select TypeID FROM Volunteer_Relate_Type where VolunteerID = '" + VID + "') and ParentCode = '2' ";
                var res = connection.Query<VBaseType>(sql);
                if (res.ToList().Count > 0)
                {
                    foreach (VBaseType item in res)
                    {
                        Services += item.Name + "、";
                    }
                }

                connection.Close();
                return Services;

            }
        }


        //获取 志愿者擅长技能
        public string GetVSkills(string VID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var Services = string.Empty;
                connection.Open();

                string sql = " select * from  VBaseType where id in (select TypeID FROM Volunteer_Relate_Type where VolunteerID = '" + VID + "') and ParentCode = '1' ";
                var res = connection.Query<VBaseType>(sql);
                if (res.ToList().Count > 0)
                {
                    foreach (VBaseType item in res)
                    {
                        Services += item.Name + "、";
                    }
                }

                connection.Close();
                return Services;

            }
        }

        // 通过 社区名称 获取社区ID
        public string GetIDByName (string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var ID = string.Empty;
                connection.Open();

                string sql = " select  code  FROM  user_Depart where name = '"+ name + "' ";
                var res = connection.Query<BySQLMiddle>(sql);
                if (res.ToList().Count > 0)
                {
                    foreach (BySQLMiddle item in res)
                    {
                        ID = item.column;
                    }
                }

                connection.Close();
                return ID;

            }
        }


        //获取 技能对应的资质证明文件
        public List<SkillAndFileViewModel> GetSkillandFiles(string VID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = " select cc.id as SkillID,cc.name as SkillName,dd.ID as fileID,dd.Url,dd.Path,dd.bak1 from (select bb.*,aa.VolunteerID  "+
                           " FROM Volunteer_Relate_Type aa left join VBaseType bb on aa.TypeID = bb.ID "+
                           " where aa.VolunteerID = '"+ VID + "' and bb.ParentCode = '1') as cc right join VAttachment dd "+
                           " on cc.VolunteerID = dd.formid and cc.ID = dd.bak1 where dd.formid = '"+ VID + "' order by cc.id";
                var res = connection.Query<SkillAndFileViewModel>(sql).ToList();

                connection.Close();
                return res;

            }
        }

    }
}
