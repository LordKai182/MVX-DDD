using Infra;
using Infra.Entidades;
using Infra.EntidadesNaoPersistidas;
using Infra.EntitdadesSolicita;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repositorios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static Utils.CNAB400;

namespace Repositorios.Repositorio
{
    public class SoService : ISoService
    {

        public AcessoUsuario _Acesso;
       
        private Class1 dbb = new Class1(true, "solicita");
        private Class1 Mundbr =  new Class1(true);
        public int CriarContratoPorSO(int NumeroSO)
        {
            
            int resp = 0;
            if (Mundbr.Contrato.Count(x => x.NumeroSO == NumeroSO.ToString() && x.Ativo == true) > 0)
            {
                resp = Mundbr.Contrato.First(x => x.NumeroSO == NumeroSO.ToString()).ContratoId;
            }

            else
            {
                var PropostaSO = dbb.ContratosSO.First(x => x.codigo == NumeroSO);
                var resumo = dbb.propostas_produtos_resumo.First(x => x.idpropprod == PropostaSO.idpropprod);
                int[] tipos_pro = { 4, 2, 2 };
                int[] tipos_opr = { 1, 2, 3 };
                if (tipos_pro.Contains((int)PropostaSO._propostas_produtos.tipo_pro))
                {

                }
                if (PropostaSO._propostas_produtos.tipo_pro.ToString() == "1")
                {
                    resp = ConvertSOParaContrato(PropostaSO, resumo);
                }
            }
            
            return resp;
        }
        public List<EventosDeContrato> BuscaEventoSo(int NumeroSO, int ContratoId)
        {
            List<EventosDeContrato> ListaDeEventos = new List<EventosDeContrato>();

            int resp = 0;
           
                var PropostaSO = dbb.ContratosSO.First(x => x.codigo == NumeroSO);
                var resumo = dbb.propostas_produtos_resumo.First(x => x.idpropprod == PropostaSO.idpropprod);
                int[] tipos_pro = { 4, 2, 2 };
                int[] tipos_opr = { 1, 2, 3 };
                if (tipos_pro.Contains((int)PropostaSO._propostas_produtos.tipo_pro))
                {
                    if((int)PropostaSO._propostas_produtos.tipo_pro == 2 || (int)PropostaSO._propostas_produtos.tipo_pro == 3)
                    {
                        ListaDeEventos.Add(GeraEventoUpDwValor(PropostaSO,ContratoId, (int)PropostaSO._propostas_produtos.tipo_pro));
                         var result =  VerificarPrazo(ListaDeEventos[0]);
                         if(result != null)
                         {
                           ListaDeEventos.Add(result); 
                         }
                         if (PropostaSO._c_vencimentos != null)
                         {
                            var vencimento = VerificarVencimento(ListaDeEventos[0], Convert.ToInt32(PropostaSO._c_vencimentos.vencimento));
                          if(vencimento != null)
                           {
                            ListaDeEventos.Add(vencimento);
                           }
                        }
                    }
                    if ((int)PropostaSO._propostas_produtos.tipo_oper == 1 || (int)PropostaSO._propostas_produtos.tipo_oper == 2)
                    {
                        ListaDeEventos.Add(GeraEventoUpDwProduto(PropostaSO, ContratoId, (int)PropostaSO._propostas_produtos.tipo_oper));
                    }
                    }
                if (PropostaSO._propostas_produtos.tipo_pro.ToString() == "1")
                {
                    return null;
                }
            

            return ListaDeEventos;
        }

        #region METODOS
        private EventosDeContrato GeraEventoUpDwProduto(contratos So, int ContratoId,int produto)
        {
            EventosDeContrato _Evento = new EventosDeContrato();

            _Evento.Consultor = So._propostas_produtos._propostas.consultor;
            _Evento.Prazo = (int)So._propostas_produtos.prazo;
            _Evento.ValorAntigo = So._propostas_produtos.valor_atual.ToString();
            _Evento.ValorNovo = So._propostas_produtos.velocidade.ToString();
            _Evento.DataCriacao = DateTime.Now;
            _Evento.DataContratacao = Convert.ToDateTime(So.dt_cc);
            _Evento.NumeroSO = So.codigo.ToString();
            _Evento.ContratoId = ContratoId;
            _Evento.UsuarioId = 1;
            if (produto == 1)
            {
                _Evento.EventoDescricao = "Upgrade Produto: " + So._propostas_produtos.servico_atual;
            }
            if (produto == 2)
            {
                _Evento.EventoDescricao = "Downgrade Produto: " + So._propostas_produtos.servico_atual;
            }
           
            
            return _Evento;

        }
        private EventosDeContrato VerificarPrazo(EventosDeContrato evento)
        {
            var dg = new Class1(true);

            var Contrato = dg.Contrato.First(x => x.ContratoId == evento.ContratoId);
            int _prazo = Contrato._MapaFaturamento.First().MesValidade;
            if(evento.Prazo != _prazo)
            {
                EventosDeContrato _Evento = new EventosDeContrato();

                _Evento.Consultor = evento.Consultor;
                _Evento.Prazo = evento.Prazo;
                _Evento.ValorAntigo = _prazo.ToString();
                _Evento.ValorNovo = evento.Prazo.ToString();
                _Evento.DataCriacao = DateTime.Now;
                _Evento.DataContratacao = Convert.ToDateTime(evento.DataContratacao);
                _Evento.NumeroSO = evento.NumeroSO.ToString();
                _Evento.ContratoId = evento.ContratoId;
                _Evento.UsuarioId = 1;
                _Evento.EventoDescricao = "Renovação";
                return _Evento;
            }

           return null;

        }
        public void  SalvarEventos(List<EventosDeContrato> ListaEventos)
        {
            var dg = new Class1(true);
            foreach (var item in ListaEventos)
            {
                dg.EventosDeContrato.Add(item);
                dg.SaveChanges();
            }


        }


        private EventosDeContrato VerificarVencimento(EventosDeContrato evento,int Vencimento)
        {
            var dg = new Class1(true);
            var Contrato = dg.Contrato.First(x => x.ContratoId == evento.ContratoId);
            int _vencimento = Contrato._MapaFaturamento.First().Vencimento;
            if (Vencimento != _vencimento)
            {
                EventosDeContrato _Evento = new EventosDeContrato();

                _Evento.Consultor = evento.Consultor;
                _Evento.Prazo = evento.Prazo;
                _Evento.ValorAntigo = _vencimento.ToString();
                _Evento.ValorNovo = Vencimento.ToString();
                _Evento.DataCriacao = DateTime.Now;
                _Evento.DataContratacao = Convert.ToDateTime(evento.DataContratacao);
                _Evento.NumeroSO = evento.NumeroSO.ToString();
                _Evento.ContratoId = evento.ContratoId;
                _Evento.UsuarioId = 1;
                _Evento.EventoDescricao = "Alterar Vencimento";
                return _Evento;
            }

            return null;

        }
        private EventosDeContrato GeraEventoUpDwValor(contratos So, int ContratoId, int servico)
        {
            EventosDeContrato _Evento = new EventosDeContrato();

            _Evento.Consultor = So._propostas_produtos._propostas.consultor;
            _Evento.Prazo = (int)So._propostas_produtos.prazo;
            _Evento.ValorAntigo = So._propostas_produtos.mensalidade_atual.ToString();
            _Evento.ValorNovo = So._propostas_produtos.mensalidade.ToString();
            _Evento.DataCriacao = DateTime.Now;
            _Evento.DataContratacao = Convert.ToDateTime(So.dt_cc);
            _Evento.NumeroSO = So.codigo.ToString();
            _Evento.ContratoId = ContratoId;
            _Evento.UsuarioId = 1;
            if(servico == 2)
            {
                _Evento.EventoDescricao = "Upgrade Receita";
            }
            if (servico == 3)
            {
                _Evento.EventoDescricao = "Downgrade Receita";
            }
          

            return _Evento;

        }


        private int  ConvertSOParaContrato(contratos so, propostas_produtos_resumo resumo)
        {

            
            IDbContextTransaction transaction = Mundbr.Database.BeginTransaction();
            try
            {
              #region CONTRATO

             Contrato _Contrato = new Contrato();

            _Contrato.DataCriacao = DateTime.Now;
            _Contrato.ClienteId = RetornaCliente(so);
            _Contrato.DataDaAssinatura = resumo.data_ass_contrato;
            _Contrato.UsuarioId = 1;
            _Contrato.DataDaContratacao = resumo.data_ass_contrato;
            _Contrato.RenovacaoAutomatica = Convert.ToBoolean(resumo.renova_auto);
            _Contrato.Consultor = so._propostas_produtos._propostas.consultor;
            _Contrato.Alias =  so._propostas_produtos.cliente;
            _Contrato.Ativo = true;
            _Contrato.ProdutoId = RetornaProduto(so, resumo);
            _Contrato.PropriedadesContrato = RetornaPropriedadeDeContrato(so, resumo, _Contrato.ProdutoId, so.codigo.ToString());
            _Contrato.Valor = (decimal)so._propostas_produtos.mensalidade;
            _Contrato.ContratoStatusId = 11;
            _Contrato.NumeroSO = so.codigo.ToString();
            if(so._propostas_produtos.mensalidade_atual != null || so._propostas_produtos.mensalidade_atual > 0)
            {
                _Contrato.Valor = (decimal)so._propostas_produtos.mensalidade_atual;
            }
            _Contrato.MesValidade = (int)so._propostas_produtos.prazo;

                Mundbr.Contrato.Add(_Contrato);
                SalavaMapa(so,resumo, so.codigo.ToString(), _Contrato.ContratoId);

            if(resumo.dados_ips > 0)
            {
                GravaIP(so, resumo, so.codigo.ToString(), _Contrato.ContratoId);
              
            }
            if (so._propostas_produtos.instalacao > 0)
            {
                GravaInstalacao(so, resumo, so.codigo.ToString(), _Contrato.ContratoId);

            }
                #endregion

                #region ENDEREÇO

                GravaEndereco(so,_Contrato.ClienteId,_Contrato.ContratoId);

                #endregion

                #region CONTATO

                GravaContatos(resumo, _Contrato.ContratoId, _Contrato.ClienteId, so._propostas_produtos.cliente);

                #endregion

               
                Mundbr.SaveChanges();
                transaction.Commit();

                return _Contrato.ContratoId;
            }
            catch(Exception erro)
            {
                transaction.Rollback();
                return 0;
            }

          
        }

        public void GravaEndereco(contratos so, int ClienteId, int ContratoId)
        {
         
            ClienteEndereco endereco = new ClienteEndereco();
            var ende = so._propostas_produtos._propostas_enderecos;
            Predio Predio = new Predio();
            if(ende.predio.Trim() != "PNA")
            {
                Predio = Mundbr.Predio.First(x => x.Nome == ende.predio);
            }
            else
            {
                Predio = Mundbr.Predio.First(x => x.PredioId == 1);
            }
            endereco.BairroId = Mundbr.Bairros.First(x => x.Nome.ToUpper() == ende.bairro.ToUpper()).BairroId;
            endereco.ClienteId = ClienteId;
            endereco.ContratoId = ContratoId;
            endereco.Complemento = ende.complemento;
            endereco.Logradouro = ende.endereco;
            endereco.Cep = ende.cep.ToString();
            endereco.TipoLogradouroId = 1;
            endereco.Numero = ende.numero;
            endereco.PredioId = Predio.PredioId;

            Mundbr.ClienteEndereco.Add(endereco);

        }
        public void GravaContatos(propostas_produtos_resumo resumo, int COntratoId, int ClienteId, string responsavel)
        {
            
            ClienteContato ContatoCobranca = new ClienteContato();
            ContatoCobranca.ClienteId = ClienteId;
            ContatoCobranca.ContratoId = COntratoId;
            ContatoCobranca.Nome = resumo.resp_cobranca;
            ContatoCobranca.Email = resumo.email_resp_cobranca;
            ContatoCobranca.TelefoneFixo = resumo.tel_resp_cobranca;

            Mundbr.ClienteContato.Add(ContatoCobranca);

            ClienteContato ContatoTecnico = new ClienteContato();
            ContatoTecnico.ClienteId = ClienteId;
            ContatoTecnico.ContratoId = COntratoId;
            ContatoTecnico.Nome = resumo.resp_tecnico;
            ContatoTecnico.Email = resumo.email_resp_tecnico;
            ContatoTecnico.TelefoneFixo = resumo.tel_resp_tecnico;

            Mundbr.ClienteContato.Add(ContatoTecnico);

            ClienteContato ContatoResp = new ClienteContato();
            ContatoResp.ClienteId = ClienteId;
            ContatoResp.ContratoId = COntratoId;
            ContatoResp.Nome = responsavel;
            ContatoResp.Email = resumo.email_contratante;
            ContatoResp.TelefoneFixo = resumo.tel_contratante;

            Mundbr.ClienteContato.Add(ContatoResp);

        }

        public int RetornaCliente(contratos proposta)
        {
           

            string cnpj = proposta._propostas_produtos.cnpj_cpf.Replace(".", "").Replace("/", "").Replace("-", "");

            if(Mundbr.cliente.Count(x=>x.CpfCnpj == cnpj) > 0)
            {
                var cliente = Mundbr.cliente.First(x => x.CpfCnpj == cnpj);
                cliente.InscEstadual = proposta.iestadual;
                cliente.InscMunicipal = proposta.imunicipal;
                cliente.RazaoSocial = proposta._propostas_produtos.cliente;
                cliente.Email = proposta._propostas_produtos.email;
                cliente.Telefone = proposta._propostas_produtos.telefone;
                Mundbr.cliente.Update(cliente);

                return cliente.ClienteId;
            }
            else
            {
                Cliente cliente = new Cliente();
                cliente.InscEstadual = proposta.iestadual;
                cliente.InscMunicipal = proposta.imunicipal;
                cliente.RazaoSocial = proposta._propostas_produtos.cliente;
                cliente.Email = proposta._propostas_produtos.email;
                cliente.Telefone = proposta._propostas_produtos.telefone;
                cliente.CpfCnpj = cnpj;
                cliente.TipopessoaId = cnpj.Length == 14 ? 1 : 2;
                cliente.TipoVipId = 1;
                cliente.StatusClienteId = 1;
                Mundbr.cliente.Update(cliente);

                return cliente.ClienteId;

            }


            return 0;
        }

        private int RetornaProduto(contratos proposta, propostas_produtos_resumo resumo)
        {
           

            string Prod = proposta._propostas_produtos._p_servicos._p_classes.classe.ToUpper();

            try
            {

                if (Prod.Contains("MUNDINET"))
                {
                    Prod = "MUNDINET";

                }

                if (Prod.Contains("COMBO RESIDENCIAL"))
                {

                    if (resumo.vozcb_tipo_serv == "P1")
                    {
                        Prod = "Combo P1";
                    }

                    if (resumo.vozcb_tipo_serv == "P2")
                    {
                        Prod = "Combo P2";
                    }



                }
                if (Prod.Contains("MUNDIRESIDENCIAL"))
                {
                    if (resumo.vozcb_tipo_serv == "")
                    {
                        Prod = "Mundi Residencial";
                    }
                    if (resumo.vozcb_tipo_serv == null)
                    {
                        Prod = "Mundi Residencial";
                    }
                    if (resumo.vozcb_tipo_serv == "P1")
                    {
                        Prod = "Combo P1";
                    }

                    if (resumo.vozcb_tipo_serv == "P2")
                    {
                        Prod = "Combo P2";
                    }



                }
                if (Prod.Contains("MA SECURE"))
                {
                    Prod = "MundiAccess Secure";
                }
                if (Prod.Contains("MUNDISECURITY"))
                {
                    if (proposta._propostas_produtos._p_servicos.servico.Contains("MSPLT"))
                    {
                        Prod = "MundiSecurity Platinum";
                    }
                    if (proposta._propostas_produtos._p_servicos.servico.Contains("MSLGTSTD"))
                    {
                        Prod = "MundiSecurity Light Standard";
                    }
                    if (proposta._propostas_produtos._p_servicos.servico.Contains("MSGLD"))
                    {
                        Prod = "MundiSecurity Gold";
                    }
                    if (proposta._propostas_produtos._p_servicos.servico.Contains("MSSTD"))
                    {
                        Prod = "MundiSecurity Standard";
                    }
                    if (proposta._propostas_produtos._p_servicos.servico.Contains("MSTSEC"))
                    {
                        Prod = "MundiSecurity TotalSec";
                    }

                  
                }


                int idproduto = Mundbr.Produto.First(x => x.Nome.ToUpper() == Prod).ProdutoId;
                return idproduto;
            }
            catch
            {

                return 0;
            }
        
        }

        private string RetornaPropriedadeDeContrato(contratos proposta, propostas_produtos_resumo resumo, int produtoId, string NumeroSo)
        {
            
            try
            {
                List<PropriedadeProduto> listaPropriedade = new List<PropriedadeProduto>();
                var Produto = Mundbr.Produto.First(x => x.ProdutoId == produtoId);

                if (Produto.Propriedades != null)
                {
                    string[] prot = Produto.Propriedades.Split('|');
                    foreach (var item in prot)
                    {
                        try
                        {
                            int pp = Convert.ToInt32(item);
                            var PropriedadeProduto = Mundbr.PropriedadeProduto.First(x => x.PropriedadeProdutoId == pp);
                            PropriedadeProduto.ValorDigitado = "";
                            if (PropriedadeProduto.PropriedadeNome.ToUpper() == "VELOCIDADE")
                            {
                                PropriedadeProduto.ValorDigitado = proposta._propostas_produtos.velocidade == null || proposta._propostas_produtos.velocidade == 0 ? proposta._propostas_produtos._p_servicos.valor.ToString() : proposta._propostas_produtos.velocidade.ToString();
                                PropriedadeProduto.NumeroSO = NumeroSo;
                            }
                            if (PropriedadeProduto.PropriedadeNome.ToUpper() == "UPLOAD")
                            {
                                PropriedadeProduto.ValorDigitado = proposta._propostas_produtos.upload.ToString();
                                PropriedadeProduto.NumeroSO = NumeroSo;
                            }
                            if (PropriedadeProduto.PropriedadeNome.ToUpper() == "DOWNLOAD")
                            {
                                PropriedadeProduto.ValorDigitado = proposta._propostas_produtos.download.ToString();
                                PropriedadeProduto.NumeroSO = NumeroSo;
                            }
                            listaPropriedade.Add(PropriedadeProduto);

                        }
                        catch
                        {


                        }

                    }
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(listaPropriedade);
            }
            catch
            {


                return "[]";
            }

         
        }

        private void  GravaInstalacao(contratos proposta, propostas_produtos_resumo resumo,  string NumeroSo, int Contratoid)
        {
           
            ContratoNrc instalacao = new ContratoNrc();
            instalacao.DataCadastro = DateTime.Now;
            instalacao.ContratoId = Contratoid;
            instalacao.ProdutoId = 6;
            instalacao.UsuarioId = 1;
            instalacao.Valor = (decimal)proposta._propostas_produtos.instalacao;
            instalacao.NumeroSO = NumeroSo;

            Mundbr.ContratoNrc.Add(instalacao);

            SalavaMapaVinculado(proposta, resumo, NumeroSo, instalacao.ContratoNrcId);

        }

        private void GravaIP(contratos proposta, propostas_produtos_resumo resumo, string NumeroSo, int Contratoid)
        {
          
            ContratoNrc ip = new ContratoNrc();
            ip.DataCadastro = DateTime.Now;
            ip.ContratoId = Contratoid;
            ip.ProdutoId = 7;
            ip.UsuarioId = 1;
            ip.Valor = (decimal)resumo.dados_ips_mensal;
            ip.NumeroSO = NumeroSo;
            #region PROP
            List<PropriedadeProduto> listaPropriedadeIP = new List<PropriedadeProduto>();
            var Produtos = Mundbr.Produto.First(x => x.ProdutoId == 7);
            if (Produtos.Propriedades != null)
            {
                string[] prot = Produtos.Propriedades.Split('|');
                foreach (var item in prot)
                {
                    try
                    {
                        int pp = Convert.ToInt32(item);
                        var PropriedadeProduto = Mundbr.PropriedadeProduto.First(x => x.PropriedadeProdutoId == pp);
                        if (PropriedadeProduto.PropriedadeNome.ToUpper() == "QUANTIDADEIP")
                        {
                            PropriedadeProduto.ValorDigitado = resumo.dados_ips.ToString();
                            PropriedadeProduto.NumeroSO = NumeroSo;
                        }

                        listaPropriedadeIP.Add(PropriedadeProduto);

                    }
                    catch
                    {


                    }

                }
            }
            else
            {
                ip.PropriedadesContrato = "[]";
            }

            #endregion
            ip.PropriedadesContrato = Newtonsoft.Json.JsonConvert.SerializeObject(listaPropriedadeIP);
            Mundbr.ContratoNrc.Add(ip);

            SalavaMapaVinculado(proposta, resumo, NumeroSo, ip.ContratoNrcId);


        }

        public void SalavaMapa(contratos proposta, propostas_produtos_resumo resumo,string NumeroSo, int Contratoid)
        {
        
            MapaFaturamento _Mapa = new MapaFaturamento();

            _Mapa.ContratoId = Contratoid;
            _Mapa.DataCriacao = DateTime.Now;
            _Mapa.CodigoFiscal = 0;
            _Mapa.Desconto = 0;
            _Mapa.MesParcelas = 1;
            _Mapa.MesValidade = Convert.ToInt32(proposta._propostas_produtos.prazo);
            _Mapa.PlanoId = 174;
            _Mapa.RenovacaoAutomatica = Convert.ToBoolean(resumo.renova_auto);
            try
            {
                _Mapa.Vencimento =  Convert.ToInt32(dbb.c_vencimentos.First(x => x.id == proposta.vencimento).vencimento);

            }
            catch 
            {
                _Mapa.Vencimento = 0;


            }
            Mundbr.MapaFaturamento.Add(_Mapa);


        }

        public void SalavaMapaVinculado(contratos proposta, propostas_produtos_resumo resumo, string NumeroSo, int Contratoid)
        {
           

            MapaFaturamento _Mapa = new MapaFaturamento();

            _Mapa.ContratoNrcId = Contratoid;
            _Mapa.DataCriacao = DateTime.Now;
            _Mapa.CodigoFiscal = 0;
            _Mapa.Desconto = 0;
            _Mapa.MesParcelas = 1;
            _Mapa.MesValidade = Convert.ToInt32(proposta._propostas_produtos.prazo);
            _Mapa.PlanoId = 174;
            _Mapa.RenovacaoAutomatica = Convert.ToBoolean(resumo.renova_auto);
            try
            {
                _Mapa.Vencimento = Convert.ToInt32(dbb.c_vencimentos.First(x => x.id == proposta.vencimento).vencimento);

            }
            catch
            {
                _Mapa.Vencimento = 0;


            }
            Mundbr.MapaFaturamento.Add(_Mapa);


        }
       
        #endregion


    }
}
