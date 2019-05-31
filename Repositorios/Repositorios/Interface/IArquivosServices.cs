using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Repositorios.Interface
{
    public interface IArquivosServices
    {
        byte[] Download(FormCollection formulario);

        void Imprimir(FormCollection formulario);

        List<CenarioArquivo> RetornaListaArquivos();

        void Descricao(FormCollection formulario);
    }
}
