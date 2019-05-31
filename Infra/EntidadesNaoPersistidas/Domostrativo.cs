using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesNaoPersistidas
{
    public class Domostrativo
    {
        public string Cnpj { get; set; }
        public string Titulo { get; set; }
        public string NCircuito { get; set; }
        public string Codigo { get; set; }
        public string NomeCircuito { get; set; }
        public string QtdCircuito { get; set; }
        public string Servico { get; set; }
        public string PontaA { get; set; }
        public string PontaB { get; set; }
        public string DataAtivacao { get; set; }
        public string Disponibilidade { get; set; }
        public string DisponibilidadeHs { get; set; }
        public string ValorLiquido { get; set; }
        public string ValorBruto { get; set; }
        public string TotalBruto { get; set; }
        public string TotalLiquido { get; set; }
        public decimal CTotBruto { get; set; }
        public decimal CTotLiquido { get; set; }

    }
}
