using DataAccess.Interfaces;
using DataAccess.Implementations;

namespace DataAccess.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Mail { get; set; } = string.Empty;
        public bool Active { get; set; }
        public string Password { get; set }
    }
}
