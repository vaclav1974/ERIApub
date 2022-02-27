using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERIA.Models
{
    public class WorkTask
    {

        [Required]
        [Key]
        public int TaskId { get; set; }

        [Required(ErrorMessage ="zadejte úkol")]
        [StringLength(100,ErrorMessage ="úkol může mít maximálně 100 znaků")]
        [Display(Name ="Úkol")]
        public string Input { get; set; }

        [Required(ErrorMessage ="Zadejte platné datum")]
        [DataType(DataType.DateTime)]
        [Display(Name ="Od")]
        public DateTime TillDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Zadejte platné datum")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Do")]

        public DateTime FromDate { get; set; }

        [Display(Name = "Zvol druh úkolu")]
        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        
        public WorkType WorkType { get; set; }

    }
}
