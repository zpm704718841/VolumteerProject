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
using Dto.IRepository.IntellWeChat;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Dto.Repository.IntellWeChat
{
    public class SQLRepository: ISQLRepository
    {
        protected string _connectionString = string.Empty;

        public SQLRepository(string con)
        {
            _connectionString = con;
        }

        // 通过 社区名称 获取社区ID
        public string GetIDByName(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var ID = string.Empty;
                connection.Open();

                string sql = " select  code  FROM  user_Depart where name = '" + name + "' ";
      

                var res = connection.Query<dynamic>(sql);
                if (res.ToList().Count != 0)
                {
                    var model = new BySQLMiddle();

                    foreach (dynamic item in res)
                    {
                        model.column = item.code;
                    }
                    ID = model.column;
                }
                return ID;

            }
        }
    }
}
