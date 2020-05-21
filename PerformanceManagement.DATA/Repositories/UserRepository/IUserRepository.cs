using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceManagement.DATA.Repositories
{
    public interface IUserRepository 
    {
        IEnumerable<User> GetUsers();

        User GetUserById(int? userId);

        Task<IList<User>> GetUserByUsername(string username);

        IEnumerable<Badge> GetAllUserbadgesForAuser(int? userId);
        void Edit(User user);


    }
}
