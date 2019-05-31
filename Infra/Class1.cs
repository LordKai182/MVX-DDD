using Infra.Entidades;
using Infra.EntidadesApi;
using Infra.EntidadesMundidata;
using Infra.EntitdadesSolicita;
using Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infra
{
    public class Class1 : DbContext
    {
        private NpgsqlConnectionStringBuilder Conn = new NpgsqlConnectionStringBuilder();
        //
        private NpgsqlConnectionStringBuilder RetornaConexao(bool Producao, string Banco)
        {
            NpgsqlConnectionStringBuilder sqlBuilder = new NpgsqlConnectionStringBuilder();
            if (Producao)
            {
                if (Banco == "api")
                {
                    sqlBuilder = new NpgsqlConnectionStringBuilder
                    {
                        Host = "192.168.0.18",
                        Database = "MvxApi",
                        Username = "postgres",
                        Password = "discovoador",
                        Pooling = false,
                        //Encoding = "windows-1252",
                        //ClientEncoding = "sql-ascii",
                        ConvertInfinityDateTime = true,


                    };
                }
                    if (Banco == "solicita")
                {
                    sqlBuilder = new NpgsqlConnectionStringBuilder
                    {
                        Host = "192.168.0.18",
                        Database = "solicita",
                        Username = "postgres",
                        Password = "discovoador",
                        Pooling = false,
                        //Encoding = "windows-1252",
                        //ClientEncoding = "sql-ascii",
                        ConvertInfinityDateTime = true,


                    };
                }
                if (Banco == "mundidata")
                {
                    sqlBuilder = new NpgsqlConnectionStringBuilder
                    {
                        Host = "192.168.0.91",
                        Database = "mundidata",
                        Username = "postgres",
                        Password = "A!S@D#F$",
                        Pooling = false,
                        Encoding = "windows-1252",
                        ClientEncoding = "sql-ascii",
                        ConvertInfinityDateTime = true,


                    };
                }
                if (Banco == "mundidata_sp")
                {
                    sqlBuilder = new NpgsqlConnectionStringBuilder
                    {
                        Host = "192.168.0.91",
                        Database = "mundidata_sp",
                        Username = "postgres",
                        Password = "A!S@D#F$",
                        Pooling = false,
                        //Encoding = "windows-1252",
                        //ClientEncoding = "sql-ascii",
                        ConvertInfinityDateTime = true,


                    };
                }
                if (Banco == "mundidata_bh")
                {
                    sqlBuilder = new NpgsqlConnectionStringBuilder
                    {
                        Host = "192.168.0.91",
                        Database = "mundidata_sp",
                        Username = "postgres",
                        Password = "A!S@D#F$",
                        Pooling = false,
                        //Encoding = "windows-1252",
                        //ClientEncoding = "sql-ascii",
                        ConvertInfinityDateTime = true,


                    };
                }
            }
            if (!Producao)
            {
                if (Banco == "solicita")
                {
                    sqlBuilder = new NpgsqlConnectionStringBuilder
                    {
                        Host = "192.168.0.18",
                        Database = "solicita",
                        Username = "postgres",
                        Password = "discovoador",
                        Pooling = false,
                        //Encoding = "windows-1252",
                        //ClientEncoding = "sql-ascii",
                        ConvertInfinityDateTime = true,


                    };
                }
                if (Banco == "mundidata")
                {
                    sqlBuilder = new NpgsqlConnectionStringBuilder
                    {
                        Host = "192.168.0.91",
                        Database = "mundidata",
                        Username = "postgres",
                        Password = "A!S@D#F$",
                        Pooling = false,
                        //Encoding = "windows-1252",
                        //ClientEncoding = "sql-ascii",
                        ConvertInfinityDateTime = true,


                    };
                }
                if (Banco == "mundidata_sp")
                {
                    sqlBuilder = new NpgsqlConnectionStringBuilder
                    {
                        Host = "192.168.0.91",
                        Database = "mundidata_sp",
                        Username = "postgres",
                        Password = "A!S@D#F$",
                        Pooling = false,
                        //Encoding = "windows-1252",
                        //ClientEncoding = "sql-ascii",
                        ConvertInfinityDateTime = true,


                    };
                }
                if (Banco == "mundidata_bh")
                {
                    sqlBuilder = new NpgsqlConnectionStringBuilder
                    {
                        Host = "192.168.0.91",
                        Database = "mundidata_sp",
                        Username = "postgres",
                        Password = "A!S@D#F$",
                        Pooling = false,
                        //Encoding = "windows-1252",
                        //ClientEncoding = "sql-ascii",
                        ConvertInfinityDateTime = true,


                    };
                }
            }

            return sqlBuilder;
        }
        private NpgsqlConnectionStringBuilder RetornaConexao(bool Producao)
        {
            NpgsqlConnectionStringBuilder sqlBuilder = new NpgsqlConnectionStringBuilder();
            if (Producao)
            {
                 sqlBuilder = new NpgsqlConnectionStringBuilder
                {
                     Host = "127.0.0.1",
                     Database = "Mund_BR",
                     Username = "postgres",
                     Password = "discovoador",
                     Pooling = false,
                    //Encoding = "windows-1252",
                    //ClientEncoding = "sql-ascii",
                    ConvertInfinityDateTime = true,


                };
            }
            if (!Producao)
            {
                sqlBuilder = new NpgsqlConnectionStringBuilder
                {
                    Host = "192.168.0.18",
                    Database = "Mund_BR",
                    Username = "postgres",
                    Password = "discovoador",
                    Pooling = false,
                    Encoding = "windows-1252",
                    ClientEncoding = "sql-ascii",
                    ConvertInfinityDateTime = true,

                };
            }

            return sqlBuilder;
        }
        public Class1(bool Producao)
            : base()
        {
            Conn = RetornaConexao(Producao);
        }

        public Class1(bool Producao, string Banco)
         : base()
        {
            Conn = RetornaConexao(Producao,Banco);
        }


        public DbSet<ApiFuncionario> ApiFuncionario { get; set; }
        public DbSet<ApiUsuario> ApiUsuario { get; set; }

        #region DBSET MUNDIDATA

        public DbSet<mvx_empresas_contatos> mvx_empresas_contatos { get; set; }
        public DbSet<financeiro_painel_controle> financeiro_painel_controle { get; set; }
        public DbSet<comercial_tipo_mvx> comercial_tipo_mvx { get; set; }
        public DbSet<mvx_empresas> mvx_empresas { get; set; }
        public DbSet<mvx_funcionarios> mvx_funcionarios { get; set; }
        public DbSet<mvx_empresas_servicos> mvx_empresas_servicos { get; set; }
        public DbSet<mvx_empresas_dados_atuais> mvx_empresas_dados_atuais { get; set; }
        public DbSet<comercial_tipo_servicos> comercial_tipo_servicos { get; set; }
        public DbSet<comercial_tipo_contratacao> comercial_tipo_contratacao { get; set; }
        public DbSet<mvx_empresas_historico_endereco> mvx_empresas_historico_endereco { get; set; }

        public DbSet<comercial_hist_st_empresas_serv> comercial_hist_st_empresas_serv { get; set; }



        #endregion

        #region DBSETS

        public DbSet<EventosDeContrato> EventosDeContrato { get; set; }
        public DbSet<ContratoGrupo> ContratoGrupo { get; set; }
        public DbSet<LinhaCobranca> LinhaCobranca { get; set; }
        public DbSet<BoletoRemessa> BoletoRemessa { get; set; }
        public DbSet<PrediosRelacionamento> PrediosRelacionamento { get; set; }
        public DbSet<PlanoEmpresa> PlanoEmpresa { get; set; }
        public DbSet<contratos> ContratosSO { get; set; }
        public DbSet<p_classes> p_classes  { get; set; }
        public DbSet<p_servicos> p_servicos { get; set; }
        public DbSet<propostas> propostas { get; set; }
        public DbSet<propostas_enderecos> propostas_enderecos { get; set; }
        public DbSet<propostas_produtos> propostas_produtos { get; set; }
        public DbSet<propostas_produtos_resumo> propostas_produtos_resumo { get; set; }
        public DbSet<c_vencimentos> c_vencimentos { get; set; }
        public DbSet<Cfop> Cfop { get; set; }
        public DbSet<VendedorRelacionamento> VendedorRelacionamento { get; set; }
        public DbSet<ComercialTipoOperadora> ComercialTipoOperadora { get; set; }
        public DbSet<ContratoRelac> ContratoRelac { get; set; }
        public DbSet<ContratoNrc> ContratoNrc { get; set; }
        public DbSet<TipoClienteFaturamento> TipoClienteFaturamento { get; set; }
        public DbSet<ClientesRelacionamento> ClientesRelacionamento { get; set; }
        public DbSet<MapeamentoEvento> MapeamentoEvento { get; set; }
        public DbSet<BoletoHistorico> BoletoHistorico { get; set; }
        public DbSet<NfHistorico> NfHistorico { get; set; }
        public DbSet<ProdutoHistorico> ProdutoHistorico { get; set; }
        public DbSet<ContratoEvento> ContratoEvento { get; set; }
        public DbSet<NotaFiscalLote> NotaFiscalLote { get; set; }
        public DbSet<ContratoMovimentoFiscal> ContratoMovimentoFiscal { get; set; }
        public DbSet<ContratoStatus> ContratoStatus { get; set; }
       
        //public DbSet<PlanoEmpresa> PlanoEmpresa { get; set; }
        public DbSet<ContratoHistorico> ContratoHistorico { get; set; }
        public DbSet<LimiteDesconto> LimiteDesconto { get; set; }
    
        public DbSet<MapeamentoFiscal> MapeamentoFiscal { get; set; }
       
        public DbSet<EmpresaEndereco> EmpresaEndereco { get; set; }
        public DbSet<ProdutoMapeamento> ProdutoMapeamento { get; set; }
        public DbSet<Plano> Plano { get; set; }
        public DbSet<ClienteContato> ClienteContato { get; set; }
        public DbSet<Banco> Banco { get; set; }
        public DbSet<Bau> Bau { get; set; }
        public DbSet<Contrato> Contrato { get; set; }
        public DbSet<ClienteEndereco> ClienteEndereco { get; set; }
        public DbSet<BancoVencimento> BancoVencimento { get; set; }
        public DbSet<Boleto> Boleto { get; set; }
        //public DbSet<Cedente> Cedente { get; set; }
        public DbSet<CenarioArquivo> CenarioArquivos { get; set; }
        public DbSet<Cenario> Cenario { get; set; }
        public DbSet<PropriedadeProduto> PropriedadeProduto { get; set; }
        public DbSet<BaseCDR> BaseCDR { get; set; }
        public DbSet<BaseProbe> BaseProbe { get; set; }
        public DbSet<CDRData> CDRData { get; set; }
        public DbSet<TipoProduto> TipoProduto { get; set; }
        public DbSet<CDRDataProbe> CDRDataProbe { get; set; }
        public DbSet<MapaFaturamento> MapaFaturamento { get; set; }
        public DbSet<TipoVip> TipoVip { get; set; }
      
        public DbSet<NotaFiscal> NotaFiscal { get; set; }
        
        //public DbSet<nf_resposta> NfResposta { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<TipoPessoa> TipoPessoa { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Predio> Predio { get; set; }
        public DbSet<TipoLogradouro> TipoLogradouro { get; set; }
        public DbSet<Cliente> cliente { get; set; }
      
        public DbSet<StatusCliente> StatusCliente { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<SetorFuncionario> SetorFuncionario { get; set; }
        public DbSet<FuncionarioCargo> FuncionarioCargo { get; set; }//TIRA
        public DbSet<MapaFaturamentoComplemento> MapaFaturamentoComplemento { get; set; }//TIRA


        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #region TESTE
           
            string strConexao = Conn.ConnectionString;
            #endregion
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseNpgsql(strConexao);
        }
      
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiFuncionario>().HasKey(c => c.FuncionarioId);
            modelBuilder.Entity<ApiUsuario>().HasKey(c => c.UsuarioId);

            #region BIULDERS
            
            modelBuilder.ApplyConfiguration<EventosDeContrato>(new EventosDeContratoMap());
            modelBuilder.ApplyConfiguration<ContratoGrupo>(new ContratoGrupoMap());
            modelBuilder.ApplyConfiguration<LinhaCobranca>(new LinhaCobrancaMap());
            modelBuilder.ApplyConfiguration<BoletoRemessa>(new BoletoRemessaMap());
            modelBuilder.ApplyConfiguration<PrediosRelacionamento>(new PrediosRelacionamentoMap());
            modelBuilder.ApplyConfiguration<Cfop>(new CfopMap());
            modelBuilder.ApplyConfiguration<BaseCDR>(new BaseCDRMap());
            modelBuilder.ApplyConfiguration<BaseProbe>(new BaseProbeMap());
            modelBuilder.ApplyConfiguration<CDRData>(new CDRDataMap());
            modelBuilder.ApplyConfiguration<CDRDataProbe>(new CDRDataProbeMap());
            modelBuilder.ApplyConfiguration<Bairro>(new BairroMap());
            modelBuilder.ApplyConfiguration<Banco>(new BancoMap());
            modelBuilder.ApplyConfiguration<BancoVencimento>(new BancoVencimentoMap());
            modelBuilder.ApplyConfiguration<Bau>(new BauMap());
            modelBuilder.ApplyConfiguration<Boleto>(new BoletoMap());
            modelBuilder.ApplyConfiguration<BoletoHistorico>(new BoletoHistoricoMap());
            modelBuilder.ApplyConfiguration<Cedente>(new CedenteMap());
            modelBuilder.ApplyConfiguration<Cenario>(new CenarioMap());
            modelBuilder.ApplyConfiguration<CenarioArquivo>(new CenarioArquivoMap());
            modelBuilder.ApplyConfiguration<Cidade>(new CidadeMap());
            modelBuilder.ApplyConfiguration<Cliente>(new ClienteMap());
           // modelBuilder.ApplyConfiguration<ContratoGrupo>(new ContratoGrupoMap());
            modelBuilder.ApplyConfiguration<ClienteContato>(new ClienteContatoMap());
            modelBuilder.ApplyConfiguration<ClienteEndereco>(new ClienteEnderecoMap());
            modelBuilder.ApplyConfiguration<ComercialTipoOperadora>(new ComercialTipoOperadoraMap());
            modelBuilder.ApplyConfiguration<Contrato>(new ContratoMap());
            modelBuilder.ApplyConfiguration<ContratoEvento>(new ContratoEventoMap());
            modelBuilder.ApplyConfiguration<ContratoHistorico>(new ContratoHistoricoMap());
            modelBuilder.ApplyConfiguration<ContratoMovimentoFiscal>(new ContratoMovimentoFiscalMap());
            modelBuilder.ApplyConfiguration<ContratoRelac>(new ContratoRelacMap());
            modelBuilder.ApplyConfiguration<ContratoStatus>(new ContratoStatusMap());
            modelBuilder.ApplyConfiguration<ContratoNrc>(new ContratoNrcMap());
            modelBuilder.ApplyConfiguration<Empresa>(new EmpresaMap());
            modelBuilder.ApplyConfiguration<EmpresaEndereco>(new EmpresaEnderecoMap());
            modelBuilder.ApplyConfiguration<Estado>(new EstadoMap());
            modelBuilder.ApplyConfiguration<FuncionarioCargo>(new FuncionarioCargoMap());
            modelBuilder.ApplyConfiguration<MapaFaturamento>(new MapaFaturamentoMap());
            modelBuilder.ApplyConfiguration<MapeamentoEvento>(new MapeamentoEventoMap());
            modelBuilder.ApplyConfiguration<MapeamentoFiscal>(new MapeamentoFiscalMap());
            modelBuilder.ApplyConfiguration<NfHistorico>(new NfHistoricoMap());
            modelBuilder.ApplyConfiguration<NotaFiscal>(new NotaFiscalMap());
            modelBuilder.ApplyConfiguration<NotaFiscalLote>(new NotaFiscalLoteMap());
            modelBuilder.ApplyConfiguration<Plano>(new PlanoMap());
            modelBuilder.ApplyConfiguration<PlanoEmpresa>(new PlanoEmpresaMap());
            modelBuilder.ApplyConfiguration<Predio>(new PredioMap());
            modelBuilder.ApplyConfiguration<PredioEndereco>(new PredioEnderecoMap());
            modelBuilder.ApplyConfiguration<Produto>(new ProdutoMap());
            modelBuilder.ApplyConfiguration<ProdutoDetalhe>(new ProdutoDetalheMap());
            modelBuilder.ApplyConfiguration<ProdutoHistorico>(new ProdutoHistoricoMap());
            modelBuilder.ApplyConfiguration<ProdutoMapeamento>(new ProdutoMapeamentoMap());
            modelBuilder.ApplyConfiguration<PropriedadeProduto>(new PropriedadeProdutoMap());
            modelBuilder.ApplyConfiguration<SetorFuncionario>(new SetorFuncionarioMap());
            modelBuilder.ApplyConfiguration<StatusCliente>(new StatusClienteMap());
            modelBuilder.ApplyConfiguration<TipoClienteFaturamento>(new TipoClienteFaturamentoMap());
            modelBuilder.ApplyConfiguration<TipoLogradouro>(new TipoLogradouroMap());
            modelBuilder.ApplyConfiguration<TipoPessoa>(new TipoPessoaMap());
            modelBuilder.ApplyConfiguration<TipoVip>(new TipoVipMap());
            modelBuilder.ApplyConfiguration<Usuario>(new UsuarioMap());
            modelBuilder.ApplyConfiguration<MapaFaturamentoComplemento>(new MapaFaturamentoComplementoMap());
            #endregion

            #region MUNDIDATA

            modelBuilder.Entity<comercial_tipo_mvx>().HasKey(c => c.cod_tipo_mvx);
            modelBuilder.Entity<mvx_empresas>().HasKey(c => c.cod_empresa);
            modelBuilder.Entity<mvx_empresas_dados_atuais>().HasKey(c => c.cod_dado_atual);
            modelBuilder.Entity<mvx_empresas_servicos>().HasKey(c => c.cod_empresa_servico);
            modelBuilder.Entity<comercial_tipo_servicos>().HasKey(c => c.cod_tipo_servico);
            modelBuilder.Entity<comercial_tipo_contratacao>().HasKey(c => c.cod_tipo_contratacao);
            modelBuilder.Entity<comercial_hist_st_empresas_serv>().HasKey(c => c.cod_hist_empresa_serv);
            modelBuilder.Entity<mvx_funcionarios>().HasKey(c => c.cod_funcionario);
            modelBuilder.Entity<mvx_empresas_historico_endereco>().HasKey(c => c.cod_hist_end_empresas);
            modelBuilder.Entity<mvx_empresas_contatos>().HasKey(c => c.cod_empresa_contato);
            modelBuilder.Entity<financeiro_painel_controle>().HasKey(c => c.cod_painel_controle);

            
            //modelBuilder.Entity<comercial_tipo_servicos>().HasKey(c => c.cod_tipo_servico);
            //modelBuilder.Entity<RetornoSO>().HasKey(c => c.id);
            //modelBuilder.Entity<EnderecoSO>().HasKey(c => c.idpropend);
            #endregion

            #region MUNDISO
            modelBuilder.Entity<contratos>().HasKey(c => c.id);
            modelBuilder.Entity<p_classes>().HasKey(c => c.id);
            modelBuilder.Entity<p_servicos>().HasKey(c => c.id);
            modelBuilder.Entity<propostas>().HasKey(c => c.codprop);
            modelBuilder.Entity<propostas_enderecos>().HasKey(c => c.idpropend);
            modelBuilder.Entity<propostas_produtos>().HasKey(c => c.idpropprod);
            modelBuilder.Entity<propostas_produtos_resumo>().HasKey(c => c.idresumo);
            modelBuilder.Entity<c_vencimentos>().HasKey(c => c.id);
            #endregion
        }
    }
}
