using System;
using Microsoft.EntityFrameworkCore;
using Thynk_Code.Data.Interfaces;

namespace Thynk_Code.Data
{
    public class RepositoryReadOnly<T> : BaseRepository<T>, IRepositoryReadOnly<T> where T : class
    {
        public RepositoryReadOnly(DbContext context) : base(context)
        {
        }
    }
}
