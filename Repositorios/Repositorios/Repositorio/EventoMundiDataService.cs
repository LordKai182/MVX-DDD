using Infra;
using Infra.Entidades;
using Repositorios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Repositorio
{
    public class EventoMundiDataService : IEventoMundiDataService
    {

        Class1 MundiData ;
        Class1 MundBr = new Class1(true);
        private void AbrebancoMundiData(string cod_empresa)
        {
            if (cod_empresa.Substring(0, 2) == "11")
            {
                MundiData = new Class1(true, "mundidata");
            }
            if (cod_empresa.Substring(0, 2) == "31")
            {
                MundiData = new Class1(true, "mundidata");
            }
            if (cod_empresa.Substring(0, 2) != "31" && cod_empresa.Substring(0, 2) != "11")
            {
                MundiData = new Class1(true, "mundidata");
            }
        }
        private Cliente RetornaCliente(int DadosBabelId)
        {
            var Cliente = MundBr.ContratoRelac.First(x => x.DadosBabelId == DadosBabelId)._Contrato._Cliente;
            return Cliente;
        }


        public bool AlterarAlias(List<string> Alias, string NovoAlias)
        {
            try
            {
                foreach (var cliente_id in Alias)
                {
                    AbrebancoMundiData(cliente_id);
                    int cod_empresa = Convert.ToInt32(cliente_id);
                    var ClienteMundiData = MundiData.mvx_empresas_dados_atuais.First(x => x.cod_empresa == cod_empresa);
                    ClienteMundiData.razao_social = NovoAlias;
                    MundiData.mvx_empresas_dados_atuais.Update(ClienteMundiData);
                    MundiData.SaveChanges();
                }
                
                return true;
            }
            catch(Exception erro)
            {
                return false;
            }
           
        }

        public bool AlterarCodigoFiscal(List<string> Alias)
        {
            throw new NotImplementedException();
        }

        public bool AlterarEndereco(List<string> Alias)
        {
            throw new NotImplementedException();
        }

        public bool AlterarIE(List<string> Alias, string IE)
        {
            int cod_empresa = 0;
            try
            {
                foreach (var cliente_id in Alias)
                {
                    AbrebancoMundiData(cliente_id);
                    cod_empresa = Convert.ToInt32(cliente_id);
                    var ClienteMundiData = MundiData.mvx_empresas_dados_atuais.First(x => x.cod_empresa == cod_empresa);
                    ClienteMundiData.dsc_ins_estadual = IE;
                    MundiData.mvx_empresas_dados_atuais.Update(ClienteMundiData);
                    MundiData.SaveChanges();
                }

                var Cliente = RetornaCliente(cod_empresa);
                Cliente.InscEstadual = IE;
                MundBr.cliente.Update(Cliente);
                MundBr.SaveChanges();

                return true;
            }
            catch (Exception erro)
            {
                return false;
            }
        }

        public bool AlterarIM(List<string> Alias, string IM)
        {
            int cod_empresa = 0;
            try
            {
                foreach (var cliente_id in Alias)
                {
                    AbrebancoMundiData(cliente_id);
                    cod_empresa = Convert.ToInt32(cliente_id);
                    var ClienteMundiData = MundiData.mvx_empresas_dados_atuais.First(x => x.cod_empresa == cod_empresa);
                    ClienteMundiData.dsc_ins_municipal = IM;
                    MundiData.mvx_empresas_dados_atuais.Update(ClienteMundiData);
                    MundiData.SaveChanges();
                }

                var Cliente = RetornaCliente(cod_empresa);
                Cliente.InscMunicipal = IM;
                MundBr.cliente.Update(Cliente);
                MundBr.SaveChanges();

                return true;
            }
            catch (Exception erro)
            {
                return false;
            }
        }

        public bool AlterarRazaoSocial(List<string> Alias, string RazaoSocial)
        {
            int cod_empresa = 0;
            try
            {
                foreach (var cliente_id in Alias)
                {
                    AbrebancoMundiData(cliente_id);
                    cod_empresa = Convert.ToInt32(cliente_id);
                    var ClienteMundiData = MundiData.mvx_empresas_dados_atuais.First(x => x.cod_empresa == cod_empresa);
                    ClienteMundiData.razao_social = RazaoSocial;
                    MundiData.mvx_empresas_dados_atuais.Update(ClienteMundiData);
                    MundiData.SaveChanges();
                }

                var Cliente = RetornaCliente(cod_empresa);
                Cliente.RazaoSocial = RazaoSocial;
                MundBr.cliente.Update(Cliente);
                MundBr.SaveChanges();

                return true;
            }
            catch (Exception erro)
            {
                return false;
            }
        }

        public bool AlterarTipoClienteFaturamento(List<string> Alias)
        {
            throw new NotImplementedException();
        }

        public List<string> RetornaAlias(int ClienteId)
        {
            var resposta = MundBr.ContratoRelac.Where(x => x.MundibrId == ClienteId && x.ContratoId != null).Select(x=>x.DadosBabelId.ToString()).Distinct().ToList();

            return resposta.ToList();
        }
    }
}
