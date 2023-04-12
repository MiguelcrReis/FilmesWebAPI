using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    #region Injeção de Depêndencia
    private readonly FilmeContext _context;
    private readonly IMapper _mapper;
    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    #endregion

    #region Adiciona Filme
    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);

        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ConsultaFilmePorId), new { id = filme.Id }, filme);
    }
    #endregion

    #region Consulta Filmes
    [HttpGet]
    public IEnumerable<ReadFilmeDto> ConsultaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
    }
    #endregion

    #region Consulta Filme Por Id
    [HttpGet("{id}")]
    public IActionResult ConsultaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
        if (filme == null) return NotFound();

        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);

        return Ok(filmeDto);
    }
    #endregion

    #region Atualiza Filme
    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);

        if (filme == null) return NotFound();

        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();

        return NoContent();
    }
    #endregion

    #region Atualiza Filme Parcial
    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);

        if (filme == null) return NotFound();

        var filmeAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeAtualizar, ModelState);

        if (!TryValidateModel(filmeAtualizar)) return ValidationProblem(ModelState);

        _mapper.Map(filmeAtualizar, filme);
        _context.SaveChanges();

        return NoContent();
    }
    #endregion

    #region Deleta Filme
    [HttpDelete("{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);

        if (filme == null) return NotFound();

        _context.Filmes.Remove(filme);
        _context.SaveChanges();

        return NoContent();
    }
    #endregion
}
