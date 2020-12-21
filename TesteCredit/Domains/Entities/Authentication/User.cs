using System;
namespace TesteCredit.Domains.Entities.Authentication
{
    public class User
    {
        public string UserName { get; set; }
        public string PassWordHash { get; set; }
    }
}
