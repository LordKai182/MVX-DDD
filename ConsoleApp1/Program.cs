using RestClient.Classes;
using RestClient.Interface;
using RestClient.Repositorio;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        class Usuario
        {
            public string username { get; set; }
            public string password { get; set; }
            public string grant_type { get; set; }
        }

        static void Main(string[] args)
        {


         string teste =  RestRegistraUsuario().ToString();

            Console.WriteLine("Hello World!");
        }

        public static async Task<string> RestRegistraUsuario()
        {
          
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("http://192.168.125.238:8087/token?username=henrique&password=menadel182&grant_type=password"))
                {
                    //response.Headers.Add("Authorization", _Usuario.UsuarioToken);

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
