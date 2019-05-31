using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesNaoPersistidas
{
    public class ArquivoBau
    {
        public int Codigo { get; set; }
        public string TipoArquivo { get; set; }
        public string Cenario { get; set; }
        public string CnpjCpf { get; set; }
        public string Razao { get; set; }
        public decimal Valor { get; set; }
        public string Usuario { get; set; }
        public string Status { get; set; }
        public int Qtd { get; set; }
        public DateTime DataSolicitada { get; set; }
    }
}
