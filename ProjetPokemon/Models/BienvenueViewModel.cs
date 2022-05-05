using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetPokemon.Web.Models
{
    public class BienvenueViewModel
    {
        public string Nom { get; set; }
        public string Message { get; set; }
        public int PokemonTotal { get; set; }
        public int TrainerTotal { get; set; }
        public int PokecenterTotal { get; set; }
    }
}
