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
    public class Volunteer_MessageRepository: IVolunteer_MessageRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<Volunteer_Message> DbSet;


        public Volunteer_MessageRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<Volunteer_Message>();
        }

        public virtual void Add(Volunteer_Message obj)
        {
            DbSet.Add(obj);
        }

        public virtual Volunteer_Message GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(Volunteer_Message obj)
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

        public  Volunteer_Message GetByID(string ID, string VID)
        {
            Volunteer_Message model = new Volunteer_Message();
            //查询条件
            var predicate = WhereExtension.True<Volunteer_Message>();//初始化where表达式

            if (!String.IsNullOrEmpty(ID))
            {
                predicate = predicate.And(p => p.ID.Contains(ID));
            }
            if (!String.IsNullOrEmpty(VID))
            {
                predicate = predicate.And(p => p.VID.Contains(VID));
            }
            var result = DbSet.Where(predicate)
              .OrderBy(o => o.CreateDate).ToList();

            if (result.Count() > 0)
            {
                model = result.First();
            }
            return model;
        }

        public List<Volunteer_Message> GetByVID(string VID)
        {
            //查询条件
            var predicate = WhereExtension.True<Volunteer_Message>();//初始化where表达式
            if (!String.IsNullOrEmpty(VID))
            {
                predicate = predicate.And(p => p.VID.Contains(VID));
            }
            var result = DbSet.Where(predicate)
              .OrderByDescending(o => o.CreateDate).ToList();


            return result;
        }

    }
}
