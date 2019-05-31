using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesNaoPersistidas
{
    public class BoletoImpressao
    {
        public string CabecalhoTelefone { get; set; }
        public string CabecalhoSacado { get; set; }
        public string cabecalhoEndereco { get; set; }
        ///SEQUENCIA E NUMERO BANCO
        public string Banco { get; set; }
        public string NumeroBoleto { get; set; }
        ///CORPO
        public string CorpoCedente { get; set; }
        public string CorpoDataDocumento { get; set; }
        public string CorpoNumeroDoc { get; set; }
        public string CorpoEspecieDoc { get; set; }
        public string CorpoAceite { get; set; }
        public string CorpoDataDoProcessamento { get; set; }
        public string CorpoCarteira { get; set; }
        public string CorpoEspecie { get; set; }
        public string CorpoQuantidade { get; set; }
        public string CorpoValor { get; set; }
        public string CorpoVencimento { get; set; }
        public string CorpoAgenciaCodigoCedente { get; set; }
        public string CorpoNossoNumero { get; set; }
        public string CorpoValorDoDocumento { get; set; }
        public string CorpoDescontoAbatimento { get; set; }
        public string CorpoOutrasDeducoes { get; set; }
        public string CorpoMoraMulta { get; set; }
        public string CorpoOutrosAcrecimos { get; set; }
        public string CorpoValorCobrado { get; set; }
        public string CorpoSacado { get; set; }
    }
}
