using System;
using System.Collections.Generic;

namespace Asiakkaiden_Tilaukset.Models
{
    public partial class Tilaus
    {
        public Tilaus()
        {
            TilausRivi = new HashSet<TilausRivi>();
        }

        public int TilausId { get; set; }
        public int? AsiakasId { get; set; }
        public DateTime? TilausPvm { get; set; }
        public DateTime? ToimitusPvm { get; set; }
        public decimal? Tilaussumma { get; set; }

        public virtual Asiakas Asiakas { get; set; }
        public virtual ICollection<TilausRivi> TilausRivi { get; set; }
    }
}
