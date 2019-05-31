using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entidades
{

    [Table("FuncionarioCargo", Schema = "dbo")]

    public class FuncionarioCargo
    {
        //public FuncionarioCargo()
        //{
        //    this._Usuario = new List<Usuario>();
        //}
        public int FuncionarioCargoId { get; set; }
        public string FuncionarioCargoNome { get; set; }

        #region FKs
        //public virtual ICollection<Usuario> _Usuario { get; set; }
        #endregion
    }
}
