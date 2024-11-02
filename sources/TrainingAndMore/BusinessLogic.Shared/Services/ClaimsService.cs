using BusinessLogic.Shared.Interfaces;
using Data.Entities;
using Newtonsoft.Json;
using Shared.Enums;
using System.Security.Claims;

namespace BusinessLogic.Shared.Services
{
    public class ClaimsService
    {
        private Dictionary<string, string> _claimsDictionary;

        public Dictionary<string, string> Claims => _claimsDictionary;

        public ClaimsService()
        {
            _claimsDictionary = LoadClaimsData();
        }

        public T? GetClaimsValue<T>(string claimType)
        {
            if (_claimsDictionary.ContainsKey(claimType))
            {
                var selectedClaimField = _claimsDictionary[claimType];
                var type = typeof(T);

                if (selectedClaimField == null)
                {
                    return default(T?);
                }

                if (type == typeof(int))
                {
                    return (T)Convert.ChangeType(int.Parse(selectedClaimField), type);
                }

                if (type == typeof(string))
                {
                    return (T)Convert.ChangeType(selectedClaimField, type);
                }

                if (type == typeof(Guid))
                {
                    return (T)Convert.ChangeType(new Guid(selectedClaimField), type);
                }

                if (type == typeof(DateTime))
                {
                    return (T)Convert.ChangeType(DateTime.Parse(selectedClaimField), type);
                }

            }

            return default;
        }

        public AppUserEntity? LoadCurrentUserFromClaims()
        {
            var userDataJson = GetClaimsValue<string>(ClaimTypes.UserDataClaim);

            if (!string.IsNullOrWhiteSpace(userDataJson))
            {
                return JsonConvert.DeserializeObject<AppUserEntity>(userDataJson);
            }

            return null;
        }

        private Dictionary<string, string> LoadClaimsData()
        {
            var claimsDictionary = new Dictionary<string, string>();
            var claims = ClaimsPrincipal.Current?.Claims.ToList() ?? new List<Claim>();

            foreach (var claim in claims)
            {
                if (!claimsDictionary.ContainsKey(claim.Type))
                {
                    claimsDictionary.Add(claim.Type, claim.Value);
                }
            }

            return claimsDictionary;
        }


    }
}
