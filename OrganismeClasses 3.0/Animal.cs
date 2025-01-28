using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrganismClasses
{
    [Table("Animal")]
    public class Animal : Organism
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        //habitat = Leefgebied
        [Column("Habitat")]
        public string Habitat { get; set; } = "DefaultHabitat";

        public Animal(string name, Origins origin, string habitat)
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