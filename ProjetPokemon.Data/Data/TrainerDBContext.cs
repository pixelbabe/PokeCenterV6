using Microsoft.EntityFrameworkCore;
using ProjetPokemon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPokemon.Data.Data
{
    public class TrainerDBContext : DbContext
    {
        private readonly string _connexionString;

        public TrainerDBContext(string connexion)
        {
            _connexionString = connexion;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.UseSqlServer(_connexionString);
        }

        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.Trainer)
                .WithMany(b => b.Pokemons);
        }*/
    }
}
