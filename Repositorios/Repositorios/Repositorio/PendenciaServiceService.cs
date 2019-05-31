using Infra;
using Infra.Entidades;
using Infra.EntidadesMundidata;
using Microsoft.EntityFrameworkCore;
using Repositorios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Repositorios.Repositorio
{
    public class PendenciaService : IPendenciaService
    {
        Class1 MundiBr;
        Class1 MundiData;

        public PendenciaService(bool Producao)
        {
            MundiBr = new Class1(Producao);
            MundiData = new Class1(true, "mundidata");
        }

        #region METODOS DE RETORNO

        private string RetornaQueryComecialTipoServico(Produto _produto, List<PropriedadeProduto> _propriedades, int PlanoId, string Imposto)
        {
            string Argumentos = string.Empty;
            bool fracionado = false;
            int NumeroDeEmpresa = 0;
            var Empresas = MundiBr.PlanoEmpresa.Where(x => x.PlanoId == PlanoId);
            foreach (var _empresa in Empresas)
            {
                if (_empresa.EmpresaId != 2 && _empresa.EmpresaId != 7)
                {
                    NumeroDeEmpresa++;

                }


            }
            if (NumeroDeEmpresa > 1)
            {
                fracionado = true;
            }
            if (Imposto.Contains("ICMS"))
            {
                Imposto = "ICMS";
            }
            if (Imposto.Contains("Débito"))
            {
                Imposto = "DEBITO";
            }
            if (_produto.Nome == "IP Wan")
            {
                #region ARGUMENTOS PARA PRODUTOS SEM PROPRIEDADES

                Argumentos = " tipo = '" + _produto.Classe + "' and ind_ativo = 1" +
                " and " + PlanoId + " = ANY (plano_id) and '" + Imposto + "' = ANY (imposto)  and fracionado = " + fracionado;
                return Argumentos;
                #endregion
            }
            if (_produto.Nome.Contains("MundiSecurity"))
            {
                #region ARGUMENTOS PARA MUNDISECURITY

                Argumentos = " dsc_abrev_servico = '" + _produto.Classe + "' and ind_ativo = 1" +
                " and " + PlanoId + " = ANY (plano_id) and '" + Imposto + "' = ANY (imposto) and fracionado = " + fracionado;
                return Argumentos;

                #endregion
            }
            if (_produto.Classe == "SBD")
            {
                #region ARGUMENTOS PARA PRODUTOS SEM PROPRIEDADES

                Argumentos = " tipo = '" + _produto.Classe + "' and ind_ativo = 1" +
                " and " + PlanoId + " = ANY (plano_id) and '" + Imposto + "' = ANY (imposto)  and fracionado = " + fracionado;
                return Argumentos;
                #endregion
            }
            if (_propriedades == null || _produto.Classe == "MBGP" || _produto.Classe == "MS" || _produto.Classe == "SBDMSE")
            {
                #region ARGUMENTOS PARA PRODUTOS SEM PROPRIEDADES

                Argumentos = " tipo = '" + _produto.Classe + "' and ind_ativo = 1" +
                " and " + PlanoId + " = ANY (plano_id) and '" + Imposto + "' = ANY (imposto)  and fracionado = " + fracionado;
                return Argumentos;
                #endregion
            }
            if (_produto.Classe == "L2L")
            {
                #region ARGUMENTOS PARA Lan 2 Lan

                Argumentos = " tipo = '" + _produto.Classe + "' and ind_ativo = 1" +
                " and banda = " + _propriedades.First(x => x.PropriedadeNome == "VELOCIDADE").ValorDigitado.Replace("_", "") +
                " and " + PlanoId + " = ANY (plano_id) and '" + Imposto + "' = ANY (imposto) and ind_promo = 0 ";
                return Argumentos;
                #endregion
            }
            if (_propriedades.Count(x => x.PropriedadeNome == "VELOCIDADE") > 0)
            {
                #region ARGUMENTOS PARA VELOCIDADE

                Argumentos = " tipo = '" + _produto.Classe + "' and ind_ativo = 1" +
                " and banda = " + _propriedades.First(x => x.PropriedadeNome == "VELOCIDADE").ValorDigitado.Replace("_", "") +
                " and " + PlanoId + " = ANY (plano_id) and '" + Imposto + "' = ANY (imposto) and ind_promo = 0 and fracionado = " + fracionado;

                #endregion
            }
            if (_propriedades.Count(x => x.PropriedadeNome == "QUANTIDADEIP") > 0)
            {
                #region ARGUMENTOS PARA QUANTIDADEIP


                Argumentos = " tipo = '" + _produto.Classe + "' and ind_ativo = 1" +
            " and banda = " + _propriedades.First(x => x.PropriedadeNome == "QUANTIDADEIP").ValorDigitado.Replace("_", "") +
            " and " + PlanoId + " = ANY (plano_id) and '" + Imposto + "' = ANY (imposto) ";

                #endregion
            }

            if (_propriedades.Count(x => x.PropriedadeNome == "DOWNLOAD") > 0)
            {
                #region ARGUMENTOS PARA DOWNLOAD UPLOAD 


                Argumentos = " tipo = '" + _produto.Classe + "' and ind_ativo = 1" +
            " and banda = " + _propriedades.First(x => x.PropriedadeNome == "DOWNLOAD").ValorDigitado.Replace("_", "") +
            " and upload = " + _propriedades.First(x => x.PropriedadeNome == "UPLOAD").ValorDigitado.Replace("_", "") +
            " and " + PlanoId + " = ANY (plano_id) and '" + Imposto + "' = ANY (imposto) and ind_promo = 0 and fracionado = " + fracionado;

                #endregion
            }



            return Argumentos;
        }

        public mvx_empresas CadastraEmpresa(int ClienteId)
        {
            throw new NotImplementedException();
        }

        public comercial_tipo_servicos RetornaTipoServicos(Produto _produto, List<PropriedadeProduto> _propriedades, int PlanoId, string Imposto)
        {

            string sql = RetornaQueryComecialTipoServico(_produto, _propriedades, PlanoId, Imposto);
            return MundiData.comercial_tipo_servicos.FromSql(sql).ToList()[0];
        }

        public int RetornaTipoMxv(string Cnpj, string imposto)
        {

            string ind_debito = string.Empty;
            if (imposto.ToUpper().Contains("DEBITO"))
            {
                ind_debito = " and ind_debito = 1";
            }
            comercial_tipo_mvx tipo_mvx = new comercial_tipo_mvx();
            string Args = " cnpj = '" + Cnpj + "'" + ind_debito;
            var Retorno = MundiData.comercial_tipo_mvx.FromSql("select * from comercial_tipo_mvx where " + Args);
            return Retorno.First().cod_tipo_mvx;
        }

        public int RetornaTipoContratacao(int MesValidade)
        {
            string Args = string.Empty;

            comercial_tipo_contratacao contratacao = new comercial_tipo_contratacao();
            if (MesValidade == 0)
            {
                Args = " dsc_tipo_contratacao = 'Indeterminado'";
            }
            else
            {
                Args = " dsc_tipo_contratacao = '" + MesValidade.ToString() + " meses'";
            }
            var Respota = MundiData.comercial_tipo_contratacao.FromSql("select * from comercial_tipo_contratacao where " + Args);
            //retorno = new ConversaoEConexao<comercial_tipo_contratacao>().RetornaLista(contratacao, Args, banco.Database.Connection.ConnectionString)[0].cod_tipo_contratacao;
            return Respota.First().cod_tipo_contratacao;
        }

        public mvx_empresas RetornaCliente(Cliente Cliente, string Uf, int CodPredio, bool mvx, int TipoCliente)
        {
            
            bool _parceiro = true;

            mvx_empresas empresas = new mvx_empresas();
            try
            {

                if (MundiBr.ComercialTipoOperadora.Count(x => x.cnpj.Contains(Cliente.CpfCnpj)) > 0)
                {
                    var operadora = MundiBr.ComercialTipoOperadora.First(x => x.cnpj == Cliente.CpfCnpj);
                    empresas.cod_empresa_ind = 1;
                    empresas.num_ddd_telefone = Cliente.PrefixoTelefone;
                    empresas.num_telefone = Cliente.Telefone;
                    empresas.dsc_mail = Cliente.Email;
                    empresas.cod_predio = CodPredio;
                    empresas.cod_atividade = 51;
                    empresas.ind_mvx_services = mvx ? 1 : 0;
                    empresas.dsc_observacao = "MUNDIBR => Mundiadata Cliente BR : " + Cliente.ClienteId.ToString();
                    empresas.dsc_vpn = "Nao Definido";
                    empresas.cod_operadora = operadora.cod_empresa;
                    empresas.cod_cliente_tipo = TipoCliente;
                    if (Uf != operadora.uf)
                    {
                        empresas.ind_wholesale = 1;
                    }

                }
                if (MundiBr.ComercialTipoOperadora.Count(x => x.cnpj.Contains(Cliente.CpfCnpj)) == 0 && _parceiro)
                {
                    empresas.cod_empresa_ind = 1;
                    empresas.num_ddd_telefone = Cliente.PrefixoTelefone;
                    empresas.num_telefone = Cliente.Telefone;
                    empresas.dsc_mail = Cliente.Email;
                    empresas.cod_predio = CodPredio;
                    empresas.cod_atividade = 51;
                    empresas.ind_mvx_services = mvx ? 1 : 0;
                    empresas.dsc_observacao = "MUNDIBR => Mundiadata Cliente BR : " + Cliente.ClienteId.ToString();
                    empresas.dsc_vpn = "Nao Definido";
                    empresas.cod_operadora = 0;
                    empresas.cod_cliente_tipo = TipoCliente;
                }
                return empresas;
            }
            catch (Exception erro)
            {

                empresas.cod_empresa_ind = 1;
                return empresas;
            }
        }

        public List<Contrato> RetornaContratos()
        {


            var resposta = MundiBr.Contrato.Where(x => x.NumeroSO != "0" && x._MapaFaturamento.Count() > 0 && x.Exportado == false && x.Ativo == true).ToList();

            return resposta;
        }

        public List<ContratoNrc> RetornaContratosVinculados()
        {


            var resposta = MundiBr.ContratoNrc.Where(x => x._Contrato.Exportado == true && x.Exportado == false && x.Ativo == true && x._Contrato._ContratoRelacionamento.Count() > 0 && x.ProdutoId != 6).ToList();


            return resposta;
        }

        public List<mvx_funcionarios> RetornaVendedores()
        {


            return MundiData.mvx_funcionarios.Where(x => x.cod_perfil == 4 || x.cod_perfil == 20 || x.cod_perfil == 15 || x.cod_perfil == 1).OrderBy(x => x.nom_funcionario).ToList();
        }

        public void CadastraEmpresaHistoricoEndereco(int cod_empresa, string Sala, int cod_predio, string Cep, string Andar, string Complemento, string Bloco)
        {
            mvx_empresas_historico_endereco historico = new mvx_empresas_historico_endereco();
            historico.cod_empresa = cod_empresa;
            historico.dsc_sala = Sala;
            historico.cod_endereco_predio = cod_predio;
            historico.dsc_cep = Cep;
            historico.num_andar = Convert.ToInt32(Andar);
            historico.complemento = Complemento;
            historico.num_bloco = Bloco;

            MundiData.mvx_empresas_historico_endereco.Add(historico);
            //MundiData.SaveChanges();

            
        }

        public void CadastrarMvxEmpresasContatos(int ContratoId, int cod_empresa)
        {

            foreach (var item in MundiBr.ClienteContato.Where(x => x.Ativo == true && x.ContratoId == ContratoId))
            {
                mvx_empresas_contatos contatos = new mvx_empresas_contatos();
                contatos.cod_empresa = cod_empresa;
                contatos.nom_contato = item.Nome;
                contatos.dsc_mail = item.Email;
                if (item.FuncionarioCargo.Contains("Responsavel Cobrança"))
                {
                    contatos.cod_empresa_tipo_contato = 5;

                }
                if (item.FuncionarioCargo.Contains("Responsavel Técnico"))
                {
                    contatos.cod_empresa_tipo_contato = 6;

                }
                if (item.FuncionarioCargo.Contains("Contratante"))
                {
                    contatos.cod_empresa_tipo_contato = 10;

                }
               
                try
                {
                    contatos.num_telefone = Convert.ToInt32(item.TelefoneFixo.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""));
                }
                catch
                {


                }

                MundiData.mvx_empresas_contatos.Add(contatos);
                //MundiData.SaveChanges();
            }
        }

        public void CadastraDadosAtuais(int PredioId, Cliente Cliente, int ClienteMunditataId, ClienteEndereco Endereco, string ProdutoAbreviacao, int Cod_pricipal, int TipoCliente)
        {
            mvx_empresas_dados_atuais DadosAtuais = new mvx_empresas_dados_atuais();
            DadosAtuais.cod_empresa = ClienteMunditataId;
            DadosAtuais.razao_social = Cliente.RazaoSocial;
            DadosAtuais.nom_fantasia = Cliente.NomeFantasia;
            DadosAtuais.cod_principal = Cod_pricipal;
            DadosAtuais.cod_tipo_cliente = TipoCliente;
            DadosAtuais.dsc_endereco = Endereco.Logradouro + " " + Endereco.Numero.ToString();
            DadosAtuais.dsc_endereco_comp = Endereco._Bairro.Nome + " " + Endereco._Bairro._Cidade.Nome + " " + Endereco._Bairro._Cidade._Estado.Estadosigla;
            DadosAtuais.num_bloco = Endereco.Bloco == "" ? "0" : Endereco.Bloco;
            DadosAtuais.num_andar = Endereco.Numero;
            DadosAtuais.dsc_sala = Endereco.Complemento;
            DadosAtuais.dsc_ins_municipal = Cliente.InscMunicipal;
            DadosAtuais.nom_predio = Endereco._Predio.Nome;
            DadosAtuais.cod_predio = PredioId;
            DadosAtuais.cod_status_empresa = 3;
            DadosAtuais.dsc_servicos = ProdutoAbreviacao;
            DadosAtuais.dsc_ins_estadual = Cliente.InscEstadual;
            if (Cliente.CpfCnpj.Length == 14)
            {
                DadosAtuais.dsc_cnpj = new Utils.Formatacao().FormataCnpjCpf(Cliente.CpfCnpj).Replace("CPF: ", "").Replace("CNPJ: ", "");
            }
            if (Cliente.CpfCnpj.Length == 11)
            {
                DadosAtuais.dsc_cpf = new Utils.Formatacao().FormataCnpjCpf(Cliente.CpfCnpj).Replace("CPF: ", "").Replace("CNPJ: ", "");
            }

            MundiData.mvx_empresas_dados_atuais.Add(DadosAtuais);
            MundiData.SaveChanges();
        }
        
        public void CadastraFinanceiroPainelDeControle(MapaFaturamento Mapeamento, int ClienteIdMundidata)
        {

            financeiro_painel_controle financeiro = new financeiro_painel_controle();
            financeiro.data_atual = DateTime.Now;
            financeiro.cod_funcionario = 137;
            financeiro.cod_empresa = ClienteIdMundidata;
            financeiro.dia_vencimento = Mapeamento.Vencimento;
            financeiro.cod_contabil = Mapeamento.CodigoFiscal == 0 ? "0" : Mapeamento.CodigoFiscal.ToString();
            financeiro.dsc_observacao = "N/A";
            financeiro.ind_pagamento_adiantado = 0;
            financeiro.ind_mau_pagador = 0;
            MundiData.financeiro_painel_controle.Add(financeiro);
            //MundiData.SaveChanges();


        }
        
        #endregion

        private int CadastraCliente(Cliente _Cliente, int CodPredio, bool ParceiroMvx, bool MvxService,string UF, int TipoClienteFaturamentoId, ClienteEndereco EnderecoInstalacao, int ContratoId, MapaFaturamento MapaDeFaturamento)
        {
            var Empresa = RetornaCliente(_Cliente, UF, CodPredio, MvxService, TipoClienteFaturamentoId);

            MundiData.mvx_empresas.Add(Empresa);

            CadastraEmpresaHistoricoEndereco(Empresa.cod_empresa, EnderecoInstalacao.Sala, CodPredio,EnderecoInstalacao.Cep ,EnderecoInstalacao.Andar, EnderecoInstalacao.Complemento, EnderecoInstalacao.Bloco);

            CadastrarMvxEmpresasContatos(ContratoId, Empresa.cod_empresa);

            CadastraFinanceiroPainelDeControle(MapaDeFaturamento, Empresa.cod_empresa);

            MundiData.SaveChanges();

            return Empresa.cod_empresa;
        }


        public void EnviarContrato(int ContratoId, int cod_empresa, int cod_principal, int cod_vendedor)
        {
           
          
            #region VARIAVEIS

            var _Contrato = MundiBr.Contrato.First(x => x.ContratoId == ContratoId);
            int CodPredioMundiBr = _Contrato._InstalacaoEndereco.First(x => x.TipoLogradouroId == 1).PredioId;
            int CodPredioMundiData = MundiBr.PrediosRelacionamento.First(x => x.PredioMundibrId == CodPredioMundiBr).PredioBabelId;
            int CodidoPar =  cod_empresa == 0 ? 1 : (int)(MundiData.mvx_empresas_servicos.Where(x => x.cod_empresa == cod_empresa).Max(x => x.cod_servico_par) + 1);
            var ClienteEndereco = _Contrato._InstalacaoEndereco.First(x => x.TipoLogradouroId == 1);
            var MapaFaturamento = _Contrato._MapaFaturamento.First();
            List<comercial_tipo_servicos> ListaComercialTipoServico = new List<comercial_tipo_servicos>();
            List<Tuple<string, string, string>> listaCnpj = new List<Tuple<string, string, string>>();
            var ListaPropriedades = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PropriedadeProduto>>(_Contrato.PropriedadesContrato);

            #endregion

            #region CADASTRO CLIENTE 

            if (cod_empresa == 0)
            {
              cod_empresa = CadastraCliente(_Contrato._Cliente, CodPredioMundiData, _Contrato.ParceiroMvx, false, ClienteEndereco._Bairro._Cidade._Estado.Estadosigla, (int)MapaFaturamento.TipoClienteFaturamentoId, ClienteEndereco, _Contrato.ContratoId, MapaFaturamento);

            }

            #endregion


            foreach (var imposto in _Contrato._MapaFaturamento.Last()._Plano._PlanoEmpresa)
            {
                listaCnpj.Add(new Tuple<string, string, string>(MundiBr.Empresa.First(x => x.EmpresaId == imposto.EmpresaId).Cnpj, imposto.Imposto, imposto.Receita.ToString()));
                ListaComercialTipoServico.Add(RetornaTipoServicos(_Contrato._Produto, ListaPropriedades, imposto.PlanoId, imposto.Imposto));

            }
            for (int i = 0; i < ListaComercialTipoServico.Count(); i++)
            {
                CadastraMvxEmpresasServico(ListaComercialTipoServico[i], CodidoPar, cod_empresa, RetornaTipoMxv(listaCnpj[i].Item1, listaCnpj[i].Item2), RetornaTipoContratacao(_Contrato._MapaFaturamento.First(x => x.MesValidade != '0').MesValidade), _Contrato, Convert.ToDouble(listaCnpj[i].Item3), cod_vendedor, listaCnpj[i].Item1, i);

            }

        }

        public void CadastraMvxEmpresasServico(comercial_tipo_servicos _comercial_tipo_servicos, int CodPar, int CodEmpresa, int tipo_mvx, int tipo_contratacao, Contrato _Contrato, double Receita, int cod_vendedor, string Cnpj, int volta)
        {
           
            int empresa = _Contrato._MapaFaturamento.First()._Plano._PlanoEmpresa.First(x => x.Receita == Receita).EmpresaId;
            string Emp = MundiData.Empresa.First(x => x.EmpresaId == empresa).NomeFantasia;
            string imposto = _Contrato._MapaFaturamento.First()._Plano._PlanoEmpresa.First(x => x.Receita == Receita).Imposto;
            mvx_empresas_servicos Servico = new mvx_empresas_servicos();

            Servico.cod_tipo_servico = _comercial_tipo_servicos.cod_tipo_servico;
            if (Cnpj == "18362892000136")
            {
                Servico.cod_tipo_servico = 481;
            }
            if (Cnpj == "18362892000217")
            {
                Servico.cod_tipo_servico = 433;
            }
            Servico.vlr_mensalidade = Convert.ToDecimal(new Utils.Calculos().CalculoPorcentagem(Receita, (double)new Utils.Calculos().CalculoConfaz(_Contrato)));
            if (volta == 0)
            {
                Servico.vlr_instalacao = _Contrato._ContratoNrc.Count(x => x.ProdutoId == 6) > 0 ? new Utils.Calculos().CalculoConfazInstalcao(_Contrato, _Contrato._ContratoNrc.First(x => x.ProdutoId == 6).Valor) : Convert.ToDecimal("0.00");

            }
            else
            {
                Servico.vlr_instalacao = Convert.ToDecimal("0.00");

            }
            Servico.cod_empresa = CodEmpresa;
            Servico.cod_vendedor = cod_vendedor;
            Servico.cod_tipo_contratacao = tipo_contratacao;
            Servico.cod_tipo_mvx = tipo_mvx;
            Servico.cod_servico_par = CodPar;
            Servico.cod_antigo_servico = 0;
            string codigoContra = _Contrato.Codigo.Substring(0, _Contrato.Codigo.Length - 1) + "A";

            if (_Contrato.ProdutoId == 33 || _Contrato.ProdutoId == 18)
            {

                var lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PropriedadeProduto>>(_Contrato.PropriedadesContrato);

                Servico.velocidade_down = Convert.ToInt32(lista.First(x => x.PropriedadeNome == "DOWNLOAD").ValorDigitado);
                Servico.velocidade_up = Convert.ToInt32(lista.First(x => x.PropriedadeNome == "UPLOAD").ValorDigitado);

            }
            if (_Contrato.ContratoStatusId == 10)
            {
                try
                {
                    var contratoAntigo = MundiData.Contrato.First(x => x.Codigo == codigoContra);
                    Servico.cod_par_antigo_serv = contratoAntigo._ContratoRelacionamento.First().CodPar;
                }
                catch
                {


                }

                Servico.dsc_observacao = "Cliente solicitou substituição em " + _Contrato.DataDaContratacao.ToString("dd/MM/yyyy");
            }
            Servico.cod_responsavel = 137;
            Servico.vlr_mensalidade_icms = Convert.ToDecimal("0.00");
            Servico.vlr_instalacao_iss = Convert.ToDecimal("0.00");
            Servico.vlr_instalacao_pis_cofins = Convert.ToDecimal("0.00");
            Servico.vlr_instalacao_icms = Convert.ToDecimal("0.00");
            Servico.vlr_mensalidade_iss = Convert.ToDecimal("0.00");
            Servico.vlr_mensalidade_pis_cofins = Convert.ToDecimal("0.00");
            string Args = string.Empty;
            if (_Contrato._InstalacaoEndereco.Count(f => f.TipoLogradouroId == 4) > 0)
            {
                var Ende = _Contrato._InstalacaoEndereco.Where(x => x.TipoLogradouroId == 4);
                Servico.ponta_b = "Rua: " + Ende.First().Logradouro + ", Número" + Ende.First().Numero + " Sala: " + Ende.First().Sala + " Comp.:" + Ende.First().Complemento + " - Bloco:" + Ende.First()._Bairro._Cidade.Nome + " - Estado:" + Ende.First()._Bairro._Cidade._Estado.Estadonome + " Cep.:" + Ende.First().Cep;
            }
            if (Cnpj == "18362892000136")
            {
                Args = " cod_empresa = " + CodEmpresa + " and cod_tipo_servico = 481 ;";
            }
            if (Cnpj == "18362892000217")
            {
                Args = " cod_empresa = " + CodEmpresa + " and cod_tipo_servico = 433 ;";
            }
            if (Cnpj != "18362892000217" && Cnpj != "18362892000136")
            {
                Args = " cod_empresa = " + CodEmpresa + " and cod_tipo_servico = " + _comercial_tipo_servicos.cod_tipo_servico + " ;";
            }

            MundiData.mvx_empresas_servicos.Add(Servico);
            MundiData.SaveChanges();

            if (_Contrato.ContratoStatusId == 10)
            {
                CadastraHist(Servico, _Contrato.DataDaContratacao, 22);
            }
            else
            {
                CadastraHist(Servico, _Contrato.DataDaContratacao, 1);
            }

        }

        public void CadastraHist(mvx_empresas_servicos ComercialTipoServico, DateTime DataContratacao, int status)
        {
            comercial_hist_st_empresas_serv com = new comercial_hist_st_empresas_serv();
            com.cod_empresa_servico = Convert.ToInt32(ComercialTipoServico.cod_empresa_servico);
            com.data_status = DateTime.Now;
            com.cod_status_serv = status;
            com.data_atual = DateTime.Now;
            com.data_contratacao = DataContratacao;
            MundiData.comercial_hist_st_empresas_serv.Add(com);
            MundiData.SaveChanges();
        }

        public void ProcessaPendencia(FormCollection formulario)
        {
            string[] Contratos = formulario["ContratosIds"].Split(',');
            string Evento = formulario["EventoId"];
            int VendedorId = Convert.ToInt32(formulario["VendedorId"]);
            DateTime DataContratacao = Convert.ToDateTime(formulario["DataContratacao"]);

            foreach (var item in Contratos)
            {
                try
                {
                    int ContratoId = Convert.ToInt32(item);
                    EnviarContrato(ContratoId, 0, 0, VendedorId);
                }
                catch 
                {

                    
                }
            }

        }

        public List<EventosDeContrato> RetornaEventos()
        {
            var db = new Class1(true);
            return db.EventosDeContrato.Where(x => x.Exportado == false).OrderBy(x => x.DataCriacao).ToList();
        }
    }
}

