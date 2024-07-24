using FinShark.DTOs.Comment;
using FinShark.DTOs.Stock;
using FinShark.Model;

namespace FinShark.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedBy = commentModel.AppUser.UserName,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }

        public static Comment ToCommentFromCreateCommentDto(this CreateCommentRequestDto createCommentDto, int stockId) 
        {
            return new Comment
            {
                Title = createCommentDto.Title,
                Content = createCommentDto.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdateCommentRequestDto(this UpdateCommentRequestDto updateCommentRequestDto, int commentId)
        {
            return new Comment
            {
                Id = commentId,
                Title = updateCommentRequestDto.Title,
                Content = updateCommentRequestDto.Content
            };
        }



    }
}
