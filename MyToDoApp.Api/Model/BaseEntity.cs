using System.ComponentModel.DataAnnotations;

namespace MyToDoApp.Api.Model
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
