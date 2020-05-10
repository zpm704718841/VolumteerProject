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
    public class V_ReadLogRepository: IV_ReadLogRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<V_ReadLog> DbSet;

        public V_ReadLogRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<V_ReadLog>();
        }
        public virtual void Add(V_ReadLog obj)
        {
            DbSet.Add(obj);
        }

        public virtual V_ReadLog GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(V_ReadLog obj)
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

        //获取用户阅读政策记录
        public V_ReadLog GetReadLog(string openid, string type)
        {
            V_ReadLog log = new V_ReadLog();
            //查询条件
            var predicate = WhereExtension.True<V_ReadLog>();//初始化where表达式
            //type='policy'  为有隐私政策 
            predicate = predicate.And(p => p.type.Contains(type));
            if (!String.IsNullOrEmpty(openid))
            {
                predicate = predicate.And(p => p.openid.Contains(openid));
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
