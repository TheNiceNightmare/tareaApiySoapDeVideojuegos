using Microsoft.AspNetCore.Http;
using tareaApiySoap.Models;
using Microsoft.AspNetCore.Mvc;

namespace tareaApiySoap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGamesController : ControllerBase
    {
        private static List<VideoGames> _videogames = new List<VideoGames> {

            new VideoGames
            {
                Id = 1,
                GameTitle = "Outer Wilds",
                GameGenre = "Action-adventure"
            },
            new VideoGames
            {
                Id = 2,
                GameTitle = "The Last of Us",
                GameGenre = "Action-adventure; Survival horror"
            },
            new VideoGames
            {
               Id = 3,
               GameTitle = "Fallout 4",
               GameGenre = "Action role-playing"
            },

            new VideoGames
            {
               Id = 4,
               GameTitle = "BioShock Infinite",
               GameGenre = "First-person shooter"
            },

            new VideoGames
            {
                Id = 5,
                GameTitle = "It Takes Two",
                GameGenre = "Action-adventure, platform"
            }

        };

        [HttpGet]
        public IEnumerable<VideoGames> GetAllGames()
        {
            return _videogames;
        }

        [HttpGet("{id}")]
        public IActionResult GetGamesById(int id)
        {
            var game = _videogames.Find(game => game.Id == id);

            if (game == null)
            {
                return NotFound($"No se encontró ningún videojuego con el ID {id}."); // Devuelve un código 404 con un mensaje
            }

            return Ok(game);
        }

        [HttpPost()]
        public IActionResult SaveGame([FromBody] VideoGames newGame)
        {
            // Verificar si ya existe un juego con el mismo ID
            if (_videogames.Any(game => game.Id == newGame.Id))
            {
                return Conflict($"Ya existe un videojuego con el ID {newGame.Id}."); // Devuelve un código 409 (Conflict) con un mensaje
            }

            // Si no hay conflicto, agrega el nuevo juego
            _videogames.Add(newGame);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGame(int id, [FromBody] VideoGames updatedGame, string newTitle = null)
        {
            var game = _videogames.Find(game => game.Id == id);

            if (game == null)
            {
                return NotFound(); // Devolver un código 404 si no se encuentra la canción
            }

            if (!string.IsNullOrEmpty(newTitle))
            {
                game.GameTitle = newTitle;
            }
            else if (updatedGame != null)
            {
                // Actualizar con la información proporcionada en el cuerpo de la solicitud
                game.GameTitle = updatedGame.GameTitle;
                game.GameGenre = updatedGame.GameGenre;
                // Puedes agregar más propiedades según sea necesario
            }
            else
            {
                return BadRequest("Se requiere al menos un título nuevo o un videojuego actualizado en el cuerpo de la solicitud.");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGame(int id)
        {
            var gameDelete = _videogames.Find(game => game.Id == id);

            if (gameDelete == null)
            {
                return NotFound();
            }

            _videogames.Remove(gameDelete);

            return Ok("Se ha eliminado el videojuego");
        }
    }
}
