
using IMDbWebAPI.Data;
using IMDbWebAPI.Domain;
using Microsoft.AspNetCore.Mvc;

namespace IMDbWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    private readonly ApplicationDbContext context;

    public MoviesController(ApplicationDbContext context)
    {
        this.context = context;
    }

    // GET https://localhost:8000/movies                 hämta alla fordon
    // GET https://localhost:8000/Movies?Title=Clueless  hämta alla fordon av specifikt märke
    [HttpGet]
    public IEnumerable<Movie> GetMovies([FromQuery] string? title)//string? säger att den kan va null eller title
    {
        IEnumerable<Movie> movies;

        if (string.IsNullOrEmpty(title))
        {
            movies = context.Movie.ToList();
        }
        else
        {
            movies = context.Movie.Where(x => x.Title == title);
        }

        return movies;
    }

   // GET https://localhost:8000/movies/{id}              hämta en film beronde på dess id
    [HttpGet("{id}")]
    public ActionResult<Movie> GetMovie(int id)
    {
        var movie = context.Movie.FirstOrDefault(x => x.Id == id);

        if (movie is null)
            return NotFound(); // 404 Not Found

        return movie;
    }


    // POST https://localhost:8000/movies
    // { "title": "", ... }
    [HttpPost]
    public ActionResult<Movie> CreateMovie(Movie movie)
    {
        // { "title": "Aliens", ... } -> Movie - model binding kallas detta för

        context.Movie.Add(movie);

        // Här skickas ett SQL INSERT-kommando till databashanteraren, som kommer skapa
        // upp en rad i tabellen "Movie", samt så kommer även Id i movie att sättas.
        context.SaveChanges();

        // Returnerar "201 Created", samt skickar även med en representation av filmen 
        // som skapades upp, som skicks som JSON till klienten
        // // { "id": 1, "title": "Aliens", ... }
         return CreatedAtAction( // 201 Created // ge oss tillbaka extra info om vårat objekt 
            nameof(GetMovie),
            new { id = movie.Id },
            movie);
        //return Created("", movie); // om man inte vill skicka med någon locationheader - lämna den tom.. 
    }
        // DELETE http://localhost:8000/movies/1
    [HttpDelete("{id}")] // kallas attribut
    public IActionResult DeleteMovie(int id)
    {
        var movie = context.Movie.FirstOrDefault(x => x.Id == id);//Plocka ut fordon baserat på id

        if (movie is null)// om fordonet inte finns
        {
            return NotFound(); // returnera 404 Not Found
        }

        context.Movie.Remove(movie);// om fordoner finns så vill vi radera

        // Detta skickar en SQL DELETE till databashanteraren
        // Exempelvis "DELETE FROM Vehicle WHERE Id = 1"
        context.SaveChanges();

        return NoContent(); // returnera204 No Content - 
    }
}
