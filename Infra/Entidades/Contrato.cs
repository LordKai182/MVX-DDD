using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Infra.Entidades
{
    [Table("Contrato", Schema = "dbo")]
    public class Contrato
    {

    
        public Contrato()
        {
            this._EventosDeContrato = new List<EventosDeContrato>();
            this._ContratoNrc = new List<ContratoNrc>();
            this._InstalacaoEndereco = new List<ClienteEndereco>();
            this._ClienteContato = new List<ClienteContato>();
            this._ContratoHistorico = new List<ContratoHistorico>();
            this._MapaFaturamento = new List<MapaFaturamento>();
            this._ContratoRelacionamento = new List<ContratoRelac>();
            this._MapaFaturamentoComplemento = new List<MapaFaturamentoComplemento>();
            this.Ativo = true;
        }

        public int ContratoId { get; set; }
        public DateTime DataCriacao { get;  set; }
        public DateTime DataAlteracao { get; set; }
        public DateTime DataDaContratacao { get; set; }
        public DateTime DataDaAssinatura { get; set; }
        public DateTime DataInstalacao { get; set; }
        public DateTime DataExportado { get; set; }
        public string MultaContratual { get; set; }
        public string Consultor { get; set; }
        public string CodigoVelho { get; set; }
        public int? FaturamentoregraId { get; set; }
        public string NumeroSO { get; set; }
        public int UsuarioId { get;  set; }
        public int ClienteId { get; set; }
        public int ProdutoId { get;  set; }
        bool Parceiro { get; set; }
        public int ProdutoDetalheId { get;  set; }
        public bool Promocional { get; set; }
        public decimal Valor { get; set; }
        public string TipoCenario { get; set; }
        public string PropriedadesContrato { get; set; }
        public bool Associado { get; set; }
        public string ContratoPai { get; set; }
        public int? PlanoId { get; set; }
        public int ContratoStatusId { get; set; }
        public string Codigo { get; set; }
        public bool Exportado { get; set; }
        public bool Ativo { get; set; }
        public int MesValidade { get; set; }
        public bool RenovacaoAutomatica { get; set; }
        public int? ContratoGrupoId { get; set; }
        public bool FaturaSeparado { get; set; }
        public string Alias { get; set; }
        public bool ParceiroMvx { get; set; }
       // public string ProdutoAbreviacao { get; set; }
       // public bool Lido { get; set; }
        public int? CodigoVendedor { get; set; }
        

        public Contrato(DateTime dataCriacao, int usuarioId, int produtoId, int clienteId)
        {
            this.DataCriacao = dataCriacao;
            this.UsuarioId = usuarioId;
            this.ProdutoId = produtoId;
            this.ClienteId = clienteId;
            this.Ativo = true;
        }
        
      

        #region RELACIONAMENTOS
        public virtual ICollection<ContratoNrc> _ContratoNrc { get; set; }

           public virtual Produto _Produto { get; set; }
           public virtual Cliente _Cliente { get; set; }
           public virtual Usuario _Usuario { get; set; }
           public virtual ICollection<ClienteEndereco> _InstalacaoEndereco { get; set; }
           //public virtual ICollection<MapeamentoDeContrato> _MapeamentoDeContrato { get; set; }
           public virtual ICollection<ClienteContato> _ClienteContato { get; set; }
           public virtual ICollection<ContratoHistorico> _ContratoHistorico { get; set; }
           //public virtual ICollection<ContratoEvento> _ContratoEvento { get; set; }
           //public virtual ICollection<Contrato> _ContratoAssociacao { get; set; }
           public virtual ICollection<MapaFaturamento> _MapaFaturamento { get; set; }
        //public virtual ICollection<ContratoRelac> _ContratoRelacionamento { get; set; }
        public virtual ICollection<ContratoRelac> _ContratoRelacionamento { get; set; }
           //public virtual Plano _Plano { get; set; }
           public virtual ContratoStatus _ContratoStatus { get; set; }
           public virtual ContratoGrupo _ContratoGrupo { get; set; }


        public virtual ICollection<MapaFaturamentoComplemento> _MapaFaturamentoComplemento { get; set; }
        public virtual ICollection<EventosDeContrato> _EventosDeContrato { get; set; }


        #endregion

    }



}
