using System;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using TesteCredit.Domains.Repositories.Authentication;

namespace TesteCredit.Infra.Repositories.Auth
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly string _connectionString;

        public AuthenticationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> GetSignInAsync(string user, string hashPassword)
        {
            var query = $@"SELECT Active FROM Users WHERE UserName=@User AND Password=@Password";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<string>(query, new { User = user, Password = hashPassword });
            }
        }
    }
}
