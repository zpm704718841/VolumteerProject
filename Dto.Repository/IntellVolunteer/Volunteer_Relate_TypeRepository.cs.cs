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
    public class Volunteer_Relate_TypeRepository : IVolunteer_Relate_TypeRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<Volunteer_Relate_Type> DbSet;

        public Volunteer_Relate_TypeRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<Volunteer_Relate_Type>();
        }

        public virtual void Add(Volunteer_Relate_Type obj)
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

        public virtual Volunteer_Relate_Type GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(Volunteer_Relate_Type obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public bool CheckInfo(string typeID, string VID)
        {
            var res = false;
            //查询条件
            var predicate = WhereExtension.True<Volunteer_Relate_Type>();//初始化where表达式

            if (!String.IsNullOrEmpty(typeID) && !String.IsNullOrEmpty(VID))
            {
                predicate = predicate.And(p => p.TypeID.Contains(typeID));
                predicate = predicate.And(p => p.VolunteerID.Contains(VID));
            }

            var result = DbSet.Where(predicate).ToList();
            if (result.Count > 0)
            {
                res = true;
            }

            return res;
        }


        public List<Volunteer_Relate_Type> GetMyTypeList(string VID)
        {
            var res = new Volunteer_Relate_Type();
            //查询条件
            var predicate = WhereExtension.True<Volunteer_Relate_Type>();//初始化where表达式

            if (!String.IsNullOrEmpty(VID))
            {
                predicate = predicate.And(p => p.VolunteerID.Contains(VID));
            }
            var result = DbSet.Where(predicate).ToList();

            return result;
        }

        public void RemoveAll(string userid)
        {
            List<Volunteer_Relate_Type> list = DbSet.Where(o => o.VolunteerID == userid).ToList();

            foreach (var item in list)
            {
                DbSet.Remove(item);
                SaveChanges();
            }
          
        }

    }
}
