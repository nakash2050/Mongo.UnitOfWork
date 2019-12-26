﻿using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace UOW.Repositories.Contracts
{
    public interface IMongoContext : IDisposable
    {
        void AddCommand(Func<Task> func);

        Task<int> SaveChanges();

        IMongoCollection<T> GetCollection<T>(string name);
    }
}
