﻿using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.BadgeRepository
{
   public interface IBadgeRepository
    {
        //public void Create(Badge badge);

        //public void Update(Badge badgeParam);
        //void Delete(Guid BadgeId);


        public IEnumerable<Badge> GetAll();
        public Badge GetBadgeById(int? badgeId);
        public IEnumerable<Badge> GetUserBadge(int userId);
        public void Create(Badge badge);

    }
}
