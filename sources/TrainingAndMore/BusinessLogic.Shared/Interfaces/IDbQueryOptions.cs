using Data.Entities;
using System.Linq.Expressions;

namespace BusinessLogic.Shared.Interfaces
{
    public interface IDbQueryOptions<T> where T : AEntityBase
    {
        public bool AsNoTracking { get; set; }
        public Func<T, bool>? WhereExpression { get; set; }
    }
}
