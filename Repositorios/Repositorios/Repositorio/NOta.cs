using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Repositorio
{

    public class NotaUnica
    {
        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd", IsNullable = false)]
        public partial class ConsultarLoteRpsResposta
        {

            private ConsultarLoteRpsRespostaListaNfse listaNfseField;

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfse ListaNfse
            {
                get
                {
                    return this.listaNfseField;
                }
                set
                {
                    this.listaNfseField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfse
        {

            private ConsultarLoteRpsRespostaListaNfseCompNfse compNfseField;

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfse CompNfse
            {
                get
                {
                    return this.compNfseField;
                }
                set
                {
                    this.compNfseField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfse
        {

            private ConsultarLoteRpsRespostaListaNfseCompNfseNfse nfseField;

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfseNfse Nfse
            {
                get
                {
                    return this.nfseField;
                }
                set
                {
                    this.nfseField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfseNfse
        {

            private ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfse infNfseField;

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfse InfNfse
            {
                get
                {
                    return this.infNfseField;
                }
                set
                {
                    this.infNfseField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfse
        {

            private ushort numeroField;

            private string codigoVerificacaoField;

            private System.DateTime dataEmissaoField;

            private ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseIdentificacaoRps identificacaoRpsField;

            private System.DateTime dataEmissaoRpsField;

            private byte naturezaOperacaoField;

            private byte optanteSimplesNacionalField;

            private byte incentivadorCulturalField;

            private System.DateTime competenciaField;

            private ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseServico servicoField;

            private ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfsePrestadorServico prestadorServicoField;

            private ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseTomadorServico tomadorServicoField;

            private ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseOrgaoGerador orgaoGeradorField;

            /// <remarks/>
            public ushort Numero
            {
                get
                {
                    return this.numeroField;
                }
                set
                {
                    this.numeroField = value;
                }
            }

            /// <remarks/>
            public string CodigoVerificacao
            {
                get
                {
                    return this.codigoVerificacaoField;
                }
                set
                {
                    this.codigoVerificacaoField = value;
                }
            }

            /// <remarks/>
            public System.DateTime DataEmissao
            {
                get
                {
                    return this.dataEmissaoField;
                }
                set
                {
                    this.dataEmissaoField = value;
                }
            }

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseIdentificacaoRps IdentificacaoRps
            {
                get
                {
                    return this.identificacaoRpsField;
                }
                set
                {
                    this.identificacaoRpsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
            public System.DateTime DataEmissaoRps
            {
                get
                {
                    return this.dataEmissaoRpsField;
                }
                set
                {
                    this.dataEmissaoRpsField = value;
                }
            }

            /// <remarks/>
            public byte NaturezaOperacao
            {
                get
                {
                    return this.naturezaOperacaoField;
                }
                set
                {
                    this.naturezaOperacaoField = value;
                }
            }

            /// <remarks/>
            public byte OptanteSimplesNacional
            {
                get
                {
                    return this.optanteSimplesNacionalField;
                }
                set
                {
                    this.optanteSimplesNacionalField = value;
                }
            }

            /// <remarks/>
            public byte IncentivadorCultural
            {
                get
                {
                    return this.incentivadorCulturalField;
                }
                set
                {
                    this.incentivadorCulturalField = value;
                }
            }

            /// <remarks/>
            public System.DateTime Competencia
            {
                get
                {
                    return this.competenciaField;
                }
                set
                {
                    this.competenciaField = value;
                }
            }

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseServico Servico
            {
                get
                {
                    return this.servicoField;
                }
                set
                {
                    this.servicoField = value;
                }
            }

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfsePrestadorServico PrestadorServico
            {
                get
                {
                    return this.prestadorServicoField;
                }
                set
                {
                    this.prestadorServicoField = value;
                }
            }

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseTomadorServico TomadorServico
            {
                get
                {
                    return this.tomadorServicoField;
                }
                set
                {
                    this.tomadorServicoField = value;
                }
            }

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseOrgaoGerador OrgaoGerador
            {
                get
                {
                    return this.orgaoGeradorField;
                }
                set
                {
                    this.orgaoGeradorField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseIdentificacaoRps
        {

            private uint numeroField;

            private string serieField;

            private byte tipoField;

            /// <remarks/>
            public uint Numero
            {
                get
                {
                    return this.numeroField;
                }
                set
                {
                    this.numeroField = value;
                }
            }

            /// <remarks/>
            public string Serie
            {
                get
                {
                    return this.serieField;
                }
                set
                {
                    this.serieField = value;
                }
            }

            /// <remarks/>
            public byte Tipo
            {
                get
                {
                    return this.tipoField;
                }
                set
                {
                    this.tipoField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseServico
        {

            private ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseServicoValores valoresField;

            private byte itemListaServicoField;

            private ushort codigoTributacaoMunicipioField;

            private string discriminacaoField;

            private uint codigoMunicipioField;

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseServicoValores Valores
            {
                get
                {
                    return this.valoresField;
                }
                set
                {
                    this.valoresField = value;
                }
            }

            /// <remarks/>
            public byte ItemListaServico
            {
                get
                {
                    return this.itemListaServicoField;
                }
                set
                {
                    this.itemListaServicoField = value;
                }
            }

            /// <remarks/>
            public ushort CodigoTributacaoMunicipio
            {
                get
                {
                    return this.codigoTributacaoMunicipioField;
                }
                set
                {
                    this.codigoTributacaoMunicipioField = value;
                }
            }

            /// <remarks/>
            public string Discriminacao
            {
                get
                {
                    return this.discriminacaoField;
                }
                set
                {
                    this.discriminacaoField = value;
                }
            }

            /// <remarks/>
            public uint CodigoMunicipio
            {
                get
                {
                    return this.codigoMunicipioField;
                }
                set
                {
                    this.codigoMunicipioField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseServicoValores
        {

            private decimal valorServicosField;

            private byte issRetidoField;

            private decimal valorIssField;

            private decimal baseCalculoField;

            private decimal aliquotaField;

            private decimal valorLiquidoNfseField;

            /// <remarks/>
            public decimal ValorServicos
            {
                get
                {
                    return this.valorServicosField;
                }
                set
                {
                    this.valorServicosField = value;
                }
            }

            /// <remarks/>
            public byte IssRetido
            {
                get
                {
                    return this.issRetidoField;
                }
                set
                {
                    this.issRetidoField = value;
                }
            }

            /// <remarks/>
            public decimal ValorIss
            {
                get
                {
                    return this.valorIssField;
                }
                set
                {
                    this.valorIssField = value;
                }
            }

            /// <remarks/>
            public decimal BaseCalculo
            {
                get
                {
                    return this.baseCalculoField;
                }
                set
                {
                    this.baseCalculoField = value;
                }
            }

            /// <remarks/>
            public decimal Aliquota
            {
                get
                {
                    return this.aliquotaField;
                }
                set
                {
                    this.aliquotaField = value;
                }
            }

            /// <remarks/>
            public decimal ValorLiquidoNfse
            {
                get
                {
                    return this.valorLiquidoNfseField;
                }
                set
                {
                    this.valorLiquidoNfseField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfsePrestadorServico
        {

            private ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfsePrestadorServicoIdentificacaoPrestador identificacaoPrestadorField;

            private string razaoSocialField;

            private string nomeFantasiaField;

            private ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfsePrestadorServicoEndereco enderecoField;

            private ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfsePrestadorServicoContato contatoField;

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfsePrestadorServicoIdentificacaoPrestador IdentificacaoPrestador
            {
                get
                {
                    return this.identificacaoPrestadorField;
                }
                set
                {
                    this.identificacaoPrestadorField = value;
                }
            }

            /// <remarks/>
            public string RazaoSocial
            {
                get
                {
                    return this.razaoSocialField;
                }
                set
                {
                    this.razaoSocialField = value;
                }
            }

            /// <remarks/>
            public string NomeFantasia
            {
                get
                {
                    return this.nomeFantasiaField;
                }
                set
                {
                    this.nomeFantasiaField = value;
                }
            }

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfsePrestadorServicoEndereco Endereco
            {
                get
                {
                    return this.enderecoField;
                }
                set
                {
                    this.enderecoField = value;
                }
            }

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfsePrestadorServicoContato Contato
            {
                get
                {
                    return this.contatoField;
                }
                set
                {
                    this.contatoField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfsePrestadorServicoIdentificacaoPrestador
        {

            private ulong cnpjField;

            private uint inscricaoMunicipalField;

            /// <remarks/>
            public ulong Cnpj
            {
                get
                {
                    return this.cnpjField;
                }
                set
                {
                    this.cnpjField = value;
                }
            }

            /// <remarks/>
            public uint InscricaoMunicipal
            {
                get
                {
                    return this.inscricaoMunicipalField;
                }
                set
                {
                    this.inscricaoMunicipalField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfsePrestadorServicoEndereco
        {

            private string enderecoField;

            private byte numeroField;

            private string complementoField;

            private string bairroField;

            private uint codigoMunicipioField;

            private string ufField;

            private uint cepField;

            /// <remarks/>
            public string Endereco
            {
                get
                {
                    return this.enderecoField;
                }
                set
                {
                    this.enderecoField = value;
                }
            }

            /// <remarks/>
            public byte Numero
            {
                get
                {
                    return this.numeroField;
                }
                set
                {
                    this.numeroField = value;
                }
            }

            /// <remarks/>
            public string Complemento
            {
                get
                {
                    return this.complementoField;
                }
                set
                {
                    this.complementoField = value;
                }
            }

            /// <remarks/>
            public string Bairro
            {
                get
                {
                    return this.bairroField;
                }
                set
                {
                    this.bairroField = value;
                }
            }

            /// <remarks/>
            public uint CodigoMunicipio
            {
                get
                {
                    return this.codigoMunicipioField;
                }
                set
                {
                    this.codigoMunicipioField = value;
                }
            }

            /// <remarks/>
            public string Uf
            {
                get
                {
                    return this.ufField;
                }
                set
                {
                    this.ufField = value;
                }
            }

            /// <remarks/>
            public uint Cep
            {
                get
                {
                    return this.cepField;
                }
                set
                {
                    this.cepField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfsePrestadorServicoContato
        {

            private uint telefoneField;

            private string emailField;

            /// <remarks/>
            public uint Telefone
            {
                get
                {
                    return this.telefoneField;
                }
                set
                {
                    this.telefoneField = value;
                }
            }

            /// <remarks/>
            public string Email
            {
                get
                {
                    return this.emailField;
                }
                set
                {
                    this.emailField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseTomadorServico
        {

            private ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseTomadorServicoIdentificacaoTomador identificacaoTomadorField;

            private string razaoSocialField;

            private ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseTomadorServicoEndereco enderecoField;

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseTomadorServicoIdentificacaoTomador IdentificacaoTomador
            {
                get
                {
                    return this.identificacaoTomadorField;
                }
                set
                {
                    this.identificacaoTomadorField = value;
                }
            }

            /// <remarks/>
            public string RazaoSocial
            {
                get
                {
                    return this.razaoSocialField;
                }
                set
                {
                    this.razaoSocialField = value;
                }
            }

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseTomadorServicoEndereco Endereco
            {
                get
                {
                    return this.enderecoField;
                }
                set
                {
                    this.enderecoField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseTomadorServicoIdentificacaoTomador
        {

            private ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseTomadorServicoIdentificacaoTomadorCpfCnpj cpfCnpjField;

            private uint inscricaoMunicipalField;

            /// <remarks/>
            public ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseTomadorServicoIdentificacaoTomadorCpfCnpj CpfCnpj
            {
                get
                {
                    return this.cpfCnpjField;
                }
                set
                {
                    this.cpfCnpjField = value;
                }
            }

            /// <remarks/>
            public uint InscricaoMunicipal
            {
                get
                {
                    return this.inscricaoMunicipalField;
                }
                set
                {
                    this.inscricaoMunicipalField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseTomadorServicoIdentificacaoTomadorCpfCnpj
        {

            private ulong cnpjField;

            /// <remarks/>
            public ulong Cnpj
            {
                get
                {
                    return this.cnpjField;
                }
                set
                {
                    this.cnpjField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseTomadorServicoEndereco
        {

            private string enderecoField;

            private byte numeroField;

            private string complementoField;

            private string bairroField;

            private uint codigoMunicipioField;

            private string ufField;

            private uint cepField;

            /// <remarks/>
            public string Endereco
            {
                get
                {
                    return this.enderecoField;
                }
                set
                {
                    this.enderecoField = value;
                }
            }

            /// <remarks/>
            public byte Numero
            {
                get
                {
                    return this.numeroField;
                }
                set
                {
                    this.numeroField = value;
                }
            }

            /// <remarks/>
            public string Complemento
            {
                get
                {
                    return this.complementoField;
                }
                set
                {
                    this.complementoField = value;
                }
            }

            /// <remarks/>
            public string Bairro
            {
                get
                {
                    return this.bairroField;
                }
                set
                {
                    this.bairroField = value;
                }
            }

            /// <remarks/>
            public uint CodigoMunicipio
            {
                get
                {
                    return this.codigoMunicipioField;
                }
                set
                {
                    this.codigoMunicipioField = value;
                }
            }

            /// <remarks/>
            public string Uf
            {
                get
                {
                    return this.ufField;
                }
                set
                {
                    this.ufField = value;
                }
            }

            /// <remarks/>
            public uint Cep
            {
                get
                {
                    return this.cepField;
                }
                set
                {
                    this.cepField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.abrasf.org.br/ABRASF/arquivos/nfse.xsd")]
        public partial class ConsultarLoteRpsRespostaListaNfseCompNfseNfseInfNfseOrgaoGerador
        {

            private uint codigoMunicipioField;

            private string ufField;

            /// <remarks/>
            public uint CodigoMunicipio
            {
                get
                {
                    return this.codigoMunicipioField;
                }
                set
                {
                    this.codigoMunicipioField = value;
                }
            }

            /// <remarks/>
            public string Uf
            {
                get
                {
                    return this.ufField;
                }
                set
                {
                    this.ufField = value;
                }
            }
        }
    }
}