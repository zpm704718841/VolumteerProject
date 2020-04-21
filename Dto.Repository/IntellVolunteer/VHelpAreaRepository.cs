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
    public class VHelpAreaRepository : IVHelpAreaRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<VHelpArea> DbSet;

        public VHelpAreaRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<VHelpArea>();
        }

        public virtual void Add(VHelpArea obj)
        {
            DbSet.Add(obj);
        }

        public virtual VHelpArea GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public VHelpArea SearchInfoByID(string id)
        {
            var res = new VHelpArea();
            //查询条件
            var predicate = WhereExtension.True<VHelpArea>();//初始化where表达式

            if (!String.IsNullOrEmpty(id))
            {
                predicate = predicate.And(p => p.ID.Contains(id));
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

        // //获取我的互助信息(志愿者ID、所属社区ID、活动类型ID、状态 等)
        public List<VHelpArea> GetByIDList(List<string> ids)
        {
            var predicate = WhereExtension.True<VHelpArea>();//初始化where表达式

            predicate = predicate.And(p => ids.Contains(p.ID));

            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.CreateDate).ToList();

            return result;
        }


        public virtual void Update(VHelpArea obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }


        public List<VHelpArea> SearchMyInfoByWhere(GetMyListViewModel VSearchModel)
        {
            //查询条件
            var predicate = WhereExtension.True<VHelpArea>();//初始化where表达式
            // 不显示 已删除的信息 20191111
            predicate = predicate.And(p => !p.Status.Equals("3"));
            if (!String.IsNullOrEmpty(VSearchModel.CommunityID))
            {
                predicate = predicate.And(p => p.CommunityID.Contains(VSearchModel.CommunityID));
            }
            if (!String.IsNullOrEmpty(VSearchModel.TypeIDs))
            {
                predicate = predicate.And(p => p.TypeID.Contains(VSearchModel.TypeIDs));
            }
            if (!String.IsNullOrEmpty(VSearchModel.Status))
            {
                predicate = predicate.And(p => p.Address.Contains(VSearchModel.Status));
            }

            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.CreateDate).ToList();

            return result;
        }


        //根据条件查询互助信息 （ 互助标题Title、所属组织架构Community,互助地址Address,擅长技能Type）
        public List<VHelpArea> SearchInfoByWhere(AllSearchViewModel VSearchViewModel)
        {
            //查询条件
            var predicate = WhereExtension.True<VHelpArea>();//初始化where表达式
            // 不显示 已删除的信息 status=3 20191111  ,不显示 居民发布未审核的信息  status=9;审核不通过信息 status=8 (20191120)
            predicate = predicate.And(p => !p.Status.Equals("3"));
            predicate = predicate.And(p => !p.Status.Equals("9"));
            predicate = predicate.And(p => !p.Status.Equals("8"));

            if (!String.IsNullOrEmpty(VSearchViewModel.Title))
            {
                predicate = predicate.And(p => p.Title.Contains(VSearchViewModel.Title));
            }
            if (!String.IsNullOrEmpty(VSearchViewModel.Community))
            {
                predicate = predicate.And(p => p.Community.Contains(VSearchViewModel.Community));
            }
            if (!String.IsNullOrEmpty(VSearchViewModel.CommunityID))
            {
                predicate = predicate.And(p => p.CommunityID.Contains(VSearchViewModel.CommunityID));
            }
            if (!String.IsNullOrEmpty(VSearchViewModel.Address))
            {
                predicate = predicate.And(p => p.Address.Contains(VSearchViewModel.Address));
            }
            if (!String.IsNullOrEmpty(VSearchViewModel.Type))
            {
                predicate = predicate.And(p => p.Type.Contains(VSearchViewModel.Type));
            }
            if (!String.IsNullOrEmpty(VSearchViewModel.Status))
            {
                predicate = predicate.And(p => p.Status.Contains(VSearchViewModel.Status));
            }
            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.CreateDate).ToList();

            return result;
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

    
        //根据条件查询用户
        private Expression<Func<VHelpArea, bool>> SearchVAWhere(VHelpAreaSearchViewModel VSearchViewModel)
        {
            var predicate = WhereExtension.True<VHelpArea>();//初始化where表达式
            // 不显示 已删除的信息 status=3 20191111  ,不显示 居民发布未审核的信息  status=9;审核不通过信息 status=8 (20191120)
            predicate = predicate.And(p => !p.Status.Equals("3"));
            predicate = predicate.And(p => !p.Status.Equals("9"));
            predicate = predicate.And(p => !p.Status.Equals("8"));
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
            if (!String.IsNullOrEmpty(VSearchViewModel.TypeID))
            {
                predicate = predicate.And(p => p.TypeID.Contains(VSearchViewModel.TypeID));
            }

            return predicate;
        }


        //获取所有互助信息  （无参数）
        public List<VHelpArea> GetHelpAreaAll()
        {
            //查询条件
            var predicate = WhereExtension.True<VHelpArea>();

            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.CreateDate).ToList();

            return result;
        }



        //获取我发布的 互助信息
        public List<VHelpArea> GetMyPublishInfo(string VID, string status)
        {      //查询条件
            var predicate = WhereExtension.True<VHelpArea>();
            if (!String.IsNullOrEmpty(VID))
            {
                predicate = predicate.And(p => p.CreateUser.Equals(VID));
            }
            if (!String.IsNullOrEmpty(status))
            {
                predicate = predicate.And(p => p.Status.Equals(status));
            }
            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.CreateDate).ToList();

            return result;

        }


        //首页推荐 排除已结束、已认领的互助信息  
        public List<VHelpArea> GetMyRecommendList()
        {
            var predicate = WhereExtension.True<VHelpArea>();

            //predicate = predicate.And(p => p.Status.Equals("0"));
            
            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.CreateDate).ToList();

            return result;

        }
    }
}
