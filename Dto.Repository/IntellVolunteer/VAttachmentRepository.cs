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
    public class VAttachmentRepository : IVAttachmentRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<VAttachment> DbSet;

        public VAttachmentRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<VAttachment>();
        }

        public virtual void Add(VAttachment obj)
        {
            DbSet.Add(obj);
        }

        public virtual VAttachment GetById(Guid id)
        {
            return DbSet.Find(id);
        }



        public virtual void Update(VAttachment obj)
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

        public List<VAttachment> GetMyList(string formid)
        {
            //查询条件
            var predicate = WhereExtension.True<VAttachment>();//初始化where表达式
 
            if (!String.IsNullOrEmpty(formid))
            {
                predicate = predicate.And(p => p.formid.Contains(formid));
            }
           
            var result = DbSet.Where(predicate)
                .OrderBy(o => o.CreateDate).ToList();

            return result;
        }


        public void RemoveAll(string formid,string type)
        {
            //查询条件
            var predicate = WhereExtension.True<VAttachment>();//初始化where表达式

            if (!String.IsNullOrEmpty(formid))
            {
                predicate = predicate.And(p => p.formid.Contains(formid));
            }
            if (!String.IsNullOrEmpty(type))
            {
                predicate = predicate.And(p => p.type.Contains(type));
            }
            List<VAttachment> list = DbSet.Where(predicate).ToList();

            foreach (var item in list)
            {
                DbSet.Remove(item);
                SaveChanges();
            }

        }

    }
}
