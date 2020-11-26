using System;
using System.Collections.Generic;
using System.Linq;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Enums;

namespace EverBank.FinanciamentoImobiliario.Contratos.Domain.ServicosDominio
{
    public class SistemaAmortizacaoSac : SistemaAmortizacao
    {
        public SistemaAmortizacaoSac():base(TipoSistemaAmortizacao.Sac)
        {
            
        }
        
        public override void AmortizarValorPrestacao(ContratoFinanciamento contrato, decimal valor)
        {
            List<PrestacaoFinanciamento> prestacoesPendentes = contrato.Prestacoes.Where(c => c.StatusPagamentoPrestacao == StatusPagamentoPrestacao.Pendente).ToList();

            decimal novoValorAmortizacaoParcela = prestacoesPendentes.First().ValorAmortizacao + (valor / prestacoesPendentes.Count);

            decimal saldoDevedor = contrato.SaldoDevedor;

            foreach(PrestacaoFinanciamento prestacao in prestacoesPendentes)
            {
                prestacao.AmortizarPrestacao(saldoDevedor, novoValorAmortizacaoParcela);
                saldoDevedor -= novoValorAmortizacaoParcela;
            }
        }

        public override void AmortizarPrazo(ContratoFinanciamento contrato, decimal valor)
        {
            
        }

        public override List<PrestacaoFinanciamento> GerarTabelaFinanciamento(ContratoFinanciamento contrato)
        {
            List<PrestacaoFinanciamento> prestacoes = new List<PrestacaoFinanciamento>();

            decimal valorAmortizacaoPrestacao = contrato.ValorFinanciado / contrato.PrazoFinanciamento;
            decimal saldoDevedor = contrato.ValorFinanciado;
            decimal taxaJurosMensal = contrato.TaxaDeJurosAnual/12;
            decimal valorPrestacaoInicial = valorAmortizacaoPrestacao + (taxaJurosMensal/100*contrato.ValorFinanciado);
            
            for(int numPrestacao = 1;numPrestacao<=contrato.PrazoFinanciamento;numPrestacao++)
            {
                decimal valorJurosPrestacao = saldoDevedor*taxaJurosMensal/100;
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
    }
}
