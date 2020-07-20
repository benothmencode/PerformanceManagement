using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.SystemeRepository
{
    public class SystemeRepository : ISystemeRepository
    {

        private readonly PerformanceManagementDBContext _context;

        public SystemeRepository(PerformanceManagementDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Systeme> GetSystemes()
        {
            return _context.Systemes.Include(s => s.Badges).Include(s => s.SystemeUsers).ToList();
        }
     
        public Systeme GetSystemeById(int SystemeId)
        {
            return _context.Systemes.Include(s => s.Badges).Include(s => s.SystemeUsers).Where(s => s.Id == SystemeId).FirstOrDefault();
        }
        public void CreateSysteme(Systeme systeme)
        {
            systeme.Created = DateTime.Now;
            systeme.SystemIsArchieved = false;
            _context.Add(systeme);
            _context.SaveChanges();
        }

        public void UpdateSysteme(Systeme systeme)
        {
            _context.Update(systeme);
            _context.SaveChanges();
        }


        public bool SystemeExists(int systemeId)
        {
            return _context.Systemes.Any(a => a.Id == systemeId);
        }

        public Systeme GetGitlab(string title)
        {
            return _context.Systemes.FirstOrDefault(s => s.SystemName == title);
        }

        public bool CreateSystemUser(SystemeUser systemeUser )
        {
            var saved = 0;
            if (systemeUser != null)
            {
                _context.Add(systemeUser);
                saved= _context.SaveChanges();
            };
            return saved >= 0 ? true : false;
        }

        public void updatesystemUser(SystemeUser systemeUser)
        {
            if (systemeUser != null)
            {
                _context.Update(systemeUser);
                _context.SaveChanges() ;
            }
        }

        public List<Systeme> GetSystemes(int userId)
        {
            return _context.SystemeUsers.Where(us => us.UserId == userId).Select(us => us.Systeme).ToList();
        }


        public bool DisableSystem(int systemId , bool result)
        {
            var systeme = _context.Systemes.Include(s=> s.Badges).Include(s => s.SystemeUsers).Where(s => s.SystemIsArchieved == result).FirstOrDefault(s => s.Id == systemId);
            var saved = 0;
            if (systeme.SystemIsArchieved != true)
            {
                foreach (var badge in systeme.Badges)
                {
                    badge.SystemIsArchieved = true;
                    badge.IsArchieved = true;
                }
                foreach (var userSystem in systeme.SystemeUsers)
                {
                    userSystem.SystemIsArchieved = true;
                }
                systeme.SystemIsArchieved = true;
            }
            else if (systeme.SystemIsArchieved == true)
            {
                foreach (var badge in systeme.Badges)
                {
                    badge.SystemIsArchieved = false;
                    badge.IsArchieved = false;
                }
                foreach (var userSystem in systeme.SystemeUsers)
                {
                    userSystem.SystemIsArchieved = false;
                }
                systeme.SystemIsArchieved = false;
            }
            _context.Update(systeme);
            saved = _context.SaveChanges();

            return saved >= 0 ? true : false;
        }




    }
}
