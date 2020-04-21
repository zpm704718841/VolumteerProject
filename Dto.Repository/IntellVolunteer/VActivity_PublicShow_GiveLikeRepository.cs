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
    public class VActivity_PublicShow_GiveLikeRepository : IVActivity_PublicShow_GiveLikeRepository
    {

        protected readonly DtolContext Db;
        protected readonly DbSet<VActivity_PublicShow_GiveLike> DbSet;


        public VActivity_PublicShow_GiveLikeRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<VActivity_PublicShow_GiveLike>();
        }

        public virtual void Add(VActivity_PublicShow_GiveLike obj)
        {
            DbSet.Add(obj);
        }

        public virtual VActivity_PublicShow_GiveLike GetById(Guid id)
        {
            return DbSet.Find(id);
        }


        public virtual void Update(VActivity_PublicShow_GiveLike obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }


        public void RemoveNew(VActivity_PublicShow_GiveLike giveLike)
        {
            DbSet.Remove(giveLike);
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

        //获取当前志愿者针对该公益秀的点赞信息
        public VActivity_PublicShow_GiveLike GetLike(string VID,string PublicShowID)
        {
            VActivity_PublicShow_GiveLike giveLike = new VActivity_PublicShow_GiveLike();
            var predicate = WhereExtension.True<VActivity_PublicShow_GiveLike>();//初始化where表达式

            predicate = predicate.And(p => p.VID.Equals(VID));
            predicate = predicate.And(p => p.PublicShowID.Equals(PublicShowID));

            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.CreateDate).ToList();
            if (result.Count > 0)
            {
                giveLike = result.First();
            }
            return giveLike;
        }


        //获取该公益秀的 点赞信息
        public List<VActivity_PublicShow_GiveLike> GetLikeList(string PublicShowID)
        {
            VActivity_PublicShow_GiveLike giveLike = new VActivity_PublicShow_GiveLike();
            var predicate = WhereExtension.True<VActivity_PublicShow_GiveLike>();//初始化where表达式

            predicate = predicate.And(p => p.PublicShowID.Equals(PublicShowID));

            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.CreateDate).ToList();
 
            return result;
        }
    }
}
