using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    public class Systeme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SystemName { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public IList<Badge> Badges { get; set; }
        public List<SystemeUser> SystemeUsers { get; set; }

    }

}

//commit counter = yconecty aal gitlab mettre a jour la table user badge avec config mta3 badge ychouf l periodicité
//    et il met a jour le nbre de commit de user dans la table userbadge = classe entre system et user badge / objet http client
    
//    hangfire.net =projet open source pour plannifier des taches.
