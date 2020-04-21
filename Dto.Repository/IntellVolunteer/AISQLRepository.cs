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
    public class AISQLRepository: IAISQLRepository
    {
        protected string _connectionString = string.Empty;

        public AISQLRepository(string con)
        {
            _connectionString = con;
        }

        //积分信息插入到微官网积分表
        public int InsertPoints(AIpointMiddle middle)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                int res = 0;
                connection.Open();

                string insert = " insert into ET_points (id,UserId,unionid,points,type,createTime,tableName,mobile) values('"+ middle.ID + "','" +
                middle.UserID + "','"+ middle.unionid + "','"+ middle.points + "','"+ middle.type + "','"+DateTime.Now+"','"+ middle.tableName + "','"+ middle.mobile + "') ";

                res = connection.Execute(insert);
                return res;

            }
        }
    }
}
