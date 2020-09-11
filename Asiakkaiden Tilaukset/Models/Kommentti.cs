using System;
using System.Collections.Generic;

namespace Asiakkaiden_Tilaukset.Models
{
    public partial class Kommentti
    {
        public int KommenttiId { get; set; }
        public string Otsikko { get; set; }
        public string Kommenttiteksti { get; set; }
        public string Nimi { get; set; }
        public DateTime? LuontiPvm { get; set; }
        public int? Arvio { get; set; }

        public virtual Tuote KommenttiNavigation { get; set; }
    }
}
