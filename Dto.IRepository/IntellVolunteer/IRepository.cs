using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(Guid id);
        void Update(TEntity obj);
        void Remove(Guid id);
        int SaveChanges();
        void Dispose();
    }
}
