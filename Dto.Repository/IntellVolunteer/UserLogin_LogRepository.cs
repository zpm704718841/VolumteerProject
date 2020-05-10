using Dtol;
using Dtol.dtol;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using System.Linq.Expressions;
using Dtol.EfCoreExtion;
using ViewModel.VolunteerModel.MiddleModel;
using Dto.IRepository.IntellVolunteer;
using ViewModel.VolunteerModel.ResponseModel;
 

namespace Dto.Repository.IntellVolunteer
{
    public class UserLogin_LogRepository: IUserLogin_LogRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<UserLogin_Log> DbSet;

        public UserLogin_LogRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<UserLogin_Log>();
        }
        public virtual void Add(UserLogin_Log obj)
        {
            DbSet.Add(obj);
        }

        public virtual UserLogin_Log GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(UserLogin_Log obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }


        //获取用户最新的一次登录记录 20200402
        public UserLogin_Log GetUserLogin(string uid)
        {
            UserLogin_Log log = new UserLogin_Log();
            //查询条件
            var predicate = WhereExtension.True<UserLogin_Log>();//初始化where表达式
            //status='true'  为有效登录时间信息
            predicate = predicate.And(p => p.status.Contains("true"));
            if (!String.IsNullOrEmpty(uid))
            {
                predicate = predicate.And(p => p.uid.Contains(uid));
            }
            var result = DbSet.Where(predicate).ToList();
            if (result.Count != 0)
            {
                log = result.First();
            }
            else
            {
                log = null;
            }
            return log;
        }
    }
}
