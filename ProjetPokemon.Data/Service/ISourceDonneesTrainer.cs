using ProjetPokemon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPokemon.Data.Service
{
    public interface ISourceDonneesTrainer
    {
        IEnumerable<Trainer> GetAll();
        Trainer Get(int id);

        void Add(Trainer nouveauClient);
        bool Update(Trainer clientModifié);

        bool Delete(int id);

        Trainer GetAllPokemons(int id);
    }
}
