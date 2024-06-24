using System.ComponentModel.DataAnnotations;

namespace FinShark.DTOs.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must have a minimun of 5 characters")]
        [MaxLength(255, ErrorMessage = "Content must have a maximun of 255 characters ")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Title must have a minimun of 5 characters")]
        [MaxLength(255, ErrorMessage = "Content must have a maximun of 255 characters ")]
        public string Content { get; set; } = string.Empty;
    }
}
