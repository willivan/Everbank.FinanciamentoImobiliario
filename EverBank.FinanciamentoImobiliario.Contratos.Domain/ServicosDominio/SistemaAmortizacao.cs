using System;
using System.Collections.Generic;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Enums;

namespace EverBank.FinanciamentoImobiliario.Contratos.Domain.ServicosDominio
{
    public abstract class SistemaAmortizacao
    {
        public TipoSistemaAmortizacao TipoSistemaAmortizacao{get;private set;}

        public SistemaAmortizacao(TipoSistemaAmortizacao sistema)
        {
            TipoSistemaAmortizacao = sistema;    
        }

        public abstract void AmortizarPrazo(ContratoFinanciamento contrato, decimal valor);

        public abstract void AmortizarValorPrestacao(ContratoFinanciamento contrato, decimal valor);

        public abstract List<PrestacaoFinanciamento> GerarTabelaFinanciamento(ContratoFinanciamento contrato);
    }
}
