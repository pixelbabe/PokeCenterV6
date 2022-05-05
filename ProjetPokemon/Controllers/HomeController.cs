using Microsoft.AspNetCore.Mvc;
using ProjetPokemon.Data.Data;
using ProjetPokemon.Data.Service;
using ProjetPokemon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetPokemon.Web.Controllers
{
    public class HomeController : Controller
    {

        const string connexionString = "Server=(localdb)\\mssqllocaldb;Database=PokemonDB;Trusted_Connection=true;";
        private readonly TrainerDBContext db = new TrainerDBContext(connexionString);

        public IActionResult Index()
        {
            ISourceDonneesPokemon sourcePokemonSQL = new DonneesPokemonSQL(db);
            ISourceDonneesTrainer sourceTrainerSQL = new DonneesTrainerSQL(db);
            db.Database.EnsureCreated();

            var pokemons = sourcePokemonSQL.GetAll();
            var trainers = sourceTrainerSQL.GetAll();
            var pkmnCount = 0;
            var trainerCount = 0;

            foreach (var p in pokemons)
            {
                pkmnCount += 1;
            }

            foreach (var t in trainers)
            {
                trainerCount += 1;
            }

            var viewModel = new BienvenueViewModel
            {
                Nom = "Vincent",
                Message = "Bienvenu au PokéCenter,",
                PokemonTotal = pkmnCount,
                TrainerTotal = trainerCount
                //PokecenterTotal = pokecenterCount
            };

            return View(viewModel);
        }
    }
}
