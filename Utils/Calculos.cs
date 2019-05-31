using Infra;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Calculos
    {

        //
        /// <summary>
        /// 
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="TipoCliente"></param>
        /// <param name="ValorCalculo"></param>
        /// <param name="Uf"></param>
        /// <param name="_PlanoId"></param>
        /// <returns></returns>
        public decimal CalculoConfazSemContrato(int TipoCliente, decimal ValorCalculo, string Uf, int _PlanoId)
        {
            decimal resultado = 0;
            var db = new Class1(true);
            foreach (var empresa in db.PlanoEmpresa.Where(x => x.PlanoId == _PlanoId))
            {
                if (empresa.Imposto == "ISS")
                {
                    resultado = resultado + CalculoConfazInstalcao(TipoCliente, (decimal)CalculoPorcentagem(empresa.Receita, (double)ValorCalculo), Uf);
                }
                if (empresa.Imposto.Contains("ICMS"))
                {
                    resultado = resultado + CalculaConfazPlanoEmpresaICMS(TipoCliente, (decimal)CalculoPorcentagem(empresa.Receita, (double)ValorCalculo), Uf);
                }
            }
            if (resultado == 0)
            {
                return ValorCalculo;
            }
            return resultado;
        }
        public decimal CalculaConfazPlanoEmpresaICMS(int TipoCliente, decimal ValorCalculo, string Uf)
        {
            try
            {
                decimal ValorContrato = ValorCalculo;
                int ClienteTipo = TipoCliente;
                if (ClienteTipo == 4)
                {//Com 
                    double pis = 0.65;
                    double confins = 3;

                    double percentual = (pis + confins);


                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorContrato));
                }
                if (ClienteTipo == 5)
                {//SEM

                    double pis = 0.65;
                    double confins = 3;
                    double icms = 25;
                    string UF = Uf;

                    if (UF == "RJ")
                    {
                        icms = 32;
                    }
                    if (UF == "SP")
                    {
                        icms = 25;
                    }
                    if (UF == "MG")
                    {
                        icms = 0;
                    }

                    double percentual = (pis + confins + icms);




                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorContrato));
                }
                if (ClienteTipo == 9)
                {//Com 
                    double pis = 0.65;
                    double confins = 3;
                    double rff = 1.5;
                    double percentual = (pis + confins + rff);


                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorContrato));
                }
                if (ClienteTipo == 10)
                {//SEM

                    double pis = 0.65;
                    double confins = 3;
                    double icms = 25;
                    double rff = 1.5;
                    string UF = Uf;

                    if (UF == "RJ")
                    {
                        icms = 32;
                    }
                    if (UF == "SP")
                    {
                        icms = 25;
                    }
                    if (UF == "MG")
                    {
                        icms = 0;
                    }

                    double percentual = (pis + confins + icms + rff);




                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorContrato));
                }
                return ValorContrato;
            }
            catch
            {

                return 0;
            }


        }
        public decimal CalculoConfazInstalcao(int TipoCliente, decimal ValorInstalacao, string Uf)
        {
            try
            {
                decimal ValorContrato = ValorInstalacao;
                int ClienteTipo = TipoCliente;
                if (ClienteTipo == 4)
                {//Com 
                    double pis = 0.65;
                    double confins = 3;
                    double iss = 5;
                    double percentual = (pis + confins + iss);


                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorInstalacao));
                }
                if (ClienteTipo == 5)
                {//SEM

                    double pis = 0.65;
                    double confins = 3;
                    double iss = 5;
                    string UF = Uf;

                    if (UF == "RJ")
                    {
                        iss = 5;
                    }
                    if (UF == "SP")
                    {
                        iss = 5;
                    }
                    if (UF == "MG")
                    {
                        iss = 5;
                    }

                    double percentual = (pis + confins + iss);




                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorInstalacao));
                }
                if (ClienteTipo == 9)
                {//Com 
                    double pis = 0.65;
                    double confins = 3;
                    double iss = 5;
                    double Rff = 1.5;
                    double percentual = (pis + confins + iss + Rff);


                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorInstalacao));
                }
                if (ClienteTipo == 10)
                {//SEM

                    double pis = 0.65;
                    double confins = 3;
                    double iss = 5;
                    double Rff = 1.5;
                    string UF = Uf;

                    if (UF == "RJ")
                    {
                        iss = 5;
                    }
                    if (UF == "SP")
                    {
                        iss = 5;
                    }
                    if (UF == "MG")
                    {
                        iss = 5;
                    }

                    double percentual = (pis + confins + iss + Rff);




                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorInstalacao));
                }
                return ValorInstalacao;
            }
            catch
            {

                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoCliente"></param>
        /// <param name="ValorCalculo"></param>
        /// <param name="Uf"></param>
        /// <returns></returns>
        public decimal CalculoConfazSemContrato(int TipoCliente, decimal ValorCalculo, string Uf)
        {
            try
            {
                decimal ValorContrato = ValorCalculo;
                int ClienteTipo = TipoCliente;
                if (ClienteTipo == 4)
                {//Com 
                    double pis = 0.65;
                    double confins = 3;

                    double percentual = (pis + confins);


                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorContrato));
                }
                if (ClienteTipo == 5)
                {//SEM

                    double pis = 0.65;
                    double confins = 3;
                    double icms = 25;
                    string UF = Uf;

                    if (UF == "RJ")
                    {
                        icms = 32;
                    }
                    if (UF == "SP")
                    {
                        icms = 25;
                    }
                    if (UF == "MG")
                    {
                        icms = 0;
                    }

                    double percentual = (pis + confins + icms);




                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorContrato));
                }
                if (ClienteTipo == 9)
                {//Com 
                    double pis = 0.65;
                    double confins = 3;
                    double rff = 1.5;
                    double percentual = (pis + confins + rff);


                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorContrato));
                }
                if (ClienteTipo == 10)
                {//SEM

                    double pis = 0.65;
                    double confins = 3;
                    double icms = 25;
                    double rff = 1.5;
                    string UF = Uf;

                    if (UF == "RJ")
                    {
                        icms = 32;
                    }
                    if (UF == "SP")
                    {
                        icms = 25;
                    }
                    if (UF == "MG")
                    {
                        icms = 0;
                    }

                    double percentual = (pis + confins + icms + rff);




                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorContrato));
                }
                return ValorContrato;
            }
            catch
            {

                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Contrato"></param>
        /// <param name="ValorCalculo"></param>
        /// <returns></returns>
        public decimal CalculoConfaz(Contrato _Contrato, decimal ValorCalculo)
        {
            try
            {
                decimal ValorContrato = ValorCalculo;
                int ClienteTipo = (int)_Contrato._MapaFaturamento.First().TipoClienteFaturamentoId;
                if (ClienteTipo == 4)
                {//Com 
                    double pis = 0.65;
                    double confins = 3;

                    double percentual = (pis + confins);


                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorContrato));
                }
                if (ClienteTipo == 5)
                {//SEM

                    double pis = 0.65;
                    double confins = 3;
                    double icms = 25;
                    string UF = _Contrato._InstalacaoEndereco.First(x => x.TipoLogradouroId == 1)._Bairro._Cidade._Estado.Estadosigla;

                    if (UF == "RJ")
                    {
                        icms = 32;
                    }
                    if (UF == "SP")
                    {
                        icms = 25;
                    }
                    if (UF == "MG")
                    {
                        icms = 0;
                    }

                    double percentual = (pis + confins + icms);




                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorContrato));
                }
                if (ClienteTipo == 9)
                {//Com 
                    double pis = 0.65;
                    double confins = 3;
                    double Rff = 1.5;
                    double percentual = (pis + confins + Rff);


                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorContrato));
                }
                if (ClienteTipo == 10)
                {//SEM

                    double pis = 0.65;
                    double confins = 3;
                    double icms = 25;
                    double Rff = 1.5;
                    string UF = _Contrato._InstalacaoEndereco.First(x => x.TipoLogradouroId == 1)._Bairro._Cidade._Estado.Estadosigla;

                    if (UF == "RJ")
                    {
                        icms = 32;
                    }
                    if (UF == "SP")
                    {
                        icms = 25;
                    }
                    if (UF == "MG")
                    {
                        icms = 0;
                    }

                    double percentual = (pis + confins + icms + Rff);




                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorContrato));
                }
                return ValorContrato;
            }
            catch
            {

                return 0;
            }
        }
        /// <summary>
        /// CALCULA O CONFAZ
        /// </summary>
        /// <param name="_Contrato"></param>
        /// <returns></returns>
        public decimal CalculoConfaz(Contrato _Contrato)
        {
            try
            {
                decimal ValorContrato = _Contrato.Valor;
                int ClienteTipo = (int)_Contrato._MapaFaturamento.First().TipoClienteFaturamentoId;
                if (ClienteTipo == 4)
                {//Com 
                    double pis = 0.65;
                    double confins = 3;

                    double percentual = (pis + confins);


                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(_Contrato.Valor));
                }
                if (ClienteTipo == 5)
                {//SEM

                    double pis = 0.65;
                    double confins = 3;
                    double icms = 25;
                    string UF = _Contrato._InstalacaoEndereco.First(x => x.TipoLogradouroId == 1)._Bairro._Cidade._Estado.Estadosigla;

                    if (UF == "RJ")
                    {
                        icms = 32;
                    }
                    if (UF == "SP")
                    {
                        icms = 25;
                    }
                    if (UF == "MG")
                    {
                        icms = 0;
                    }

                    double percentual = (pis + confins + icms);




                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(_Contrato.Valor));
                }
                if (ClienteTipo == 9)
                {//Com 
                    double pis = 0.65;
                    double confins = 3;
                    double Rff = 1.5;
                    double percentual = (pis + confins + Rff);


                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(_Contrato.Valor));
                }
                if (ClienteTipo == 10)
                {//SEM

                    double pis = 0.65;
                    double confins = 3;
                    double icms = 25;
                    double Rff = 1.5;
                    string UF = _Contrato._InstalacaoEndereco.First(x => x.TipoLogradouroId == 1)._Bairro._Cidade._Estado.Estadosigla;

                    if (UF == "RJ")
                    {
                        icms = 32;
                    }
                    if (UF == "SP")
                    {
                        icms = 25;
                    }
                    if (UF == "MG")
                    {
                        icms = 0;
                    }

                    double percentual = (pis + confins + icms + Rff);




                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(_Contrato.Valor));
                }
                return _Contrato.Valor;
            }
            catch
            {

                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Contrato"></param>
        /// <param name="ValorInstalacao"></param>
        /// <returns></returns>
        public decimal CalculoConfazInstalcao(Contrato _Contrato, decimal ValorInstalacao)
        {
            try
            {
                decimal ValorContrato = ValorInstalacao;
                int ClienteTipo = (int)_Contrato._MapaFaturamento.First().TipoClienteFaturamentoId;
                if (ClienteTipo == 4)
                {//Com 
                    double pis = 0.65;
                    double confins = 3;
                    double iss = 5;
                    double percentual = (pis + confins + iss);


                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorInstalacao));
                }
                if (ClienteTipo == 5)
                {//SEM

                    double pis = 0.65;
                    double confins = 3;
                    double iss = 5;
                    string UF = _Contrato._InstalacaoEndereco.First(x => x.TipoLogradouroId == 1)._Bairro._Cidade._Estado.Estadosigla;

                    if (UF == "RJ")
                    {
                        iss = 5;
                    }
                    if (UF == "SP")
                    {
                        iss = 5;
                    }
                    if (UF == "MG")
                    {
                        iss = 5;
                    }

                    double percentual = (pis + confins + iss);




                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorInstalacao));
                }
                if (ClienteTipo == 9)
                {//Com 
                    double pis = 0.65;
                    double confins = 3;
                    double iss = 5;
                    double Rff = 1.5;
                    double percentual = (pis + confins + iss + Rff);


                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorInstalacao));
                }
                if (ClienteTipo == 10)
                {//SEM

                    double pis = 0.65;
                    double confins = 3;
                    double iss = 5;
                    double Rff = 1.5;
                    string UF = _Contrato._InstalacaoEndereco.First(x => x.TipoLogradouroId == 1)._Bairro._Cidade._Estado.Estadosigla;

                    if (UF == "RJ")
                    {
                        iss = 5;
                    }
                    if (UF == "SP")
                    {
                        iss = 5;
                    }
                    if (UF == "MG")
                    {
                        iss = 5;
                    }

                    double percentual = (pis + confins + iss + Rff);




                    return (decimal)CalculoPorcentagemDesconto(percentual, Convert.ToDouble(ValorInstalacao));
                }
                return ValorInstalacao;
            }
            catch
            {

                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="valorAliquota"></param>
        /// <param name="valorItem"></param>
        /// <param name="qtditem"></param>
        /// <param name="A_vICMS"></param>
        /// <param name="A_pICMS"></param>
        public void CalculaICMS(decimal valorAliquota, decimal valorItem, decimal qtditem, out decimal A_vICMS, out string A_pICMS)
        {
            double vAliquota = Convert.ToDouble(valorAliquota.ToString().Replace(",", "."));

            A_vICMS = Decimal.Round((decimal)((double)valorAliquota * (double)(Convert.ToDecimal(valorItem.ToString().Replace('.', ',')) * Convert.ToDecimal(qtditem.ToString().Replace('.', ',')) / 100)), 2);
            A_pICMS = vAliquota.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="porcentagem"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public double CalculoPorcentagem(double porcentagem, double valor)
        {
            return ((double)porcentagem / 100) * valor;
        }
        public double CalculoPorcentagemComConfaz(double porcentagem, double valor, string imposto, int TipoFaturamento, string Uf)
        {
            double Valor = ((double)porcentagem / 100) * valor;
            if (imposto == "ISS")
            {
                return (double)CalculoConfazInstalcao(TipoFaturamento, (decimal)Valor, Uf);
            }
            if (imposto.Contains("ICMS"))
            {
                return (double)CalculoConfazSemContrato(TipoFaturamento, (decimal)Valor, Uf);
            }

            return Valor;
        }
        public double CalculoPorcentagemComConfazBoleto(double porcentagem, double valor, string imposto, int TipoFaturamento, string Uf)
        {
            double Valor = ((double)porcentagem / 100) * valor;

            return Valor;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="porcentagem"></param>
        /// <param name="Valor"></param>
        /// <returns></returns>
        public double CalculoPorcentagemDesconto(double porcentagem, double Valor)
        {

            double valor = Valor;
            double percentual = porcentagem / 100.0;
            double valor_final = valor - (percentual * valor);

            return valor_final;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ValorFormatado"></param>
        private static void FormatValorParaXML(decimal value, out string ValorFormatado)
        {
            string valueRetorno = string.Empty;
            try
            {
                //1º RETIRAR CASAS DECIMAIS
                decimal numero = Decimal.Round(Decimal.Zero, 2);
                numero = Decimal.Floor(value);
                valueRetorno = numero.ToString();

                //2º PEGAR DIZIMA 0,95000
                decimal dizima = (numero - value) * -1;
                if (dizima > 0)
                {
                    string dizimaString = dizima.ToString();
                    dizimaString = (dizimaString.Substring(dizimaString.IndexOf(','), dizimaString.Length - dizimaString.IndexOf(','))).Replace(",", "");
                    switch (dizimaString.Length)
                    {
                        case 1:
                            dizimaString = "." + dizimaString + "0";
                            break;
                        case 2:
                            dizimaString = "." + dizimaString;
                            break;
                        case 3:
                            dizimaString = "." + dizimaString.Substring(0, 2);
                            break;
                        case 4:
                            dizimaString = "." + dizimaString.Substring(0, 2);
                            break;
                        case 5:
                            dizimaString = "." + dizimaString.Substring(0, 2);
                            break;
                        case 6:
                            dizimaString = "." + dizimaString.Substring(0, 2);
                            break;
                        case 7:
                            dizimaString = "." + dizimaString.Substring(0, 2);
                            break;
                        case 8:
                            dizimaString = "." + dizimaString.Substring(0, 2);
                            break;
                        case 9:
                            dizimaString = "." + dizimaString.Substring(0, 2);
                            break;
                        case 10:
                            dizimaString = "." + dizimaString.Substring(0, 2);
                            break;
                        default:
                            break;
                    }
                    valueRetorno += dizimaString;
                }
                else
                    valueRetorno += ".00";
            }
            catch (Exception ex)
            {

            }
            ValorFormatado = valueRetorno;
        }


        public double MostrarPorcentagem(double prorrata, double valor)
        {
            return (100 - (valor - prorrata) / valor * 100);
        }
        public double MostrarPorcentagemB(double prorrata, double valor)
        {
            return ((prorrata - valor) / valor * 100);
        }


    }
}
