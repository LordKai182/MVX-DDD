namespace Infra.EntidadesMundidata
{
    public class mvx_empresas
    {
        string _cnpj = string.Empty;
        bool _parceiro = false;
        bool _mvxService = false;
        string _UfCliente = string.Empty;
        int _TipoClientefaturamentoComercial = 0;
        //Contexto db = new Contexto();
        public mvx_empresas()
        {

        }
        public mvx_empresas(string cnpj, bool parceiro,bool mvxService,string ufCliente,int TipoClientefaturamentoComercial)
        {
            _cnpj = cnpj;
            _parceiro = parceiro;
            _mvxService = mvxService;
            _UfCliente = ufCliente;
        }
        public int cod_empresa { get; set; }

        public int? num_ddd_telefone { get; set; }

        public string num_telefone { get; set; }

        public int? num_ddd_fax { get; set; }

        public string num_fax { get; set; }

        public string dsc_url { get; set; }

        public string dsc_mail { get; set; }

        public int? cod_predio { get; set; } ///CODIGO DO PREDIO

        public int? cod_predio_anterior { get; set; }

        public int? cod_atividade { get; set; }
        /// <summary>
        /// MARCAR PARA 51
        /// </summary>
        public int? cod_nota { get; set; }
        public string dsc_observacao { get; set; }

        public int? num_andar { get; set; }

        public int? cod_empresa_ind { get; set; }
        /// <summary>
        ///  SELEC NA TABELA comercial_tipo_operadora
        ///  ACHANDO O CNPJ LA MARACR PARA 4, NAO ACHANDO E SENDO PARCEIRO MARCAR PARA 2, NAO MARCADO PARCEIRO E NEM ACHANDO MARACAR PARA 1
        /// </summary>
        /// 
        //public int cod_empresa_ind
        //{
        //    get { return (int)MarcaCodEmpresaInd(_cnpj); }
        //    set { ; }
        //}
        public int? ind_mvx_services { get; set; }
        /// <summary>
        /// MARACAR PARA 1 QUANDO EMPRESA MUNDIVOX SERVICOS FOR PARTE DO FATURAMENTO DESTE CLIENTE
        /// </summary>
        /// 
        //public int ind_mvx_services
        //{
        //    get { return (int)MarcaMvx(_mvxService); }
        //    set {; }
        //}
        public int? cod_cliente_tipo { get; set; }
        /// <summary>
        /// INDICADO COMERCIAL na TABELA TIPOCLIENTE FATURAMENTO LAMBDA TipoClientefaturamento
        /// </summary>
        //public int cod_cliente_tipo
        //{
        //    get { return (int)_TipoClientefaturamentoComercial; }
        //    set {; }
        //}




        public string dsc_vpn { get; set; }

        public int? num_computadores { get; set; }

        public string proposta { get; set; }

        public int? sem_email { get; set; }

       
        public int? cod_operadora { get; set; }
        /// <summary>
        /// MARCAR COM O CODIGO DA OPERADORA RETORNADO PARA MARCAR 4 NA PROPRIEDADE cod_empresa_ind
        /// </summary>
        public int? ind_wholesale { get; set; }
        /// <summary>
        /// COMERCIAL_TIPO_OPERADORA LOCAL CASO SEJA UMA UF DIFERENTE DA DO CLIENTE CADASTRADO E SE COD_OPERADORA FOR PARA 4, MARCAR PARA 1
        /// </summary>
      
        public string  email_nf { get; set; }

        public int? ind_area_risco { get; set; }

        public int? ind_contato_imediato { get; set; }

        #region METODOS
        private int MarcaCodEmpresaInd(string cnpj)
        {
           
            try
            {
               
                //if (db.ComercialTipoOperadora.Count(x => x.cnpj == cnpj) > 0)
                //{
                //    var operadora = db.ComercialTipoOperadora.First(x => x.cnpj == cnpj);
                //    cod_operadora = operadora.cod_operadora;
                //    if(_UfCliente != operadora.uf)
                //    {
                //        ind_wholesale = 1;
                //    }
                //    return 4;
                //}
                //if (db.ComercialTipoOperadora.Count(x => x.cnpj == cnpj) == 0 && _parceiro)
                //{
                //    return 2;
                //}

            }
            catch
            {

                return 1;
            }
            return 1;
        }

        private int MarcaMvx(bool mvx)
        {
            if (mvx)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}
