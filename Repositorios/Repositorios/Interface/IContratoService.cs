using Infra.Entidades;
using Infra.EntidadesNaoPersistidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Repositorios.Interface
{
    public interface IContratoService 
    {
        #region API
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Cliente"></param>
        /// <returns></returns>
        Cliente ApiAlteraCliente(Cliente _Cliente);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ContratoId"></param>
        /// <param name="Propriedade"></param>
        /// <param name="Valor"></param>
        void ApiAlterarDetalhesDoProduto(int _ContratoId, string Propriedade, string Valor);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProdutoId"></param>
        /// <param name="UsuarioId"></param>
        /// <param name="Valor"></param>
        /// <param name="ClienteId"></param>
        /// <param name="NumeroSo"></param>
        /// <returns></returns>
        int ApiContratoNovo(int ProdutoId, int UsuarioId, decimal Valor, int ClienteId, string NumeroSo);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ClienteFormulario"></param>
        /// <param name="ContratoId"></param>
        void ApiInsereEndereco(ClienteEndereco _ClienteFormulario, int ContratoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Contato"></param>
        /// <returns></returns>
        ClienteContato ApiInsereContato(ClienteContato _Contato);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_MapaFaturamento"></param>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        MapaFaturamento SalvaMapaDeFaturamento(MapaFaturamento _MapaFaturamento, int ContratoId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ApiRetornoCliente> ApiClientes();
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Contrato> ListaContratos();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Cliente> ListaClientes();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClienteId"></param>
        /// <returns></returns>
        List<Contrato> ListaContratosPorCliente(int ClienteId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        List<ClienteEndereco> ListaEnderecosPorContrato(int ContratoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        void DeleteContrato(int ContratoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        List<PropriedadeProduto> PropriedadeContratoDigitado(int ContratoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        List<PropriedadeProduto> PropriedadeContratoVazio(int ContratoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        List<ContratoNrc> listaContratoVinculados(int ContratoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        void DeletaContratoVinculado(int ContratoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        void SalvaMapaDeFaturamento(int ContratoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProdutoId"></param>
        /// <param name="UsuarioId"></param>
        /// <param name="Valor"></param>
        /// <param name="ClienteId"></param>
        /// <returns></returns>
        int IniciaContrato(int ProdutoId, int UsuarioId, decimal Valor, int ClienteId, string NumeroSo);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <param name="UsuarioId"></param>
        /// <param name="Valor"></param>
        /// <param name="ClienteId"></param>
        /// <returns></returns>
        int IniciaContratoVinculado(int ContratoId, int UsuarioId, decimal Valor, int ClienteId, string NumeroSo);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="COntratoNrcId"></param>
        void SalvaMapaDefaturamentoVinculado(int ContratoNrcId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        /// <returns></returns>
        int InicaContratoNovo(FormCollection formulario);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        void AlterarDetalhesDoProduto(FormCollection formulario);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Contrato"></param>
        /// <returns></returns>
        Contrato RetornaContrato(int Contrato);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClienteId"></param>
        /// <returns></returns>
        List<Contrato> RetornaContratosPorCliente(int ClienteId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <param name="TipoLogradouro"></param>
        /// <returns></returns>
        ClienteEndereco RetornaEnderecoPorContrato(int ContratoId, int TipoLogradouro);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        void InsereContato(FormCollection formulario);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContatoId"></param>
        void DeletaContato(int ContatoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        void SalvaMapaDeFaturamento(FormCollection formulario);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        Cliente RetornaClienteDoContrato(int ContratoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        Cliente AlteraCliente(FormCollection formulario);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        /// <param name="ContratoId"></param>
        void InsereClienteEndereco(FormCollection formulario, int ContratoId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        void IniciaContratoVinculado(FormCollection formulario);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formulario"></param>
        void IniciaContato(FormCollection formulario);
    }
}
