using BusinessLogic.Shared.Services;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Shared
{
    public abstract class ABusinessLogic
    {
        private readonly AppDataContext _context;
        private readonly AppUserEntity? _currentUser;
        private readonly ClaimsService _claimsService = new ClaimsService();
        public AppUserEntity? CurrentUser => _currentUser;
        
        protected ABusinessLogic(AppDataContext context)
        {
            _context = context;
            _currentUser = _claimsService.LoadCurrentUserFromClaims();
        }

        protected async Task SaveChanges()
        {
            var modifiedEntries = _context.ChangeTracker.Entries()
               .Where(x => x.State == EntityState.Modified ||
               x.State == EntityState.Added);

            foreach (var entry in modifiedEntries)
            {
                if (entry != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        ((AEntityBase)entry.Entity).CreatedBy = CurrentUser?.UserName ?? "System";
                        ((AEntityBase)entry.Entity).CreatedAt = DateTime.Now.ToString("yyyy-MM-dd");
                        ((AEntityBase)entry.Entity).UpdatedBy = CurrentUser?.UserName ?? "System";
                        ((AEntityBase)entry.Entity).UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd");

                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        ((AEntityBase)entry.Entity).UpdatedBy = CurrentUser?.UserName ?? "System";
                        ((AEntityBase)entry.Entity).UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

      
    }
}

