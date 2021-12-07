using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Model
{
    public class TodoItem
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }
        
        // 新增屬性
        [Column(TypeName = "nvarchar(500)")]
        public string Content { get; set; }

        public bool IsComplete { get; set; }
    }
}
