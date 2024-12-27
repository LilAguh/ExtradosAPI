using DataAccess.Interfaces;
using DataAccess.Implementations;
using System.Xml.Linq;

namespace DataAccess.Models
{
    public class User
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Mail { get; set; }
        public int? Age { get; set; } // Edad opcional
        public string? Password { get; set; }
        public bool Active { get; set; } = true;
    }
}
