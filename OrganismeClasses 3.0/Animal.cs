using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganismClasses
{
    internal class Animal : Organism
    {
        internal enum Habitats
        {
            Forest,
            Plains,
            Pond,
            Shed
        }

        //habitat = Leefgebied
        public Habitats Habitat { get; set; } = Habitats.Forest;

        public Animal(string name, Origins origin, Habitats habitat)
        {
            Name = name;
            Origin = origin;
            Habitat = habitat;
        }

        public override string DryDescription()
        {
            return $"Name: {Name}\tType: Animal\tOrigin: {Origin}\tHabitat:{Habitat}\n";
        }
    }
}