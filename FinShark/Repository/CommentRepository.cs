using FinShark.Data;
using FinShark.DTOs.Comment;
using FinShark.Interfaces;
using FinShark.Model;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetAllAsync()
        {           
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Comment?> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();

            var commentDto = await _context.Comments.FirstOrDefaultAsync(x => x.Id == commentModel.Id);

            if (commentDto == null)
                return null;

            return commentDto;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateCommentRequestDto)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (commentModel == null)
                return null;

            commentModel.Title = updateCommentRequestDto.Title;
            commentModel.Content = updateCommentRequestDto.Content;            

            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<bool> CommentExists(int id)
        {
            return await _context.Comments.AnyAsync(x => x.Id == id);
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (commentModel == null)
                return null;

            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
        }
    }
}
