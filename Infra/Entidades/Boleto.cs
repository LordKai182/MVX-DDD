using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("Boleto", Schema = "dbo")]
    public class Boleto
    {
        public int LinhaCobrancaId { get; set; }
        public int BoletoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimentoOriginal { get; set; }
        public DateTime DataVencimentoProrrogacao { get; set; }
        public decimal ValorTotal { get; set; }
        public string Cedente { get; set; }
        public string CnpjCedente { get; set; }
        public string Sacado { get; set; }
        public string CnpJCpfSacado { get; set; }
        public int Status { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorBoleto { get; set; }
        public decimal ValorPago { get; set; }
        public string Observacao { get; set; }
        public string  CodigoBanco { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataStatus { get; set; }
        public string NossoNumero { get; set; }
        public string NumeroDocumento { get; set; }
        public int UsuarioStatus { get; set; }
        public decimal ValorMulta { get; set; }
        public int? CenarioId { get; set; }
        public int CodigoRetorno { get; set; }
        public string AutenticacaoCodigo { get; set; }
        public string SacadoNome { get; set; }
        public string EspecieMoeda { get; set; }
        public string localPagamento { get; set; }
        public string Agencia { get; set; }
        public string Aceite { get; set; }
        public string Carteira { get; set; }
        public string EspecieDocumento { get; set; }
        public int BoletoRemessaId { get; set; }
        public byte[] PDF { get; set; }



    }
}
