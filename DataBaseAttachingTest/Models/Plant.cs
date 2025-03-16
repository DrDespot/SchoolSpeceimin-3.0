using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataBaseAttachingTest.Models
{
    public class Plant : Organism
    {
        public Plant(int id, string name, string origin, string description, string latitude, string longitude, string date, string time)
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
