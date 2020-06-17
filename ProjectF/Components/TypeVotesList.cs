using Microsoft.AspNetCore.Mvc.Rendering;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.Components
{
    public class TypeVotesList
    {

        private List<TypeVote> _allTypeVote = new List<TypeVote>();

        public TypeVotesList(List<TypeVote> allTypeVote)
        {
            _allTypeVote = allTypeVote;
        }

        public List<SelectListItem> GetvoteTypeList()
        {
            var items = new List<SelectListItem>();
            foreach (var typeVote in _allTypeVote)
            {
                items.Add(new SelectListItem
                {
                    Text = typeVote.Libellé,
                    Value = typeVote.Id.ToString()
                });
            }

            return items;
        }
    }
}
