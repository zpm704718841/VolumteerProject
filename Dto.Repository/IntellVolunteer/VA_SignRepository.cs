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
    public class VA_SignRepository: IVA_SignRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<VA_Sign> DbSet;

        public VA_SignRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<VA_Sign>();
        }

        public virtual void Add(VA_Sign obj)
        {
            DbSet.Add(obj);
        }
        public virtual VA_Sign GetById(Guid id)
        {
            return DbSet.Find(id);
        }
        public virtual void Update(VA_Sign obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public  void RemoveNew(VA_Sign obj)
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

        public int GetCount(string ContentID,string typeID)
        {
            //查询条件
            var predicate = WhereExtension.True<VA_Sign>();//初始化where表达式

            if (!String.IsNullOrEmpty(ContentID) && !String.IsNullOrEmpty(typeID))
            {
                predicate = predicate.And(p => p.ContentID.Contains(ContentID));
                predicate = predicate.And(p => p.ramark.Contains(typeID));
            }

            var result = DbSet.Where(predicate).ToList().Count;


            return result;
        }


        public List<String> GetMyList(string VID)
        {
            //查询条件
            var predicate = WhereExtension.True<VA_Sign>();//初始化where表达式

            if (!String.IsNullOrEmpty(VID))
            {
                predicate = predicate.And(p => p.VID.Equals(VID));
            }

            var result = DbSet.Where(predicate).OrderByDescending(o => o.CreateDate).ToList();
            var res = result.Select(p => p.ContentID).Distinct().ToList();
            return res;

        }

        public VA_Sign GetNewSign(string VID, string ContentID)
        {
            VA_Sign res = new VA_Sign();
            //查询条件
            var predicate = WhereExtension.True<VA_Sign>();//初始化where表达式

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

        //获取该活动的报名人数
        public int GetSingNum(string ContentID)
        {
            int res = 0;
            //查询条件
            var predicate = WhereExtension.True<VA_Sign>();//初始化where表达式

            if (!String.IsNullOrEmpty(ContentID))
            {
                predicate = predicate.And(p => p.ContentID.Equals(ContentID));
            }

            var result = DbSet.Where(predicate).ToList();
            res = result.Count;
            return res;
        }

 



    }
}
