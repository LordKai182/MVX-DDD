using System;
using System.Collections.Generic;

namespace RestMvx.Interface
{
    public interface IRestMvxService
    {

        String LoginPortalMundiBr(string userName, string password);

        String GetRequisicao(string Uri, string Authorization);

        String PostRequisicao(string Uri, List<KeyValuePair<string, string>> Parametros, string Authorization);


    }
}
