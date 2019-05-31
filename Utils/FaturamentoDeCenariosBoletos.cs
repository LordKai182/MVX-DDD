using Impactro.Cobranca;
using Impactro.Layout;
using Infra.Entidades;
using System;
using System.Linq;

namespace Utils
{
    public class FaturamentoDeCenariosBoletos
    {
        public BoletoInfo RetornaBoletoRegra(string NossoNumero, string Valor, string Vencimento, int n, decimal ValorBoleto, decimal Desconto, DateTime DataValidade)
        {
            BoletoInfo Boleto = new BoletoInfo();
            Boleto.BoletoID = n;
            Boleto.NossoNumero = (Int32.Parse(NossoNumero) + n).ToString();
            Boleto.NumeroDocumento = (Int32.Parse(NossoNumero) + n).ToString();
            Boleto.ValorDocumento = double.Parse((ValorBoleto - Desconto).ToString());
            Boleto.DataDocumento = DateTime.Now;
            Boleto.DataVencimento = DateTime.Parse(Vencimento);
            Boleto.Instrucoes = "Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente";

            // outros campos opcionais
            Boleto.ValorMora = (Boleto.ValorDocumento / 100 * 0.033); // Vale lembrar que o juros pode ser tão pequeno que as vezes pode sair como isento
            Boleto.PercentualMulta = 0.02;
            Boleto.ValorDesconto = (double)Desconto;
            Boleto.DataDesconto = DateTime.Now;
            //Boleto.ValorOutras = -n; // abatimentos 
            Boleto.Instrucao1 = 6; // Protestar
            Boleto.Instrucao2 = 7; // Depois de 7 dias do vencimento

            // No bRadesco não é usado o campo Comando, mas outros bancos podem usar
            // Boleto.Comando = 0;

            // As linhas a seguir customiza qualquer valor sem precisar usar o evento 'r.onRegBoleto' o que torna a implementação mais simples
            // A forma mais pratica e segura é sempre usar os enumeradores
            // Mas é possivel usar as duas opções como neste exemplo, mas os valores personalizados tem sempre prioridade pois são inserridos por ultimo apos todos calculos, e processamento de eventos, portanto use com cuidado!
            Boleto.SetRegEnumValue(CNAB400Remessa1Sicredi.TipoJuros, "B");    // (posição 19) // Apenas se atente para a diferença do nome para SetRegEnumValue()
            Boleto.SetRegKeyValue("CNAB400Remessa1Sicredi.Alteracao", "E");   // (posição 71) // É possivel adicionar o nome e valor do enumerador, isso é compativel com VB6
                                                                              // Cuidado ao deixar algo explicito diretamente: 
                                                                              // Boleto.SetRegKeyValue("Emissao", "B"); // posição 74 // ou simplesmente informar o nome do campo, mas cuidado pois há layouts que usam mais de um tipo de registro e as vezes tem nomes iguais mas as funções podem ser diferentes
            Boleto.SetRegEnumValue(CNAB400Remessa1Bradesco.Condicao, 1); // Apenas para Bradesco enviar o boleto para residencia

            return Boleto;
        }

        public SacadoInfo RetornaSacado(Cliente _cliente, Contrato Contrato)
        {
            SacadoInfo Sacado = new SacadoInfo();
            Sacado.Sacado = _cliente.RazaoSocial;
            Sacado.Documento = _cliente.CpfCnpj;
            Sacado.Endereco = _cliente._ClienteEndereco.First(x => x.TipoLogradouroId == 1).Logradouro;
            Sacado.Cidade = _cliente._ClienteEndereco.First(x => x.TipoLogradouroId == 1)._Bairro._Cidade.Nome;
            Sacado.Bairro = _cliente._ClienteEndereco.First(x => x.TipoLogradouroId == 1)._Bairro.Nome;
            Sacado.Cep = _cliente._ClienteEndereco.First(x => x.TipoLogradouroId == 1).Cep;
            Sacado.UF = _cliente._ClienteEndereco.First(x => x.TipoLogradouroId == 1)._Bairro._Cidade._Estado.Estadosigla;
            Sacado.Email = _cliente.Email;
            //Sacado.Avalista = "Avalista";

            return Sacado;
        }
        public SacadoInfo RetornaSacado(Cliente _cliente)
        {
            
            SacadoInfo Sacado = new SacadoInfo();
            Sacado.Sacado = _cliente.RazaoSocial;
            Sacado.Documento = _cliente.CpfCnpj;
            Sacado.Endereco = _cliente._ClienteEndereco.First(x => x.TipoLogradouroId == 2).Logradouro;
            Sacado.Cidade = _cliente._ClienteEndereco.First(x => x.TipoLogradouroId == 2)._Bairro._Cidade.Nome;
            Sacado.Bairro = _cliente._ClienteEndereco.First(x => x.TipoLogradouroId == 2)._Bairro.Nome;
            Sacado.Cep = _cliente._ClienteEndereco.First(x => x.TipoLogradouroId == 2).Cep;
            Sacado.UF = _cliente._ClienteEndereco.First(x => x.TipoLogradouroId == 2)._Bairro._Cidade._Estado.Estadosigla;
            Sacado.Email = _cliente.Email;
            
            //Sacado.Avalista = "Avalista";

            return Sacado;
        }
    }
}
