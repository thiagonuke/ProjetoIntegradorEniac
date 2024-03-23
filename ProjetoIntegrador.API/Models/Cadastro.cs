using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Desafio_Processo.API.Models
{
    public class Cadastros
    {
        [Key]
        public string? Email { get; set; }    
        public string? Nome { get; set; }    
        public string? senha { get; set; }    
        public string? Grupo { get; set; }    
    }
}
