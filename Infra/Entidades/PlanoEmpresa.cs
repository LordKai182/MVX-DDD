using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("PlanoEmpresa", Schema = "dbo")]

    public class PlanoEmpresa
    {
    
        public PlanoEmpresa()
        {
            //this._Empresa = new List<Empresa>();

            this._MapaFaturamentoComplemento = new List<MapaFaturamentoComplemento>();
        }
        public int PlanoEmpresaId { get; set; }
        [ForeignKey("_Empresa"), Column("EmpresaId")]
        public int EmpresaId { get; set; }
        public int PlanoId { get; set; }
        public string Imposto { get; set; }
        public string Codigo { get; set; }
        public double Receita { get; set; }

        #region FKs
        public virtual Plano _Plano { get; set; }
        public virtual Empresa _Empresa { get; set; }
        //public virtual ICollection<Empresa> _Empresa { get  ; set; }
        public virtual ICollection<MapaFaturamentoComplemento> _MapaFaturamentoComplemento { get; set; }
        #endregion
    }
}
