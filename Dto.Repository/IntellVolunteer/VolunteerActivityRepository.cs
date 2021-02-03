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
    public class VolunteerActivityRepository : IVolunteerActivityRepository
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
            DbSet.Add(obj);
        }

        public virtual VolunteerActivity GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        //根据志愿活动ID查询具体信息
        public VolunteerActivity GetByID(string id)
        {
            return DbSet.Find(id);
        }

        //根据志愿活动id list 查询活动列表
        public List<VolunteerActivity> GetByIDList(List<string> ids)
        {
            var predicate = WhereExtension.True<VolunteerActivity>();//初始化where表达式

            predicate = predicate.And(p => ids.Contains(p.ID));

            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.CreateDate).ToList();

            return result;
        }


        public virtual void Update(VolunteerActivity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }


        //根据条件查询
        public List<VolunteerActivity> SearchInfoByWhere(AllSearchViewModel VSearchViewModel)
        {
            //查询条件
            var predicate = WhereExtension.True<VolunteerActivity>();//初始化where表达式
            
            // 不显示 已删除的信息 status=0  (20191128)
            predicate = predicate.And(p => !p.Status.Equals("0"));

            predicate = predicate.And(p => (p.Title.Contains(VSearchViewModel.Title) || p.Community.Contains(VSearchViewModel.Community) || p.Address.Contains(VSearchViewModel.Address) || p.Type.Contains(VSearchViewModel.Type) ));


            if (!String.IsNullOrEmpty(VSearchViewModel.Status))
            {
                predicate = predicate.And(p => (p.Status.Equals(VSearchViewModel.Status)));
            }
            if (!String.IsNullOrEmpty(VSearchViewModel.CommunityID))
            {
                predicate = predicate.And(p => p.CommunityID.Equals(VSearchViewModel.CommunityID));
            }
            if (!String.IsNullOrEmpty(VSearchViewModel.TypeID))
            {
                predicate = predicate.And(p => p.TypeIDs.Equals(VSearchViewModel.TypeID));
            }

            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.CreateDate).ToList();

            return result;
        }


        //活动列表页 根据

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public int GetTypeCount(VolunteerActivityTypeModel typeModel)
        {
            var predicate = WhereExtension.True<VolunteerActivity>();//初始化where表达式

            if (!String.IsNullOrEmpty(typeModel.TypeIDs))
            {
                predicate = predicate.And(p => p.TypeIDs.Contains(typeModel.TypeIDs));
            }
            var result = DbSet.Where(predicate).ToList();

            return result.Count;
        }

        public int GetTypeCounts(string typeID)
        {
            var count = 0;
            var predicate = WhereExtension.True<VolunteerActivity>();
            if (!String.IsNullOrEmpty(typeID))
            {
                predicate = predicate.And(p => p.TypeIDs.Contains(typeID));
            }

            var result = DbSet.Where(predicate).ToList();
            count = result.Count;
            return count;
        }


        //按条件查询 志愿活动列表
        public List<VolunteerActivity> SearchMyInfoByWhere(GetMyListViewModel VSearchModel)
        {
            //查询条件
            var predicate = WhereExtension.True<VolunteerActivity>();//初始化where表达式


            if (!String.IsNullOrEmpty(VSearchModel.CommunityID))
            {
                predicate = predicate.And(p => p.CommunityID.Contains(VSearchModel.CommunityID));
            }
            if (!String.IsNullOrEmpty(VSearchModel.TypeIDs))
            {
                predicate = predicate.And(p => p.TypeIDs.Contains(VSearchModel.TypeIDs));
            }
            if (!String.IsNullOrEmpty(VSearchModel.Status))
            {
                predicate = predicate.And(p => p.Address.Contains(VSearchModel.Status));
            }

            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.CreateDate).ToList();

            return result;
        }


 
        //根据条件查询用户
        private Expression<Func<VolunteerActivity, bool>> SearchVAWhere(VolunteerActivitySearchViewModel VSearchViewModel)
        {
            var predicate = WhereExtension.True<VolunteerActivity>();//初始化where表达式

            if (!String.IsNullOrEmpty(VSearchViewModel.ID))
            {
                predicate = predicate.And(p => p.ID.Contains(VSearchViewModel.ID));
            }
            if (!String.IsNullOrEmpty(VSearchViewModel.Title))
            {
                predicate = predicate.And(p => p.Title.Contains(VSearchViewModel.Title));
            }
            if (!String.IsNullOrEmpty(VSearchViewModel.CommunityID))
            {
                predicate = predicate.And(p => p.CommunityID.Contains(VSearchViewModel.CommunityID));
            }
            if (!String.IsNullOrEmpty(VSearchViewModel.Address))
            {
                predicate = predicate.And(p => p.Address.Contains(VSearchViewModel.Address));
            }
            if (!String.IsNullOrEmpty(VSearchViewModel.TypeIDs))
            {
                predicate = predicate.And(p => p.TypeIDs.Contains(VSearchViewModel.TypeIDs));
            }

            return predicate;
        }


        //查询全部志愿活动
        public List<VolunteerActivity> GetVolunteerActivityAll()
        {
            //查询条件
            var predicate = WhereExtension.True<VolunteerActivity>();
 
            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.CreateDate).ToList();

            return result;
        }


        //首页推荐 排除已结束、已满员的 活动
        public List<VolunteerActivity> GetMyRecommendList()
        {
            //查询条件
            var predicate = WhereExtension.True<VolunteerActivity>();

            //predicate = predicate.And(p => p.Status.Equals("1"));
 

            var result = DbSet.Where(predicate).OrderByDescending(o => o.CreateDate)
              .Skip(1)
             .Take(10)
          .ToList();

            return result;
        }


    }
}
