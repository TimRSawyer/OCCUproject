using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcExercise.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class SimpleDataSet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? Name { get; set; }
        public string? Rank { get; set; }
        public string? Position { get; set; }
        public string? Posting { get; set; }

        [Display(Name = "Last Updated")]
        [Required]
        public DateTime UpdateTimeStamp { get; set; }
    }
}