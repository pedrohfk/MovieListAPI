using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MovieList.Core.Init;
using System.Threading.Tasks;

namespace MovieListAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class InitController : BaseController
    {
        public async Task<IActionResult> InitMemoryDatabase()
        {
            await Mediator.Send(new Init());

            return Ok();
        }
    }
}
