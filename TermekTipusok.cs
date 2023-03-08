using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaktarAlkalmazas
{
    class TermekTipusok
    {
        public int Id{ get; set; }
        public string TipusNev{ get; set; }
        public string TipusokAdatai => $"{Id}, {TipusNev}";
    }
}
