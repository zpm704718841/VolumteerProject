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

namespace Dto.Repository.IntellVolunteer
{
    public class VA_HandleRepository : IVA_HandleRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<VA_Handle> DbSet;

        public VA_HandleRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<VA_Handle>();
        }

        public virtual void Add(VA_Handle obj)
        {
            DbSet.Add(obj);
        }

        public virtual VA_Handle GetById(Guid id)
        {
            return DbSet.Find(id);
        }



        public virtual void Update(VA_Handle obj)
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

        public VA_Handle GetNewSign(string VID, string ContentID)
        {
            VA_Handle res = new VA_Handle();
            //查询条件
            var predicate = WhereExtension.True<VA_Handle>();//初始化where表达式

            if (!String.IsNullOrEmpty(VID))
            {
                predicate = predicate.And(p => p.VID.Equals(VID));
            }
            if (!String.IsNullOrEmpty(ContentID))
            {
                predicate = predicate.And(p => p.ContentID.Equals(ContentID));
            }
            var result = DbSet.Where(predicate).OrderByDescending(o => o.CreateDate).ToList();
            if (result.Count > 0)
            {
                res = result.First();
            }
            return res;
        }


        //获取该志愿者 参与志愿活动次数
        public string GetMyInTimes(string VID)
        {
            string times = "0";

            var predicate = WhereExtension.True<VA_Handle>();//初始化where表达式
            predicate = predicate.And(p => p.type.Equals("in"));
            if (!String.IsNullOrEmpty(VID))
            {
                predicate = predicate.And(p => p.VID.Equals(VID));
            }
            var result = DbSet.Where(predicate).OrderByDescending(o => o.CreateDate).ToList();
            times = result.Count.ToString();
            return times;
        }


        public List<VA_Handle> GetMySign(string VID, string ContentID)
        {
            //查询条件
            var predicate = WhereExtension.True<VA_Handle>();//初始化where表达式

            if (!String.IsNullOrEmpty(VID))
            {
                predicate = predicate.And(p => p.VID.Equals(VID));
            }
            if (!String.IsNullOrEmpty(ContentID))
            {
                predicate = predicate.And(p => p.ContentID.Equals(ContentID));
            }
            var result = DbSet.Where(predicate).OrderByDescending(o => o.CreateDate).ToList();
  
            return result;
        }

    }
}
