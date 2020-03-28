using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectF.ModelsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.Components
{
    public class SystemesList
    {
        private List<SystemeEntityDto> _allSystemes = new List<SystemeEntityDto>();

        public SystemesList(List<SystemeEntityDto> allSystemes)
        {
            _allSystemes = allSystemes;
        }

        public List<SelectListItem> GetSystemesList()
        {
            var items = new List<SelectListItem>();
            foreach (var systeme in _allSystemes)
            {
                items.Add(new SelectListItem
                {
                   Text = systeme.SystemName ,
                   Value = systeme.Id.ToString()
                });
            }

            return items;
        }
    }
}
