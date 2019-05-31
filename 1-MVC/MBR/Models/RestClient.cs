using Infra.EntidadesNaoPersistidas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MBR.Models
{
    public class RestClient
    {
        public object URI { get; private set; }

        public async Task<string> RestRegistraUsuario(AcessoUsuario _Usuario)
        {
            URI = new UriBuilder("");
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("http://192.168.125.238:8087/api/Cenario/SumarioVoz"))
                {
                    response.Headers.Add("Authorization", _Usuario.UsuarioToken);

                    if (response.IsSuccessStatusCode)
                    {
                        var retorno = await response.Content.ReadAsStringAsync();
                        return retorno;
                    }
                    else
                    {
                        return "";
                    }
                }
            }

        }
    }
}