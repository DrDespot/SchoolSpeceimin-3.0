using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrganismClasses.Animal;

namespace OrganismClasses
{
    internal class Plant : Organism
    {
        public double HeightInMeters { get; set; } = 9.999;

        public Plant(string name, Origins origin, double heightinmeters)
        {
            Name = name;
            Origin = origin;
            HeightInMeters = heightinmeters;
        }

        public override string DryDescription()
        {
            return $"Name: {Name}\tType: Plant\tOrigin: {Origin}\tHeight:{HeightInMeters} m\n";
        }
    }
}
