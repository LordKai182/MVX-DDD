using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntidadesMundidata
{
    public class mvx_agendamentos
    {
        public int? cod_agendamento { get; set; }
        public int? cod_agendamento_servico { get; set; }
        public int? cod_empresa { get; set; }
        public int? cod_servico_par { get; set; }
        public DateTime? data_solicitacao { get; set; }
        public DateTime? data_previsao_execucao { get; set; }
        public DateTime? data_agendamento_execucao { get; set; }
        public DateTime? horario_agendamento_execucao { get; set; }
        public DateTime? data_inicio_execucao { get; set; }
        public DateTime? horario_inicio_execucao { get; set; }
        public DateTime? data_termino_execucao { get; set; }
        public DateTime? horario_termino_execucao { get; set; }
        public int? cod_solicitador { get; set; }
        public int? cod_executor { get; set; }
        public int? cod_tecnico { get; set; }
        public int? cod_tecnico_aux { get; set; }
        public string dsc_observacao { get; set; }
        public int? ind_cliente_problema { get; set; }
        public int? ind_status_agendamento { get; set; }
        public int? ind_financeiro { get; set; }
        public int? cod_tipo_tarefa_a { get; set; }
        public int? ind_pendente { get; set; }
        public int? cod_cancelamento { get; set; }
        public int? ind_negado_tecnico { get; set; }
        public DateTime? horario_fim_agendamento_execucao { get; set; }
        public int? ind_dinamico { get; set; }
        public int? ind_mail { get; set; }



    }
}
