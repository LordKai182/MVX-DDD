using Infra;
using Infra.Entidades;
using Infra.EntidadesNaoPersistidas;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositorios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Repositorios.Repositorio
{
    public class ContratoService : IContratoService
    {
        Class1 db;
        void AbreConexao()
        {
            db = new Class1(true);
        }
        void FechaConexao()
        {
            db.Database.CloseConnection();
        }

        #region API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Cliente"></param>
        /// <returns></returns>
        public Cliente ApiAlteraCliente(Cliente _Cliente)
        {
            AbreConexao();

            _Cliente.TipopessoaId = _Cliente.CpfCnpj.Length == 14 ? 1 : 2;
            _Cliente.CpfCnpj = _Cliente.CpfCnpj.Replace("CNPJ:", "").Trim();
            _Cliente.TipoVipId = 1;
            _Cliente.StatusClienteId = 1;
            db.cliente.Update(_Cliente);
            db.SaveChanges();
            FechaConexao();
            return _Cliente;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ContratoId"></param>
        /// <param name="Propriedade"></param>
        /// <param name="Valor"></param>
        public void ApiAlterarDetalhesDoProduto(int _ContratoId, string Propriedade, string Valor)
        {
            using (var context = new Class1(true))
            {
                List<PropriedadeProduto> Lst = new List<PropriedadeProduto>();
               
                var Contrato = context.Contrato.First(x => x.ContratoId == _ContratoId);
                Lst = PropriedadeContratoDigitado(Contrato.ContratoId);
                context.Entry(Contrato).State = EntityState.Detached;
                string Tipo = Propriedade;
                string valor = Valor;
                try
                {
                    foreach (var item in Lst)
                    {
                        if (item.PropriedadeNome == Tipo)
                        {
                            item.ValorDigitado = valor;
                        }
                    }

                    try
                    {

                        Contrato.PropriedadesContrato = Newtonsoft.Json.JsonConvert.SerializeObject(Lst);

                        context.Contrato.Update(Contrato);
                        context.SaveChanges();
                    }
                    catch (Exception erro)
                    {

                    }


                }
                catch
                {


                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProdutoId"></param>
        /// <param name="UsuarioId"></param>
        /// <param name="Valor"></param>
        /// <param name="ClienteId"></param>
        /// <param name="NumeroSo"></param>
        /// <returns></returns>
        public int ApiContratoNovo(int ProdutoId, int UsuarioId, decimal Valor, int ClienteId, string NumeroSo)
        {
            return IniciaContrato(ProdutoId, UsuarioId, Valor, ClienteId, NumeroSo);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ClienteFormulario"></param>
        /// <param name="ContratoId"></param>
        public void ApiInsereEndereco(ClienteEndereco _ClienteFormulario, int ContratoId)
         {

            AbreConexao();
            try
            {
              db.ClienteEndereco.Add(_ClienteFormulario);
              db.SaveChanges();
                
            }
            catch (Exception erro)
            {
               throw new Exception(String.Format("Eror ao Inserrir {0}",erro.Message));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Contato"></param>
        /// <returns></returns>
        public ClienteContato ApiInsereContato(ClienteContato _Contato)
        {
             AbreConexao();
            try
            {
                _Contato.Ativo = true;
                db.ClienteContato.Add(_Contato);
                db.SaveChanges();

                return _Contato;
            }
            catch (Exception erro)
            {

                throw new Exception(String.Format("Nao foi possivel inserir o Cliente {0}",erro.Message));
            }
         
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_MapaFaturamento"></param>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        public MapaFaturamento SalvaMapaDeFaturamento(MapaFaturamento _MapaFaturamento, int ContratoId)
        {
            try
            {
                AbreConexao();
                _MapaFaturamento.ContratoId = ContratoId;
                db.MapaFaturamento.Add(_MapaFaturamento);
                db.SaveChanges();
                FechaConexao();
                return _MapaFaturamento;
            }
            catch (Exception erro)
            {

                throw new Exception(String.Format("Não foi possivel inserir o Mapa de Faturamento {0}",erro.Message));
            }
          
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ApiRetornoCliente> ApiClientes()
        {
            AbreConexao();
          
            IEnumerable<ApiRetornoCliente> corss =
                from cliente in
                db.cliente
               
               
                select new ApiRetornoCliente
                {
                    Codigo = cliente.CodRelacionamento,
                    Cnpj = cliente.CpfCnpj,
                    IE = cliente.InscEstadual == null ? "ISENTO" : cliente.InscEstadual,
                    RazaoSocial = cliente.RazaoSocial,
                    ClienteId = cliente.ClienteId,
                    Ativos = cliente._Contrato.Count(x => x.ContratoStatusId == 5),
                    Cancelados = cliente._Contrato.Count(x => x.ContratoStatusId == 3),
                    Solicitacoes = cliente._Contrato.Where(x => x._EventosDeContrato.Count() > 0).Count()
                };

            List<ApiRetornoCliente> Lista = new List<ApiRetornoCliente>();
           return Lista = corss.ToList();
        }
      
        #endregion

        public Cliente AlteraCliente(FormCollection formulario)
        {
            AbreConexao();
            Cliente _clie = new Cliente();
            var cliente = new Formulario<Cliente>().LerFormulario(formulario, _clie);
            cliente.TipopessoaId = cliente.CpfCnpj.Length == 14 ? 1 : 2;
            cliente.CpfCnpj = cliente.CpfCnpj.Replace("CNPJ:", "").Trim();
            cliente.TipoVipId = 1;
            cliente.StatusClienteId = 1;
            db.cliente.Update(cliente);
            db.SaveChanges();
            FechaConexao();
            return cliente;
        }

        public void AlterarDetalhesDoProduto(FormCollection formulario)
        {
            using (var context = new Class1(true))
            {
                List<PropriedadeProduto> Lst = new List<PropriedadeProduto>();
                int ContratoId = Convert.ToInt32(formulario["ContratoId"]);
                var Contrato = context.Contrato.First(x => x.ContratoId == ContratoId);
                Lst = PropriedadeContratoDigitado(Contrato.ContratoId);
                context.Entry(Contrato).State = EntityState.Detached;
                string Tipo = string.Empty;
                string valor = string.Empty;
                try
                {
                    foreach (var key in formulario.Keys)
                    {
                        switch (key.ToString())
                        {
                            case "Nome":
                                Tipo = formulario[key.ToString()];
                                valor = formulario["Valor"];
                                break;

                        }
                    }
                    foreach (var item in Lst)
                    {
                        if (item.PropriedadeNome == Tipo)
                        {
                            item.ValorDigitado = valor;
                        }
                    }

                    try
                    {

                        Contrato.PropriedadesContrato = Newtonsoft.Json.JsonConvert.SerializeObject(Lst);

                        context.Contrato.Update(Contrato);
                        context.SaveChanges();
                    }
                    catch (Exception erro)
                    {

                    }


                }
                catch
                {


                }
            }
        }

        public void DeletaContato(int ContatoId)
        {
            AbreConexao();
            var contato = db.ClienteContato.First(x => x.ClienteContatoId == ContatoId);
            db.ClienteContato.Remove(contato);
            db.SaveChanges();
            FechaConexao();
        }

        public void DeletaContratoVinculado(int ContratoNrcId)
        {
            AbreConexao();
            var vinculado = db.ContratoNrc.First(x => x.ContratoNrcId == ContratoNrcId);
            db.Remove(vinculado);
            db.SaveChanges();
          
            db.SaveChanges();
            FechaConexao();

        }

        public void DeleteContrato(int ContratoId)
        {
            AbreConexao();
            db.Contrato.FromSql("DELETE FROM dbo.\"ContratoRelac\" WHERE \"ContratoId\" = " + ContratoId.ToString() + ";");
            db.Contrato.FromSql("DELETE FROM dbo.\"ClienteContato\" WHERE \"ContratoId\" = " + ContratoId.ToString() + ";");
            db.Contrato.FromSql("DELETE FROM dbo.\"ClienteEndereco\" WHERE \"ContratoId\" = " + ContratoId.ToString() + ";");
            db.Contrato.FromSql("DELETE FROM dbo.\"MapaFaturamento\" WHERE \"ContratoId\" = " + ContratoId.ToString() + ";");
            db.Contrato.FromSql("DELETE FROM dbo.\"ContratoVinculado\" WHERE \"ContratoId\" = " + ContratoId.ToString() + ";");
            db.Contrato.FromSql("DELETE FROM dbo.\"ContratoRelac\" WHERE \"ContratoId\" = " + ContratoId.ToString() + ";");
            db.Contrato.FromSql("DELETE FROM dbo.\"Contrato\" WHERE \"ContratoId\" = " + ContratoId.ToString() + ";");
            FechaConexao();

        }

        public int InicaContratoNovo(FormCollection formulario)
        {
            string NumeroSO = formulario["NumeroSO"];
            int UsuarioId = Convert.ToInt32(formulario["UsuarioId"]);
            int ClienteId = Convert.ToInt32(formulario["ClienteId"]);
            int ProdutoId = Convert.ToInt32(formulario["ProdutoId"]);
            decimal Valor = Convert.ToDecimal(formulario["Valor"]);

            return IniciaContrato(ProdutoId, UsuarioId, Valor, ClienteId, NumeroSO);

        }

        public int IniciaContrato(int ProdutoId, int UsuarioId, decimal Valor, int ClienteId, string NumeroSo)
        {
            try
            {
                #region INICIACONTRATO

                AbreConexao();

                var usu = db.Usuario.First(x => x.UsuarioId == UsuarioId);
                var pro = db.Produto.First(x => x.ProdutoId == ProdutoId);

                List<PropriedadeProduto> _LstPropriedadeProduto = new List<PropriedadeProduto>();
                Cliente cli = new Cliente();
               if(ClienteId != 0)
                { 
                   
                    cli = db.cliente.First(x => x.ClienteId == ClienteId);
                }
                if(ClienteId == 0)
                {

                    cli = new Cliente();
                    cli.CpfCnpj = "";
                    cli.RazaoSocial = "";
                    cli.CodRelacionamento = "BR" + (db.cliente.Count() + 1).ToString().PadLeft(6, '0');
                    cli.NomeFantasia = "";
                    cli.Cnae = "";
                    cli.Email = "";
                    cli.InscEstadual = "";
                    cli.InscMunicipal = "";
                    cli.Telefone = "";
                    cli.TipopessoaId = cli.CpfCnpj.Length == 18 ? 1 : 2;
                    cli.UsuarioCadastro = 1;
                    cli.StatusClienteId = 1;
                    cli.TipoVipId = 1;
                    db.cliente.Add(cli);
                    db.SaveChanges();
                }

                Contrato _Contrato = new Contrato(DateTime.Now, usu.UsuarioId, pro.ProdutoId, cli.ClienteId);
                _Contrato.NumeroSO = NumeroSo;
                _Contrato.ContratoStatusId = 11;
                _Contrato.Valor = Convert.ToDecimal(Valor);
                _Contrato.DataCriacao = DateTime.Now;
                _Contrato.Codigo = "";
                _Contrato.Ativo = true;
                try
                {

                    foreach (var item in pro.Propriedades.Split('|'))
                    {
                        try
                        {
                            int codigo = Convert.ToInt32(item);
                            _LstPropriedadeProduto.Add(db.PropriedadeProduto.First(x => x.PropriedadeProdutoId == codigo));
                        }
                        catch
                        {
                            _Contrato.PropriedadesContrato = "[]";
                        }

                    }
                }
                catch
                {
                    _Contrato.PropriedadesContrato = "[]";

                }
                _Contrato.PropriedadesContrato = Newtonsoft.Json.JsonConvert.SerializeObject(_LstPropriedadeProduto);
                db.Contrato.Add(_Contrato);
                db.SaveChanges();

                MapaFaturamento _MapaFaturamento = new MapaFaturamento();
                _MapaFaturamento.ContratoId = _Contrato.ContratoId;
                _MapaFaturamento.DataCriacao = DateTime.Now;
                _MapaFaturamento.CodigoFiscal = 0;
                _MapaFaturamento.Desconto = 0;
                _MapaFaturamento.MesParcelas = 1;
                _MapaFaturamento.MesValidade = 0;
                _MapaFaturamento.PlanoId = 174;
                db.MapaFaturamento.Add(_MapaFaturamento);
                db.SaveChanges();
                FechaConexao();

             

                #endregion

                return _Contrato.ContratoId;
            }
            catch (Exception erro)
            {

                return 0;
            }
        }

        public int IniciaContratoVinculado(int ContratoId, int UsuarioId, decimal Valor, int ClienteId, string NumeroSo)
        {
            throw new NotImplementedException();
        }

        public void IniciaContratoVinculado(FormCollection formulario)
        {
            throw new NotImplementedException();
        }

        public void InsereClienteEndereco(FormCollection formulario, int ContratoId)
        {
            ClienteFormulario _ClienteFormulario = new ClienteFormulario();
            _ClienteFormulario = new Formulario<ClienteFormulario>().LerFormulario(formulario, _ClienteFormulario);
            InsereClienteEnderecoForm(_ClienteFormulario, ContratoId);
        }

        public  void InsereClienteEnderecoForm(ClienteFormulario _ClienteFormulario, int COntratoId)
        {
            
                  var db = new Class1(true);
            try
            {
                
                    ClienteEndereco _ClienteEndereco = new ClienteEndereco();
    
                    _ClienteEndereco.Cep = _ClienteFormulario.cep;
                    _ClienteEndereco.Complemento = _ClienteFormulario.complemento;
                    _ClienteEndereco.Logradouro = _ClienteFormulario.rua;
                    _ClienteEndereco.Municipio = _ClienteFormulario.cidade;
                    _ClienteEndereco.ContratoId = Convert.ToInt32(_ClienteFormulario.Contra);
                    _ClienteEndereco.Numero = Convert.ToInt32(_ClienteFormulario.numero);
                    _ClienteEndereco.BairroId = VerificaBairro(_ClienteFormulario.bairro.ToUpper(), _ClienteFormulario.cidade.ToUpper(), _ClienteFormulario.uf, _ClienteFormulario.ibge);
                    _ClienteEndereco.ClienteId = Convert.ToInt32(_ClienteFormulario.ClienteId);
                    _ClienteEndereco.TipoLogradouroId = Convert.ToInt32(_ClienteFormulario.tipologr);
                    _ClienteEndereco.Sala = _ClienteFormulario.Sala;
                    _ClienteEndereco.Andar = _ClienteFormulario.Andar;
                    _ClienteEndereco.Bloco = _ClienteFormulario.Bloco;
                    if (_ClienteFormulario.tipologr == "1" && _ClienteFormulario.PredioId == "1" || _ClienteFormulario.tipologr == "4" && _ClienteFormulario.PredioId == "1" || _ClienteFormulario.tipologr == "1" && _ClienteFormulario.PredioId == "")
                    {
                        _ClienteEndereco.PredioId = 1;
                    }
                    if (_ClienteFormulario.tipologr != "1")
                    {
                        _ClienteEndereco.PredioId = _ClienteFormulario.PredioId == "" || _ClienteFormulario.PredioId == null ? 99999 : Convert.ToInt32(_ClienteFormulario.PredioId);
                    }
                    if (_ClienteFormulario.tipologr == "4")
                    {
                        _ClienteEndereco.Ponta = "2";
                    }
                    db.ClienteEndereco.Add(_ClienteEndereco);
                    db.SaveChanges();

                    //Msg = "mensagemPadrao('Endereço Salvo.','success');";

                


            }
            catch (Exception erro)
            {
                //Msg = "mensagemPadrao('Houve Erro ao cadatsrar o Endereço.','error');";
            }
        }

        public static int VerificaBairro(string Bairro, string Cidade, string Estado, string IBGE)
        {
            var db = new Class1(true);
            int ID = 0;
            try
            {
                int IDD = db.Bairros.First(x => x.Nome == Bairro && x._Cidade.Nome == Cidade && x._Cidade._Estado.Estadosigla == Estado).BairroId;
                ID = IDD;
                return ID;
            }
            catch
            {
                if (db.Cidades.Count(c => c.Nome == Cidade) == 0)
                {
                    Cidade _Cidade = new Cidade();
                    _Cidade.Nome = Cidade;
                    _Cidade.EstadoId = db.Estados.First(x => x.Estadosigla == Estado).EstadoId;
                    _Cidade.CodIbge = Convert.ToInt32(IBGE);
                    db.Cidades.Add(_Cidade);
                    db.SaveChanges();
                    Infra.Entidades.Bairro _Bairro = new Infra.Entidades.Bairro();
                    _Bairro.Nome = Bairro;
                    _Bairro.CidadeId = _Cidade.CidadeId;
                    db.Bairros.Add(_Bairro);
                    db.SaveChanges();
                    ID = _Bairro.BairroId;
                }
                else
                {
                    int CidadeID = db.Cidades.First(x => x.Nome == Cidade).CidadeId;
                    Infra.Entidades.Bairro _Bairro = new Infra.Entidades.Bairro();
                    _Bairro.Nome = Bairro;
                    _Bairro.CidadeId = CidadeID;
                    db.Bairros.Add(_Bairro);
                    db.SaveChanges();
                    ID = _Bairro.BairroId;
                }


            }

            return ID;
        }

        public void InsereContato(FormCollection formulario)
        {
            var db = new Class1(true);
            ClienteContato _cont = new ClienteContato();
            _cont = new Formulario<ClienteContato>().LerFormulario(formulario, _cont);
            _cont.Ativo = true;
            db.ClienteContato.Add(_cont);
            db.SaveChanges();
        }

        public List<Cliente> ListaClientes()
        {
            AbreConexao();
            var resposta = db.cliente.ToList();
            FechaConexao();

            return resposta;
        }

        public List<Contrato> ListaContratos()
        {
            AbreConexao();
            return null;
        }

        public List<Contrato> ListaContratosPorCliente(int ClienteId)
        {
            throw new NotImplementedException();
        }

        public List<ContratoNrc> listaContratoVinculados(int ContratoId)
        {
            throw new NotImplementedException();
        }

        public List<ClienteEndereco> ListaEnderecosPorContrato(int ContratoId)
        {
            throw new NotImplementedException();
        }

        public List<PropriedadeProduto> PropriedadeContratoDigitado(int ContratoId)
        {
            try
            {
                AbreConexao();
                List<PropriedadeProduto> ListaSemPromo = new List<PropriedadeProduto>();
                var Contrato = db.Contrato.First(x => x.ContratoId == ContratoId);
                var propri = JsonConvert.DeserializeObject<List<PropriedadeProduto>>(Contrato.PropriedadesContrato);
                if (Contrato.Promocional)
                {
                    return propri;
                }
                else
                {

                    foreach (var item in propri)
                    {
                        if (!item.PropriedadeNome.Contains("PROMO"))
                        {
                            ListaSemPromo.Add(item);


                        }
                    }
                    return ListaSemPromo;
                    FechaConexao();
                }

                return null;
            }
            catch
            {

                return null;
                FechaConexao();
            }
        }

        public List<PropriedadeProduto> PropriedadeContratoVazio(int ContratoId)
        {
            throw new NotImplementedException();
        }

        public Cliente RetornaClienteDoContrato(int ContratoId)
        {
            throw new NotImplementedException();
        }

        public Contrato RetornaContrato(int Contrato)
        {
            AbreConexao();
            var resposta = db.Contrato.First(x => x.ContratoId == Contrato);
            FechaConexao();
            return resposta;
        }

        public List<Contrato> RetornaContratosPorCliente(int ClienteId)
        {
            AbreConexao();
            var resposta = db.Contrato.Where(x => x.ClienteId == ClienteId).ToList();
            FechaConexao();
            return resposta;
        }

        public ClienteEndereco RetornaEnderecoPorContrato(int ContratoId, int TipoLogradouro)
        {
            AbreConexao();
            try
            {
              return  db.ClienteEndereco.First(x => x.ContratoId == ContratoId && x.TipoLogradouroId == TipoLogradouro);
            }
            catch
            {

                return null;
            }


        }

        public void SalvaMapaDeFaturamento(int ContratoId)
        {
            throw new NotImplementedException();
        }

        public void SalvaMapaDeFaturamento(FormCollection formulario)
        {
            AbreConexao();
            MapaFaturamento mapa = new MapaFaturamento();
            var teste =  new Formulario<MapaFaturamento>().LerFormulario(formulario, mapa);
            db.MapaFaturamento.Add(teste);
            db.SaveChanges();
            FechaConexao();
        }

        public void SalvaMapaDefaturamentoVinculado(int ContratoNrcId)
        {
            throw new NotImplementedException();
        }

        public void IniciaContato(FormCollection formulario)
        {
            AbreConexao();
            ClienteContato _cont = new ClienteContato();
            var teste = new Formulario<ClienteContato>().LerFormulario(formulario, _cont);
            _cont.Ativo = true;
            db.Add(teste);
            db.SaveChanges();
            FechaConexao();
        }
    }
}
