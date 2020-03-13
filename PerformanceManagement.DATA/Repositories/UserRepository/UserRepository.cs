using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PerformanceManagement.DATA.Repositories
{
    public class UserRepository :IUserRepository
    {
      
       private readonly PerformanceManagementDBContext _context;
       public UserRepository(PerformanceManagementDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Badge> GetAllUserbadgesForAuser(int userId)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int? userId)
        {
            return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.Where(u => u.Username == username).FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Created).ToList();
        }
    }
}
