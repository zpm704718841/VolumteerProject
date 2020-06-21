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
using ViewModel.VolunteerBackground.RequsetModel;
using ViewModel.VolunteerModel.RequsetModel.NormalViewModel;

namespace Dto.Repository.IntellVolunteer
{
    public class MydutyClaim_SignRepository: IMydutyClaim_SignRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<MydutyClaim_Sign> DbSet;

        public MydutyClaim_SignRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<MydutyClaim_Sign>();
        }

        public virtual void Add(MydutyClaim_Sign obj)
        {
            DbSet.Add(obj);
        }
        public virtual MydutyClaim_Sign GetById(Guid id)
        {
            return DbSet.Find(id);
        }
        public virtual void Update(MydutyClaim_Sign obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void RemoveNew(MydutyClaim_Sign obj)
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

        //根据条件查询
        public List<MydutyClaim_Sign> GetByParas(SearchByIDAnduidModel model)
        {
            //查询条件
            var predicate = WhereExtension.True<MydutyClaim_Sign>();//初始化where表达式

            if (!String.IsNullOrEmpty(model.uid))
            {
                predicate = predicate.And(p => p.Userid.Equals(model.uid));
            }
            if (!String.IsNullOrEmpty(model.MydutyClaim_InfoID))
            {
                predicate = predicate.And(p => p.MydutyClaim_InfoID.Equals(model.MydutyClaim_InfoID));
            }
            var result = DbSet.Where(predicate).OrderByDescending(o => o.CreateDate).ToList();
 
            return result;
        }

        public MydutyClaim_Sign GetByParasOne(SearchByIDAnduidModel model)
        {
            //查询条件
            var predicate = WhereExtension.True<MydutyClaim_Sign>();//初始化where表达式

            if (!String.IsNullOrEmpty(model.uid))
            {
                predicate = predicate.And(p => p.Userid.Equals(model.uid));
            }
            if (!String.IsNullOrEmpty(model.MydutyClaim_InfoID))
            {
                predicate = predicate.And(p => p.MydutyClaim_InfoID.Equals(model.MydutyClaim_InfoID));
            }
            var result = DbSet.Where(predicate).OrderByDescending(o => o.CreateDate).ToList();
            if(result.Count>0)
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
