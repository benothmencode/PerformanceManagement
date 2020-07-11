﻿using PerformanceManagement.ENTITIES;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.SystemeRepository
{
    public interface ISystemeRepository
    {
        IEnumerable<Systeme> GetSystemes();
        //IEnumerable<Badge> GetBadges(int SystemeId);
        Systeme GetSystemeById(int ServiceId);

        void CreateSysteme(Systeme Systeme);
        void UpdateSysteme(Systeme Systeme);
        void DeleteSysteme(Systeme Systeme);
        public Systeme GetGitlab(string title);
        bool SystemeExists(int systemeId);

        void updatesystemUser(SystemeUser systemeUser);
    }
}
