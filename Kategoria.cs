using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaktarAlkalmazas
{
    class Kategoria
    {
        public int Id { get; set; }
        public string KategoriaNev { get; set; }
        public string Kategoriak => $"{Id}, {KategoriaNev}";
    }
}
