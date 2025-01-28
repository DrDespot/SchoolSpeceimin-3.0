using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganismClasses
{
    [Table("Organism")]
    public class Organism
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        public enum Types
        {
            Animal,
            Plant
        }

        public enum Origins
        {
            Native,
            Foreign
        }


        [Column("Name")]
        public string Name { get; set; } = "DefaultName";

        [Column("Type")]
        public Types Type { get; set; } = Types.Animal;

        [Column("Origin")]
        public Origins Origin { get; set; } = Origins.Native;

        public virtual string DryDescription()
        {
            return $"Name:\t\t{Name}\nType:\t\t{Type}\nOrigin:\t\t{Origin}\n";
        }

    }

}