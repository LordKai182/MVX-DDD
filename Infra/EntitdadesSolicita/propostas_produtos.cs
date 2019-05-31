using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntitdadesSolicita
{
    [Table("propostas_produtos", Schema = "public")]
    public class propostas_produtos
    {
        //public propostas_produtos()
        //{
        //    this._propostas_enderecos = new List<propostas_enderecos>();
        //}

        [ForeignKey("_propostas_enderecos"), Column("idpropprod")]
        public Int64? idpropprod { get; set; }
        public virtual propostas_enderecos _propostas_enderecos { get; set; }
        public Int64? codprop { get; set; }
        public virtual propostas _propostas { get; set; }
        [ForeignKey("_p_servicos"), Column("produto")]
        public int produto { get; set; }
        public virtual p_servicos _p_servicos  { get; set; }
        public int? minutos { get; set; }
        public Int16? ddr { get; set; }
        public Int16? canais { get; set; }
        public Int16? ramais { get; set; }
        public Int16? tipo_pro { get; set; } //1= Novo Serviço, 4 = Renovação, 5 = Mudança, 2 Upgrade Receita, 3 Donwgrade Receita
        public Single? download { get; set; }
        public Single? upload { get; set; }
        public decimal? mensalidade { get; set; }
        public decimal? instalacao { get; set; }
        public Int16? prazo { get; set; }
        public Single? velocidade { get; set; }
        public Int16? status { get; set; }
        public string temperatura { get; set; }
        public string codcli { get; set; }
        public string cliente { get; set; }
        public string responsavel { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public DateTime? data_hora { get; set; }
        public Int16? filial { get; set; }
        public Int16? pontas { get; set; }
        public DateTime? dtfecha { get; set; }
        public DateTime? dtconv { get; set; }
        public Int16? equipe { get; set; }
        public string servico_atual { get; set; }
        public decimal? mensalidade_atual { get; set; }
        public Int16? quantidade { get; set; }
        public string serv_mud { get; set; }
        public Int16? devolvida { get; set; }
        public Int16? mundidata { get; set; }
        public Int16? tipo_oper { get; set; } // 1 Upgrade Produto, 2 = Downgrade Produto, 3= Troce de Produto
        public string cnpj_cpf { get; set; }
        public decimal? mensalidade_si { get; set; }
        public decimal? instalacao_si { get; set; }
        public Int16? classe_mud { get; set; }
        public Int16? vip { get; set; }
        public Int16? classe_atual { get; set; }
        public Single? valor_atual { get; set; }
        public string irf8 { get; set; }
        public Int16? lead { get; set; }
        public Int16? altrec { get; set; }
    }
}
