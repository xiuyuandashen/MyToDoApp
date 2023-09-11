using System.ComponentModel.DataAnnotations;

namespace MyToDoApp.Api.Model
{
    public class Memo : BaseEntity
    {
        [StringLength(100)]
        public string? Title { get; set; }
        [StringLength(255)]
        public string? Content { get; set; }
    }
}
