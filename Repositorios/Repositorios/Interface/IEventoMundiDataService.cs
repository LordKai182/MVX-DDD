using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Interface
{
    public interface IEventoMundiDataService
    {
        bool AlterarRazaoSocial(List<string> Alias, string RazaoSocial);
        bool AlterarAlias(List<string> Alias, string NovoAlias);
        bool AlterarEndereco(List<string> Alias);
        bool AlterarIE(List<string> Alias, string IE);
        bool AlterarIM(List<string> Alias, string IM);
        bool AlterarCodigoFiscal(List<string> Alias);
        bool AlterarTipoClienteFaturamento(List<string> Alias);

        List<string> RetornaAlias(int ClienteId);
    }
}
