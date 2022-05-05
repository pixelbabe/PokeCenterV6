using Microsoft.EntityFrameworkCore;
using ProjetPokemon.Data.Data;
using ProjetPokemon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPokemon.Data.Service
{
    public class DonneesTrainerSQL : ISourceDonneesTrainer
    {
        private readonly TrainerDBContext db;

        public DonneesTrainerSQL(TrainerDBContext db)
        {
            this.db = db;
        }

        public void Add(Trainer nouveauTrainer)
        {
            db.Trainers.Add(nouveauTrainer);
            db.SaveChanges();
        }

        public bool Delete(int id)
        {
            var currentTrainer = Get(id);           
            if (currentTrainer != null)
            {
                db.Trainers.Remove(currentTrainer);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public Trainer Get(int id)
        {
            return db.Trainers.Find(id);
        }

        public IEnumerable<Trainer> GetAll()
        {
            return db.Trainers.Include(t => t.TrainerPokemons);
        }

        public bool Update(Trainer trainerModifiée)
        {
            var currentTrainer = Get(trainerModifiée.Id);
            if (currentTrainer != null)
            {
                currentTrainer.Nom = trainerModifiée.Nom;
                currentTrainer.Region = trainerModifiée.Region;
                db.SaveChanges();

                return true;
            }
            else
                return false;
        }

        public Trainer GetAllPokemons(int id)
        {
            var trainerId = Get(id);
            Trainer trainer = db.Trainers
                .Include(t => t.TrainerPokemons)
                .Single(t => t.Id == trainerId.Id);
            return trainer;
           
        }

        
    }

}
