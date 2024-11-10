using BusinessLogic.Shared.Interfaces;
using Data.Entities;

namespace BusinessLogic.Shared.Models
{
    public class DbQueryOptions<T> : IDbQueryOptions<T> where T : AEntityBase
    {
        public bool AsNoTracking { get; set; }
        public Func<T, bool>? WhereExpression { get; set; } = null;
        
    }
}
