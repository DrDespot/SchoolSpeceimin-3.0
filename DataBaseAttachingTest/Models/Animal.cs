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
        public string Description { get; private set; }
        public string Geolocation { get; private set; }
        public string Date { get; private set; }
        public string Time { get; private set; }

        public Animal(int id, string name, string description, string geolocation, string date, string time)
        {
            Id = id;
            Name = name;
            Description = description;
            Geolocation = geolocation;
            Date = date;
            Time = time;
        }

    }
}
