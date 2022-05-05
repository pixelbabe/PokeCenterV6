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

        //private readonly ISourceDonneesPokemon source = Startup.SourceDonnees;

        const string connexionString = "Server=(localdb)\\mssqllocaldb;Database=PokemonDB;Trusted_Connection=true;";
        //private readonly TrainerDBContext db = new TrainerDBContext(connexionString);
        private readonly TrainerDBContext db = new TrainerDBContext(connexionString);

        public IActionResult Index()
        {
            ISourceDonneesPokemon sourceSQL = new DonneesPokemonSQL(db);
            db.Database.EnsureCreated();
            return View(sourceSQL.GetAll());
        }

        public IActionResult Create()
        {
            //ISourceDonneesPokemon sourceSQL = new DonneesPokemonSQL(db);
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
              
                    //p.Nickname = pokemon.Nickname;
                    //p.ElementType = pokemon.ElementType;

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

        /*private void populateElementTypeDropdown()
        {
            var elementTypeEnumData = from ElementType e in Enum.GetValues(typeof(ElementType))
                                      select new
                                      {
                                          ID = (int)e,
                                          Name = e.ToString()
                                      };
            ViewBag.elementTypeEnumData = new SelectList(elementTypeEnumData, "ID", "Name");
        }

        private void populateSpeciesDropdown()
        {
            //https://www.c-sharpcorner.com/UploadFile/f1047f/bind-enum-to-dropdownlist-in-Asp-Net-mvc/
            var speciesEnumData = from Species s in Enum.GetValues(typeof(Species))
                                  select new
                                  {
                                      ID = (int)s,
                                      Name = s.ToString()
                                  };

            ViewBag.speciesEnumData = new SelectList(speciesEnumData, "ID", "Name");
        }*/
        /*private int getNextId()
        {
            int maxId = 0;
            foreach (var f in source.GetAll())
            {
                if (f.Id > maxId)
                {
                    maxId = f.Id;
                }
            }

            return maxId + 1;
        }*/
    }
}
