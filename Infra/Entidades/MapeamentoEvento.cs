using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("MapeamentoEvento", Schema = "dbo")]
    public  class MapeamentoEvento
    {
        public MapeamentoEvento()
        {
            this.DataEvento = DateTime.Now;
        }

        public int MapeamentoEventoId { get; set; }
        public string Evento { get; set; }
        public string Tabela { get; set; }
        public string Descricao { get; set; }
        public string Servico { get; set; }
        public string Id { get; set; }
        public string Chave { get; set; }
        public string Codigo { get; set; }
        public string Banco { get; set; }
        //public int? IdEnd { get; set; }
        public DateTime DataEvento { get; set; }
        public DateTime DataFeito { get; set; }
        public bool Feito { get; set; }
        public string Itens { get; set; }

    }
}
