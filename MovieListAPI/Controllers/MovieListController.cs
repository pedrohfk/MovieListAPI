using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MovieList.Core.MovieList;
using MovieList.Core.MovieList.ListMovieList.Handlers.Queries;
using MovieList.Core.MovieList.ListWinnerMovieList.Queries;
using System.Threading.Tasks;

namespace MovieListAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class MovieListController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetProducerWinners()
        {
            var result = await Mediator.Send(new MovieListCommand());

            return Ok(result);
        }

        [HttpGet]     
        [Route("list")]
        public async Task<IActionResult> GetListMovieList()
        {
            // Por ser um banco em memória não há necessidade de uma paginação. Porém pode ser feito para melhor visualizar os dados retornados.
            var result = await Mediator.Send(new ListMovieCommand());

            return Ok(result);
        }

        [HttpGet]
        [Route("winners")]
        public async Task<IActionResult> GetWinnerListMovieList()
        {
            var result = await Mediator.Send(new ListWinnerMovieListCommand());

            return Ok(result);
        }
    }
}
