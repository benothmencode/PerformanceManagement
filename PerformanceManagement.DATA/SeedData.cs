﻿using Microsoft.AspNetCore.Identity;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.DATA
{
    public static class SeedData
    {
        
        public static void Seed(UserManager<User> userManager, RoleManager<AppRole> roleManager)
        {
          
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
       
        private static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("Admin").Result == null)
            {
                var user = new User
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    Active = true ,
                    Userimage = "avatar5.png"


                };
                var result = userManager.CreateAsync(user, "P@ssword1").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }

        }
        private static void SeedRoles(RoleManager<AppRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new AppRole
                {
                    Name = "Administrator"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                var role = new AppRole
                {
                    Name = "Employee"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}