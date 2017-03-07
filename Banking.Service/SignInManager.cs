using System.Data.Entity;
using System.Threading.Tasks;
using Banking.Core;
using Banking.Data.Infrastructure;
using Banking.Model;

namespace Banking.Service
{
    public interface ISignInManager
    {
        Task<OperationResult<User>> Authentication(string login, string password);
        Task<OperationDetails> Register(string login, string password);
    }

    public class SignInManager : ISignInManager
    {
        public IUoWFactory<IBankingUoW> UoWFactory { get; set; }

        public SignInManager(IUoWFactory<IBankingUoW> factory)
        {
            UoWFactory = factory;
        }

        public Task<OperationResult<User>> Authentication(string login, string password)
        {
            return Operation.Wrap<User>(async res =>
            {
                User user;
                using (var uow = UoWFactory.Create())
                {
                    user = await uow.Users.Where(u => u.Login == login).FirstOrDefaultAsync();
                }
                if (user == null)
                {
                    res.SetError("Invalid login");
                    return null;
                }
                if (user.Password != password)
                {
                    res.SetError("Invalid password");
                    return null;
                }
                return user;
            });
        }
        public Task<OperationDetails> Register(string login, string password)
        {
            return Operation.Wrap(async res =>
            {
                if (login == null)
                {
                    res.SetError(nameof(login), "Argument null");
                    return null;
                }
                if (password == null)
                {
                    res.SetError(nameof(password), "Argument null");
                    return null;
                }
                var user = new User { Login = login, Password = password };
                using (var uow = UoWFactory.Create())
                {
                    uow.Users.Add(user);
                    var storedItems = await uow.CommitAsync();
                }
                return res;
            });
        }
    }
}
