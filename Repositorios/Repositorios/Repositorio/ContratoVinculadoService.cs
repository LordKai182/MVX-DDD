using Infra;
using Infra.Entidades;
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
    public class ContratoVinculadoService : IContratoVinculadoService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public int IniciarContratoVinculado(FormCollection form)
        {
            string SO = form["NumeroSO"];
            string Valor = form["Valor"];
            int Produto = Convert.ToInt32(form["ProdutoId"]);
            string ContratoPai = form["ContratoId"];
            int ContratoId = Convert.ToInt32(ContratoPai);

            return IniciarContratoVinculado(SO, Valor, Produto, ContratoId);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NumeroSO"></param>
        /// <param name="Valor"></param>
        /// <param name="ProdutoId"></param>
        /// <param name="ContratoId"></param>
        /// <returns></returns>
        public int IniciarContratoVinculado(string NumeroSO, string Valor, int ProdutoId, int ContratoId)
        {
            var db = new Class1(true);
            var _Contrato = db.Contrato.First(x => x.ContratoId == ContratoId);
            ContratoNrc _contrato = new ContratoNrc();
            _contrato.ContratoId = _Contrato.ContratoId;
            _contrato.DataCadastro = DateTime.Now;
            _contrato.UsuarioId = _Contrato.UsuarioId;
            _contrato.Valor = Convert.ToDecimal(Valor);
            _contrato.NumeroSO = NumeroSO;
            _contrato.ProdutoId = ProdutoId;

            if (_contrato.ProdutoId == 7)
            {
                List<PropriedadeProduto> listaPropriedadeIP = new List<PropriedadeProduto>();
                var Produtos = db.Produto.First(x => x.ProdutoId == 7);
                if (Produtos.Propriedades != null)
                {
                    string[] prot = Produtos.Propriedades.Split('|');
                    foreach (var item in prot)
                    {
                        try
                        {
                            int pp = Convert.ToInt32(item);
                            var PropriedadeProduto = db.PropriedadeProduto.First(x => x.PropriedadeProdutoId == pp);
                            if (PropriedadeProduto.PropriedadeNome.ToUpper() == "QUANTIDADEIP")
                            {
                                PropriedadeProduto.ValorDigitado = "";
                                PropriedadeProduto.NumeroSO = NumeroSO;
                            }

                            listaPropriedadeIP.Add(PropriedadeProduto);

                        }
                        catch
                        {


                        }

                    }
                    _contrato.PropriedadesContrato = Newtonsoft.Json.JsonConvert.SerializeObject(listaPropriedadeIP);
                }
                else
                {
                    _contrato.PropriedadesContrato = "[]";
                }
            }
            if (_contrato.ProdutoId == 6)
            {
                _contrato.Exportado = true;
            }

            db.ContratoNrc.Add(_contrato);
            db.SaveChanges();
            MapaFaturamento _MapaFaturamento = new MapaFaturamento();
            _MapaFaturamento.ContratoNrcId = _contrato.ContratoNrcId;
            _MapaFaturamento.DataCriacao = DateTime.Now;
            _MapaFaturamento.CodigoFiscal = 0;
            _MapaFaturamento.Desconto = 0;
            _MapaFaturamento.MesParcelas = 1;
            _MapaFaturamento.MesValidade = 0;
            _MapaFaturamento.PlanoId = 174;
            db.MapaFaturamento.Add(_MapaFaturamento);
            db.SaveChanges();

            return _contrato.ContratoNrcId;
        }

        public ContratoNrc RetornaContratoVinculado(int ContratoNrcId)
        {
            var db = new Class1(true);
            var Vinculado = db.ContratoNrc.First(x => x.ContratoNrcId == ContratoNrcId);
            return Vinculado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mapa"></param>
        /// <returns></returns>
        public MapaFaturamento SalvaMapaFaturamento(MapaFaturamento Mapa)
        {
            var db = new Class1(true);

            db.MapaFaturamento.Add(Mapa);
            db.SaveChanges();

            return Mapa;

        }
        public void SalvaMapaDeFaturamento(FormCollection formulario)
        {
            var db = new Class1(true);
            MapaFaturamento mapa = new MapaFaturamento();
            var teste = new Formulario<MapaFaturamento>().LerFormulario(formulario, mapa);
            teste.ContratoNrcId = teste.ContratoId;
            teste.ContratoId = null;
            db.MapaFaturamento.Add(teste);
            db.SaveChanges();
            
        }
        public void DeleteContratoVinculado(int ContratoNrcId)
        {
            var db = new Class1(true);
            db.Contrato.FromSql("DELETE FROM dbo.\"ContratoRelac\" WHERE \"ContratoNrcId\" = " + ContratoNrcId.ToString() + ";");
            db.Contrato.FromSql("DELETE FROM dbo.\"MapaFaturamento\" WHERE \"ContratoNrcId\" = " + ContratoNrcId.ToString() + ";");
            db.Contrato.FromSql("DELETE FROM dbo.\"ContratoNrc\" WHERE \"ContratoNrcId\" = " + ContratoNrcId.ToString() + ";");

            db.SaveChanges();

        }

        public void AlterarDetalhesDoProduto(FormCollection formulario)
        {
            using (var context = new Class1(true))
            {
                List<PropriedadeProduto> Lst = new List<PropriedadeProduto>();
                int ContratoId = Convert.ToInt32(formulario["ContratoId"]);
                var Contrato = context.ContratoNrc.First(x => x.ContratoNrcId == ContratoId);
                Lst = Contrato._ListaPropriedade(Contrato.ContratoNrcId);
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

                        context.ContratoNrc.Update(Contrato);
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
    }
}
