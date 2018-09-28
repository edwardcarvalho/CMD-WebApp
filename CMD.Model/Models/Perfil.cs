using System.ComponentModel.DataAnnotations;

namespace CMD.Model.Models
{
    public class Perfil
    {
        [Key]
        public long PerfilId { get; set; }
        public string Descricao { get; set; }
    }
}