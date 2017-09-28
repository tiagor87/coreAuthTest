using domain.Models;

namespace domain.Services
{
    public class AccountService : Disposable
    {
        public Account GetById(long id)
        {
            return id.Equals(1) ?
                new Account
                {
                    Id = 1,
                    Owner = new User
                    {
                        Id = 1
                    }
                } :
                new Account
                {
                    Id = 2,
                    Owner = new User
                    {
                        Id = 2
                    }
                };
        }
    }
}