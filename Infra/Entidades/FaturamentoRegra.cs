using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{

    [Table("FaturamentoRegra", Schema = "dbo")]
    public class FaturamentoRegra
    {
        public FaturamentoRegra()
        {
            //this._FaturamentoRegraEmpresa = new HashSet<FaturamentoRegraEmpresa>();
        }
        public int FaturamentoregraId { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public bool Ativo { get; set; }
        public int Boleto { get; set; }
        public int NotaFiscal { get; set; }
        public int Protocolado { get; set; }
        public int Prorrata { get; set; }

        public int FaturamentoRegraEmpresaId { get; set; }

       // public virtual ICollection<FaturamentoRegraEmpresa> _FaturamentoRegraEmpresa { get; set; }

    }
}
