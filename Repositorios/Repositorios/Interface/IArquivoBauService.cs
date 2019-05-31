using Infra.Entidades;
using Infra.EntidadesNaoPersistidas;
using Repositorios.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Repositorios.Interface
{
    public interface IArquivoBauService
    {

        List<Bau> RetornaBau();

        bool CriaNfse(FormCollection formulario);

        byte[] CriaRemessa(FormCollection formulario);

        void ConsultaNF(FormCollection formulario);

        List<ArquivosLinhaCobranca> RetornoArquivos();
    }
}
