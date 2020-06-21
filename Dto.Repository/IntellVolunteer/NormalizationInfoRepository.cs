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
using Z.EntityFramework.Plus;

namespace Dto.Repository.IntellVolunteer
{
    public class NormalizationInfoRepository: INormalizationInfoRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<Normalization_Info> DbSet;


        public NormalizationInfoRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<Normalization_Info>();
        }

        public void Add(Normalization_Info obj)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Normalization_Info GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Normalization_Info>  NormalizationSearch(NormalSearchViewModel normalSearchViewModel)
        {
            var preciate = SearchVAWhere(normalSearchViewModel);
            var result = DbSet.AsQueryable().Where(preciate).ToList();
            return result;
        }

        //根据条件查询用户
        private Expression<Func<Normalization_Info, bool>> SearchVAWhere(NormalSearchViewModel  normalSearchViewModel)
        {
            var predicate = WhereExtension.True<Normalization_Info>();//初始化where表达式
            // 不显示 已删除的信息 status=3 20191111  ,不显示 居民发布未审核的信息  status=9;审核不通过信息 status=8 (20191120)
            //predicate = predicate.And(p => p.title.Contains(normalSearchViewModel.title));
            predicate = predicate.And(p => p.CommunityNameCode.Contains(normalSearchViewModel.CommunityNameCode));
            //predicate = predicate.And(p => p.XiaoCommunityName.Contains(normalSearchViewModel.XiaoCommunityName));
            //predicate = predicate.And(p => p.status.Contains(normalSearchViewModel.status));
            //predicate = predicate.And(p => p.CheckInTime.Contains(normalSearchViewModel.CheckInTime));
            //predicate = predicate.And(p => p.CheckOutTime.Contains(normalSearchViewModel.CheckOutTime));
            //predicate = predicate.And(p => p.DutyStartTime.Value.ToString().Contains(normalSearchViewModel.DutyStartTime));
            //predicate = predicate.And(p => p.DutyEndTime.Value.ToString().Contains(normalSearchViewModel.DutyEndTime));
            return predicate;
        }

        public Normalization_Info NormalizationContainDuty(NormalizationContainSearchViewModel normalizationContainSearchViewModel)
        {
            //小区等主要信息，以及值班需要上报的信息
            var tempresult = DbSet.Where(a => a.id == normalizationContainSearchViewModel.id)
                                  .IncludeFilter(c => c.ondutyClaims_Infos.Where(a=>a.ClaimTime.Value.ToString()
                                  .Contains(normalizationContainSearchViewModel.clamtime)) 
                                  )
                                  .FirstOrDefault();
            Console.WriteLine(tempresult);

            if (tempresult.ondutyClaims_Infos.Count() < 1)
            {
                return tempresult;
            }
           //需要根据上报的值班信息查询目前所有个人已经上报的信息
            for (int i=0; i < tempresult.ondutyClaims_Infos.Count();i++)
            {
                //上面查询结果的补充
                 Db.MydutyClaim_Info.Where(a => a.OndutyClaims_InfoId == tempresult.ondutyClaims_Infos[i].id && a.status== normalizationContainSearchViewModel.status).ToList();
                //if (temp.Count() == 0)
                //{
                //    continue;
                //}
               // tempresult.ondutyClaims_Infos[i].mydutyClaim_Infos.AddRange(temp);
            }

            var temp = tempresult.ondutyClaims_Infos.Where(a=>a.SubdistrictID.Equals(normalizationContainSearchViewModel.SubdistrictID)).OrderBy(o => o.StartTime).ToList();
            tempresult.ondutyClaims_Infos = temp;
            return tempresult;
        }

        public Normalization_Info NormalizationByID(string id)
        {
            //小区等主要信息，以及值班需要上报的信息
            var tempresult = DbSet.Where(a => a.id == id).FirstOrDefault();
           
            return tempresult;
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Normalization_Info obj)
        {
            throw new NotImplementedException();
        }
    }
}
