using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public abstract class AEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedAt { get; set; }
    }
}
