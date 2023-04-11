using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private static List<Filme> filmes = new List<Filme>();
    private static int id = 0;

    [HttpPost]
    public void AdicionaFilme([FromBody] Filme filme)
    {
        filme.Id = id++;
        filmes.Add(filme);
        Console.WriteLine($"{filme.Titulo}\n{filme.Genero}\n{filme.Diretor}\n{filme.Duracao}");
    }

    [HttpGet]
    public IEnumerable<Filme> ConsultarFilmes()
    {
        return filmes;
    }

    [HttpGet("{id}")]
    public Filme? ConsultaFilmePorId(int id)
    {
        return filmes.FirstOrDefault(f => f.Id == id);
    }
}
