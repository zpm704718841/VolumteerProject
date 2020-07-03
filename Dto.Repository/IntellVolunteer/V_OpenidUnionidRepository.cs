using Dto.IRepository.IntellVolunteer;
using Dtol;
using Dtol.dtol;
using Dtol.EfCoreExtion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dto.Repository.IntellVolunteer
{
   public class V_OpenidUnionidRepository: IV_OpenidUnionidRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<V_OpenidUnionid> DbSet;

        public V_OpenidUnionidRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<V_OpenidUnionid>();
        }
 
        public virtual void Add(V_OpenidUnionid obj)
        {
            DbSet.Add(obj);
        }
        public virtual V_OpenidUnionid GetById(Guid id)
        {
            return DbSet.Find(id);
        }
        public virtual void Update(V_OpenidUnionid obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void RemoveNew(V_OpenidUnionid obj)
        {
            DbSet.Remove(obj);
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

        public V_OpenidUnionid GetByParasOne(string openid)
        {
            //查询条件
            var predicate = WhereExtension.True<V_OpenidUnionid>();//初始化where表达式

            if (!String.IsNullOrEmpty(openid))
            {
                predicate = predicate.And(p => p.openid.Equals(openid));
            }
            
            var result = DbSet.Where(predicate).ToList();
            if (result.Count > 0)
            {
                return result.First();
            }
            else
            {
                return null;
            }
        }
    }
}
