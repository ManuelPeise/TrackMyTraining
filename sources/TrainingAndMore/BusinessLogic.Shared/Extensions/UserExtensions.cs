using Data.Entities;
using Shared.Models.Auth;

namespace BusinessLogic.Shared.Extensions
{
    public static class UserExtensions
    {
        public static UserExportModel ToExportModel(this AppUserEntity entity)
        {
            return new UserExportModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                Name = entity.Name,
                UserName = entity.UserName,
                Email = entity.Email,
                DateOfBirth = entity.DateOfBirth,
                IsActive = entity.IsActive,
                UserRole = Helpers.GetUserRoleName(entity.UserRole)
            };
        }


    }
}
