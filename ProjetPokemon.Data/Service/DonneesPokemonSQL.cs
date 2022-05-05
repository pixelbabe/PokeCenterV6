using ProjetPokemon.Data.Models;
using ProjetPokemon.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPokemon.Data.Service
{
    public class DonneesPokemonSQL : ISourceDonneesPokemon
    {
        //private readonly PokemonDBContext db;
        private readonly TrainerDBContext db;

        public DonneesPokemonSQL(TrainerDBContext db)
        {
            this.db = db;
        }

        public IEnumerable<Pokemon> GetAll()
        {
            return db.Pokemons;
        }

        public void Add(Pokemon newPokemon)
        {
            db.Pokemons.Add(newPokemon);
            db.SaveChanges();
        }

        public bool Delete(int id)
        {
            var currentPkmn = Get(id);
            if (currentPkmn != null)
            {
                db.Pokemons.Remove(currentPkmn);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public Pokemon Get(int id)
        {
            return db.Pokemons.Find(id);
        }

        public bool Update(Pokemon updatedPokemon)
        {
            var currentPkmn = Get(updatedPokemon.Id);
            if (currentPkmn != null)
            {
                currentPkmn.Nickname = updatedPokemon.Nickname;
                currentPkmn.ElementType = updatedPokemon.ElementType;
                currentPkmn.TrainerId = updatedPokemon.TrainerId;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }

        
    }
}
