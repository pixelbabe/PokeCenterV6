using Microsoft.AspNetCore.Mvc;
using ProjetPokemon.Data.Models;
using ProjetPokemon.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetPokemon.Data.Data;
using ProjetPokemon.Web.Models;

namespace ProjetPokemon.Web.Controllers
{
    public class TrainerController : Controller
    {        
        const string connexionString = "Server=(localdb)\\mssqllocaldb;Database=PokemonDB;Trusted_Connection=true;";
        private readonly TrainerDBContext db = new TrainerDBContext(connexionString);     
        public IActionResult Index()
        {
            ISourceDonneesTrainer sourceSQL = new DonneesTrainerSQL(db);
            db.Database.EnsureCreated();            
            return View(sourceSQL.GetAll());
        }

        public IActionResult Details(int id)
        {
            ISourceDonneesTrainer sourceSQL = new DonneesTrainerSQL(db);
            db.Database.EnsureCreated();
            //Trainer trainerTrouvé = sourceSQL.Get(id);
            Trainer trainerTrouvé = sourceSQL.GetAllPokemons(id);      
            if (trainerTrouvé == null)
                return View("PasTrouve", id);
            else
                return View(trainerTrouvé);
        }

        public IActionResult Create()
        {
            var vm = new TrainerViewModel
            {
                Trainer = new Trainer(-1, "Nothing", Regions.Kanto)
            };
            return View(vm);
            //return View(new Trainer(-1, "Nothing", Regions.Kanto));
        }

        [HttpPost]
        public IActionResult Create(Trainer nouveauTrainer)
        {
            if (ModelState.IsValid)
            {               
                /*foreach (var t in Trainers)
                {
                    if (t != null && t.Equals(nouveauTrainer))
                    {
                        ViewBag.MessageErreurs = "Ce trainer existe deja";
                        return View(new Trainer(nouveauTrainer.Id, nouveauTrainer.Nom, nouveauTrainer.Region));
                    }
                }*/                               
                ISourceDonneesTrainer sourceSQL = new DonneesTrainerSQL(db);
                db.Database.EnsureCreated();
                sourceSQL.Add(nouveauTrainer);
                //source.Add(nouveauTrainer);// je ne sais pas ou il fait le plus 1 dans le id
                return RedirectToAction("Index");
                

            }

            ViewBag.MessageErreurs = "";
            foreach (var v in ModelState)
            {
                foreach (var erreur in ModelState[v.Key].Errors)
                {
                    ViewBag.MessageErreurs += erreur.ErrorMessage;
                }
            }
            return View(new Trainer(-1, nouveauTrainer.Nom, nouveauTrainer.Region));
        }

        public ActionResult Delete(int id)
        {
            ISourceDonneesTrainer sourceSQL = new DonneesTrainerSQL(db);
            db.Database.EnsureCreated();
            Trainer trainerTrouvé = sourceSQL.Get(id);
                 
            if (trainerTrouvé == null)
                return View("PasTrouve", id);
            else
                return View(trainerTrouvé);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ISourceDonneesTrainer sourceSQL = new DonneesTrainerSQL(db);
            db.Database.EnsureCreated();
            if(sourceSQL.Delete(id))
            {
                return RedirectToAction("Index");
            }
            else
                return View("PasTrouve", id);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ISourceDonneesTrainer sourceSQL = new DonneesTrainerSQL(db);           
            db.Database.EnsureCreated();
            //Trainer trainerTrouvé = sourceSQL.Get(id);
            Trainer trainerTrouvé = sourceSQL.GetAllPokemons(id); 
            
            if (trainerTrouvé == null)
            {
                return View("PasTrouve", id);
            }

            var vm = new TrainerViewModel
            {
                Trainer = trainerTrouvé
            };
            return View(vm);

            //return View(trainerTrouvé);
        }

        [HttpPost]
        public IActionResult Edit(Trainer trainerModif)
        {
            if (ModelState.IsValid)
            {
                /*foreach (var t in Trainers)
                {
                    if (t != null && t.Equals(trainerModif) && trainerModif.Equals(trainerTrouvé) == false)
                    {
                        ViewBag.MessageErreurs = "Ce trainer existe deja";
                        return View(trainerModif);
                    }
                }*/
                ISourceDonneesTrainer sourceSQL = new DonneesTrainerSQL(db);
                db.Database.EnsureCreated();
                if (!sourceSQL.Update(trainerModif))
                //if (!source.Update(trainerModif))
                {
                    return View("PasTrouvé", trainerModif.Id);
                }
                else
                {
                    return RedirectToAction("Details", new { id = trainerModif.Id });
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

            return View(trainerModif);


        }

    }
}
