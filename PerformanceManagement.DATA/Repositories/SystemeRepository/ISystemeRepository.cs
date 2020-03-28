using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.SystemeRepository
{
    public interface ISystemeRepository
    {
        IEnumerable<Systeme> GetSystemes();
        //IEnumerable<Badge> GetBadges(int SystemeId);
        Systeme GetSystemeById(int SystemeId);
    }
}
