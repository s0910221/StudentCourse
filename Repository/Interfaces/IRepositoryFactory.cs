using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IRepositoryFactory
    {
        IGenericRepository<T> GetGenericRepository<T>() where T : class;
    }
}
