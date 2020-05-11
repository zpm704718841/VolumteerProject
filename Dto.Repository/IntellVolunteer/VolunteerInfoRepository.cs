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


    public class VolunteerInfoRepository : IVolunteerInfoRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<Volunteer_Info> DbSet;

        public VolunteerInfoRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<Volunteer_Info>();
        }

        public virtual void Add(Volunteer_Info obj)
        {
            DbSet.Add(obj);
        }

        public virtual Volunteer_Info GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<Volunteer_Info> GetVolunteerAll(VolunteerSearchViewModel VSearchViewModel)
        {
            var predicate = SearchVolunteerWhere(VSearchViewModel);

            return DbSet.Where(predicate);
        }



        public virtual void Update(Volunteer_Info obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public Volunteer_Info SearchInfoByID(string id)
        {
            var res = new Volunteer_Info();
            //查询条件
            var predicate = WhereExtension.True<Volunteer_Info>();//初始化where表达式
            predicate = predicate.And(p => !p.Status.Equals("4"));
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


        public IQueryable<Volunteer_Info> GetInfoByVON(string VON)
        {
            IQueryable<Volunteer_Info> Volunteer_Infos = DbSet.Where(uid => uid.VNO.Equals(VON));
            return Volunteer_Infos;
        }

        //根据条件查询

        public List<Volunteer_Info> SearchInfoByWhere(VolunteerSearchViewModel VSearchViewModel)
        {
            //查询条件
            var predicate = SearchVolunteerWhere(VSearchViewModel);

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

        public List<Volunteer_Info> SearchInfoByWhereForBackGround(VolunteerInfoSearchViewModel VSearchViewModel)
        {
            #region 未使用
            //var dtValue = DbSet.Where(t => 1 == 1);
            //if (VSearchViewModel.VNO != "")
            //    dtValue = dtValue.Where(t => t.VNO.Equals(VSearchViewModel.VNO));
            //if (VSearchViewModel.Name != "")
            //    dtValue = dtValue.Where(t => t.Name.Contains(VSearchViewModel.Name));
            //if (VSearchViewModel.CommunityID != "")
            //    dtValue = dtValue.Where(t => t.CommunityID.Equals(VSearchViewModel.CommunityID));
            //if (VSearchViewModel.Gender != "")
            //    dtValue = dtValue.Where(t => t.Gender.Equals(VSearchViewModel.Gender));
            //if (VSearchViewModel.Political != "")
            //    dtValue = dtValue.Where(t => t.Political.Contains(VSearchViewModel.Political));

            ////test.OrderBy(o => o.CreateDate).ToList();
            ////var predicate = SearchVolunteerForBGWhere(VSearchViewModel);

            //var result = dtValue.OrderBy(o => o.CreateDate).ToList();
            #endregion
            int SkipNum = VSearchViewModel.pageViewModel.CurrentPageNum * VSearchViewModel.pageViewModel.PageSize;

            //查询条件
            var predicate = SearchVolunteerForBGWhere(VSearchViewModel);

            var result = DbSet.Where(predicate)
                .Skip(SkipNum)
                .Take(VSearchViewModel.pageViewModel.PageSize)
                .OrderBy(o => o.CreateDate).ToList();


            return result;
        }

        public string GetMaxVNO()
        {
            string vno = string.Empty;
            var predicate = WhereExtension.True<Volunteer_Info>();

       
            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.CreateDate).ToList();
            if (result.Count > 0)
            {
                vno = result.First().VNO;
            }
            return vno;
        }

        #region 查询条件
        //根据条件查询用户
        private Expression<Func<Volunteer_Info, bool>> SearchVolunteerWhere(VolunteerSearchViewModel VSearchViewModel)
        {
            var predicate = WhereExtension.True<Volunteer_Info>();//初始化where表达式

            if (!String.IsNullOrEmpty(VSearchViewModel.VNO))
            {
                predicate = predicate.And(p => p.VNO.Contains(VSearchViewModel.VNO));
            }
            if (!String.IsNullOrEmpty(VSearchViewModel.Name))
            {
                predicate = predicate.And(p => p.Name.Contains(VSearchViewModel.Name));
            }
            if (!String.IsNullOrEmpty(VSearchViewModel.CertificateID))
            {
                predicate = predicate.And(p => p.CertificateID.Contains(VSearchViewModel.CertificateID));
            }
            if (!String.IsNullOrEmpty(VSearchViewModel.Mobile))
            {
                predicate = predicate.And(p => p.Mobile.Contains(VSearchViewModel.Mobile));
            }

            return predicate;
        }

        private Expression<Func<Volunteer_Info, bool>> SearchVolunteerForBGWhere(VolunteerInfoSearchViewModel VSearchViewModel)
        {
            var predicate = WhereExtension.True<Volunteer_Info>();//初始化where表达式
            if (VSearchViewModel.VNO != "")
                predicate = predicate.And(p => p.VNO.Equals(VSearchViewModel.VNO));
            if (VSearchViewModel.Name != "")
                predicate = predicate.And(p => p.Name.Contains(VSearchViewModel.Name));
            if (VSearchViewModel.CommunityID != "")
                predicate = predicate.And(p => p.CommunityID.Equals(VSearchViewModel.CommunityID));
            if (VSearchViewModel.Gender != "")
                predicate = predicate.And(p => p.Gender.Contains(VSearchViewModel.Gender));
            if (VSearchViewModel.Political != "")
                predicate = predicate.And(p => p.Political.Contains(VSearchViewModel.Political));
            return predicate;
        }

        public IQueryable<Volunteer_Info> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion 查询条件

        public IQueryable<Volunteer_Info> GetVolunteerAll(VolunteerInfoSearchViewModel VSearchViewModel)
        {
            var predicate = SearchVolunteerForBGWhere(VSearchViewModel);

            return DbSet.Where(predicate);
        }

        public void UpdateByModel(VolunteerInfoUpdateViewModel viewModel)
        {
            var model = DbSet.Single(m => m.ID == viewModel.ID);
            model.Status = viewModel.Status;
            model.UpdateDate = DateTime.Now;
            model.UpdateUser = viewModel.UpdateUser;
            DbSet.Update(model);

        }

        public List<User_Depart> GetDepartAll()
        {
            var result = Db.user_Depart.ToList();
            return result;
        }

        //保存修改信息
        public void EditInfo(Volunteer_Info delmodel)
        {
            var model = DbSet.Single(m => m.ID == delmodel.ID);
            model.Address = delmodel.Address;
            model.Community = delmodel.Community;
            model.CommunityID = delmodel.CommunityID;
            model.Subdistrict = delmodel.Subdistrict;
            model.SubdistrictID = delmodel.SubdistrictID;
            model.Unit = delmodel.Unit;
            model.UnitID = delmodel.UnitID;
            model.ServiceTime = delmodel.ServiceTime;
            model.UpdateUser = delmodel.ID;
            model.UpdateDate = DateTime.Now;
            //修改个人信息后需重新接受审核（待审核状态）
            model.Status = "0";
            DbSet.Update(model);
    
        }

    }
}
