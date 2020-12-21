using System;
using System.Threading.Tasks;

namespace TesteCredit.Domains.Repositories.Authentication
{
    public interface IAuthenticationRepository
    {
        Task<string> GetSignInAsync(string user, string hashPassword);
    }
}
