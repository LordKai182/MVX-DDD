using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesNaoPersistidas
{
    public class NfseImpressao
    {
        public string Cabecalho { get; set; }
        public string NumeroNota { get; set; }
        public string DataHoraEmissao { get; set; }
        public string CodigoVerificacao { get; set; }
        public string PrestadoServico { get; set; }
        public string TomadorServico { get; set; }
        public string DescriminacaoServico { get; set; }
        public string ValorDaNota { get; set; }
        public string ServicoPrestado { get; set; }
        public string ValorDeducao { get; set; }
        public string valorDescontoIncons { get; set; }
        public string ValorBaseCalculo { get; set; }
        public string ValorAliquota { get; set; }
        public string ValorISS { get; set; }
        public string ValorCreditoIPTU { get; set; }
        public string OutrasInformacoes { get; set; }
    }
}
