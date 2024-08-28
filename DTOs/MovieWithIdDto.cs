using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class MovieWithIdDto
    {
        public int Id { get; set; }
        [MinLength(1)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public DateTime Year { get; set; }
        public string Genre { get; set; }
    }
}
