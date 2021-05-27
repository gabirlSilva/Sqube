using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace SqlInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Store([FromQuery]string cidade, [FromQuery]string nome)
        {
            var connectionString = "";

            await using var connection = new NpgsqlConnection(connectionString);

            var id = 1;

            var souUmaInjection = "INSERT INTO pessoa VALUES (" + id + ", " + nome + ", " + cidade + ")";

            await using var command =
                new NpgsqlCommand(souUmaInjection, connection);

            await command.ExecuteNonQueryAsync();
            
            return Ok();
        }
    }
}