using ProjetPokemon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPokemon.Data.Service
{
    public class DonneesTrainerEnMemoire : ISourceDonneesTrainer
    {
        private readonly List<Trainer> trainers;

        public DonneesTrainerEnMemoire()
        {
            trainers = new List<Trainer>
            {
            new Trainer(1, "Bob", Regions.Kanto),
            new Trainer(2, "May Mayo", Regions.Galar),
            new Trainer(3, "McJacky", Regions.Alola)

            };
    }

        public IEnumerable<Trainer> GetAll()
        {          
            return trainers.OrderBy(c => c.Nom);
        }

        public void Add(Trainer nouveauClient)
        {
            int maxId = 0;
            foreach (var c in trainers)
            {
                if (c.Id > maxId)
                {
                    maxId = c.Id;
                }
            }

            nouveauClient.Id = maxId + 1;
            trainers.Add(nouveauClient);
        }

        public bool Delete(int id)
        {
            var currentClient = Get(id);
            if (currentClient != null && trainers.Remove(currentClient))
            {
                return true;
            }
            else
                return false;
        }

        public Trainer Get(int id)
        {
            return trainers.Find(t => t.Id == id);
           
        }


        public bool Update(Trainer trainerModif)
        {
            var currentTrainer = Get(trainerModif.Id);
            if (currentTrainer != null)
            {
                currentTrainer.Nom = trainerModif.Nom;
                currentTrainer.Region = trainerModif.Region;

                return true;
            }
            else
                return false;
        }

        public Trainer AddTrainer(int id)
        {
            throw new NotImplementedException();
        }

        public Trainer GetAllPokemons(int id)
        {
            throw new NotImplementedException();
        }
    }
}
