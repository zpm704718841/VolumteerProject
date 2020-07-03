using Dto.IRepository.IntellVolunteer;
using System;
using System.Collections.Generic;
using System.Text;
using Dtol.Easydtol;
using Dtol;
using Microsoft.EntityFrameworkCore;
using Dtol.EfCoreExtion;
using System.Linq;

namespace Dto.Repository.IntellVolunteer
{
    public class ET_pointsRepository: IET_pointsRepository
    {
        protected readonly EasyDtolContext Db;
        protected readonly DbSet<ET_points> DbSet;

        public ET_pointsRepository(EasyDtolContext context)
        {
            Db = context;
            DbSet = Db.Set<ET_points>();
        }
        public virtual void Add(ET_points obj)
        {
            DbSet.Add(obj);
        }

        public virtual ET_points GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(ET_points obj)
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

        //删除信息
        public void RemoveInfo(ET_points points)
        {
            DbSet.Remove(points);
        }


        //根据ID 获取 积分记录
        public ET_points GetByID(string id)
        {
            //查询条件
            var predicate = WhereExtension.True<ET_points>();//初始化where表达式

            if (id != "")
            {
                predicate = predicate.And(p => p.ID.Contains(id));
            }

            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.CreateDate).ToList();

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
