﻿using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.UserBadgeRepository
{
    public interface IUserBadgeRepository
    {
        public List<UserBadge> GetUsersBadge(Badge badge);
        public List<UserBadge> GetUsersBadge(int UserId);
        public List<UserBadge> GetUserBadges();

        bool UpdateProgression(Progression progression);
        Progression GetLastProgressionofUserbadge(int ubID);

        UserBadge GetUserBadge(int UserId, int BadgeId);
        public void CreateUserBadge(int idUser, int idBadge);
        public bool UserBadgeExist(int idUser, int idBadge, DateTime Date);
        public void UpdateUserbadge(UserBadge userBadge);
    }
}
