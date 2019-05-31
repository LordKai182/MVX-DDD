using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{
    [Table("Cliente", Schema = "dbo")]
    public  class Cliente 
    {
        /// <summary>
        /// 
        /// </summary>
        public Cliente()
        {
            this._ClienteEndereco = new List<ClienteEndereco>();
            this._Contrato = new List<Contrato>();
            this._ClienteContato = new List<ClienteContato>();
            //this._ClienteRelacionamento = new List<Cliente>();
        }
        public int ClienteId { get; set; }
        public string RazaoSocial { get; set; }
        public string RazaoNota { get; set; }
        public string NomeFantasia { get; set; }
        public string CpfCnpj { get; set; }
        public string TipoPessoa { get; set; }
        public string InscEstadual { get; set; }
        public string InscMunicipal { get; set; }
        public string Cnae { get; set; }
        public int StatusClienteId { get; set; }
        bool Parceiro { get; set; }
        public bool Confaz { get; set; }
        public int TipoVipId { get; set; }
        //public string Alias { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime? DataCadastro { get; set; }
        public int? UsuarioCadastro { get; set; }
        public string CodRelacionamento { get; set; }
        public int TipopessoaId { get; set; }
        //public int? RelacionamentoId { get; set; }
        public string TipoRelacao { get; set; }
        public int PrefixoTelefone { get; set; }
        public int? TipoClienteFaturamentoId { get; set; }
        //Foreing Keys
        public virtual StatusCliente _StatusCliente { get; set; }
        public virtual TipoVip _Tipovip { get; set; }
        public virtual ICollection<ClienteEndereco> _ClienteEndereco { get; set; }
        public virtual ICollection<Contrato> _Contrato { get; set; }
        public virtual ICollection<ClienteContato> _ClienteContato { get; set; }
        //public virtual ICollection<Cliente> _ClienteRelacionamento { get; set; }
        public virtual TipoPessoa _TipoPessoa { get; set; }
        public virtual TipoClienteFaturamento _TipoClienteFaturamento { get; set; }


      
    }
}
