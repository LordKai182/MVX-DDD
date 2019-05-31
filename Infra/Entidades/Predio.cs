using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("Predio", Schema = "dbo")]
    public  class Predio
    {
        //public Predio()
        //{
        //    this._ClienteEndereco = new List<ClienteEndereco>();
        //}

        public int PredioId { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Observacao { get; set; }
        public string Area { get; set; }
        public bool Ativo { get; set; }
        public bool Migrado { get; set; }

        public virtual ICollection<ClienteEndereco> _ClienteEndereco { get; set; }

        public string _UF(int PredioId)
        {
            try
            {
                return null;
            }
            catch
            {

                return null;
            }
        }
    }
}
