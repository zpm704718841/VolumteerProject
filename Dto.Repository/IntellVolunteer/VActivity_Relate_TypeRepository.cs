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


namespace Dto.Repository.IntellVolunteer
{
    public class VActivity_Relate_TypeRepository : IVActivity_Relate_TypeRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<VActivity_Relate_Type> DbSet;

        public VActivity_Relate_TypeRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<VActivity_Relate_Type>();
        }

        public virtual void Add(VActivity_Relate_Type obj)
        {
            DbSet.Add(obj);
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

        public virtual VActivity_Relate_Type GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(VActivity_Relate_Type obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public List<VActivity_Relate_Type> GetRelateList(string id)
        {
            //查询条件
            var predicate = WhereExtension.True<VActivity_Relate_Type>();//初始化where表达式

            if (!String.IsNullOrEmpty(id))
            {
                predicate = predicate.And(p => p.ContentID.Contains(id));
            }
            var result = DbSet.Where(predicate).ToList();


            return result;
        }


        public int GetSum(string ContentID, string typeID)
        {
            //查询条件
            var predicate = WhereExtension.True<VActivity_Relate_Type>();//初始化where表达式

            if (!String.IsNullOrEmpty(ContentID) )
            {
                predicate = predicate.And(p => p.ContentID.Equals(ContentID));
            }
            if (!String.IsNullOrEmpty(typeID))
            {
                predicate = predicate.And(p => p.TypeID.Equals(typeID));
            }
            var list = DbSet.Where(predicate).ToList();
            var result = 0;
            if (list.Count > 0)
            {
                result = list.First().Count;
            }
            return result;
        }
    }
}
