using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Interface
{
    public interface ISoService
    {
        int CriarContratoPorSO(int  NumeroSO);
        List<EventosDeContrato> BuscaEventoSo(int NumeroSO, int ContratoId);
        void SalvarEventos(List<EventosDeContrato> ListaEventos);
    }
}
