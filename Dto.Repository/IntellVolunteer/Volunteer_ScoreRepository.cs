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
    public class Volunteer_ScoreRepository : IVolunteer_ScoreRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<Volunteer_Score> DbSet;

        public Volunteer_ScoreRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<Volunteer_Score>();
        }

        public virtual void Add(Volunteer_Score obj)
        {
            DbSet.Add(obj);
        }

        public virtual Volunteer_Score GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(Volunteer_Score obj)
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

        //积分
        public string GetMyScore(SearchByVIDModel searchModel)
        {
            //查询条件
            var predicate = WhereExtension.True<Volunteer_Score>();//初始化where表达式

            if (!String.IsNullOrEmpty(searchModel.VID))
            {
                predicate = predicate.And(p => p.VID.Contains(searchModel.VID));
            }

            var res = DbSet.Where(predicate).ToList();
            double sums = 0;
            res.ForEach(o => { sums += o.Score; });


            string result = sums.ToString();
            return result;
        }

 

    }
}
