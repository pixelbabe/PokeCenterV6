using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetPokemon.Data.Data;
using ProjetPokemon.Data.Models;
using ProjetPokemon.Data.Service;
using ProjetPokemon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetPokemon.Web.Controllers
{

    public class PokemonController : Controller
    {
        const string connexionString = "Server=(localdb)\\mssqllocaldb;Database=PokemonDB;Trusted_Connection=true;";
        private readonly TrainerDBContext db = new TrainerDBContext(connexionString);

        public IActionResult Index()
        {
            ISourceDonneesPokemon sourceSQL = new DonneesPokemonSQL(db);
            db.Database.EnsureCreated();
            return View(sourceSQL.GetAll());
        }

        public IActionResult Create()
        {
            
            ISourceDonneesTrainer sourceSQL = new DonneesTrainerSQL(db);
            db.Database.EnsureCreated();

            var vm = new PokemonViewModel
            {
                GetTrainers = sourceSQL.GetAll()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(Pokemon newPokemon)
        {

            ISourceDonneesPokemon sourceSQL = new DonneesPokemonSQL(db);
            db.Database.EnsureCreated();
            if (ModelState.IsValid)
            {
                
              
                foreach (var t in sourceSQL.GetAll())
                {
                    /*if (t != null && t.Equals(newPokemon))
                    {
                        ViewBag.MessageErreurs = "Ce Pokémon existe déjà";                        
                        return View(newPokemon);
                    }*/
                }
                if (TryValidateModel(newPokemon, nameof(Pokemon)))//non necessaire avec un  paramettre de type complexe
                {
                    sourceSQL.Add(newPokemon);
                    return RedirectToAction("Index");
                }

            }

            ViewBag.MessageErreurs = "";
            foreach (var v in ModelState)
            {
                foreach (var erreur in ModelState[v.Key].Errors)
                {
                    ViewBag.MessageErreurs += erreur.ErrorMessage;
                }
            }
            var vm = new PokemonViewModel { };
            return View(vm);
        }

     

        public IActionResult Details(int id)
        {
            ISourceDonneesPokemon sourceSQL = new DonneesPokemonSQL(db);
            ISourceDonneesTrainer sourceSQL2 = new DonneesTrainerSQL(db);
            db.Database.EnsureCreated();
            Pokemon pkmnTrouve = sourceSQL.Get(id);

            var vm = new PokemonViewModel
            {
                //GetTrainers = sourceSQL2.GetAll(),
                UnPokemon = pkmnTrouve,
                UnTrainer = sourceSQL2.Get(pkmnTrouve.TrainerId)
            };
            if (pkmnTrouve == null)
                return View("PasTrouve", id);
            else
                return View(vm);
        }

        public IActionResult Edit(int id)
        {
            ISourceDonneesPokemon sourceSQL = new DonneesPokemonSQL(db);
            ISourceDonneesTrainer sourceSQL2 = new DonneesTrainerSQL(db);
            db.Database.EnsureCreated();
            Pokemon pkmnTrouve = sourceSQL.Get(id);

            var vm = new PokemonViewModel
            {
                GetTrainers = sourceSQL2.GetAll(),
                UnPokemon = pkmnTrouve
            };

           
            if (pkmnTrouve == null)
                return View("PasTrouve", id);
            else
                return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(Pokemon pokemon)
        {
            ISourceDonneesPokemon sourceSQL = new DonneesPokemonSQL(db);
            db.Database.EnsureCreated();           
            if (ModelState.IsValid)
            {
                Pokemon p = sourceSQL.Get(pokemon.Id);
                if (p == null)
                {
                    return View("PasTrouve");
                }

            else
            {
                    sourceSQL.Update(pokemon);

                    return RedirectToAction("Details", p);
                }

            }

           
            ViewBag.MessageErreurs = "";
            foreach (var v in ModelState)
            {
                foreach (var erreur in ModelState[v.Key].Errors)
                {
                    ViewBag.MessageErreurs += erreur.ErrorMessage;
                }
            }

            return View(pokemon);
        }


        public ActionResult Delete(int id)
        {
            ISourceDonneesPokemon sourceSQL = new DonneesPokemonSQL(db);
            db.Database.EnsureCreated();
            Pokemon pokemon = sourceSQL.Get(id);
            if (pokemon == null)
                return View("PasTrouve", id);
            else
                return View(pokemon);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ISourceDonneesPokemon sourceSQL = new DonneesPokemonSQL(db);
            db.Database.EnsureCreated();
            Pokemon pokemon = sourceSQL.Get(id);
      
            if (pokemon != null && sourceSQL.Delete(id))
            {
                return RedirectToAction("Index");
            }
            else
                return View("PasTrouve", id);
        }

    }
}
