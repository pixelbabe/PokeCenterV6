using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPokemon.Data.Models
{
    public class Pokemon : IEquatable<Pokemon>, IComparable<Pokemon>
    {
        public int Id { get; set; }

        public string Name
        {
            get {
                if (this.Nickname != null)
                {
                    return this.Nickname;
                } else
                {
                    return Enum.GetName(typeof(Species), this.Species);
                }

            } 
        }

        public Species Species { get; set; }

        public ElementType ElementType { get; set; }

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Le surnom doit contenir seulement des caractères alphanumériques")]
        [StringLength(16, ErrorMessage = "Le surnom ne doit pas dépasser 16 caractères")]
        

        public string? Nickname { get; set; }

        public Pokemon()
        {
        }

        public Pokemon(int id, Species species)
        {
            this.Id = id;
            this.Species = species;
        }


        public Pokemon(int id, Species species, ElementType elementType)
        {
            this.Id = id;
            this.Species = species;
            this.ElementType = elementType;
        }

        public Pokemon(Species species)
        {
            this.Species = species;
        }

        [Required(ErrorMessage = "Un Trainer doit être associé à un Pokémon")]
        public int TrainerId { get; set; }
        
        public Trainer? Trainer { get; set; }

        public bool Equals(Pokemon other)
        {
            if (other == null)
                return false;
            return Id.Equals(other.Id);
        }


        //TODO: Implementer ca avec trainer
        public int CompareTo(Pokemon other)
        {
            if (other == null) return -1;

            int result = Nickname.CompareTo(other.Nickname);
           
            return result;
        }

        public string TrainerNom()
        {
            return Trainer.Nom;
        }
    }
}
