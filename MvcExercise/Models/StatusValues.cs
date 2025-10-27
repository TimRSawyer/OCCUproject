using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcExercise.Models
{
    public class StatusValues
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ID { get; set; }

        [Display(Name = "Status")]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StatusValue { get; set; }
    }
}
