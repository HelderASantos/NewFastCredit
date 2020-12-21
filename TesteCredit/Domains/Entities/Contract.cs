using System;
namespace TesteCredit.Domains.Entities
{
    public class Contract
    {
        public long Id { get; set; }
        public string Service { get; set; }
        public object TermsOfCommitment { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
