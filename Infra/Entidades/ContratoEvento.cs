using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ContratoEvento", Schema = "dbo")]
    public class ContratoEvento
    {

        public ContratoEvento()
        {
            this.DataEvento = DateTime.Now;
        }
        public int ContratoEventoId { get; set; }
        public int EventoId { get; set; }
        public int UsuarioId { get; set; }
        public int ContratoId { get; set; }
        public string Valor { get; set; }
        public bool Aceite { get; set; }
        public DateTime DataEvento { get; set; }
        public DateTime DataAceite { get; set; }



        public virtual Contrato _Contrato { get; set; }
        public virtual Usuario _Usuario { get; set; }

    }
}
