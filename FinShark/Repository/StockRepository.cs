using FinShark.Data;
using FinShark.DTOs.Stock;
using FinShark.Helpers;
using FinShark.Interfaces;
using FinShark.Mappers;
using FinShark.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;

namespace FinShark.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
                stocks = stocks.Where(x => x.Symbol.Contains(queryObject.Symbol));

            if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
                stocks = stocks.Where(x => x.CompanyName.Contains(queryObject.CompanyName));

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals(nameof(Stock.Symbol), StringComparison.OrdinalIgnoreCase))
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(x => x.Symbol) : stocks.OrderBy(x => x.Symbol);
                if (queryObject.SortBy.Equals(nameof(Stock.CompanyName), StringComparison.OrdinalIgnoreCase))
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(x => x.CompanyName) : stocks.OrderBy(x => x.CompanyName);
            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

            return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Stock?> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            
            var stockDto = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == stockModel.Id);

            if (stockDto == null)
                return null;

            return stockModel;
        }
        
        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequest updateStockDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null)
                return null;

            stockModel.Symbol = updateStockDto.Symbol;
            stockModel.CompanyName = updateStockDto.CompanyName;
            stockModel.Purchase = updateStockDto.Purchase;
            stockModel.LastDiv = updateStockDto.LastDiv;
            stockModel.Industry = updateStockDto.Industry;
            stockModel.MarketCap = updateStockDto.MarketCap;

            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null)
                return null;

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(x => x.Id == id);
        }
    }
}
