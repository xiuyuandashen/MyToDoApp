using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MyToDoApp.Api.Model
{
    public class ToDo:BaseEntity
    {
        [StringLength(20)]
        public string Title { get; set; }

        [StringLength(100)]
        public string? Content { get; set; }

        [AllowNull]
        public int? Status { get; set; }
    }

}
