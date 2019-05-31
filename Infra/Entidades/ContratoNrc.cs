using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ContratoVinculado", Schema = "dbo")]
    public class ContratoNrc
    {

        public ContratoNrc()
        {
            this._Relacioamento = new List<ContratoRelac>();
            this._MapaFaturamento = new List<MapaFaturamento>();
            this.Ativo = true;
        }

        public int ContratoNrcId { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public DateTime DataInstalacao { get; set; }
        public int ProdutoId { get; set; }
        public decimal Valor { get; set; }
        public bool Faturado { get; set; }
        public int? ContratoId { get; set; }
        //public int MapaFaturamtoId { get; set; }
        public bool PrePago { get; set; }
        public string NumeroSO { get; set; }
        public int UsuarioId { get; set; }
        public string PropriedadesContrato { get; set; }
        //public int Planoid { get; set; }
        public int Status { get; set; }
        public int MesValidade { get; set; }
        public DateTime DataContratacao { get; set; }
        public DateTime DataAssinatura { get; set; }
        public bool Exportado { get; set; }
        public virtual Contrato _Contrato { get; set; }
        public virtual Produto _Produto { get; set; }
        public virtual ICollection<MapaFaturamento> _MapaFaturamento { get; set; }


        public virtual ICollection<ContratoRelac> _Relacioamento { get; set; }

        //public virtual IEnumerable<ContratoRelac> _Relacioamento
        //{
        //    get { return GetMyProperty(this.ContratoNrcId); }
        //    set { ; }
        //}

         
      
        public static List<ContratoRelac> GetMyProperty(int ContratoNrcId)
        {
            return null;
        }

        public List<PropriedadeProduto> _ListaPropriedade(int ContratoNrcId)
        {
            var db = new Class1(true);
            try
            {
                List<PropriedadeProduto> ListaSemPromo = new List<PropriedadeProduto>();


                var resposta = db.ContratoNrc.First(x => x.ContratoNrcId == ContratoNrcId);
                ListaSemPromo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PropriedadeProduto>>(resposta.PropriedadesContrato);

                return ListaSemPromo;
            }
            catch
            {

                return null;
            }

        }


    }
}
