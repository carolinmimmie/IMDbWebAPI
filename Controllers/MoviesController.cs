
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

    // GET https://localhost:8000/movies
    [HttpGet]
    public IEnumerable<Movie> GetMovies()
    {
        var movies = context.Movie.ToList();

        return movies;
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
        return Created("", movie); 
    }
}