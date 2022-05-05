using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPokemon.Data.Models
{
    public class Trainer :  IEquatable<Trainer>, IComparable<Trainer>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom est requis")]
        [Display(Name = "Nom d'un client")]
        public string Nom { get; set; }

        public Regions Region { get; set; }

        public Trainer(){}

        public Trainer( int id, string nom, Regions region)
        {
            Id = id;
            Nom = nom;
            Region = region;
        }

        public List<Pokemon> TrainerPokemons { get; set; }
        public int CompareTo([AllowNull] Trainer other)
        {
            if (other == null) return -1;
            int result = Nom.CompareTo(other.Nom);
            if (result == 0)
                result = Region.CompareTo(other.Region);
            return result;
        }

        public bool Equals([AllowNull] Trainer other)
        {
            if (other == null)
                return false;
            else
                return other.Nom.Equals(this.Nom) && other.Region.Equals(this.Region);
        }


    }
}
