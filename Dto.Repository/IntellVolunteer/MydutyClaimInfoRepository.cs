using Dto.IRepository.IntellVolunteer;
using Dtol;
using Dtol.dtol;
using Dtol.EfCoreExtion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;
using ViewModel.VolunteerModel.RequsetModel.NormalViewModel;
using ViewModel.VolunteerModel.ResponseModel.NormalViewModel;

namespace Dto.Repository.IntellVolunteer
{
    public class MydutyClaimInfoRepository : IMydutyClaimInfoRepository
    {

        protected readonly DtolContext Db;
        protected readonly DbSet<MydutyClaim_Info> DbSet;

        public MydutyClaimInfoRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<MydutyClaim_Info>();
        }

        public void Add(MydutyClaim_Info obj)
        {
            DbSet.Add(obj);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public MydutyClaim_Info GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<MydutyClaim_Info> getMydutyInfo(MydutyClaimInfoSearchViewModel mydutyClaimInfoSearchViewModel)
        {
            var preciate = Searchwhere(mydutyClaimInfoSearchViewModel);
            var result = DbSet.Where(preciate).Include(a=>a.OndutyClaims_Info.Normalization_Info).OrderByDescending(a=>a.StartDutyTime).ToList();
            return result;
        }

        //根据条件查询用户
        private Expression<Func<MydutyClaim_Info, bool>> Searchwhere(MydutyClaimInfoSearchViewModel  mydutyClaimInfoSearchViewModel)
        {
            var predicate = WhereExtension.True<MydutyClaim_Info>();//初始化where表达式

            //未开始  已结束  已取消

            //未开始
            if (mydutyClaimInfoSearchViewModel.status == "0")
            {
                // 不显示 已删除的信息 status=3 20191111  ,不显示 居民发布未审核的信息  status=9;审核不通过信息 status=8 (20191120)
                predicate = predicate.And(p => p.StartDutyTime > DateTime.Now);
            
                predicate = predicate.And(p => p.status.Contains("1"));

            }
            //已结束
            else if (mydutyClaimInfoSearchViewModel.status == "1")
            {
                predicate = predicate.And(p => p.EndDutyTime < DateTime.Now);
                predicate = predicate.And(p => p.status.Contains("1"));
            }
            // 已取消
            else if (mydutyClaimInfoSearchViewModel.status == "2")
            {
                //predicate = predicate.And(p => p.EndDutyTime < DateTime.Now);
                predicate = predicate.And(p => p.status.Contains("2"));
            }


            predicate = predicate.And(p => p.Userid.Contains(mydutyClaimInfoSearchViewModel.Userid));
            predicate = predicate.And(p => p.UserName.Contains(mydutyClaimInfoSearchViewModel.UserName));
            predicate = predicate.And(p => p.EndDutyTime.Value.ToString().Contains(mydutyClaimInfoSearchViewModel.EndDutyTime));
            predicate = predicate.And(p => p.CreateDate.Value.ToString().Contains(mydutyClaimInfoSearchViewModel.CreateDate));




            return predicate;
        }


        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Update(MydutyClaim_Info obj)
        {
            DbSet.Update(obj);
        }

        public MydutyClaim_Info GetInfoById(string id)
        {
           return  DbSet.FirstOrDefault(a=>a.id==id);
        }


        public List<MydutyClaim_Info> GetByOndutyClaims_InfoID(string id)
        {
            var predicate = WhereExtension.True<MydutyClaim_Info>();//初始化where表达式
            predicate = predicate.And(p => p.OndutyClaims_InfoId.Equals(id));
      
            var result = DbSet.Where(predicate).OrderBy(a => a.CreateDate).ToList();
            return result;
        }


        public List<MydutyClaim_Info> GetByUid(string uid)
        {
            var predicate = WhereExtension.True<MydutyClaim_Info>();//初始化where表达式
            predicate = predicate.And(p => p.Userid.Equals(uid));

            var result = DbSet.Where(predicate).OrderBy(a => a.CreateDate).ToList();
            return result;
        }


        public MydutyClaim_Info GetByUidandID(string uid,string id)
        {
            var predicate = WhereExtension.True<MydutyClaim_Info>();//初始化where表达式
            predicate = predicate.And(p => p.Userid.Equals(uid));
            predicate = predicate.And(p => p.id.Equals(id));
            var result = DbSet.Where(predicate).OrderBy(a => a.CreateDate).ToList();
            if (result.Count > 0)
            {
                return result.First();
            }
            else
            {
                return null;
            }
            
        }
    }
}
