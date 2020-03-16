using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.DATA.Repositories
{
    public interface IUserRepository 
    {
        IEnumerable<User> GetUsers();

        User GetUserById(int? userId);

        User GetUserByUsername(string username);

        IEnumerable<Badge> GetAllUserbadgesForAuser(int? userId);
    }
}
