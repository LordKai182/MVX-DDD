using Infra.Entidades;
using Infra.EntidadesMundidata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Repositorios.Interface
{
    public interface IPendenciaService
    {
        List<Contrato> RetornaContratos();

        List<ContratoNrc> RetornaContratosVinculados();

        void EnviarContrato(int ContratoId, int cod_empresa, int cod_principal, int cod_vendedor);

        mvx_empresas CadastraEmpresa(int ClienteId);

        List<mvx_funcionarios> RetornaVendedores();

        void ProcessaPendencia(FormCollection formulario);

        List<EventosDeContrato> RetornaEventos();
    }
}
