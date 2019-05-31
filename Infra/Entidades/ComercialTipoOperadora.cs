using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidades
{
    [Table("ComercialTipoOperadora", Schema = "dbo")]
    public class ComercialTipoOperadora
    {
        public int id { get; set; }
        public int cod_operadora { get; set; }
        public string dsc_operadora { get; set; }
        public int cod_empresa { get; set; }
        public int ind_ativo { get; set; }
        public string cnpj { get; set; }
        public string uf { get; set; }
    }
}
