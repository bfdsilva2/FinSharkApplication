using FinShark.DTOs.Stock;
using FinShark.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FinShark.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap, 
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }
        public static Stock ToStockFromCreateDto(this CreateStockRequestDto createStockRequestDto)
        {
            return new Stock
            {
                Symbol = createStockRequestDto.Symbol,
                CompanyName = createStockRequestDto.CompanyName,
                Purchase = createStockRequestDto.Purchase,
                LastDiv = createStockRequestDto.LastDiv,
                Industry = createStockRequestDto.Industry,
                MarketCap = createStockRequestDto.MarketCap
            };
        }

        public static Stock ToStockFromUpdateDto(this UpdateStockRequest updateStockRequest)
        {
            return new Stock
            {
                Symbol = updateStockRequest.Symbol,
                CompanyName = updateStockRequest.CompanyName,
                Purchase = updateStockRequest.Purchase,
                LastDiv = updateStockRequest.LastDiv,
                Industry = updateStockRequest.Industry,
                MarketCap = updateStockRequest.MarketCap
            };
        }
    } 
}
