using Infra.Entidades;
using System.Collections.Generic;
using System.Web.Mvc;
using static Infra.EntidadesNaoPersistidas.CenarioDetalhes;

namespace Repositorios.Interface
{
    public interface ICenarioService
    {


        #region API

        SumarioDados ApiSumarioDados();

        SumarioDados ApiSumarioVoz();

        string Faturar(string _Cenario, string[] _Contratos);


        #endregion

        /// <summary>
        /// /
        /// </summary>
        /// <param name="Contratos"></param>
        /// <param name="Competencias"></param>
        /// <param name="Observacoes"></param>
        /// <returns></returns>
        string FaturamentoEspecial(string[] _Contratos, string[] Competencias, string Observacao,  string AnoCompetencia);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        void AplicarMensalidade(FormCollection formulario);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        List<ContratoNrc> RetornaContratosRc(int ContratoId);
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        void AplicarDesconto(FormCollection formulario);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Contrato> ListaContratosDados();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ContratoNrc> ListaContratosVinculados();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<SumarioDados> SumarioDados();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<SumarioDados> SumarioVinculado();
        /// <summary>
        /// /
        /// </summary>
        /// <param name="TipoCenario"></param>
        /// <returns></returns>
        List<Detalhes> Detalhes(string TipoCenario);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoCenario"></param>
        /// <returns></returns>
        List<Detalhes> DetalhesVinculado(string TipoCenario);
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        /// <returns></returns>
        string ProcessarEvento(FormCollection formulario);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoCenario"></param>
        /// <returns></returns>
        List<Contrato> ContratoS(string TipoCenario);
       
    }
}
