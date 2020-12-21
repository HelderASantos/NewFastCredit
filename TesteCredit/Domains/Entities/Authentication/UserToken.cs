using System;
namespace TesteCredit.Domains.Entities.Authentication
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
