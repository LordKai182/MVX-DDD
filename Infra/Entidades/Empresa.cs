using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("Empresa", Schema = "dbo")]
    public class Empresa
    {

        public Empresa()
        {
            this._EmpresaEndereco = new List<EmpresaEndereco>();
            this._Banco = new List<Banco>();
        }
        public int EmpresaId { get; set; }
        public string Cnpj { get; set; }
        public DateTime DataCadastro { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string InscEstadual { get; set; }
        public string InscMunicipal { get; set; }
        public string Cnae { get; set; }
        public string Obs { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int UsuarioCadastro { get; set; }
        public string Codigo { get; set; }
        public int UsuarioId { get; set; }

        public virtual ICollection<EmpresaEndereco> _EmpresaEndereco { get; set; }
        public virtual Usuario _Usuario { get; set; }
        public virtual ICollection<Banco> _Banco { get; set; }
        //public virtual IEnumerable<FaturamentoRegraEmpresa> _FaturamentoRegraEmpresa { get; set; }


    }
}
