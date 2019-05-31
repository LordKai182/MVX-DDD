using Newtonsoft.Json;
using RestMvx.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace RestMvx.Repositorio
{
    public class RestMvxService : IRestMvxService
    {
        public class tokeen
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string expires_in { get; set; }
        }
        public String GetRequisicao(string Uri, string Authorization)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var webUrl = "http://192.168.0.102:1230/api/";
            var uri = Uri;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(webUrl);
                client.DefaultRequestHeaders.Add("Authorization", Authorization);
                var response = client.GetAsync(uri).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public String LoginPortalMundiBr(string userName,string password)
        {
            string resultado;
            var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username",userName ),
                        new KeyValuePair<string, string> ( "Password",password )
                    };
            var content = new FormUrlEncodedContent(pairs);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
           
            using (var client = new HttpClient())
            {   
                var response = client.PostAsync("http://192.168.0.102:1230" + "/token", content).Result;
                resultado = response.Content.ReadAsStringAsync().Result;
                var resposta = JsonConvert.DeserializeObject<tokeen>(resultado);
                resultado = resposta.token_type + " " + resposta.access_token;
            }
            return resultado;
        }

        public String PostRequisicao(string Uri, List<KeyValuePair<string, string>> Parametros, string Authorization)
        {
            string resultado;
            var content = new FormUrlEncodedContent(Parametros);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
           
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Authorization);

                var response = client.PostAsync("http://192.168.0.102:1230/api/" +Uri, content).Result;
                var tre = response.Content.ReadAsStringAsync().Result;
                resultado = tre;
            }

            return resultado;
        }
    }
}
