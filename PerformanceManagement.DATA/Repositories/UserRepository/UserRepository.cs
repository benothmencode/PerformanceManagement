using Microsoft.EntityFrameworkCore;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceManagement.DATA.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly PerformanceManagementDBContext _context;
        public UserRepository(PerformanceManagementDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        

        public IEnumerable<Badge> GetAllUserbadgesForAuser(int? userId)
        {
            return _context.userBadges.Where(u => u.UserId == userId).Select(b => b.Badge).ToList();

        }

        public User GetUserById(int? userId)
        {
            return _context.Users.Include(u => u.UserBadges).Where(u => u.Id == userId).FirstOrDefault();
        }

        public async Task<IList<User>> GetUserByUsername(string Empsearch)
        {
            var empquery = from x in _context.Users select x;
            if (!string.IsNullOrEmpty(Empsearch))
            {
                empquery = empquery.Where(x => x.UserName.Contains(Empsearch) || x.FirstName.Contains(Empsearch));
            }

            return await empquery.AsNoTracking().ToListAsync();
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Created).ToList();
        }

        public void Edit(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public int? GetIdUserGitlab(int userId)
        {
            var IdGitlab = _context.SystemeUsers.Where(su => su.UserId == userId).Where(su => su.Systeme.SystemName == "Gitlab").Select(su => su.Identifier).First();
            return IdGitlab;
        }
       
      

        public bool UpdateUserProgression(UserBadge userBadge)
        {
            _context.Update(userBadge);
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }



        public bool UserExists(int userId)
        {
            return _context.Users.Any(a => a.Id == userId);
        }

        public bool DesactivateOrActivateUser(int userId, bool result)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if(user != null)
            {
              user.Active = result;
             _context.Update(user);
            
            }
           var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }
    }


    
}
