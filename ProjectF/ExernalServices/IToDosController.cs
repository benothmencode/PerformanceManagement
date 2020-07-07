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



        public List<Issue> GetTimeEntries();
       

    
       
        public  Task<String> GetIssueStatus();
        
        

        //public  Task dayevent();
    

        
        public  Task TodosBadge();
      


        public  Task<IdentifiableName> GetIssuePerProject();
       

        




    }
}
