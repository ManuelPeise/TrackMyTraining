using BusinessLogic.Shared.Repositories;
using Data.Context;
using Data.Entities;

namespace BusinessLogic.Shared.Services
{
    public class UserService
    {
        private readonly AppDataContext _context;
        private readonly DatabaseRepository<AppUserEntity> _userRepository;
        private readonly DatabaseRepository<AppUserCredentials> _credentialsRepository;

        public UserService(AppDataContext context)
        {
            _context = context;
            _userRepository = new DatabaseRepository<AppUserEntity>(context);
            _credentialsRepository = new DatabaseRepository<AppUserCredentials>(context);
        }


    }
}
