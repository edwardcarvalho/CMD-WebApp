using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace CMD.Model.Models
{
    public class StatusMedida
    {
        [Key]
        public long StatusId { get; set; }
        public string Descricao { get; set; }
    }
}