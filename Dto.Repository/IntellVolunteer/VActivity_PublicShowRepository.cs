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
    public class VActivity_PublicShowRepository : IVActivity_PublicShowRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<VActivity_PublicShow> DbSet;


        public VActivity_PublicShowRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<VActivity_PublicShow>();
        }

        public virtual void Add(VActivity_PublicShow obj)
        {
            DbSet.Add(obj);
        }

        public virtual VActivity_PublicShow GetById(Guid id)
        {
            return DbSet.Find(id);
        }


        public virtual void Update(VActivity_PublicShow obj)
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

        //展示公所有益秀信息（志愿活动ID，活动标题，活动地址，参与感受，图片列表 等等）
        public List<VActivity_PublicShow> GetPublicShowList()
        {
            var predicate = WhereExtension.True<VActivity_PublicShow>();//初始化where表达式
            predicate = predicate.And(p => !p.Status.Equals("3"));

            var result = DbSet.Where(predicate)
               .OrderByDescending(o => o.CreateDate).ToList();

            return result;
        }


        //根据id获取 该公益秀信息
        public VActivity_PublicShow SearchInfoByID(string id)
        {
            var res = new VActivity_PublicShow();
            //查询条件
            var predicate = WhereExtension.True<VActivity_PublicShow>();//初始化where表达式

            if (!String.IsNullOrEmpty(id))
            {
                predicate = predicate.And(p => p.ID.Equals(id));
            }

            var result = DbSet.Where(predicate).ToList();
            if (result.Count != 0)
            {
                res = result.First();
            }
            else
            {
                res = null;
            }

            return res;
        }

    }
}
