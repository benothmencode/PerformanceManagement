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
            return _context.Systemes.ToList();
        }
        
        //public IEnumerable<Badge> GetBadges(int SystemeId)
        //{
        //    return _context.Badges.Where(b => b.SystemeId == SystemeId).Include(b => b.Systeme).ToList();
        //}
        public Systeme GetSystemeById(int SystemeId)
        {
            return _context.Systemes.Include(s => s.Badges).Where(s => s.Id == SystemeId).FirstOrDefault();
        }

    }
}
