using System.ComponentModel.DataAnnotations;

namespace Desafio_Processo.UI.Models
{
    public class CadastrosViewModel
    {
        [Key]
        public string? Email { get; set; }
        public string? Nome { get; set; }
        public string? senha { get; set; }
        public string? Grupo { get; set; }
    }
}
