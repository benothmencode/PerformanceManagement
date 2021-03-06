﻿using PerformanceManagement.ENTITIES;
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
        public int? GetIdUserRedmine(int userId);

        IEnumerable<Badge> GetAllUserbadgesForAuser(int? userId);
        void Edit(User user);

        int? GetIdUserGitlab(int userId);
        bool UpdateUserProgression(UserBadge userBadge);

        bool UserExists(int userId);
        bool DesactivateOrActivateUser(int userId , bool result);

        IList<VoteHistory> TotalVotes(int userId);



    }
}
