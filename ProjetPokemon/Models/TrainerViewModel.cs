using System;
using System.Collections.Generic;
using ProjetPokemon.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetPokemon.Web.Models
{
    public class TrainerViewModel
    {
        public Trainer Trainer { get; set; }

        public List<string> Regions
        {
            get
            {
                var r = new List<string>();
                foreach (var element in Enum.GetNames(typeof(Regions)))
                {
                    r.Add(element);

                }
                return r;
            }
        }


    }
}
