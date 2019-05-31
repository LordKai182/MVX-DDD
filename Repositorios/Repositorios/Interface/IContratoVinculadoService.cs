using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Repositorios.Interface
{
    public interface IContratoVinculadoService
    {
        int IniciarContratoVinculado(string NumeroSO, string Valor, int ProdutoId, int ContratoId);

        int IniciarContratoVinculado(FormCollection formulario);

        ContratoNrc RetornaContratoVinculado(int ContratoNrcId);

         void AlterarDetalhesDoProduto(FormCollection formulario);

        void SalvaMapaDeFaturamento(FormCollection formulario);

        void DeleteContratoVinculado(int ContratoNrcId);
    }
}
