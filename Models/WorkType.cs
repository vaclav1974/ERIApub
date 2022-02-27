using System.ComponentModel.DataAnnotations;

namespace ERIA.Models
{
    public class WorkType
    {
        [Key]
        public int TypeId { get; set; }

        [Required]
        [StringLength(30,ErrorMessage ="může být maximálně 30 znaků dlouhý")]

        public string Description { get; set; }
        
        public ICollection<WorkTask> WorkTasks { get; set; }

    }
}