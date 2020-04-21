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
    public class VHA_HandleRepository : IVHA_HandleRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<VHA_Handle> DbSet;

        public VHA_HandleRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<VHA_Handle>();
        }

        public virtual void Add(VHA_Handle obj)
        {
            DbSet.Add(obj);
        }

        public virtual VHA_Handle GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(VHA_Handle obj)
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

        public List<VHA_Handle> GetMyHandle(string VID, string ContentID)
        {
            //查询条件
            var predicate = WhereExtension.True<VHA_Handle>();//初始化where表达式

            if (!String.IsNullOrEmpty(VID))
            {
                predicate = predicate.And(p => p.VID.Contains(VID));
            }
            if (!String.IsNullOrEmpty(ContentID))
            {
                predicate = predicate.And(p => p.ContentID.Contains(ContentID));
            }

            var result = DbSet.Where(predicate)
                .OrderBy(o => o.CreateDate).ToList();
   
            return result;
        }

        //获取 处理信息
        public VHA_Handle GetVolunteerHandle(string ID)
        {
            VHA_Handle model = new VHA_Handle();
            //查询条件
            var predicate = WhereExtension.True<VHA_Handle>();//初始化where表达式

            if (!String.IsNullOrEmpty(ID))
            {
                predicate = predicate.And(p => p.ID.Contains(ID));
            }
            var result = DbSet.Where(predicate)
              .OrderBy(o => o.CreateDate).ToList();

            if (result.Count() > 0)
            {
                model = result.First();
            }
            return model;
        }
    }
}
