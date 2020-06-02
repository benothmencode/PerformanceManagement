using Microsoft.EntityFrameworkCore;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
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
        
        //public IEnumerable<Badge> GetBadges(int SystemeId)
        //{
        //    return _context.Badges.Where(b => b.SystemeId == SystemeId).Include(b => b.Systeme).ToList();
        //}
        public Systeme GetSystemeById(int SystemeId)
        {
            return _context.Systemes.Include(s => s.Badges).Include(s => s.SystemeUsers).Where(s => s.Id == SystemeId).FirstOrDefault();
        }
        public void CreateSysteme(Systeme systeme)
        {
            //IList<Badge> badges = _context.Badges.Where(b => BadgeIds.Contains(b.Id)).ToList();
            //systeme.Badges = badges;
            _context.Add(systeme);
            _context.SaveChanges();
        }

        public void UpdateSysteme(Systeme systeme)
        {
            _context.Update(systeme);
            _context.SaveChanges();
        }

        public void DeleteSysteme(Systeme systeme)
        {
            _context.Remove(systeme);
            _context.SaveChanges();
        }


        public bool SystemeExists(int systemeId)
        {
            return _context.Systemes.Any(a => a.Id == systemeId);
        }

    }
}
