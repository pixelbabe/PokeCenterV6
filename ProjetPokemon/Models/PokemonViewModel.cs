using ProjetPokemon.Data.Data;
using ProjetPokemon.Data.Models;
using ProjetPokemon.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetPokemon.Web.Models
{
    public class PokemonViewModel
    {
        public Pokemon UnPokemon { get; set; } = new Pokemon();
        
        public IEnumerable<Trainer> GetTrainers;
        public Trainer UnTrainer { get; set; }


    }
}
