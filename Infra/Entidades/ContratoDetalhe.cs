using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ContratoDetalhe", Schema = "dbo")]
    public class ContratoDetalhe
    {

        public int ContratoDetalheId { get; set; }
        public string Tipo { get; set; }
        public string Valor { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool ExigeSO { get; set; }
        public bool GeraUpDW { get; set; }
        public bool Obrigatorio { get; set; }
        public string Mascara { get; set; }

     
    }
}
