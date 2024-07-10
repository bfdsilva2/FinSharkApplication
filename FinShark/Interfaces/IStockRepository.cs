using FinShark.DTOs.Stock;
using FinShark.Helpers;
using FinShark.Model;

namespace FinShark.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject queryObject);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequest updateStockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
