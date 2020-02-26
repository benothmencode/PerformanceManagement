using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.DATA.Repositories
{
    public interface IUserRepository 
    {
        public void Create(User user);
        void Delete(Guid UserId);
        public User GetById(Guid UserId);
        public IEnumerable<User> GetAll();
        public void Update(User userParam, string password = null);
        

    }
}
