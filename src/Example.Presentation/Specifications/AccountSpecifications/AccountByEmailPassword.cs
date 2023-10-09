namespace Example.Presentation.Specifications.AccountSpecifications;
public sealed class AccountByEmailPassword : Specification<Account>
{
    public AccountByEmailPassword(string email, string password) :
        base(e => e.Email.Equals(email) && e.Password.Equals(password)) { }
}
