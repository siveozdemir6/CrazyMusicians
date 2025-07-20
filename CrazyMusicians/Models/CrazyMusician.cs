using System.ComponentModel.DataAnnotations;

namespace CrazyMusicians.Models
{
    public class CrazyMusician
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Profession { get; set; }

        [Required]
        [MaxLength(100)]
        public string FunFact { get; set; }

    }
}
