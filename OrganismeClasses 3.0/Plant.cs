using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrganismClasses.Animal;

namespace OrganismClasses
{
    [Table("Plant")]
    public class Plant : Organism
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("HeightInMeters")]
        public double HeightInMeters { get; set; } = 9.999;

        public Plant(string name, string origin, double heightinmeters)
        {
            Name = name;
            Origin = origin;
            HeightInMeters = heightinmeters;
        }

        public override string DryDescription()
        {
            return $"Name: {Name}\n";
        }
    }
}
