using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Filme
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O {0} do filme é obrigatório!")]
    [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve ter entre {2} e {1} caractres")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O {0} do filme é obrigatório!")]
    [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve ter entre {2} e {1} caracteres")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "O {0} do filme é obrigatório!")]
    public string Diretor { get; set; }

    [Required(ErrorMessage = "O {0} do filme é obrigatório!")]
    [Range(70, 600, ErrorMessage = "A {0} do filme deve ter entre {1} e {2} minutos")]
    public int Duracao { get; set; }
}
