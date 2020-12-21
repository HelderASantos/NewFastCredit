using System;
namespace TesteCredit.Domains.Entities
{
    public class Custormers
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public Adress Adress { get; set; }
        public string Services { get; set; }
    }
}
