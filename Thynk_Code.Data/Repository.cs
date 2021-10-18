using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Thynk_Code.Data.Interfaces;
using Thynk_Code.Model.Entities;
using Thynk_Code.Model.Util;
using static Thynk_Code.Model.EnumClass;

namespace Thynk_Code.Data
{
    public class Repository<T> : BaseRepository<T>, IRepository<T> where T : BaseEntity
    {
        public Repository(DbContext context) : base(context)
        {
        }

        public T Add(T entity)
        {

            entity = SetDateIfDefaultDateIsFound(entity);
            return _dbSet.Add(entity).Entity;
        }

        public void AddAndSetTimeStamp(params T[] entities)
        {

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;

            for (int i = 0; i < entities.Length; i++)
            {
                T entity = entities[i];
                entity = SetDateIfDefaultDateIsFound(entity);
                _dbSet.Add(entity);
            }

            _dbContext.ChangeTracker.DetectChanges();

        }

        public void Add(params T[] entities)
        {
            _dbSet.AddRange(entities);
        }


        public void AddAndSetTimeStamp(IEnumerable<T> entities)
        {

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            for (int i = 0; i < entities.Count(); i++)
            {
                T entity = entities.ElementAt(i);
                entity = SetDateIfDefaultDateIsFound(entity);
                _dbSet.Add(entity);
            }
            _dbContext.ChangeTracker.DetectChanges();

        }

        public void Add(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        private T SetDateIfDefaultDateIsFound(T entity)
        {
            CurrentDate currentDate = GetCurrentDate();
            return SetDateIfDefaultDateIsFound(entity, currentDate);
        }

        public T SetDateIfDefaultDateIsFound(T entity, CurrentDate currentDate)
        {
            if (entity.CreatedAt.Date == new DateTime().Date)
            {
                entity.CreatedAt = currentDate.CurrentDateTime;
                entity.CreatedAtTimeStamp = currentDate.TimeStamp;
            }


            if (entity.UpdatedAt.Date == new DateTime().Date)
            {
                entity.UpdatedAt = currentDate.CurrentDateTime;
                entity.UpdatedAtTimeStamp = currentDate.TimeStamp;
            }
            return entity;
        }

        public void Delete(T entity)
        {
            var existing = _dbSet.Find(entity);
            if (existing != null) _dbSet.Remove(existing);
        }


        public void Delete(object id)
        {

            /* 
             * This whole long process is done to just delete a record without loading the whole of the object record into memory. 
             * I must say it is very efficient
             * 
             * If for some reason we can't delete it that way. Then we fall back to our old method of deleting
             
             */

            //var typeInfo = typeof(T).GetTypeInfo();
            //var key = _dbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            //var property = typeInfo.GetProperty(key?.Name);
            //if (property != null)
            //{
            //    var entity = Activator.CreateInstance<T>();
            //    property.SetValue(entity, id);
            //    _dbContext.Entry(entity).State = EntityState.Deleted;
            //}


            //else
            //{
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
            //}
        }

        public void Delete(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }





        public void SoftDelete(T entity)
        {
            var existing = _dbSet.Find(entity);
            existing.EntityStatus = EntityStatus.Inactive;
        }


        public void SoftDelete(object id)
        {

           
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                entity.EntityStatus = EntityStatus.Inactive;
            }
        }

        public void SoftDelete(params T[] entities)
        {
            foreach (var entity in entities)
            {
                entity.EntityStatus = EntityStatus.Inactive;
            }
        }

        public void SoftDelete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.EntityStatus = EntityStatus.Inactive;
            }
        }


        [Obsolete("Method is replaced by GetList")]
        public IEnumerable<T> Get()
        {
            return _dbSet.AsEnumerable();
        }

        [Obsolete("Method is replaced by GetList")]
        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsEnumerable();
        }

        public void Update(T entity)
        {
            entity = SetUpdatedAtTimeStamp(entity);
            _dbSet.Update(entity);
        }

        public void Update(params T[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void UpdateAndSetTimeStamp(params T[] entities)
        {
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;

            for (int i = 0; i < entities.Length; i++)
            {
                T entity = entities[i];
                entity = SetUpdatedAtTimeStamp(entity);
                _dbSet.Update(entity);
            }

            _dbContext.ChangeTracker.DetectChanges();
        }


        public void Update(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void UpdateAndSetTimeStamp(IEnumerable<T> entities)
        {
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;

            for (int i = 0; i < entities.Count(); i++)
            {
                T entity = entities.ElementAt(i);
                entity = SetUpdatedAtTimeStamp(entity);
                _dbSet.Update(entity);
            }

            _dbContext.ChangeTracker.DetectChanges();
        }


        private T SetUpdatedAtTimeStamp(T entity)
        {
            CurrentDate currentDate = GetCurrentDate();
            return SetUpdatedAtTimeStamp(entity, currentDate);
        }

        public T SetUpdatedAtTimeStamp(T entity, CurrentDate currentDate)
        {

            if (entity.UpdatedAtTimeStamp == entity.CreatedAtTimeStamp)
            {
                entity.UpdatedAt = currentDate.CurrentDateTime;
                entity.UpdatedAtTimeStamp = currentDate.TimeStamp;
            }
            else if (entity.UpdatedAtTimeStamp < currentDate.TimeStamp)
            {
                entity.UpdatedAtTimeStamp = currentDate.TimeStamp;
            }
            return entity;
        }

        public static CurrentDate GetCurrentDate()
        {
            DateTime currentDateTime = DateTime.UtcNow;
            double timeStamp = currentDateTime.ToTimeStamp();
            CurrentDate currentDate = new CurrentDate(currentDateTime, timeStamp);
            return currentDate;
        }


    }

    internal static class DateTimeExtensions
    {
        public static double ToTimeStamp(this DateTime dateInstance)
        {
            DateTime epochDateTime = new DateTime(1970, 1, 1);
            return (dateInstance - epochDateTime).TotalMilliseconds;
        }
    }
}
