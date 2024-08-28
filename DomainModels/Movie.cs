using DomainModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    public class Movie : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        public DateTime Year { get; set; }
        [Required]
        public Genres Genre { get; set; }
    }
}
