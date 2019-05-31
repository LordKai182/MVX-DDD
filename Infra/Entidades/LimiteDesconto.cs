using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("LimiteDesconto", Schema = "dbo")]

    public class LimiteDesconto
    {
        public int? UsuarioId { set; get; }
        public int LimiteDescontoId { get; set; }
        public int? SetorId{ get; set; }
        public bool SomenteJuros{ get; set; }
        public double Limite { get; set; }
        public decimal Valor { get; set; }        
        public int? ProdutoId { get; set; }
        public int? ClienteId { get; set; }
        public DateTime DataCadastro { set; get; }            

        //Foreing Keys                
        public virtual Usuario Usuario { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
