using System;
using Thynk_Code.Model.Entities;

namespace Thynk_Code.Data.Interfaces
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        IRepositoryReadOnly<T> GetReadOnlyRepository<T>() where T : BaseEntity;
    }
}
