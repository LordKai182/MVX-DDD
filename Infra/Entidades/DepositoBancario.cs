using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("DepositoBancario", Schema = "dbo")]

    public class DepositoBancario
    {
        public int DepositoBancarioId { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public string Conta { get; set; }
        public string Agencia { get; set; }
        public string CpfCnpjSacado { get; set; }
        public string NumeroSO { get; set; }
        public string ProdutoNome{ get; set; }
        public int? ProdutoId { get; set; }
        public int? ClienteId { get; set; }
        public DateTime DataCadastro { set; get; }
        public int? UsuarioId { set; get; }

        //Foreing Keys
        public virtual Usuario Usuario { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
