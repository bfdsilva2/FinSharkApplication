using FinShark.DTOs.Comment;
using FinShark.DTOs.Stock;
using FinShark.Model;

namespace FinShark.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateCommentRequestDto);
        Task<bool> CommentExists(int id);
        Task<Comment?> DeleteAsync(int id);
    }
}
