using DomainModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class MovieDto
    {
        [MinLength(1)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public DateTime Year { get; set; }
        public string Genre { get; set; }
    }
}
