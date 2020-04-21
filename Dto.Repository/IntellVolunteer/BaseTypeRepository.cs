using Dtol;
using Dtol.dtol;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using ViewModel.VolunteerModel.RequsetModel;
using System.Linq.Expressions;
using Dtol.EfCoreExtion;
using ViewModel.VolunteerModel.MiddleModel;
using Dto.IRepository.IntellVolunteer;

namespace Dto.Repository.IntellVolunteer
{
    public class BaseTypeRepository : IBaseTypeRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<VBaseType> DbSet;
      

        public BaseTypeRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<VBaseType>();
        }

        public virtual void Add(VBaseType obj)
        {
            DbSet.Add(obj);
        }

        public virtual VBaseType GetById(Guid id)
        {
            return DbSet.Find(id);
        }

       

        public virtual void Update(VBaseType obj)
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

        //根据条件查询

        public List<VBaseType> SearchInfoByWhere()
        {
            var predicate = WhereExtension.True<VBaseType>();

            var result = DbSet.Where(predicate)
                .OrderBy(o => o.ParentCode).ToList();

            return result;
        }

        public List<VBaseType> SearchInfoByWhere(string parentCode)
        {
            var predicate = WhereExtension.True<VBaseType>();
            if (!String.IsNullOrEmpty(parentCode))
            {
                predicate = predicate.And(p => p.ParentCode.Contains(parentCode));
            }

            var result = DbSet.Where(predicate)
                .OrderBy(o => o.ParentCode).ToList();

            return result;
        }


        public bool CheckStatus(string TypeID)
        {
            var res = false;

            var predicate = WhereExtension.True<VBaseType>();
            if (!String.IsNullOrEmpty(TypeID))
            {
                predicate = predicate.And(p => p.ID.Equals(TypeID));
            }
            var result = DbSet.Where(predicate).ToList();
            if (result.Count > 0)
            {
                //判断是否需要 资质证明
                if (result.First().Status == "1")
                {
                    res = true;
                }
            }
            return res;
        }


    }
}
