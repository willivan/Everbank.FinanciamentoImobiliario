using System;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Enums;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Excecoes;

namespace EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades
{
    public class PrestacaoFinanciamento
    {
        public String ID{get;private set;}

        public String NumeroDoContrato{get;private set;}

        public int NumeroPrestacao{get;private set;}

        public TipoSistemaAmortizacao SistemaAmortizacao{get;private set;}

        public decimal SaldoDevedor{get;private set;}

        public decimal TaxaJuros{get;private set;}

        public decimal ValorJuros{get;private set;}

        public decimal ValorAmortizacao{get;private set;}

        public decimal ValorTotalPrestacao{get;private set;}

        public DateTime DataVencimento{get;private set;}

        public StatusPagamentoPrestacao StatusPagamentoPrestacao{get;set;}

        public DateTime? DataPagamento{get;private set;}

        public decimal ValorPago{get;set;}

        public PrestacaoFinanciamento(String numeroDoContrato, int numeroPrestacao, TipoSistemaAmortizacao sistemaAmortizacao,decimal saldoDevedor, decimal taxaJuros, decimal valorJuros, decimal valorAmortizacao, DateTime dataVencimentoPrimeiraParcela)
        {
            ID = Guid.NewGuid().ToString();
            NumeroDoContrato = numeroDoContrato;
            NumeroPrestacao = numeroPrestacao;
            SistemaAmortizacao = sistemaAmortizacao;
            SaldoDevedor = saldoDevedor;
            TaxaJuros = taxaJuros;
            ValorJuros = valorJuros;
            ValorAmortizacao = valorAmortizacao;
            DataVencimento = dataVencimentoPrimeiraParcela.AddMonths((numeroPrestacao-1));
            StatusPagamentoPrestacao = StatusPagamentoPrestacao.Pendente;
            CalcularValorTotalPrestacao();
        }

        public void AmortizarPrestacao(decimal novoSaldoDevedor, decimal novoValorAmortizacaoParcela)
        {
            this.SaldoDevedor = novoSaldoDevedor;
            this.ValorAmortizacao = novoValorAmortizacaoParcela;
            CalcularValorTotalPrestacao();
        }

        private void CalcularValorTotalPrestacao()
        {
            ValorTotalPrestacao = ValorAmortizacao + ValorJuros;
        }

        public void Quitar(decimal valorPago, DateTime dataPagamento)
        {
            ExcecaoDominio.LancarQuando(() => valorPago != ValorTotalPrestacao, "Valor Pago é diferente do valor da prestação");
            ExcecaoDominio.LancarQuando(() => dataPagamento > DateTime.Now, "Data de pagamento não pode ser maior do que a data atual");

            DataPagamento = dataPagamento;
            ValorPago = valorPago;
        }
    }
}
