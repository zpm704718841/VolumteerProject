using Dto.IRepository.IntellVolunteerBackground;
using Dtol;
using Dtol.dtol;
using Dtol.EfCoreExtion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ViewModel.VolunteerBackground.RequsetModel;

namespace Dto.Repository.IntellVolunteerBackground
{
    public class VolunteerActivityRepository:IVolunteerActivityRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<VolunteerActivity> DbSet;

        public VolunteerActivityRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<VolunteerActivity>();

        }
        public virtual void Add(VolunteerActivity obj)
        {
            Db.Add(obj);
        }
        public virtual VolunteerActivity GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<VolunteerActivity> GetVolunteerAll(VolunteerActivitySearchViewModel VSearchViewModel)
        {
            var predicate = SearchVolunteerWhere(VSearchViewModel);

            return DbSet.Where(predicate);
        }

        public virtual void Update(VolunteerActivity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }
        public IQueryable<VolunteerActivity> GetVolunteerActivityByTitle(string title)
        {
            IQueryable<VolunteerActivity> activity = DbSet.Where(model => model.Title.Equals(title) && model.Status.Equals("1"));
            return activity;
        }
        public IQueryable<VolunteerActivity> GetVolunteerActivityForUpdate(string title,string id)
        {
            IQueryable<VolunteerActivity> activity = DbSet.Where(model => model.Title.Equals(title) && model.Status.Equals("1") && model.ID!=id);
            return activity;
        }
        //根据条件查询

        public List<VolunteerActivity> SearchInfoByWhere(VolunteerActivitySearchViewModel VSearchViewModel)
        {
            int SkipNum = VSearchViewModel.pageViewModel.CurrentPageNum * VSearchViewModel.pageViewModel.PageSize;

            //查询条件
            var predicate = SearchVolunteerWhere(VSearchViewModel);

            var result = DbSet.Where(predicate)
                .Skip(SkipNum)
                .Take(VSearchViewModel.pageViewModel.PageSize)
                .OrderBy(o => o.CreateDate).ToList();


            return result;
        }

        public IQueryable <VolunteerActivity> GetAll(VolunteerActivitySearchViewModel VSearchViewModel)
        {
            var predicate = SearchVolunteerWhere(VSearchViewModel);
            return DbSet.Where(predicate);
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

        public int DeleteByUseridList(List<string> IdList)
        {
            int DeleteRowNum = 1;
            for (int i = 0; i < IdList.Count; i++)
            {
                var model = DbSet.Single(w =>w.ID == IdList[i]);
                model.Status = "0";
                DbSet.Update(model);
                SaveChanges();
                DeleteRowNum = i + 1;
            }
            return DeleteRowNum;


        }

        //public void Delete(string id)
        //{
        //    var model = DbSet.Single(m => m.ID == id);
        //    model.Status = "0";
        //    DbSet.Update(model);
        //}
        public void Delete(VolunteerActivityDeleteViewModel delmodel)
        {
            var model = DbSet.Single(m => m.ID == delmodel.ID);
            model.Status = delmodel.Status;
            model.UpdateUser = delmodel.UpdateUser;
            model.UpdateDate = DateTime.Now;
            DbSet.Update(model);
        }
        #region 查询条件
        //根据条件查询用户
        private Expression<Func<VolunteerActivity, bool>> SearchVolunteerWhere(VolunteerActivitySearchViewModel VSearchViewModel)
        {
            var predicate = WhereExtension.True<VolunteerActivity>();//初始化where表达式
            predicate = predicate.And(p => p.Status.Equals("1"));

            if (VSearchViewModel.Title != "")
                predicate = predicate.And(p => p.Title.Contains(VSearchViewModel.Title));
            if (VSearchViewModel.Content != "")
                predicate = predicate.And(p => p.Content.Contains(VSearchViewModel.Content));
            if (VSearchViewModel.Type != "")
                predicate = predicate.And(p => p.Type.Contains(VSearchViewModel.Type));
            if (VSearchViewModel.CommunityID != "")
                predicate = predicate.And(p => p.CommunityID.Equals(VSearchViewModel.CommunityID));
            return predicate;
        }

        public IQueryable<VolunteerActivity> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion 查询条件

        public List<User_Depart> GetDepartAll()
        {
            var result = Db.user_Depart.ToList();
            return result;
        }
    }
}
