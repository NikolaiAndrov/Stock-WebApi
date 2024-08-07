﻿namespace Stock.Data.Common.Repository
{
    public interface IRepository
    {
        IQueryable<T> All<T>() where T : class;

        IQueryable<T> AllReadonly<T>() where T : class;

        Task AddAsync<T>(T  entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<int> SaveChangesAsync();

        Task<T?> GetByIdAsync<T>(object id) where T : class;
    }
}
