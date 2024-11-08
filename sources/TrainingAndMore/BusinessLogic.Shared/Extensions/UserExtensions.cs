using Data.Entities;
using Shared.Models.Administration;
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

        public static UserAdminPageUserModel ToUserAdminPageUserModel(this AppUserEntity entity) 
        {
            return new UserAdminPageUserModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.Name,
                Email = entity.Email,
                IsActive = entity.IsActive,
                Role = Helpers.GetUserRoleName(entity.UserRole),
            };
        }
    }
}
