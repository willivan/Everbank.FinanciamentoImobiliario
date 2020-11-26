using System;
using System.Collections.Generic;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Enums;

namespace EverBank.FinanciamentoImobiliario.Contratos.Domain.ServicosDominio
{
    public class SistemaAmortizacaoPrice : SistemaAmortizacao
    {
        public SistemaAmortizacaoPrice():base(TipoSistemaAmortizacao.Price)
        {
            
        }
        
        public override void AmortizarValorPrestacao(ContratoFinanciamento contrato, decimal valor)
        {
            
        }

        public override void AmortizarPrazo(ContratoFinanciamento contrato, decimal valor)
        {
            throw new NotImplementedException();
        }

        public override List<PrestacaoFinanciamento> GerarTabelaFinanciamento(ContratoFinanciamento contrato)
        {
            List<PrestacaoFinanciamento> prestacoes = new List<PrestacaoFinanciamento>();

            decimal taxaJurosMensal = contrato.TaxaDeJurosAnual/12/100;
            decimal valorPrestacaoFixa = CalcularValorPrestacao(contrato.ValorFinanciado,taxaJurosMensal,contrato.PrazoFinanciamento);
            
            decimal saldoDevedor = contrato.ValorFinanciado;
            
            for(int numPrestacao = 1;numPrestacao<=contrato.PrazoFinanciamento;numPrestacao++)
            {
                decimal valorJurosPrestacao = saldoDevedor*taxaJurosMensal;
                decimal valorAmortizacaoPrestacao = valorPrestacaoFixa-valorJurosPrestacao;
                PrestacaoFinanciamento prestacao = new PrestacaoFinanciamento(
                                                                        contrato.NumeroDoContrato,
                                                                        numPrestacao,
                                                                        contrato.TipoSistemaAmortizacao,
                                                                        saldoDevedor,
                                                                        taxaJurosMensal,
                                                                        valorJurosPrestacao,
                                                                        valorAmortizacaoPrestacao,
                                                                        contrato.VencimentoPrimeiraParcela);
                saldoDevedor -= valorAmortizacaoPrestacao;
                prestacoes.Add(prestacao);
            }

            return prestacoes;
        }

        private decimal CalcularValorPrestacao(decimal valorFinanciado, decimal taxaJurosMensal, int prazoFinanciamento)
        {
            return Convert.ToDecimal(CalcularValorPrestacaoDouble(Decimal.ToDouble(valorFinanciado),Decimal.ToDouble(taxaJurosMensal),prazoFinanciamento));
        }

        private double CalcularValorPrestacaoDouble(double valorFinanciado, double taxaJurosMensal, int prazoFinanciamento)
        {
            var potencia = Math.Pow(1+taxaJurosMensal,prazoFinanciamento);
            var linhaSuperior = potencia * taxaJurosMensal;
            var linhaInferior = potencia -1;
            var divisao = linhaSuperior/linhaInferior;
            var valorParcela = valorFinanciado * divisao;
            return valorParcela;
        }
    }
}
