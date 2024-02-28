using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Search.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm searchTerm)
        {
           var result= await searchService.SearchAsync(searchTerm.CustomerId);
            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }
    }
}
