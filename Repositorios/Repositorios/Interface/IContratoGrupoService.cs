using Infra.Entidades;
using Infra.EntidadesNaoPersistidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Infra.EntidadesNaoPersistidas.CenarioDetalhes;

namespace Repositorios.Interface
{
    public interface IContratoGrupoService
    {

        #region APi

        SumarioDados ApiSumarioGrupos();
      
        #endregion

        List<ContratoGrupo> ListaDeGrupos();

        List<Contrato> ListaDeContratosPorGrupos(int GrupoId);

        List<CenarioDetalhes.Detalhes> ListaFaturamento();

        List<CenarioDetalhes.Detalhes> ListafaturamentoPorGrupo(int GrupoId);

        List<CenarioDetalhes.SumarioDados> SumarioGrupos();

        bool RetirarContratoDoGrupo(int GrupoId, int ContratoId);

        bool AdicionarContratoAoGrupo(int GrupoId, int ContratoId);

        bool DesfazerGrupo(int GrupoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Contratos"></param>
        /// <param name="Imposto"></param>
        /// <param name="Observacao"></param>
        /// <param name="Titulo"></param>
        /// <param name="RazaoNota"></param>
        void SalvarGrupo(string[] Contratos, string[] Imposto, string[] Observacao, string Titulo, string RazaoNota);


        List<GrupoImposto> RetornaImpostos(List<Contrato> ListaCOntratos);
       

    }
}
