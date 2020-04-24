using Dto.IRepository.IntellVolunteer;
using Dtol;
using Dtol.dtol;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Repository.IntellVolunteer
{
    public class OndutyClaimsInfoRepository:IOndutyClaimsInfoRepository
    {
        protected readonly DtolContext Db;
        protected readonly DbSet<OndutyClaims_Info> DbSet;

        public OndutyClaimsInfoRepository(DtolContext context)
        {
            Db = context;
            DbSet = Db.Set<OndutyClaims_Info>();
        }

        public void Add(OndutyClaims_Info obj)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public OndutyClaims_Info GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(OndutyClaims_Info obj)
        {
            throw new NotImplementedException();
        }
    }
}
