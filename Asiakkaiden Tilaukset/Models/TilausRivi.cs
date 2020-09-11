using System;
using System.Collections.Generic;

namespace Asiakkaiden_Tilaukset.Models
{
    public partial class TilausRivi
    {
        public int TilausId { get; set; }
        public int Rivinro { get; set; }
        public int TuoteId { get; set; }
        public int? TilausLkm { get; set; }
        public decimal? Ahinta { get; set; }
        public decimal? Alennus { get; set; }

        public virtual Tilaus Tilaus { get; set; }
        public virtual Tuote Tuote { get; set; }
    }
}
