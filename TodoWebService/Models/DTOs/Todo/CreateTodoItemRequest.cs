using System.ComponentModel.DataAnnotations;

namespace TodoWebService.Models.DTOs.Todo
{
    public class CreateTodoItemRequest
    {
        [Required]
        [MinLength(5)]
        public string Text { get; set; } = string.Empty;
        [Required]
        public DateTimeOffset? EndDate { get; set; }
    }
}
