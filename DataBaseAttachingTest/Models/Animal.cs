using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataBaseAttachingTest.Models
{
    public class Animal
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Origin { get; private set; }
        public string Description { get; private set; }
        public string Latitude { get; private set; }
        public string Longitude { get; private set; }
        public string Date { get; private set; }
        public string Time { get; private set; }

        public Animal(int id, string name, string origin, string description, string latitude, string longitude, string date, string time)
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
