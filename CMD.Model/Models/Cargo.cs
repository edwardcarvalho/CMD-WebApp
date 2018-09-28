using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMD.Model.Models
{
    public partial class Cargo
    {
        [Key]
        public long CargoId { get; set; }
        public string Descricao { get; set; }
    }
}