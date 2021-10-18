using System;
namespace Thynk_Code.Data.Interfaces
{
    public interface IRepositoryReadOnly<T> : IReadRepository<T> where T : class
    {
    }
}
