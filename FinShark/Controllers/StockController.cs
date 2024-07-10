using FinShark.Data;
using FinShark.DTOs.Stock;
using FinShark.Helpers;
using FinShark.Interfaces;
using FinShark.Mappers;
using FinShark.Model;
using FinShark.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace FinShark.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {        
        private readonly IStockRepository _stockRepository;
              
        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject) 
        {
            try
            {
                if (!ModelState.IsValid)
                return BadRequest(ModelState);

                var stocks = await _stockRepository.GetAllAsync(queryObject);
                var stockDto = stocks.Select(s => s.ToStockDto());
                return Ok(stockDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error retrieving data");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var stock = await _stockRepository.GetByIdAsync(id);
                if (stock == null)
                    return NotFound();

                return Ok(stock.ToStockDto());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateStockRequestDto stockDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var stockModel = stockDto.ToStockFromCreateDto();
                await _stockRepository.CreateAsync(stockModel);

                return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inserting data");
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateStockRequest updateStockRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var stockModel = await _stockRepository.UpdateAsync(id, updateStockRequest);

                if (stockModel == null)
                    return NotFound();

                return Ok(stockModel.ToStockDto());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inserting data");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!await _stockRepository.StockExists(id))
                    return NotFound($"Stock with Id = {id} not found");

                var ret = await _stockRepository.DeleteAsync(id);

                return Ok(ret.ToStockDto());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
    }
}
