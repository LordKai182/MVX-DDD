using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("EventosDeContrato", Schema = "dbo")]
    public class EventosDeContrato
    {
        public int EventosDeContratoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int UsuarioId { get; set; }
        public string EventoDescricao { get; set; }
        public bool Exportado { get; set; }
        public int ContratoId { get; set; }
        public string ValorNovo { get; set; }
        public string ValorAntigo { get; set; }
        public int Prazo { get; set; }
        public string Consultor { get; set; }
        public int ConsultorId { get; set; }
        public DateTime DataContratacao { get; set; }
        public string NumeroSO { get; set; }


        public virtual Contrato _Contrato { get; set; }
        public virtual Usuario _Usuario { get; set; }

    }
}
