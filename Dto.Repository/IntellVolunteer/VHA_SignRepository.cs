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
    public class VHA_SignRepository: IVHA_SignRepository
    {

        protected readonly DtolContext Db;
        protected readonly DbSet<VHA_Sign> DbSet;

        public VHA_SignRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<VHA_Sign>();
        }

        public virtual void Add(VHA_Sign obj)
        {
            DbSet.Add(obj);
        }
        public virtual VHA_Sign GetById(Guid id)
        {
            return DbSet.Find(id);
        }
        public virtual void Update(VHA_Sign obj)
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


        public List<String> GetMyList(string VID)
        {
            //查询条件
            var predicate = WhereExtension.True<VHA_Sign>();//初始化where表达式

            if (!String.IsNullOrEmpty(VID))
            {
                predicate = predicate.And(p => p.VID.Equals(VID));
            }

            var result = DbSet.Where(predicate).OrderByDescending(o => o.CreateDate).ToList();
            var res = result.Select(p => p.ContentID).Distinct().ToList();
            return res;

        }

        //根据 contentID，VID 获取 该志愿者针对该互助信息 最新状态
        public VHA_Sign GetNewSign(string VID, string ContentID)
        {
            VHA_Sign res = new VHA_Sign();
            //查询条件
            var predicate = WhereExtension.True<VHA_Sign>();//初始化where表达式

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


        //志愿者互助信息退出功能 （互助ID标识ContentID、志愿者唯一VID、擅长技能TypeID为空不传参数）
        public string SetStatusBack(string VID, string ContentID)
        {
            string status = string.Empty;
            VHA_Sign res = new VHA_Sign();
            //查询条件
            var predicate = WhereExtension.True<VHA_Sign>();//初始化where表达式

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
                res.Status = "3";
                Update(res);
                int a = SaveChanges();
                if (a > 0)
                {
                    status = "success";
                }
                else
                {
                    status = "fail";
                }
            }
            return status;
        }


        //获取该 互助信息对应的 flag=1(认领信息);flag=2(处理结果信息）
        public List<VHA_Sign> GetByContentID(string ContentID,string flag)
        {
             //查询条件
            var predicate = WhereExtension.True<VHA_Sign>();//初始化where表达式

            if (!String.IsNullOrEmpty(flag))
            {
                predicate = predicate.And(p => p.flag.Equals(flag));
            }
            if (!String.IsNullOrEmpty(ContentID))
            {
                predicate = predicate.And(p => p.ContentID.Equals(ContentID));
            }
            var result = DbSet.Where(predicate).OrderByDescending(o => o.CreateDate).ToList();
            return result;
        }


        //志愿者成功参与的 互助次数（只有最终审核处理结果通过才算成功）
        public int GetMyHelpAreaNums(string VID)
        {
            int count = 0;
            //查询条件
            var predicate = WhereExtension.True<VHA_Sign>();//初始化where表达式
            predicate = predicate.And(p => p.flag.Equals("2"));
            predicate = predicate.And(p => p.Status.Equals("1"));
            predicate = predicate.And(p => p.VID.Equals(VID));
            var result = DbSet.Where(predicate).OrderByDescending(o => o.CreateDate).ToList();
            count = result.Count;

            return count;
        }

        //通过 handle 查找 sign记录表里的 opinion 审核意见
        public VHA_Sign GetMySign(string handleID)
        {
            VHA_Sign res = new VHA_Sign();
            //查询条件
            var predicate = WhereExtension.True<VHA_Sign>();//初始化where表达式

            if (!String.IsNullOrEmpty(handleID))
            {
                predicate = predicate.And(p => p.bak1.Equals(handleID));
            }
 
            var result = DbSet.Where(predicate).ToList();
            if (result.Count > 0)
            {
                res = result.First();
            }
            return res;
        }
    }
}
