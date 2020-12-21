using System;
namespace TesteCredit.Domains.Entities
{
    public class Adress
    {
        public string Street { get; set; }
        public long Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
