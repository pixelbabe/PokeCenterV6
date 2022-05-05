using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetPokemon.Data.Data;
using ProjetPokemon.Data.Models;
using ProjetPokemon.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPokemon.Test
{
    [TestClass]
    public class DonnesTrainerSQLTest
    {
        const string connexionString = "Server=(localdb)\\mssqllocaldb;Database=ProjetPokemonTest;Trusted_Connection=true;";
        private readonly TrainerDBContext db = new TrainerDBContext(connexionString);
        
        [TestMethod]
        public void TestCreationBD()
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            var expected = "ProjetPokemonTest";
            Assert.AreEqual(expected, db.Database.GetDbConnection().Database);
        }

        [TestMethod]
        public void WhenAddingNewTrainerExpecTrainersCount1()
        {
            

            //Arrange
            ISourceDonneesTrainer sourceSQL = new DonneesTrainerSQL(db);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var t = new Trainer();
            t.Nom = "Sasha";

            //Act
            sourceSQL.Add(t);

            //Assert
            Assert.IsTrue(db.Trainers.Count() == 1);
            Assert.IsTrue(db.Trainers.First() == t);
            
        }

        [TestMethod]
        public void WhenAddingNewTrainerExpecTrainersCount2()
        {


            //Arrange
            ISourceDonneesTrainer sourceSQL = new DonneesTrainerSQL(db);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var t = new Trainer();
            t.Nom = "Sasha";
            var t2 = new Trainer();
            t2.Nom = "Bob";

            sourceSQL.Add(t);
            sourceSQL.Add(t2);

            //Act
            var listTrainers = sourceSQL.GetAll();


            //Assert
            Assert.IsTrue(listTrainers.Count() == 2);

        }

        [TestMethod]
        public void UpdatingTrainer()
        {
            //Arrange
            ISourceDonneesTrainer sourceSQL = new DonneesTrainerSQL(db);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var t = new Trainer();
            t.Nom = "Sasha";

            //Act
            sourceSQL.Add(t);
            t.Nom = "Bob";
            sourceSQL.Update(t);

            //Assert
            Assert.IsTrue(t.Nom == "Bob");
        }

        [TestMethod]
        public void UpdatingNonExistingTrainer()
        {
            //Arrange
            ISourceDonneesTrainer sourceSQL = new DonneesTrainerSQL(db);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var t = new Trainer();
            t.Nom = "Sasha";

            //Act
            var updated  = sourceSQL.Update(t);

            //Assert
            Assert.IsFalse(updated);
        }

        [TestMethod]
        public void DeleteTrainerById()
        {
            //Arrange
            ISourceDonneesTrainer sourceSQL = new DonneesTrainerSQL(db);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var t = new Trainer();
            t.Nom = "Sasha";

            //Act
            sourceSQL.Add(t);
            sourceSQL.Delete(t.Id);

            //Assert
            Assert.IsTrue(db.Trainers.Count() == 0);
        }

        [TestMethod]
        public void DeleteNonExistingTrainerById()
        {
            //Arrange
            ISourceDonneesTrainer sourceSQL = new DonneesTrainerSQL(db);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var t = new Trainer();
            t.Nom = "Sasha";

            //Act
            var deleted = sourceSQL.Delete(t.Id);

            //Assert
            Assert.IsFalse(deleted);
        }
    }
}
