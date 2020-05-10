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
using ViewModel.VolunteerModel.ResponseModel;

namespace Dto.Repository.IntellVolunteer
{
    public class LoginTypeRepository : ILoginTypeRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<LoginType> DbSet;

        public LoginTypeRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<LoginType>();
        }
        public virtual void Add(LoginType obj)
        {
            DbSet.Add(obj);
        }

        public virtual LoginType GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(LoginType obj)
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

        //根据 id 获取具体登录方式
        public LoginType SearchByIDModel(string id)
        {
            var res = new LoginType();
            //查询条件
            var predicate = WhereExtension.True<LoginType>();//初始化where表达式

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


        //直接获取 人脸识别登录方式信息
        public LoginType SearchFaceModel()
        {
            var res = new LoginType();
            //查询条件
            var predicate = WhereExtension.True<LoginType>();//初始化where表达式
            predicate = predicate.And(p => p.type.Contains("Facelogin"));

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

        //获取全部登录方式
        public List<LoginType> GetList()
        {
            //查询条件
            var predicate = WhereExtension.True<LoginType>();//初始化where表达式

            //有效登录方式
            predicate = predicate.And(p => p.status.Equals("0"));

            var result = DbSet.Where(predicate)
                .OrderBy(o => o.CreateDate).ToList();


            return result;
        }
    }
}
