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
    public class LoginType_LogRepository: ILoginType_LogRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<LoginType_Log> DbSet;

        public LoginType_LogRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<LoginType_Log>();
        }
        public virtual void Add(LoginType_Log obj)
        {
            DbSet.Add(obj);
        }

        public virtual LoginType_Log GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(LoginType_Log obj)
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

        public LoginType_Log SearchByUIDModel(SearchByVIDModel user)
        {
            var res = new LoginType_Log();
            //查询条件
            var predicate = WhereExtension.True<LoginType_Log>();//初始化where表达式
            //status='true'  为有效登录方式信息
            predicate = predicate.And(p => p.status.Contains("true"));
            if (!String.IsNullOrEmpty(user.VID))
            {
                predicate = predicate.And(p => p.uid.Contains(user.VID));
            }
            var result = DbSet.Where(predicate).OrderByDescending(p => p.CreateDate).ToList();
            if (result.Count != 0)
            {
                res = result.First();
            }
            else
            {
                res = null;
            }

            return res;
        }

    }
}
