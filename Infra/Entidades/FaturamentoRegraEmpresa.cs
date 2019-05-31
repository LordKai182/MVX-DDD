using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("FaturamentoRegraEmpresa", Schema = "dbo")]
    public class FaturamentoRegraEmpresa
    {
        public FaturamentoRegraEmpresa()
        {
           // this._FaturamentoRegra = new HashSet<FaturamentoRegra>();
        }
        public int FaturamentoRegraEmpresaId { get; set; }
        public int EmpresaId { get; set; }
        public string Imposto { get; set; }
        public string TipoNota { get; set; }
        public string Codigo { get; set; }
        public double Receita { get; set; }

        #region RELACIOBAMENTOS

        public virtual Empresa _Empresa { get; set; }
       // public virtual ICollection<FaturamentoRegra> _FaturamentoRegra { get; set; }
        
        #endregion
    }
}
