using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseAttachingTest.Models
{
    public class Organism
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        // This needs to be here bc when a derived class instance gets created, the parent class need to be initialized 1st. 
        // This line makes it so that The Organism class can get instantiated with no properties,
        // so that youc an add your own constructor to the derived class. Which means more flexibility
        // in adding new properties to your derived Classes.
        public Organism() { }

        public Organism(int id, string name, string origin, string description, string latitude, string longitude, string date, string time)
        {
            Id = id;
            Name = name;
            Origin = origin;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            Date = date;
            Time = time;
        }
    }
}
