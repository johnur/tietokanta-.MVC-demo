using System;
using System.Collections.Generic;

namespace Asiakkaiden_Tilaukset.Models
{
    public partial class Tuote
    {
        public Tuote()
        {
            TilausRivi = new HashSet<TilausRivi>();
        }

        public int TuoteId { get; set; }
        public string Nimi { get; set; }
        public string Tyyppi { get; set; }
        public string Tuoteryhmä { get; set; }
        public decimal? Hinta { get; set; }

        public virtual Kommentti Kommentti { get; set; }
        public virtual ICollection<TilausRivi> TilausRivi { get; set; }
    }
}
