using Microsoft.AspNetCore.Mvc;
using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ExernalServices
{
   public  interface IToDosController
    {

        public  Task<List<Project>> GetPorjects();



       
        
        

        //public  Task dayevent();
    

        
        public  Task TodosBadge();



        public  Task<int?> verifyidredmine(int userId);
        public  Task<List<int>> listofusersidinredmine();

        public  Task<List<Issue>> GetIssuesperuser();
        public  Task<IActionResult> returnUser();
        public List<User> getUserSystemesIds();
        
            



    }
}
