using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganismClasses
{
    internal class Organism
    {
        internal enum Types
        {
            Animal,
            Plant
        }

        internal enum Origins
        {
            Native,
            Foreign
        }

        public string Name { get; set; } = "DefaultName";
        public Types Type { get; set; } = Types.Animal;
        public Origins Origin { get; set; } = Origins.Native;

        public virtual string DryDescription()
        {
            return $"Name:\t\t{Name}\nType:\t\t{Type}\nOrigin:\t\t{Origin}\n";
        }

        public virtual string FloweryDescription()
        {
            return $"The {Name} is a[n] {Type}, {Origin} to the Netherlands";
        }


    }


}