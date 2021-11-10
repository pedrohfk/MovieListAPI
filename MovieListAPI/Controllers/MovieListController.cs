using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MovieList.Core.MovieList;
using MovieList.Core.MovieList.ListMovieList.Commands.CreateMovie;
using MovieList.Core.MovieList.ListMovieList.Commands.DeleteMovie;
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

        [HttpDelete("{idMovie:int}")]       
        public async Task<IActionResult> Delete([FromRoute] int idMovie)
        {
           
            DeleteCommand delete = new DeleteCommand
            {
                id = idMovie
            };

            await Mediator.Send(delete);

            return Ok();
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateCommand createCommand)
        {
            var result = await Mediator.Send(createCommand);

            return Created("", result);
        }
    }
}
