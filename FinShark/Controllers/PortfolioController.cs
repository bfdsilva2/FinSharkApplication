using FinShark.Extensions;
using FinShark.Interfaces;
using FinShark.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(UserManager<AppUser> userManager,IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolioAsync(appUser);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if (stock == null)
                return BadRequest("Stock not found");

            var userPortfolio = await _portfolioRepository.GetUserPortfolioAsync(appUser);
            if (userPortfolio.Any(x => x.Symbol.ToLowerInvariant() == symbol.ToLowerInvariant()))
                return BadRequest("Cannot add the same stock to portfolio");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };
            
            var portfolioAdded = await _portfolioRepository.CreateAsync(portfolioModel);

            if (portfolioAdded == null)
                return StatusCode(500, "Could not create");
            else
                return Created(username, portfolioModel);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var userPortfolio = await _portfolioRepository.GetUserPortfolioAsync(appUser);

            var filteredStock = userPortfolio.Where(x => x.Symbol.ToLower() == symbol.ToLower()).ToList();
            if (filteredStock.Count() == 1)
            {
                var portfolioDeleted = await _portfolioRepository.DeleteAsync(appUser, symbol);
            }
            else
                return BadRequest("Stock not in your portfolio");

            return Ok();
        }


    }
}
