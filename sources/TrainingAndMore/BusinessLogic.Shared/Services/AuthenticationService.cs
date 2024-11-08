using BusinessLogic.Shared.Extensions;
using BusinessLogic.Shared.Interfaces;
using BusinessLogic.Shared.Models;
using BusinessLogic.Shared.Repositories;
using Data.Context;
using Data.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Enums;
using Shared.Models.Auth;
using System.Security.Claims;

namespace BusinessLogic.Shared.Services
{
    public class AuthenticationService : ABusinessLogic, IAuthenticationService
    {
        private readonly AppDataContext _appDataContext;
        private readonly IConfiguration _config;
        private readonly DatabaseRepository<AppUserEntity> _userRepository;
        private readonly DatabaseRepository<AppUserCredentials> _credentialsRepository;
        private bool disposedValue;

        public AuthenticationService(AppDataContext context, IConfiguration config) : base(context)
        {
            _appDataContext = context;
            _config = config;
            _userRepository = new DatabaseRepository<AppUserEntity>(context);
            _credentialsRepository = new DatabaseRepository<AppUserCredentials>(context);
        }

        public async Task<string?> TryLogIn(LoginRequest loginRequest)
        {
            try
            {
                var users = await _userRepository.GetBy(new DbQueryOptions<AppUserEntity>
                {
                    AsNoTracking = false,
                    WhereExpression = user => user.Email.ToLower() == loginRequest.Email.ToLower(),
                });

                if (users.Count == 1)
                {
                    var selectedUser = users.First();

                    if (!selectedUser.IsActive)
                    {
                        return null;
                    }

                    await TryLoadCredentials(selectedUser.CredentialsId);

                    var securedPassword = Helpers.GetSecuredPassword(loginRequest.Password, new Guid(selectedUser.Credentials.Salt));

                    if (securedPassword == selectedUser.Credentials.Password)
                    {
                        var tokenGenerator = new JwtTokenGenerator();

                        var (jwt, refreshToken) = tokenGenerator.GenerateToken(_config, LoadUserClaims(selectedUser), 1);

                        selectedUser.IsLoggedIn = true;
                        selectedUser.Credentials.RefreshToken = refreshToken;

                        await _userRepository.AddOrUpdateAsync(selectedUser, user => user.Email == loginRequest.Email);

                        await SaveChanges();

                        return jwt;
                    }
                    else
                    {
                        selectedUser.Credentials.FailedLogins++;

                        if (selectedUser.Credentials.FailedLogins == 3)
                        {
                            selectedUser.IsActive = false;
                        }

                        await _userRepository.AddOrUpdateAsync(selectedUser, user => user.Email == loginRequest.Email);

                        await SaveChanges();
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> TryLogOut(int userId)
        {
            try
            {
                var user = await _userRepository.GetById(userId);

                if (user == null)
                {
                    return false;
                }

                await TryLoadCredentials(user.CredentialsId);

                if (user.Credentials != null)
                {
                    user.IsLoggedIn = false;
                    user.Credentials.RefreshToken = string.Empty;

                    await _userRepository.AddOrUpdateAsync(user, usr => usr.Email == user.Email);

                    await SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<RegistrationResponse> TryRegisterUser(RegisterRequest registerRequest)
        {
            try
            {
                var existingUsers = await _userRepository.GetBy(new DbQueryOptions<AppUserEntity>
                {
                    AsNoTracking = true,
                    WhereExpression = user => user.Email.ToLower() == registerRequest.Email.ToLower()
                });

                if (!existingUsers.Any())
                {
                    var salt = Guid.NewGuid();

                    var newUser = new AppUserEntity
                    {
                        FirstName = registerRequest.FirstName,
                        Name = registerRequest.Name,
                        Email = registerRequest.Email,
                        DateOfBirth = registerRequest.DateOfBirth,
                        IsActive = false,
                        UserRole = UserRoleEnum.User,
                        Credentials = new AppUserCredentials
                        {
                            Salt = salt.ToString(),
                            Password = Helpers.GetSecuredPassword(registerRequest.Password, salt),
                            FailedLogins = 0,
                        }
                    };

                    var result = await _userRepository.AddOrUpdateAsync(newUser, user => user.Email.ToLower() == registerRequest.Email.ToLower());

                    if (result != null)
                    {
                        await SaveChanges();

                        return new RegistrationResponse
                        {
                            Success = true,
                            UserId = result.Id,
                            FirstName = result.FirstName,
                        };
                    }
                }

                return new RegistrationResponse { Success = false };
            }
            catch (Exception)
            {
                return new RegistrationResponse { Success = false };
            }
        }

        public async Task<UserAccountActivationResponse?> TryActivateAccount(UserAccountActivationRequest request)
        {
            try
            {
                var user = await _userRepository.GetById(request.UserId);

                if(user != null && user.Email.ToLower() == request.Email.ToLower())
                {
                    user.IsActive = true;

                    await _userRepository.AddOrUpdateAsync(user, x => x.Email.ToLower() == request.Email.ToLower());

                    await SaveChanges();

                    return new UserAccountActivationResponse
                    {
                        Email = request.Email,
                    };
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private async Task TryLoadCredentials(int crendentialsId)
        {
            await _credentialsRepository.GetById(crendentialsId);
        }

        private List<Claim> LoadUserClaims(AppUserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.UserIdClaim, user.Id.ToString()),
                new Claim(ClaimTypes.EmailClaim, user.Email),
                new Claim(ClaimTypes.UserDataClaim, JsonConvert.SerializeObject(user.ToExportModel())),
            };

            return claims;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _appDataContext.Dispose();
                }
                disposedValue = true;
            }
        }


        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
